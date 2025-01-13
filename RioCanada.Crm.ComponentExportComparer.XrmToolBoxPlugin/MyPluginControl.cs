using RioCanada.Crm.ComponentExportComparer.Core;
using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using System.IO.Compression;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    public partial class MyPluginControl : CustomMultipleConnectionsPluginControlBase
    {
        private Settings mySettings;
        private IOrganizationService TargetService;
        private string TargetConnectionName { get; set; }
        private ConnectionDetail SourceConnectionDetail { get; set; }
        private ConnectionDetail TargetConnectionDetail { get; set; }
        private string ZipFilePath { get; set; }
        private List<string> Logs { get; } = new List<string>();
        private BackgroundWorker CurrentBackgroundWorker { get; set; }
        private Tuple<string, string> LastResultDirectories { get; set; }
        private string CompareResultTempFolder { get; } = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"CRM_Compare_{Guid.NewGuid()}");
        private Logger Logger { get; }
        private AutoCompleteStringCollection SolutionSuggestion { get; set; }

        public MyPluginControl()
        {
            InitializeComponent();
            this.Logger = new Logger(System.IO.Path.Combine(this.CompareResultTempFolder, "Log"));
        }

        private void MyPluginControl_Load(object sender, EventArgs e)
        {
            //ShowInfoNotification("This is a notification that can lead to XrmToolBox repository", new Uri("https://github.com/MscrmTools/XrmToolBox"));

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings
                {
                    Queries = new List<string>(),
                    Configuration = new SettingConfiguration()
                };
                mySettings.Configuration.DefaultIDE = Helper.DetectDefaultIDE();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                if (mySettings.Configuration == null)
                {
                    mySettings.Configuration = new SettingConfiguration();
                }
                if (mySettings.Queries == null)
                {
                    mySettings.Queries = new List<string>();
                }
                if (mySettings.Configuration.DefaultIDE == null)
                {
                    mySettings.Configuration.DefaultIDE = Helper.DetectDefaultIDE();
                }
                textBoxOutputDir.Text = mySettings.OutputDirectory;
                mySettings.Queries.ForEach(x => listViewQueries.Items.Add(x));
                checkBoxDeleteDir.Checked = mySettings.DeleteDirectory;
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);

            if (System.IO.Directory.Exists(CompareResultTempFolder))
            {
                try
                {
                    System.IO.Directory.Delete(CompareResultTempFolder, true);
                }
                catch { }
            }
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (detail != null)
            {
                if (actionName != "AdditionalOrganization")
                {
                    LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
                    this.SetConnectionInfo(labelSrcEnv, detail);
                    buttonExport.Enabled = true;
                    buttonCompare.Enabled = true;
                    SourceConnectionDetail = detail;
                    SolutionSuggestion = null;
                }
                else
                {
                    this.SetConnectionInfo(labelTrgEnv, detail);
                    TargetService = detail.ServiceClient;
                    TargetConnectionName = detail.ConnectionName;
                    TargetConnectionDetail = detail;
                    ZipFilePath = null;
                    labelZipFile.Text = "Not selected";
                    labelZipFile.ForeColor = Color.Black;
                }
            }
        }

        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
        {
            //if (e.Action.Equals(NotifyCollectionChangedAction.Add))
            //{
            //    var detail = (ConnectionDetail)e.NewItems[0];
            //    labelTrgEnv.Text = detail.ConnectionName;
            //    labelTrgEnv.ForeColor = Color.Green;
            //    TargetService = detail.ServiceClient;
            //    TargetConnectionName = detail.ConnectionName;
            //    TargetConnectionDetail = detail;
            //}
        }

        public override void ClosingPlugin(PluginCloseInfo info)
        {
            base.ClosingPlugin(info);

            SettingsManager.Instance.Save(GetType(), mySettings);

            if (System.IO.Directory.Exists(CompareResultTempFolder))
            {
                try
                {
                    System.IO.Directory.Delete(CompareResultTempFolder, true);
                }
                catch { }
            }
        }

        #region Events
        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            buttonEdit.Enabled = e.IsSelected;
            buttonRemove.Enabled = e.IsSelected;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddQuery();
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            EditSelectedQuery();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedQuery();
        }

        private void listViewQueries_KeyDown(object sender, KeyEventArgs e)
        {
            if (listViewQueries.SelectedItems.Count == 0)
            {
                return;
            }

            if (e.KeyCode == Keys.Delete)
            {
                RemoveSelectedQuery();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                EditSelectedQuery();
            }
        }

        private void listViewQueries_DoubleClick(object sender, EventArgs e)
        {
            EditSelectedQuery();
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            listViewQueries.Columns[0].Width = listViewQueries.Width - 8;
        }

        private void textBoxOutputDir_TextChanged(object sender, EventArgs e)
        {
            mySettings.OutputDirectory = textBoxOutputDir.Text;
        }

        private void buttonBrowseDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                textBoxOutputDir.Text = fbd.SelectedPath;
            }
        }

        private void checkBoxDeleteDir_CheckedChanged(object sender, EventArgs e)
        {
            mySettings.DeleteDirectory = checkBoxDeleteDir.Checked;
        }

        private void tsbSetting_Click(object sender, EventArgs e)
        {
            FormSetting f = new FormSetting(this.mySettings.Configuration, this.Logger);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.mySettings.Configuration = f.Configuration;
                SettingsManager.Instance.Save(GetType(), mySettings);
            }
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            RunExport();
        }

        private void buttonCompare_Click(object sender, EventArgs e)
        {
            RunCompare();
        }

        private void buttonTrgEnv_Click(object sender, EventArgs e)
        {
            AddAdditionalOrganization();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            FlushLogs();

            if (buttonAbort.Enabled == false)
            {
                if (this.CurrentBackgroundWorker == null)
                {
                    this.buttonAbort.Enabled = true;
                    this.buttonAbort.Text = "Abort";
                }
            }
        }

        private void buttonAbort_Click(object sender, EventArgs e)
        {
            if (this.CurrentBackgroundWorker == null) return;
            if (!this.CurrentBackgroundWorker.WorkerSupportsCancellation) return;

            this.CurrentBackgroundWorker.CancelAsync();
            this.buttonAbort.Text = "Aborting...";
            this.buttonAbort.Enabled = false;
        }

        private void buttonQuerySetting_Click(object sender, EventArgs e)
        {
            FormQuerySetting f = new FormQuerySetting(this.mySettings.Configuration);
            if (f.ShowDialog() == DialogResult.OK)
            {
                this.mySettings.Configuration = f.Configuration;
                SettingsManager.Instance.Save(GetType(), mySettings);
            }
        }


        private void labelSrcEnv_Click(object sender, EventArgs e)
        {
            if (SourceConnectionDetail == null) return;
            var frm = new FormConnectionDetail(SourceConnectionDetail);
            frm.ShowDialog();
        }

        private void labelTrgEnv_Click(object sender, EventArgs e)
        {
            if (TargetConnectionDetail == null) return;
            var frm = new FormConnectionDetail(TargetConnectionDetail);
            frm.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/vinoddsouza/crm-component-comparer-exporter");
        }

        private void buttonZipFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Compressed file|*.zip"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.SetConnectionInfo(labelTrgEnv, null);
                    TargetService = null;
                    TargetConnectionName = null;
                    TargetConnectionDetail = null;

                    var indexItem = ExportService.GetIndexData(ofd.FileName);

                    if (indexItem == null)
                    {
                        MessageBox.Show("Selected zip file is not a valid file for comparision", "Error");
                        return;
                    }

                    ZipFilePath = ofd.FileName;

                    labelZipFile.Text = "✓ " + System.IO.Path.GetFileName(ofd.FileName);
                    labelZipFile.ForeColor = Color.Green;

                    if (indexItem.Metadata.ContainsKey("Queries"))
                    {
                        var queries = (indexItem.Metadata["Queries"] as Newtonsoft.Json.Linq.JArray)?.ToList().ConvertAll(x => x.ToString());
                        var settings = (indexItem.Metadata["Settings"] as Newtonsoft.Json.Linq.JObject)?.ToObject<ExportSetting>();

                        if (queries != null && settings != null)
                        {
                            if (queries == null) queries = new List<string>();

                            if (MessageBox.Show("Do you want to replace query and settings from zip file?", "Query replace", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                mySettings.Queries.Clear();
                                listViewQueries.Items.Clear();

                                foreach (var query in queries)
                                {
                                    mySettings.Queries.Add(query);
                                    listViewQueries.Items.Add(query);

                                    buttonAdd.Enabled = listViewQueries.Items.Count < 5;
                                }
                            }

                            if (settings != null)
                            {
                                mySettings.Configuration.IncludeAllProperty = settings.IncludeAllProperty;
                                mySettings.Configuration.IncludeSystemWebresource = settings.IncludeSystemWebresource;
                                mySettings.Configuration.IncludeSystemPluginStep = settings.IncludeSystemPluginStep;
                                mySettings.Configuration.IncludeEntityColumn = settings.IncludeEntityColumn;
                                mySettings.Configuration.IncludeEntityRelationship = settings.IncludeEntityRelationship;
                                mySettings.Configuration.IncludeEntityForm = settings.IncludeEntityForm;
                                mySettings.Configuration.IncludeEntityDashboard = settings.IncludeEntityDashboard;
                                mySettings.Configuration.IncludeEntityView = settings.IncludeEntityView;
                                mySettings.Configuration.IncludeEntityRibbon = settings.IncludeEntityRibbon;
                                mySettings.Configuration.RoleById = settings.RoleById;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }

        #endregion

        #region Functions
        void SetConnectionInfo(System.Windows.Forms.Label lbl, ConnectionDetail connectionDetail)
        {
            if (connectionDetail == null)
            {
                lbl.Text = "Not selected";
                lbl.ForeColor = Color.Black;
                return;
            }

            var compatibility = FormConnectionDetail.GetCompatibility(connectionDetail);

            switch (compatibility)
            {
                case FormConnectionDetail.Compatibility.NotCompatible:
                    lbl.Text = "⚠ " + connectionDetail.ConnectionName;
                    lbl.ForeColor = Color.OrangeRed;
                    break;
                case FormConnectionDetail.Compatibility.NotSupported:
                    lbl.Text = "✗ " + connectionDetail.ConnectionName;
                    lbl.ForeColor = Color.Red;
                    break;
                default:
                    lbl.Text = "✓ " + connectionDetail.ConnectionName;
                    lbl.ForeColor = Color.Green;
                    break;
            }
        }

        void FlushLogs()
        {
            if (this.Logs.Count == 0) return;
            var items = new List<string>();
            items.AddRange(this.Logs);
            this.Logs.RemoveRange(0, items.Count);

            this.richTextBox1.Text += string.Join(Environment.NewLine, items) + Environment.NewLine;
            this.richTextBox1.SelectionStart = this.richTextBox1.Text.Length;
            this.richTextBox1.ScrollToCaret();
        }

        private void AddQuery()
        {
            this.InitializeSolutionSuggestion();
            FormQueryEditor frm = new FormQueryEditor(this.SolutionSuggestion);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string query = frm.ResultQueryString;
                mySettings.Queries.Add(query);
                listViewQueries.Items.Add(query);

                buttonAdd.Enabled = listViewQueries.Items.Count < 5;
            }
        }

        private void EditSelectedQuery()
        {
            if (listViewQueries.SelectedItems.Count == 0)
            {
                return;
            }

            var selectedItem = listViewQueries.SelectedItems[0];

            this.InitializeSolutionSuggestion();
            FormQueryEditor frm = new FormQueryEditor(this.SolutionSuggestion, selectedItem.Text);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string query = frm.ResultQueryString;
                mySettings.Queries[selectedItem.Index] = query;
                listViewQueries.Items[selectedItem.Index].Text = query;
            }
        }

        private void RemoveSelectedQuery()
        {
            if (listViewQueries.SelectedItems.Count == 0)
            {
                return;
            }

            if (MessageBox.Show("Do you want to remove selected query?", "Remove query", MessageBoxButtons.OKCancel) != DialogResult.OK)
            {
                return;
            }

            mySettings.Queries.RemoveAt(listViewQueries.SelectedItems[0].Index);
            listViewQueries.Items.RemoveAt(listViewQueries.SelectedItems[0].Index);

            buttonAdd.Enabled = true;
        }

        private void InitializeSolutionSuggestion()
        {
            if (this.Service == null) return;
            if (SolutionSuggestion == null)
            {
                var solutions = ExportService.GetSolutionLookup(new OrganizationService(this.Service, this.GetConnectionInfo(this.ConnectionDetail)));
                var suggestions = new AutoCompleteStringCollection();

                foreach (var item in solutions) suggestions.Add(item.FirstOrDefault());
                SolutionSuggestion = suggestions;
            }
        }

        private void RunExport()
        {
            List<string> argumentQueries = new List<string>();
            bool includeAllProperty = mySettings.Configuration.IncludeAllProperty;
            bool removeRootDir = checkBoxDeleteDir.Checked;
            bool createZip = checkBoxCreateZipFile.Checked;
            var outputTempDir = CompareResultTempFolder;

            if (listViewQueries.Items.Count == 0)
            {
                MessageBox.Show("At least one query is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxOutputDir.Text))
            {
                MessageBox.Show("Output directory is required");
                return;
            }

            string outputDirectory = textBoxOutputDir.Text;
            try
            {
                outputDirectory = System.IO.Path.GetFullPath(outputDirectory);
            }
            catch
            {
                MessageBox.Show("Invalid directory");
                return;
            }

            for (var i = 0; i < listViewQueries.Items.Count; i++)
            {
                string query = listViewQueries.Items[i].Text;
                if (mySettings.Configuration.IncludeSystemWebresource)
                {
                    query += ";IncludeSystemWebresource=true";
                }
                if (mySettings.Configuration.IncludeSystemPluginStep)
                {
                    query += ";IncludeSystemPluginStep=true";
                }
                if (mySettings.Configuration.IncludeAllProperty)
                {
                    query += ";IncludeAllProperty=true";
                }
                argumentQueries.Add(query);
            }

            var service = new OrganizationService(Service, this.GetConnectionInfo(this.ConnectionDetail));
            service.OnRetryHandler = this.OnRetryHandler;
            this.Logger.Clean();

            groupBoxHelp.Visible = false;
            groupBoxOutputLog.Visible = true;

            groupBox2.Enabled = false;
            tabControl1.Enabled = false;
            richTextBox1.Text = string.Empty;
            twoProgressStatusControl1.ShowSingleProgress = true;
            twoProgressStatusControl1.Value1 = new ProgressStatusValue();
            panelProgress.Visible = true;

            BackgroundWorker bgw = new BackgroundWorker();
            this.CurrentBackgroundWorker = bgw;
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;
            DataTransformer.OnExceptionHandler = this.GetTransformExceptionHandler(bgw);

            bgw.DoWork += (worker, args) =>
            {
                void LogMessage(string message)
                {
                    this.Logger.Log(message);
                    bgw.ReportProgress(-1, new ProgressSnapshot
                    {
                        SnapshotType = ProgressSnapshotType.Log,
                        LogMessage = message,
                    });
                }

                void ReportProgress(string currentLabel, int currentProgress, int overallProgress)
                {
                    bgw.ReportProgress(-1, new ProgressSnapshot
                    {
                        SnapshotType = ProgressSnapshotType.Progress,
                        CurrentProgressLabel = currentLabel,
                        CurrentProgress = currentProgress,
                        OverallProgressLabel = "Exporting...",
                        OverallProgress = overallProgress,
                    });
                }

                ReportProgress("Preparing components...", 0, 0);
                LogMessage("Preparing components...");
                var argumentQueryResponse = ExportService.PrepareComponent(service, argumentQueries, (p) =>
                {
                    ReportProgress("Preparing components...", p, Helper.GetProgressValue(0, 20, p));
                }, bgw);

                if (bgw?.CancellationPending == true)
                {
                    args.Cancel = true;
                    return;
                }
                LogMessage("Preparing components completed.");

                System.Threading.Thread.Sleep(500);

                if (removeRootDir && System.IO.Directory.Exists(outputDirectory))
                {
                    this.SafeDeleteDirectory(outputDirectory);
                    LogMessage($"Directory \"{outputDirectory}\" deleted");
                }

                if (createZip && System.IO.Directory.Exists(System.IO.Path.Combine(outputTempDir, "export")))
                {
                    this.SafeDeleteDirectory(System.IO.Path.Combine(outputTempDir, "export"));
                }

                if (bgw?.CancellationPending == true)
                {
                    args.Cancel = true;
                    return;
                }

                if (argumentQueryResponse.TotalComponents == 0)
                {
                    LogMessage("No component found with provided query.");
                    ReportProgress("Completed", 100, 100);
                    args.Result = argumentQueryResponse;
                    return;
                }

                var outDir = createZip ? System.IO.Path.Combine(outputTempDir, "export") : outputDirectory;

                LogMessage("Exporting...");
                ReportProgress("Exporting...", 0, 20);
                ExportService.Export(service, argumentQueries, argumentQueryResponse, mySettings.Configuration.ToExportSetting(), this.Logger, (path, content, data) =>
                {
                    LogMessage(path);
                    ExportService.WriteFile(System.IO.Path.Combine(outDir, path), content, data);

                }, (snapshot) =>
                {
                    ReportProgress(snapshot.CurrentLabel + (snapshot.CurrentTotal == 0 ? "" : $" ({snapshot.CurrentCompleted}/{snapshot.CurrentTotal})"), snapshot.CurrentProgress, Helper.GetProgressValue(20, 100, snapshot.OverallProgress));
                }, bgw, createZip);

                if (bgw?.CancellationPending == true)
                {
                    args.Cancel = true;
                    return;
                }

                if (createZip)
                {
                    LogMessage("Creating zip...");
                    ExportService.EnsureDirectory(outputDirectory);
                    ZipFile.CreateFromDirectory(outDir, System.IO.Path.Combine(outputTempDir, "export.zip"));
                    var zipFileOutputPath = System.IO.Path.Combine(outputDirectory, $"Export ({ConnectionDetail.ConnectionName}) {DateTime.Now:yyyy-MM-dd-HH-mm-ss}.zip");
                    System.IO.File.Move(System.IO.Path.Combine(outputTempDir, "export.zip"), zipFileOutputPath);
                    this.SafeDeleteDirectory(System.IO.Path.Combine(outputTempDir, "export"));
                }

                LogMessage("Export completed.");
                ReportProgress("Completed", 100, 100);
                System.Threading.Thread.Sleep(500);
            };

            bgw.ProgressChanged += (worker, args) =>
            {
                if (args.UserState != null)
                {
                    if (args.UserState.GetType() == typeof(string))
                    {
                        SetWorkingMessage(args.UserState as string);
                    }
                    else if (args.UserState is ProgressSnapshot)
                    {
                        var snapshot = args.UserState as ProgressSnapshot;

                        if (snapshot.SnapshotType == ProgressSnapshotType.Log)
                        {
                            this.Logs.Add(DateTime.Now.ToString("hh:mm:ss.fff") + ": " + snapshot.LogMessage);
                        }
                        else if (snapshot.SnapshotType == ProgressSnapshotType.Progress)
                        {
                            this.twoProgressStatusControl1.Value1 = new ProgressStatusValue
                            {
                                LabelCurrent = snapshot.CurrentProgressLabel,
                                ProgressCurrent = snapshot.CurrentProgress,
                                LabelOverall = snapshot.OverallProgressLabel,
                                ProgressOverall = snapshot.OverallProgress,
                            };
                        }
                    }
                }
            };

            bgw.RunWorkerCompleted += (worker, args) =>
            {
                groupBox2.Enabled = true;
                tabControl1.Enabled = true;
                panelProgress.Visible = false;
                bgw.Dispose();
                service.Dispose();
                this.CurrentBackgroundWorker = null;
                DataTransformer.OnExceptionHandler = null;
                FlushLogs();

                if (args.Cancelled)
                {
                    this.Logger.Log("Aborted!");
                    this.Logs.Add("Aborted!");
                    FlushLogs();
                    return;
                }

                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (args.Result is Core.Models.ArgumentQueryResponse response && response.TotalComponents == 0)
                    {
                        MessageBox.Show("No component found with provided query.");
                        return;
                    }

                    if (mySettings.Configuration.OpenDirectoryAfterExport)
                    {
                        System.Diagnostics.Process.Start("explorer.exe", outputDirectory);
                    }
                    else
                    {
                        MessageBox.Show("Export completed.");
                    }
                }
            };

            bgw.RunWorkerAsync();
        }

        private void RunCompare()
        {
            List<string> argumentQueries = new List<string>();
            bool includeAllProperty = mySettings.Configuration.IncludeAllProperty;

            if (listViewQueries.Items.Count == 0)
            {
                MessageBox.Show("At least one query is required");
                return;
            }

            for (var i = 0; i < listViewQueries.Items.Count; i++)
            {
                string query = listViewQueries.Items[i].Text;
                if (mySettings.Configuration.IncludeSystemWebresource)
                {
                    query += ";IncludeSystemWebresource=true";
                }
                if (mySettings.Configuration.IncludeSystemPluginStep)
                {
                    query += ";IncludeSystemPluginStep=true";
                }
                if (mySettings.Configuration.IncludeAllProperty)
                {
                    query += ";IncludeAllProperty=true";
                }
                argumentQueries.Add(query);
            }

            if (this.TargetService == null && string.IsNullOrEmpty(ZipFilePath))
            {
                MessageBox.Show("Select target enviornment or a zip file");
                return;
            }

            if (this.TargetService != null && SourceConnectionDetail?.OrganizationServiceUrl == TargetConnectionDetail?.OrganizationServiceUrl)
            {
                MessageBox.Show("Source and Target connection should point to different environment.");
                return;
            }

            var service1 = new OrganizationService(Service, this.GetConnectionInfo(this.ConnectionDetail));
            OrganizationService service2 = string.IsNullOrEmpty(ZipFilePath) ? new OrganizationService(this.TargetService, this.GetConnectionInfo(this.ConnectionDetail)) : null;

            service1.OnRetryHandler = this.OnRetryHandler;
            if (service2 != null) service2.OnRetryHandler = this.OnRetryHandler;

            var outputTempDir = CompareResultTempFolder;

            if (System.IO.Directory.Exists(outputTempDir))
            {
                this.SafeDeleteDirectory(outputTempDir);
            }

            this.Logger.Clean();
            groupBoxHelp.Visible = false;
            groupBoxOutputLog.Visible = true;

            bool runSimultaneously = this.mySettings.Configuration.RunSimultaneously && string.IsNullOrEmpty(ZipFilePath);

            groupBox2.Enabled = false;
            tabControl1.Enabled = false;
            richTextBox1.Text = string.Empty;
            this.twoProgressStatusControl1.ShowSingleProgress = !runSimultaneously;
            this.twoProgressStatusControl1.Value1 = new ProgressStatusValue();
            this.twoProgressStatusControl1.Value2 = new ProgressStatusValue();
            panelProgress.Visible = true;

            BackgroundWorker bgw = new BackgroundWorker();
            this.CurrentBackgroundWorker = bgw;
            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;
            DataTransformer.OnExceptionHandler = this.GetTransformExceptionHandler(bgw);

            bgw.DoWork += (worker, args) =>
            {
                string overallProgressLabel = "Exporting source...";
                string overallProgressLabel2 = "Exporting target...";

                void LogMessage(string message)
                {
                    this.Logger.Log(message);
                    bgw.ReportProgress(-1, new ProgressSnapshot
                    {
                        SnapshotType = ProgressSnapshotType.Log,
                        LogMessage = message,
                    });
                }

                void ReportProgress(string currentLabel, int currentProgress, int overallProgress)
                {
                    bgw.ReportProgress(-1, new ProgressSnapshot
                    {
                        SnapshotType = ProgressSnapshotType.Progress,
                        CurrentProgressLabel = currentLabel,
                        CurrentProgress = currentProgress,
                        OverallProgressLabel = overallProgressLabel,
                        OverallProgress = overallProgress,
                    });
                }

                void ReportProgress2(string currentLabel, int currentProgress, int overallProgress)
                {
                    bgw.ReportProgress(-1, new ProgressSnapshot
                    {
                        SnapshotType = ProgressSnapshotType.Progress2,
                        CurrentProgressLabel = currentLabel,
                        CurrentProgress = currentProgress,
                        OverallProgressLabel = overallProgressLabel2,
                        OverallProgress = overallProgress,
                    });
                }

                int run(OrganizationService service, string outputDirectory, Action<string> log, Action<string, int, int> reportProgress)
                {
                    reportProgress("Preparing components...", 0, 0);
                    log("Preparing components...");
                    var argumentQueryResponse = ExportService.PrepareComponent(service, argumentQueries, (p) =>
                    {
                        reportProgress("Preparing components...", p, Helper.GetProgressValue(0, 20, p));
                    }, bgw);

                    if (bgw?.CancellationPending == true) return 0;

                    log("Preparing components completed.");

                    if (argumentQueryResponse.TotalComponents == 0)
                    {
                        return 0;
                    }

                    reportProgress("Exporting...", 0, 20);
                    ExportService.EnsureDirectory(outputDirectory);
                    ExportService.Export(service, argumentQueries, argumentQueryResponse, mySettings.Configuration.ToExportSetting(), this.Logger, (path, content, data) =>
                    {
                        log(path);
                        ExportService.WriteFile(System.IO.Path.Combine(outputDirectory, path), content, data);

                    }, (snapshot) =>
                    {
                        reportProgress(snapshot.CurrentLabel + (snapshot.CurrentTotal == 0 ? "" : $" ({snapshot.CurrentCompleted}/{snapshot.CurrentTotal})"), snapshot.CurrentProgress, Helper.GetProgressValue(20, 100, snapshot.OverallProgress));
                    }, bgw, true);

                    if (bgw?.CancellationPending == true) return 0;

                    return argumentQueryResponse.TotalComponents;
                }

                var sourceDir = System.IO.Path.Combine(outputTempDir, $"source ({ConnectionDetail.ConnectionName})");
                var targetDir = System.IO.Path.Combine(outputTempDir, $"target ({TargetConnectionName})");
                int sourceCount = 0;
                int targetCount = 0;

                if (runSimultaneously && string.IsNullOrEmpty(ZipFilePath))
                {
                    LogMessage("Exporting source and target simultaneously...");

                    var task1 = Task.Run(() =>
                    {
                        try
                        {
                            sourceCount = run(service1, sourceDir, (string msg) =>
                            {
                                LogMessage("[source] " + msg);
                            },
                            (string currentLabel, int currentProgress, int overallProgress) =>
                            {
                                ReportProgress(currentLabel, currentProgress, overallProgress);
                            });

                            if (bgw?.CancellationPending == true)
                            {
                                args.Cancel = true;
                                return;
                            }

                            ReportProgress("Completed", 100, 100);
                        }
                        catch (Exception ex)
                        {
                            bgw.CancelAsync();
                            throw ex;
                        }
                    });

                    var task2 = Task.Run(() =>
                    {
                        try
                        {
                            targetCount = run(service2, targetDir, (string msg) =>
                            {
                                LogMessage("[target] " + msg);
                            },
                            (string currentLabel, int currentProgress, int overallProgress) =>
                            {
                                ReportProgress2(currentLabel, currentProgress, overallProgress);
                            });

                            if (bgw?.CancellationPending == true)
                            {
                                args.Cancel = true;
                                return;
                            }

                            ReportProgress2("Completed", 100, 100);
                        }
                        catch (Exception ex)
                        {
                            bgw.CancelAsync();
                            throw ex;
                        }
                    });

                    Task.WaitAll(task1, task2);

                    if (bgw?.CancellationPending == true)
                    {
                        args.Cancel = true;
                        return;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(ZipFilePath))
                    {
                        LogMessage("Extracting zip file...");
                        ZipFile.ExtractToDirectory(ZipFilePath, targetDir);
                    }

                    LogMessage("Exporting source...");

                    sourceCount = run(service1, sourceDir, (string msg) =>
                    {
                        LogMessage("[source] " + msg);
                    },
                    (string currentLabel, int currentProgress, int overallProgress) =>
                    {
                        ReportProgress(currentLabel, currentProgress, overallProgress / 2);
                    });

                    if (bgw?.CancellationPending == true)
                    {
                        args.Cancel = true;
                        return;
                    }

                    ReportProgress("Completed", 100, 50);

                    if (string.IsNullOrEmpty(ZipFilePath))
                    {
                        LogMessage("Exporting target...");
                        overallProgressLabel = "Exporting target...";

                        targetCount = run(service2, targetDir, (string msg) =>
                        {
                            LogMessage("[target] " + msg);
                        },
                        (string currentLabel, int currentProgress, int overallProgress) =>
                        {
                            ReportProgress(currentLabel, currentProgress, 50 + overallProgress / 2);
                        });
                    }

                    if (bgw?.CancellationPending == true)
                    {
                        args.Cancel = true;
                        return;
                    }

                    ReportProgress("Completed", 100, 100);
                }

                if (sourceCount + targetCount == 0)
                {
                    LogMessage("No component found with provided query.");
                    args.Result = new Core.Models.ArgumentQueryResponse { };
                    return;
                }

                this.LastResultDirectories = new Tuple<string, string>(sourceDir, targetDir);

                System.Threading.Thread.Sleep(500);
            };

            bgw.ProgressChanged += (worker, args) =>
            {
                if (args.UserState != null)
                {
                    if (args.UserState.GetType() == typeof(string))
                    {
                        SetWorkingMessage(args.UserState as string);
                    }
                    else if (args.UserState is ProgressSnapshot)
                    {
                        var snapshot = args.UserState as ProgressSnapshot;

                        if (snapshot.SnapshotType == ProgressSnapshotType.Log)
                        {
                            this.Logs.Add(DateTime.Now.ToString("hh:mm:ss.fff") + ": " + snapshot.LogMessage);
                        }
                        else if (snapshot.SnapshotType == ProgressSnapshotType.Progress)
                        {
                            this.twoProgressStatusControl1.Value1 = new ProgressStatusValue
                            {
                                LabelCurrent = snapshot.CurrentProgressLabel,
                                ProgressCurrent = snapshot.CurrentProgress,
                                LabelOverall = snapshot.OverallProgressLabel,
                                ProgressOverall = snapshot.OverallProgress,
                            };
                        }
                        else if (snapshot.SnapshotType == ProgressSnapshotType.Progress2)
                        {
                            this.twoProgressStatusControl1.Value2 = new ProgressStatusValue
                            {
                                LabelCurrent = snapshot.CurrentProgressLabel,
                                ProgressCurrent = snapshot.CurrentProgress,
                                LabelOverall = snapshot.OverallProgressLabel,
                                ProgressOverall = snapshot.OverallProgress,
                            };
                        }
                    }
                }
            };

            bgw.RunWorkerCompleted += (worker, args) =>
            {
                groupBox2.Enabled = true;
                tabControl1.Enabled = true;
                panelProgress.Visible = false;
                bgw.Dispose();
                service1.Dispose();
                service2?.Dispose();
                this.CurrentBackgroundWorker = null;
                DataTransformer.OnExceptionHandler = null;
                FlushLogs();

                if (args.Cancelled && args.Error == null)
                {
                    this.Logger.Log("Aborted!");
                    this.Logs.Add("Aborted!");
                    FlushLogs();
                    return;
                }

                if (args.Error != null)
                {
                    MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (args.Result is Core.Models.ArgumentQueryResponse response && response.TotalComponents == 0)
                {
                    MessageBox.Show("No component found with provided query.");
                    return;
                }


                this.Logs.Add("Export completed. Opening compare tool...");
                FlushLogs();
                this.linkLabel1.Enabled = true;
                ViewLastResult();
            };

            bgw.RunWorkerAsync();
        }

        private bool OnRetryHandler(Exception ex)
        {
            if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
            {
                return true;
            }

            return false;
        }

        private DataTransformer.ExceptionHandler GetTransformExceptionHandler(BackgroundWorker bgw)
        {
            bool onTransformExceptionHandler(Exception ex)
            {
                if (bgw.CancellationPending) return false;

                MessageBoxManager.OK = "Continue";
                MessageBoxManager.Cancel = "Abort";
                MessageBoxManager.Register();
                var result = MessageBox.Show(
                    FormatExceptionMessage(ex) + Environment.NewLine + Environment.NewLine + "To skip transformation of the current item, click on continue or abort process and try after removing the component from the query.",
                    "Error",
                    MessageBoxButtons.OKCancel
                );
                MessageBoxManager.Unregister();
                if (result == DialogResult.OK)
                {
                    return true;
                }

                bgw.CancelAsync();
                return false;
            }

            return onTransformExceptionHandler;
        }

        private string FormatExceptionMessage(Exception ex)
        {
            if (ex == null) return null;

            string message = ex.GetType().FullName + ": " + ex.Message;

            if (ex.InnerException != null)
            {
                message += " --> " + FormatExceptionMessage(ex.InnerException);
            }

            return message;
        }

        private void SafeDeleteDirectory(string dir)
        {
            try
            {
                System.IO.Directory.Delete(dir, true);
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                {
                    this.SafeDeleteDirectory(dir);
                    return;
                }

                throw ex;
            }
        }
        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ViewLastResult();
        }

        private void ViewLastResult()
        {
            if (LastResultDirectories == null)
            {
                return;
            }

            string sourceDir = this.LastResultDirectories.Item1;
            string targetDir = this.LastResultDirectories.Item2;

            if (mySettings.Configuration.IsCompareToolConfigured && mySettings.Configuration.IsCompareToolDefault)
            {
                System.Diagnostics.Process.Start(mySettings.Configuration.CompareToolExecutablePath, $"\"{sourceDir}\" \"{targetDir}\"");
            }
            else
            {
                var f = new Comparision.FormComparisionView(
                    mySettings,
                    $@"{sourceDir}\index.json",
                    $@"{targetDir}\index.json"
                );
                f.ShowDialog();
            }
        }

        private OrganizationService.ConnectionInformation GetConnectionInfo(ConnectionDetail detail)
        {
            return new OrganizationService.ConnectionInformation
            {
                ConnectionName = detail.ConnectionName,
                EnvironmentId = detail.EnvironmentId,
                ServerName = detail.ServerName,
                OrganizationUniqueName = detail.Organization,
                OrganizationFriendlyName = detail.OrganizationFriendlyName,
                OrganizationVersion = detail.OrganizationVersion,
                OrganizationMinorVersion = detail.OrganizationMinorVersion,
                OrganizationMajorVersion = detail.OrganizationMajorVersion,
            };
        }
    }

    enum ProgressSnapshotType
    {
        Log,
        Progress,
        Progress2,
    }

    class ProgressSnapshot
    {
        public ProgressSnapshotType SnapshotType { get; set; }
        public string LogMessage { get; set; }
        public string CurrentProgressLabel { get; set; }
        public int CurrentProgress { get; set; }
        public string OverallProgressLabel { get; set; }
        public int OverallProgress { get; set; }
    }
}