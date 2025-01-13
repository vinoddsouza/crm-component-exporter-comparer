using RioCanada.Crm.ComponentExportComparer.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.Comparision
{
    partial class ComparisionView : UserControl
    {
        private Settings SettingsInternal { get; set; }
        private string SourceIndexFile { get; set; }
        private string TargetIndexFile { get; set; }
        private string SourceDirectory { get; set; }
        private string TargetDirectory { get; set; }
        private List<ComparisionLineItem> ComparisionData = new List<ComparisionLineItem>();
        private ComparisionLineItem CurrentItem { get; set; }
        private ComparisionLineItem SelectedItem { get; set; }
        private ComparisionLineItem CurrentContextItem { get; set; }
        public Settings Settings
        {
            get => this.SettingsInternal;
            set
            {
                this.SettingsInternal = value;
                this.comparisionDataView1.ShowExternalLink = value != null && value.Configuration.IsCompareToolConfigured;
            }
        }

        public bool IsContentView { get; set; }
        public string RootItemLabel { get; set; }

        private bool IncludeUnchanged { get => this.imageCheckboxUnchanged.Checked; }
        private bool IncludeOnlyInSource { get => this.imageCheckboxSource.Checked; }
        private bool IncludeOnlyInTarget { get => this.imageCheckboxTarget.Checked; }
        private bool IncludeModified { get => this.imageCheckboxModified.Checked; }
        private ComparisionStatus Filter
        {
            get
            {
                return (this.IncludeUnchanged ? ComparisionStatus.Unchanged : (ComparisionStatus)0) |
                    ((this.IncludeOnlyInSource ? ComparisionStatus.OnlyInSource : (ComparisionStatus)0)) |
                    ((this.IncludeOnlyInTarget ? ComparisionStatus.OnlyInTarget : (ComparisionStatus)0)) |
                    ((this.IncludeModified ? ComparisionStatus.Modified : (ComparisionStatus)0))
                    ;
            }
        }

        public ComparisionView()
        {
            InitializeComponent();

            this.breadcrumbControl1.ItemClick += this.BreadcrumbControl1_ItemClick;
            this.comparisionDataView1.OnOpen += this.ComparisionDataView1_OnOpen;
            this.comparisionDataView1.OnWindowOpen += this.ComparisionDataView1_OnWindowOpen;
            this.comparisionDataView1.OnBack += this.ComparisionDataView1_OnBack;
            this.comparisionDataView1.OnContextMenu += this.ComparisionDataView1_OnContextMenu;

            RenderData();
        }

        private void ComparisionDataView1_OnContextMenu(ComparisionLineItem item, Control control, Point position)
        {
            foreach (var menu in this.contextMenuStripGrid.Items)
            {
                (menu as ToolStripMenuItem).Visible = false;
                (menu as ToolStripMenuItem).Enabled = false;
            }
            this.exploreSourceFolderToolStripMenuItem.Visible = false;
            this.exploreTargetFolderToolStripMenuItem.Visible = false;
            this.compareFolderToolStripMenuItem.Visible = false;
            this.openSourceFileToolStripMenuItem.Visible = false;
            this.openTargetFileToolStripMenuItem.Visible = false;
            this.compareFileToolStripMenuItem.Visible = false;

            this.viewSourceComponentToolStripMenuItem.Visible = false;
            this.viewTargetComponentToolStripMenuItem.Visible = false;

            if (JumpToAvailable(item.SourceItem))
            {
                this.viewSourceComponentToolStripMenuItem.Visible = true;
                this.viewSourceComponentToolStripMenuItem.Enabled = true;
            }

            if (JumpToAvailable(item.TargetItem))
            {
                this.viewTargetComponentToolStripMenuItem.Visible = true;
                this.viewTargetComponentToolStripMenuItem.Enabled = true;
            }

            if (item.IsFile)
            {
                this.openSourceFileToolStripMenuItem.Visible = true;
                this.openTargetFileToolStripMenuItem.Visible = true;
                this.compareFileToolStripMenuItem.Visible = true;

                if (item.Status != ComparisionStatus.OnlyInTarget)
                {
                    this.openSourceFileToolStripMenuItem.Enabled = true;
                }

                if (item.Status != ComparisionStatus.OnlyInSource)
                {
                    this.openTargetFileToolStripMenuItem.Enabled = true;
                }

                if (item.Status != ComparisionStatus.OnlyInSource && item.Status != ComparisionStatus.OnlyInTarget)
                {
                    this.compareFileToolStripMenuItem.Enabled = true;
                }
            }
            else
            {
                this.exploreSourceFolderToolStripMenuItem.Visible = true;
                this.exploreTargetFolderToolStripMenuItem.Visible = true;
                this.compareFolderToolStripMenuItem.Visible = true;

                if (item.Status != ComparisionStatus.OnlyInTarget)
                {
                    this.exploreSourceFolderToolStripMenuItem.Enabled = true;
                }

                if (item.Status != ComparisionStatus.OnlyInSource)
                {
                    this.exploreTargetFolderToolStripMenuItem.Enabled = true;
                }

                if (Settings.Configuration.IsCompareToolConfigured && item.Status != ComparisionStatus.OnlyInSource && item.Status != ComparisionStatus.OnlyInTarget)
                {
                    this.compareFolderToolStripMenuItem.Enabled = true;
                }
            }

            if (item.ContentType == IndexLineItemContentType.SecurityRoleFolder)
            {
                var seprator = new ToolStripSeparator
                {
                    Tag = "TEMP_ITEM",
                };
                var toolStripItem = new ToolStripMenuItem
                {
                    Text = "Classic view",
                    Tag = "TEMP_ITEM",
                };
                toolStripItem.Click += (ss, ee) =>
                {
                    CurrentItem = item;
                    RenderData();
                };
                this.contextMenuStripGrid.Items.Add(seprator);
                this.contextMenuStripGrid.Items.Add(toolStripItem);
            }

            this.CurrentContextItem = item;
            this.contextMenuStripGrid.Show(control, position);
        }

        private void contextMenuStripGrid_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            for (var i = 0; i < this.contextMenuStripGrid.Items.Count; i++)
            {
                if ((this.contextMenuStripGrid.Items[i].Tag as string) == "TEMP_ITEM")
                {
                    this.contextMenuStripGrid.Items.RemoveAt(i--);
                }
            }
        }

        #region Events
        private void ComparisionDataView1_OnWindowOpen(ComparisionLineItem item)
        {
            OpenWindow(item);
        }

        private void BreadcrumbControl1_ItemClick(BreadcrumbItem item)
        {
            CurrentItem = item.Data as ComparisionLineItem;
            RenderData();
        }

        private void ComparisionDataView1_OnOpen(ComparisionLineItem item)
        {
            //if (item.ContentType == IndexLineItemContentType.SecurityRoleFolder)
            //{
            //    var path = GetRelativePath(item);
            //    var sourceFolder = System.IO.Path.Combine(this.SourceDirectory, path);
            //    var targetFolder = System.IO.Path.Combine(this.TargetDirectory, path);

            //    FormSecurityRoleComparision f = new FormSecurityRoleComparision(Settings, true, sourceFolder, targetFolder);
            //    f.ShowDialog();
            //    return;
            //}

            if (item.Children != null && item.Children.Count > 0)
            {
                CurrentItem = item;
                RenderData();
            }
            else if (item.IsFile)
            {
                OpenWindow(item);
            }
        }

        private void ComparisionDataView1_OnBack()
        {
            if (CurrentItem == null) return;

            SelectedItem = CurrentItem;
            CurrentItem = CurrentItem.Parent;
            RenderData();
        }
        #endregion

        #region Helpers
        private void RenderData()
        {
            List<ComparisionLineItem> data;
            if (CurrentItem == null)
            {
                data = ComparisionData;
            }
            else
            {
                data = CurrentItem.Children;
            }

            data = data.FindAll(x => (x.Status & this.Filter) > 0);

            this.comparisionDataView1.SetItems(data);

            if (SelectedItem != null)
            {
                this.comparisionDataView1.SetSelectedItem(SelectedItem);
                SelectedItem = null;
            }

            this.label1.Visible = data.Count == 0;

            var newBreadcrumbItems = new List<BreadcrumbItem>
                {
                    new BreadcrumbItem { Text = RootItemLabel ?? "Root" },
                };

            var breadcrumbNextItem = CurrentItem;
            while (breadcrumbNextItem != null)
            {
                newBreadcrumbItems.Insert(1, new BreadcrumbItem { Text = breadcrumbNextItem.Name, Data = breadcrumbNextItem });
                breadcrumbNextItem = breadcrumbNextItem.Parent;
            }

            this.breadcrumbControl1.SetItems(newBreadcrumbItems);
        }

        private void LoadComparisionData()
        {
            var source = GetIndexData(this.SourceIndexFile);
            var target = GetIndexData(this.TargetIndexFile);
            source.Children?.ForEach(x => x.Parent = source);
            target.Children?.ForEach(x => x.Parent = target);
            this.ComparisionData = GetComparisionData(source.Children, target.Children);
            this.RenderData();
        }

        private string GetRelativePath(ComparisionLineItem item)
        {
            if (item == null)
            {
                return string.Empty;
            }

            var path = item.Key;

            ComparisionLineItem parentItem = item;
            while ((parentItem = parentItem.Parent) != null)
            {
                path = System.IO.Path.Combine(parentItem.Key, path);
            }

            return path;
        }

        private void OpenWindow(ComparisionLineItem item)
        {
            var path = GetRelativePath(item);
            if (item.Status == ComparisionStatus.OnlyInTarget)
            {
                if (this.IsContentView) ShowContent(item.TargetItem.Content, item.Name);
                else OpenFile(System.IO.Path.Combine(this.TargetDirectory, path));
            }
            else if (item.Status == ComparisionStatus.OnlyInSource)
            {
                if (this.IsContentView) ShowContent(item.SourceItem.Content, item.Name);
                else OpenFile(System.IO.Path.Combine(this.SourceDirectory, path));
            }
            else
            {
                if (this.IsContentView)
                {
                    CompareContent(item.SourceItem.Content, item.TargetItem.Content);
                }
                else
                {
                    if (item.ContentType == IndexLineItemContentType.SecurityRole)
                    {
                        var sourceFile = System.IO.Path.Combine(this.SourceDirectory, path);
                        var targetFile = System.IO.Path.Combine(this.TargetDirectory, path);

                        FormSecurityRoleComparision f = new FormSecurityRoleComparision(Settings, false, sourceFile, targetFile);
                        f.ShowDialog();
                        return;
                    }

                    if (item.ContentType == IndexLineItemContentType.Columns)
                    {
                        var sourceFile = System.IO.Path.Combine(this.SourceDirectory, path);
                        var targetFile = System.IO.Path.Combine(this.TargetDirectory, path);

                        FormIndexItemComparision f = new FormIndexItemComparision(
                            Settings,
                            sourceFile,
                            targetFile,
                            Core.IndexLineItemGenerator.GenerateForColumns,
                            "Tables > " + item.Parent.Name + " > " + " Columns"
                        );
                        f.ShowDialog();
                        return;
                    }

                    if (item.ContentType == IndexLineItemContentType.ManyToMany)
                    {
                        var sourceFile = System.IO.Path.Combine(this.SourceDirectory, path);
                        var targetFile = System.IO.Path.Combine(this.TargetDirectory, path);

                        FormIndexItemComparision f = new FormIndexItemComparision(
                            Settings,
                            sourceFile,
                            targetFile,
                            Core.IndexLineItemGenerator.GenerateForManyToMany,
                            "Tables > " + item.Parent.Name + " > Relationship > Many To Many"
                        );
                        f.ShowDialog();
                        return;
                    }

                    if (item.ContentType == IndexLineItemContentType.OneToMany)
                    {
                        var sourceFile = System.IO.Path.Combine(this.SourceDirectory, path);
                        var targetFile = System.IO.Path.Combine(this.TargetDirectory, path);

                        FormIndexItemComparision f = new FormIndexItemComparision(
                            Settings,
                            sourceFile,
                            targetFile,
                            Core.IndexLineItemGenerator.GenerateForOneToMany,
                            "Tables > " + item.Parent.Name + " > Relationship > One To Many"
                        );
                        f.ShowDialog();
                        return;
                    }

                    if (item.ContentType == IndexLineItemContentType.ManyToOne)
                    {
                        var sourceFile = System.IO.Path.Combine(this.SourceDirectory, path);
                        var targetFile = System.IO.Path.Combine(this.TargetDirectory, path);

                        FormIndexItemComparision f = new FormIndexItemComparision(
                            Settings,
                            sourceFile,
                            targetFile,
                            Core.IndexLineItemGenerator.GenerateForOneToMany,
                            "Tables > " + item.Parent.Name + " > Relationship > Many To One"
                        );
                        f.ShowDialog();
                        return;
                    }

                    if (item.ContentType == IndexLineItemContentType.Ribbon)
                    {
                        var sourceFile = System.IO.Path.Combine(this.SourceDirectory, path);
                        var targetFile = System.IO.Path.Combine(this.TargetDirectory, path);

                        FormRibbonComparision f = new FormRibbonComparision(Settings, sourceFile, targetFile);
                        f.ShowDialog();
                        return;
                    }

                    CompareFile(System.IO.Path.Combine(this.SourceDirectory, path), System.IO.Path.Combine(this.TargetDirectory, path));
                }
            }
        }
        #endregion

        #region Public methods
        public void SetIndexFiles(string source, string target)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (string.IsNullOrWhiteSpace(target))
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (!System.IO.File.Exists(source) && !System.IO.File.Exists(target))
            {
                throw new System.IO.FileNotFoundException("Source or Target file not found");
            }

            this.SourceIndexFile = source;
            this.TargetIndexFile = target;
            this.SourceDirectory = System.IO.Path.GetDirectoryName(source);
            this.TargetDirectory = System.IO.Path.GetDirectoryName(target);

            this.LoadComparisionData();
        }

        public void SetIndexItems(IndexLineItem sourceIndexItem, IndexLineItem targetIndexItem)
        {
            if (sourceIndexItem == null)
            {
                throw new ArgumentNullException(nameof(sourceIndexItem));
            }

            if (targetIndexItem == null)
            {
                throw new ArgumentNullException(nameof(targetIndexItem));
            }

            sourceIndexItem.Children?.ForEach(x => x.Parent = sourceIndexItem);
            targetIndexItem.Children?.ForEach(x => x.Parent = targetIndexItem);
            this.ComparisionData = GetComparisionData(sourceIndexItem.Children, targetIndexItem.Children);
            this.RenderData();
        }
        #endregion

        #region Static helpers
        internal static List<ComparisionLineItem> GetComparisionData<T>(List<T> sourceData, List<T> targetData) where T : IndexLineItem
        {
            List<ComparisionLineItem> data = new List<ComparisionLineItem>();
            HashSet<string> idSet = new HashSet<string>();
            
            sourceData?.ForEach(x => idSet.Add(x.Key));
            targetData?.ForEach(x => idSet.Add(x.Key));

            var ids = idSet.ToList();

            var sourceDictonary = sourceData?.ToDictionary(x => x.Key);
            var targetDictonary = targetData?.ToDictionary(x => x.Key);

            for (var i = 0; i < ids.Count; i++)
            {
                var key = ids.ElementAt(i);
                var sourceItem = sourceDictonary?.ContainsKey(key) ?? false ? sourceDictonary[key] : null;
                var targetItem = targetDictonary?.ContainsKey(key) ?? false ? targetDictonary[key] : null;

                var defaultItem = sourceItem ?? targetItem;

                ComparisionStatus status = 0;

                List<ComparisionLineItem> children = null;
                if (sourceItem?.Children?.Count > 0 || targetItem?.Children?.Count > 0)
                {
                    children = GetComparisionData(sourceItem?.Children, targetItem?.Children);
                    children.ForEach(x => status |= x.Status);
                }
                else
                {
                    if (sourceItem != null && targetItem == null)
                    {
                        status = ComparisionStatus.OnlyInSource;
                    }
                    else if (sourceItem == null && targetItem != null)
                    {
                        status = ComparisionStatus.OnlyInTarget;
                    }
                    else if (sourceItem.Checksum != targetItem.Checksum)
                    {
                        status = ComparisionStatus.Modified;
                    }
                    else
                    {
                        status = ComparisionStatus.Unchanged;
                    }
                }

                var lineItem = new ComparisionLineItem
                {
                    Key = ids.ElementAt(i),
                    DefaultItem = defaultItem,
                    Name = defaultItem.Name,
                    Type = defaultItem.Type,
                    ContentType = defaultItem.ContentType,
                    IconName = defaultItem.IconName,
                    IsFile = defaultItem.Children == null,
                    Order = defaultItem.Order,
                    Status = status,
                    SourceItem = sourceItem,
                    TargetItem = targetItem,
                    Children = children,
                };

                children?.ForEach(x => x.Parent = lineItem);
                sourceItem?.Children?.ForEach(x => x.Parent = sourceItem);
                targetItem?.Children?.ForEach(x => x.Parent = targetItem);

                data.Add(lineItem);
            }

            data = data.OrderBy(x => x.Order).ThenBy(x => x.Name).ToList();

            return data;
        }

        private static IndexLineItem GetIndexData(string file)
        {
            if (!System.IO.File.Exists(file))
            {
                return new IndexLineItem();
            }

            var content = System.IO.File.ReadAllText(file);
            return JsonConvert.DeserializeObject<IndexLineItem>(content);
        }
        #endregion

        private void buttonOption_Click(object sender, EventArgs e)
        {
            this.exploreSourceFolderToolStripMenuItem1.Enabled = CurrentItem == null || CurrentItem.Status != ComparisionStatus.OnlyInTarget;
            this.exploreTargetFolderToolStripMenuItem1.Enabled = CurrentItem == null || CurrentItem.Status != ComparisionStatus.OnlyInSource;
            this.compareFolderToolStripMenuItem1.Enabled = Settings.Configuration.IsCompareToolConfigured && (CurrentItem == null || CurrentItem.Status == ComparisionStatus.Modified || CurrentItem.Status == ComparisionStatus.Unchanged);

            this.contextMenuStripButton.Show(this.buttonOption, new Point(-this.contextMenuStripButton.Width + this.buttonOption.Width, this.buttonOption.Height));
        }

        private void exploreSourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = System.IO.Path.Combine(this.SourceDirectory, GetRelativePath(this.CurrentContextItem));
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void exploreTargetFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = System.IO.Path.Combine(this.TargetDirectory, GetRelativePath(this.CurrentContextItem));
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void compareFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Settings.Configuration.CompareToolExecutablePath))
            {
                return;
            }

            var relativePath = GetRelativePath(this.CurrentContextItem);
            System.Diagnostics.Process.Start(
                this.Settings.Configuration.CompareToolExecutablePath,
                $"\"{this.TargetDirectory}\\{relativePath}\" \"{this.SourceDirectory}\\{relativePath}\""
            );
        }

        private void OpenFile(string path)
        {
            if (!Settings.Configuration.IsIDEDefault)
            {
                FormFileViewer.Open(Settings, path);
            }
            else
            {
                FormFileViewer.OpenFileInIDE(Settings, path);
            }
        }

        private void ShowContent(string content, string title)
        {
            FormFileViewer.ShowContent(Settings, content, title);
        }

        private void CompareFile(string pathA, string pathB)
        {
            var frmNew = new Diff.Net.FileDiffForm();
            frmNew.ShowDifferences(new Diff.Net.ShowDiffArgs(pathA, pathB, Diff.Net.DiffType.File));
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.ShowDialog();
        }

        private void CompareContent(string contentA, string contentB)
        {
            var frmNew = new Diff.Net.FileDiffForm();
            frmNew.ShowDifferences(new Diff.Net.ShowDiffArgs(contentA, contentB, Diff.Net.DiffType.Text));
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.ShowDialog();
        }

        private void openSourceFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = System.IO.Path.Combine(this.SourceDirectory, GetRelativePath(this.CurrentContextItem));
            OpenFile(path);
        }

        private void openTargetFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var path = System.IO.Path.Combine(this.TargetDirectory, GetRelativePath(this.CurrentContextItem));
            OpenFile(path);
        }

        private void compareFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var relativePath = GetRelativePath(this.CurrentContextItem);
            CompareFile($"{this.SourceDirectory}\\{relativePath}", $"{this.TargetDirectory}\\{relativePath}");
        }

        private void exploreSourceFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var path = System.IO.Path.Combine(this.SourceDirectory, GetRelativePath(this.CurrentItem));
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void exploreTargetFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var path = System.IO.Path.Combine(this.TargetDirectory, GetRelativePath(this.CurrentItem));
            System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private void compareFolderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.Settings.Configuration.CompareToolExecutablePath))
            {
                return;
            }

            var relativePath = GetRelativePath(this.CurrentItem);
            System.Diagnostics.Process.Start(
                this.Settings.Configuration.CompareToolExecutablePath,
                $"\"{System.IO.Path.Combine(this.TargetDirectory, relativePath)}\" \"{System.IO.Path.Combine(this.SourceDirectory, relativePath)}\""
            );
        }

        private void viewSourceComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JumpToComponent(this.CurrentContextItem.SourceItem);
        }

        private void viewTargetComponentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            JumpToComponent(this.CurrentContextItem.TargetItem);
        }

        private bool JumpToAvailable(IndexLineItem item)
        {
            if (item == null) return false;
            if (item.Metadata == null) return false;
            if (!item.Metadata.ContainsKey("Type")) return false;

            var type = item.Metadata["Type"] as string;

            var allowedTypes = new List<string> { "Table", "Form", "WebResource", "SecurityRole" };
            if (allowedTypes.IndexOf(type) == -1) return false;

            return true;
        }

        private void JumpToComponent(IndexLineItem item)
        {
            if (item == null) return;
            if (item.Metadata == null) return;
            if (!item.Metadata.ContainsKey("Type")) return;

            var type = item.Metadata["Type"] as string;
            var environmentId = GetEnvironmentId(item);
            var server = GetServer(item);

            switch (type)
            {
                case "Table":
                    {
                        if (string.IsNullOrEmpty(environmentId)) return;
                        var tableSchemaName = item.Metadata["SchemaName"] as string;
                        UriGenerator.Open(UriGenerator.Table(environmentId, tableSchemaName));
                    }
                    break;
                case "Form":
                    {
                        if (string.IsNullOrEmpty(environmentId)) return;
                        var tableSchemaName = item.Parent.Parent.Metadata["SchemaName"] as string;
                        var formId = item.Metadata["Id"] as string;
                        UriGenerator.Open(UriGenerator.Form(environmentId, tableSchemaName, formId));
                    }
                    break;
                case "WebResource":
                    {
                        if (string.IsNullOrEmpty(server)) return;
                        var webResourceId = item.Metadata["Id"] as string;
                        UriGenerator.Open(UriGenerator.WebResource(server, webResourceId));
                    }
                    break;
                case "SecurityRole":
                    {
                        if (string.IsNullOrEmpty(server)) return;
                        var securityRoleId = item.Metadata["Id"] as string;
                        UriGenerator.Open(UriGenerator.SecurityRole(server, securityRoleId));
                    }
                    break;
            }
        }

        private string GetEnvironmentId(IndexLineItem item)
        {
            if (item.Parent != null) return GetEnvironmentId(item.Parent);
            if (item.Metadata == null) return null;
            if (!item.Metadata.ContainsKey("EnvironmentId")) return null;
            return item.Metadata["EnvironmentId"] as string;
        }

        private string GetServer(IndexLineItem item)
        {
            if (item.Parent != null) return GetServer(item.Parent);
            if (item.Metadata == null) return null;
            if (!item.Metadata.ContainsKey("Server")) return null;
            return item.Metadata["Server"] as string;
        }

        private void imageCheckboxUnchanged_CheckedChanged(object sender, EventArgs e)
        {
            this.RenderData();
        }

        private void imageCheckboxModified_CheckedChanged(object sender, EventArgs e)
        {
            this.RenderData();
        }

        private void imageCheckboxSource_CheckedChanged(object sender, EventArgs e)
        {
            this.RenderData();
        }

        private void imageCheckboxTarget_CheckedChanged(object sender, EventArgs e)
        {
            this.RenderData();
        }
    }
}
