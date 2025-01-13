using RioCanada.Crm.ComponentExportComparer.Core.Models;
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
    public partial class FormSecurityRoleComparision : Form
    {
        private readonly Bitmap CheckGrey = AppResource.CheckGrey;
        private readonly Bitmap UncheckGrey = AppResource.UncheckGrey;
        private readonly Bitmap CheckOrange = AppResource.CheckOrange;
        private readonly Bitmap UncheckOrange = AppResource.UncheckOrange;
        private readonly Bitmap CheckGreen = AppResource.CheckGreen;
        private readonly Bitmap UncheckGreen = AppResource.UncheckGreen;
        private readonly Bitmap CheckRed = AppResource.CheckRed;
        private readonly Bitmap UncheckRed = AppResource.UncheckRed;
        private readonly Bitmap Security = AppResource.Security;
        private readonly Bitmap DownArrow = AppResource.DownArrow;
        private readonly Bitmap RightArrow = AppResource.RightArrow;
        private readonly Bitmap FileJson = AppResource.FileJsonIcon;

        private readonly int ROW_HEIGHT = 30;
        private readonly int SCROLL_WIDTH = 24;

        private bool IsDirectory { get; set; }
        private string Source { get; set; }
        private string Target { get; set; }
        public Settings Settings { get; set; }

        private bool ViewAsList { get; set; }
        private List<ComparisionLineItem> TreeViewData { get; set; }
        private List<ComparisionLineItem> ListViewData { get; set; }

        public FormSecurityRoleComparision(Settings settings, bool isDirectory, string source, string target)
        {
            InitializeComponent();

            this.Settings = settings;
            this.IsDirectory = isDirectory;
            this.Source = source;
            this.Target = target;

            // INIT HOTKEYS
            InitHotkeys();
        }

        private void InitHotkeys()
        {
            // register the hotkeys with the form
            HotKeyManager.AddHotKey(this, this.Close, Keys.Escape);
        }

        private void FormSecurityRoleComparision_Load(object sender, EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += (ss, ee) =>
            {
                Tuple<List<IndexLineItem>, List<IndexLineItem>> sourceData = null;
                Tuple<List<IndexLineItem>, List<IndexLineItem>> targetData = null;
                if (this.IsDirectory)
                {
                    sourceData = Core.IndexLineItemGenerator.GenerateForSecurityRoles(this.Source);
                    targetData = Core.IndexLineItemGenerator.GenerateForSecurityRoles(this.Target);
                }
                else
                {
                    var s = Core.IndexLineItemGenerator.GenerateForSecurityRole(this.Source);
                    var t = Core.IndexLineItemGenerator.GenerateForSecurityRole(this.Target);

                    sourceData = new Tuple<List<IndexLineItem>, List<IndexLineItem>>(new List<IndexLineItem> { s.Item1 }, s.Item2.ConvertAll(x => (IndexLineItem)x));
                    targetData = new Tuple<List<IndexLineItem>, List<IndexLineItem>>(new List<IndexLineItem> { t.Item1 }, t.Item2.ConvertAll(x => (IndexLineItem)x));
                }

                this.TreeViewData = ComparisionView.GetComparisionData(sourceData.Item1, targetData.Item1);
                this.ListViewData = ComparisionView.GetComparisionData(sourceData.Item2, targetData.Item2);
            };

            bgw.RunWorkerCompleted += (ss, ee) =>
            {
                bgw.Dispose();
                if (ee.Error != null)
                {
                    MessageBox.Show(ee.Error.Message);
                }
                else
                {
                    this.ReRenderGridView();
                    this.label1.Visible = false;
                }
            };

            bgw.RunWorkerAsync();
            this.ConfigureGrid();
        }

        #region Events
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            this.HandleOpen(this.dataGridView1.Rows[e.RowIndex]);
        }

        private void DataGridView1_Resize(object sender, EventArgs e)
        {
            this.HandleResize();
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    this.HandleOpen(this.dataGridView1.SelectedRows[0]);
                }
            }
            else if (!this.ViewAsList && e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;

                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    this.HandleCollapseChange(this.dataGridView1.SelectedRows[0], true);
                }
            }
            else if (!this.ViewAsList && e.KeyCode == Keys.Right)
            {
                e.SuppressKeyPress = true;

                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    this.HandleCollapseChange(this.dataGridView1.SelectedRows[0], false);
                }
            }
        }

        private void comparisionStatusCheckboxFilter1_OnChange(object sender, EventArgs e)
        {
            this.ReRenderGridData();
        }

        private void viewAsListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ViewAsList = !this.ViewAsList;
            this.viewAsListToolStripMenuItem.Checked = this.ViewAsList;
            this.collapseAllToolStripMenuItem.Enabled = !this.ViewAsList;
            this.expandAllToolStripMenuItem.Enabled = !this.ViewAsList;
            this.ReRenderGridView();
        }

        private void collapseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeCollapseAllStatus(true);
            this.ReRenderGridData();
        }

        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.ChangeCollapseAllStatus(false);
            this.ReRenderGridData();
        }
        #endregion

        #region Functions
        private void ConfigureGrid()
        {
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.MultiSelect = false;

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;

            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gainsboro;

            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightYellow;
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dataGridView1.DefaultCellStyle.Padding = new Padding(12, 0, 15, 0);

            this.dataGridView1.BackgroundColor = SystemColors.Control;
            this.dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            this.dataGridView1.CellDoubleClick += this.DataGridView1_CellDoubleClick;
            this.dataGridView1.Resize += this.DataGridView1_Resize;
        }

        private void ReRenderGridView()
        {
            // Clean datagridview data and columns
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();

            // Force garbage clean
            GC.Collect();

            if (this.ViewAsList)
            {
                this.ConfigureGridColumnAsListView();
                this.RenderListViewData();
            }
            else
            {
                this.ConfigureGridColumnAsTreeView();
                this.RenderTreeViewData();
            }
        }

        private void ReRenderGridData()
        {
            // Clean datagridview data
            this.dataGridView1.Rows.Clear();

            // Force garbage clean
            GC.Collect();

            if (this.ViewAsList)
            {
                this.RenderListViewData();
            }
            else
            {
                this.RenderTreeViewData();
            }
        }

        private void ConfigureGridColumnAsTreeView()
        {
            this.dataGridView1.Columns.Add(new DataGridViewImageTextBoxColumn { HeaderText = "Name", SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });

            this.HandleResize();
        }

        private void ConfigureGridColumnAsListView()
        {
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Role Name", SortMode = DataGridViewColumnSortMode.Automatic });
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Category", Width = 160, SortMode = DataGridViewColumnSortMode.Automatic });
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Type", SortMode = DataGridViewColumnSortMode.Automatic });
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Component", SortMode = DataGridViewColumnSortMode.Automatic });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });

            this.HandleResize();
        }

        private void RenderTreeViewData()
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            this.TreeViewData.ForEach(x =>
            {
                if ((x.Status & this.comparisionStatusCheckboxFilter1.Status) == 0) return;
                rows.Add(GetTreeViewRow(0, x, x.Name, (x.DefaultItem as Core.SecurityRoleIndexLineItem).Collapsed ? RightArrow : DownArrow, x.Status));

                if (x.Children != null && x.Children.Count > 0 && !(x.DefaultItem as Core.SecurityRoleIndexLineItem).Collapsed)
                {
                    x.Children.ForEach(y =>
                    {
                        if ((y.Status & this.comparisionStatusCheckboxFilter1.Status) == 0) return;
                        if ((y.DefaultItem as Core.SecurityRoleIndexLineItem).IsMetadata)
                        {
                            rows.Add(GetTreeViewRow(1, y, y.Name, FileJson, y.Status));
                        }
                        else
                        {
                            if (y.Children == null || y.Children.Count == 0) return;
                            rows.Add(GetTreeViewRow(1, y, y.Name, (y.DefaultItem as Core.SecurityRoleIndexLineItem).Collapsed ? RightArrow : DownArrow, y.Status));
                        }

                        if (y.Children != null && y.Children.Count > 0 && !(y.DefaultItem as Core.SecurityRoleIndexLineItem).Collapsed)
                        {
                            y.Children.ForEach(z =>
                            {
                                if ((z.Status & this.comparisionStatusCheckboxFilter1.Status) == 0) return;
                                rows.Add(GetTreeViewRow(2, z, z.Name, Security, z.Status));
                            });
                        }
                    });
                }
            });
            dataGridView1.Rows.AddRange(rows.ToArray());
        }

        private void RenderListViewData()
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            this.ListViewData.ForEach(x =>
            {
                if ((x.Status & this.comparisionStatusCheckboxFilter1.Status) == 0) return;
                var item = x.DefaultItem as Core.SecurityRoleIndexLineItem;
                rows.Add(GetListViewRow(x, item.RoleName, item.Category, item.PrivilegeType, item.Component, x.Status));
            });
            dataGridView1.Rows.AddRange(rows.ToArray());
        }

        private void HandleResize()
        {
            if (this.dataGridView1.Columns.Count == 0) return;

            if (this.ViewAsList)
            {
                var fixedWidth = 0;
                foreach (var column in this.dataGridView1.Columns) fixedWidth += ((DataGridViewColumn)column).Width;

                fixedWidth -= this.dataGridView1.Columns[0].Width;
                //fixedWidth -= this.dataGridView1.Columns[1].Width;
                fixedWidth -= this.dataGridView1.Columns[2].Width;
                fixedWidth -= this.dataGridView1.Columns[3].Width;
                var availableWidth = this.dataGridView1.Width - fixedWidth - SCROLL_WIDTH;
                this.dataGridView1.Columns[0].Width = Convert.ToInt32(availableWidth * 0.50);
                //this.dataGridView1.Columns[1].Width = Convert.ToInt32(availableWidth * 0.25);
                this.dataGridView1.Columns[2].Width = Convert.ToInt32(availableWidth * 0.20);
                this.dataGridView1.Columns[3].Width = Convert.ToInt32(availableWidth * 0.30);
            }
            else
            {
                var fixedWidth = 0;
                foreach (var column in this.dataGridView1.Columns) fixedWidth += ((DataGridViewColumn)column).Width;

                fixedWidth -= this.dataGridView1.Columns[0].Width;
                this.dataGridView1.Columns[0].Width = this.dataGridView1.Width - fixedWidth - SCROLL_WIDTH;
            }
        }

        private void HandleOpen(DataGridViewRow row)
        {
            if (!(row.Tag is ComparisionLineItem item)) return;

            var roleItem = item.DefaultItem as Core.SecurityRoleIndexLineItem;

            if (roleItem.IsGroup)
            {
                if (item.Children != null && item.Children.Count > 0)
                {
                    var rowIndex = row.Index;
                    var firstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex;
                    roleItem.Collapsed = !roleItem.Collapsed;
                    this.ReRenderGridData();
                    this.dataGridView1.Rows[rowIndex].Selected = true;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells[0];
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
                }
            }
            else if (roleItem.IsMetadata || roleItem.IsPrivilege)
            {
                if (item.Status == ComparisionStatus.OnlyInTarget)
                {
                    FormFileViewer.ShowContent(Settings, item.TargetItem.Content, item.Name);
                }
                else if (item.Status == ComparisionStatus.OnlyInSource)
                {
                    FormFileViewer.ShowContent(Settings, item.SourceItem.Content, item.Name);
                }
                else
                {
                    CompareContent(item.SourceItem.Content, item.TargetItem.Content);
                }
            }
        }

        private void HandleCollapseChange(DataGridViewRow row, bool flag)
        {
            if (!(row.Tag is ComparisionLineItem item)) return;

            var roleItem = item.DefaultItem as Core.SecurityRoleIndexLineItem;

            if (roleItem.IsGroup)
            {
                if (item.Children != null && item.Children.Count > 0)
                {
                    if (roleItem.Collapsed == flag) return;

                    var rowIndex = row.Index;
                    var firstDisplayedScrollingRowIndex = this.dataGridView1.FirstDisplayedScrollingRowIndex;
                    roleItem.Collapsed = flag;
                    this.ReRenderGridData();
                    this.dataGridView1.Rows[rowIndex].Selected = true;
                    this.dataGridView1.CurrentCell = this.dataGridView1.Rows[rowIndex].Cells[0];
                    this.dataGridView1.FirstDisplayedScrollingRowIndex = firstDisplayedScrollingRowIndex;
                }
            }
        }

        private void CompareContent(string contentA, string contentB)
        {
            var frmNew = new Diff.Net.FileDiffForm();
            frmNew.ShowDifferences(new Diff.Net.ShowDiffArgs(contentA, contentB, Diff.Net.DiffType.Text));
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.ShowDialog();
        }

        private DataGridViewRow GetTreeViewRow(int level, ComparisionLineItem item, string title, Bitmap icon, ComparisionStatus status)
        {
            var dr = new DataGridViewRow { Height = ROW_HEIGHT };
            dr.Tag = item;
            dr.Cells.Add(new DataGridViewImageTextBoxCell
            {
                Value = title,
                Icon = icon,
                Style = new DataGridViewCellStyle { Padding = new Padding(12 + level * 16, 0, 15, 0) },
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (status & ComparisionStatus.Unchanged) == ComparisionStatus.Unchanged ? CheckGrey : UncheckGrey,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (status & ComparisionStatus.Modified) == ComparisionStatus.Modified ? CheckOrange : UncheckOrange,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (status & ComparisionStatus.OnlyInSource) == ComparisionStatus.OnlyInSource ? CheckGreen : UncheckGreen,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (status & ComparisionStatus.OnlyInTarget) == ComparisionStatus.OnlyInTarget ? CheckRed : UncheckRed,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });
            return dr;
        }

        private DataGridViewRow GetListViewRow(ComparisionLineItem item, string roleName, string category, string type, string component, ComparisionStatus status)
        {
            var dr = new DataGridViewRow
            {
                Height = ROW_HEIGHT,
                Tag = item,
            };
            dr.Cells.AddRange(
                new DataGridViewTextBoxCell
                {
                    Value = roleName,
                }, new DataGridViewTextBoxCell
                {
                    Value = category,
                }, new DataGridViewTextBoxCell
                {
                    Value = type,
                },
                new DataGridViewTextBoxCell
                {
                    Value = component,
                },
                new DataGridViewImageCell
                {
                    Value = (status & ComparisionStatus.Unchanged) == ComparisionStatus.Unchanged ? CheckGrey : UncheckGrey,
                    Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                },
                    new DataGridViewImageCell
                    {
                        Value = (status & ComparisionStatus.Modified) == ComparisionStatus.Modified ? CheckOrange : UncheckOrange,
                        Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                    },
                    new DataGridViewImageCell
                    {
                        Value = (status & ComparisionStatus.OnlyInSource) == ComparisionStatus.OnlyInSource ? CheckGreen : UncheckGreen,
                        Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                    },
                    new DataGridViewImageCell
                    {
                        Value = (status & ComparisionStatus.OnlyInTarget) == ComparisionStatus.OnlyInTarget ? CheckRed : UncheckRed,
                        Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                    }
                );

            return dr;
        }

        private void ChangeCollapseAllStatus(bool value)
        {
            this.TreeViewData.ForEach(x =>
            {
                (x.DefaultItem as Core.SecurityRoleIndexLineItem).Collapsed = value;

                if (x.Children != null && x.Children.Count > 0)
                {
                    x.Children.ForEach(y =>
                    {
                        if ((y.DefaultItem as Core.SecurityRoleIndexLineItem).IsMetadata)
                        {
                            return;
                        }

                        (y.DefaultItem as Core.SecurityRoleIndexLineItem).Collapsed = value;
                    });
                }
            });
        }
        #endregion
    }
}
