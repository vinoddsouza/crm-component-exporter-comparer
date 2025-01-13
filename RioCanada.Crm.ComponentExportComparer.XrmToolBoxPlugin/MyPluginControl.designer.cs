namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    partial class MyPluginControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue progressStatusValue3 = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue();
            RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue progressStatusValue4 = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSetting = new System.Windows.Forms.ToolStripButton();
            this.checkBoxDeleteDir = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.buttonQuerySetting = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listViewQueries = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxOutputDir = new System.Windows.Forms.TextBox();
            this.buttonBrowseDir = new System.Windows.Forms.Button();
            this.buttonTrgEnv = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelZipFile = new System.Windows.Forms.Label();
            this.buttonZipFile = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.buttonCompare = new System.Windows.Forms.Button();
            this.labelTrgEnv = new System.Windows.Forms.Label();
            this.labelSrcEnv = new System.Windows.Forms.Label();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxCreateZipFile = new System.Windows.Forms.CheckBox();
            this.buttonExport = new System.Windows.Forms.Button();
            this.groupBoxOutputLog = new System.Windows.Forms.GroupBox();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.buttonAbort = new System.Windows.Forms.Button();
            this.twoProgressStatusControl1 = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.TwoProgressStatusControl();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBoxHelp = new System.Windows.Forms.GroupBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.toolStripMenu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxOutputLog.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.groupBoxHelp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbSetting});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.toolStripMenu.Size = new System.Drawing.Size(1192, 27);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(49, 24);
            this.tsbClose.Text = "Close";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // tsbSetting
            // 
            this.tsbSetting.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSetting.Name = "tsbSetting";
            this.tsbSetting.Size = new System.Drawing.Size(66, 24);
            this.tsbSetting.Text = "Settings";
            this.tsbSetting.Click += new System.EventHandler(this.tsbSetting_Click);
            // 
            // checkBoxDeleteDir
            // 
            this.checkBoxDeleteDir.AutoSize = true;
            this.checkBoxDeleteDir.Location = new System.Drawing.Point(133, 50);
            this.checkBoxDeleteDir.Name = "checkBoxDeleteDir";
            this.checkBoxDeleteDir.Size = new System.Drawing.Size(184, 23);
            this.checkBoxDeleteDir.TabIndex = 24;
            this.checkBoxDeleteDir.Text = "Delete directory (if exists)";
            this.checkBoxDeleteDir.UseVisualStyleBackColor = true;
            this.checkBoxDeleteDir.CheckedChanged += new System.EventHandler(this.checkBoxDeleteDir_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.buttonQuerySetting);
            this.groupBox2.Controls.Add(this.buttonRemove);
            this.groupBox2.Controls.Add(this.buttonEdit);
            this.groupBox2.Controls.Add(this.buttonAdd);
            this.groupBox2.Controls.Add(this.listViewQueries);
            this.groupBox2.Location = new System.Drawing.Point(3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(649, 199);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Queries";
            // 
            // buttonQuerySetting
            // 
            this.buttonQuerySetting.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonQuerySetting.Location = new System.Drawing.Point(567, 126);
            this.buttonQuerySetting.Name = "buttonQuerySetting";
            this.buttonQuerySetting.Size = new System.Drawing.Size(71, 28);
            this.buttonQuerySetting.TabIndex = 33;
            this.buttonQuerySetting.Text = "Options";
            this.buttonQuerySetting.UseVisualStyleBackColor = true;
            this.buttonQuerySetting.Click += new System.EventHandler(this.buttonQuerySetting_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(567, 92);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(71, 28);
            this.buttonRemove.TabIndex = 32;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonEdit.Enabled = false;
            this.buttonEdit.Location = new System.Drawing.Point(567, 59);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(71, 27);
            this.buttonEdit.TabIndex = 31;
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAdd.Location = new System.Drawing.Point(567, 26);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(71, 27);
            this.buttonAdd.TabIndex = 30;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // listViewQueries
            // 
            this.listViewQueries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewQueries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listViewQueries.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listViewQueries.FullRowSelect = true;
            this.listViewQueries.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.listViewQueries.HideSelection = false;
            this.listViewQueries.Location = new System.Drawing.Point(13, 26);
            this.listViewQueries.MultiSelect = false;
            this.listViewQueries.Name = "listViewQueries";
            this.listViewQueries.Scrollable = false;
            this.listViewQueries.ShowGroups = false;
            this.listViewQueries.ShowItemToolTips = true;
            this.listViewQueries.Size = new System.Drawing.Size(542, 157);
            this.listViewQueries.TabIndex = 27;
            this.listViewQueries.UseCompatibleStateImageBehavior = false;
            this.listViewQueries.View = System.Windows.Forms.View.Details;
            this.listViewQueries.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listViewQueries.DoubleClick += new System.EventHandler(this.listViewQueries_DoubleClick);
            this.listViewQueries.KeyDown += new System.Windows.Forms.KeyEventHandler(this.listViewQueries_KeyDown);
            this.listViewQueries.Resize += new System.EventHandler(this.listView1_Resize);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Query";
            this.columnHeader1.Width = 500;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 13);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 19);
            this.label6.TabIndex = 25;
            this.label6.Text = "Output directory";
            // 
            // textBoxOutputDir
            // 
            this.textBoxOutputDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxOutputDir.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxOutputDir.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.textBoxOutputDir.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxOutputDir.Location = new System.Drawing.Point(133, 10);
            this.textBoxOutputDir.Name = "textBoxOutputDir";
            this.textBoxOutputDir.Size = new System.Drawing.Size(459, 25);
            this.textBoxOutputDir.TabIndex = 24;
            this.textBoxOutputDir.TextChanged += new System.EventHandler(this.textBoxOutputDir_TextChanged);
            // 
            // buttonBrowseDir
            // 
            this.buttonBrowseDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseDir.Location = new System.Drawing.Point(598, 9);
            this.buttonBrowseDir.Name = "buttonBrowseDir";
            this.buttonBrowseDir.Size = new System.Drawing.Size(26, 24);
            this.buttonBrowseDir.TabIndex = 26;
            this.buttonBrowseDir.Text = "...";
            this.buttonBrowseDir.UseVisualStyleBackColor = true;
            this.buttonBrowseDir.Click += new System.EventHandler(this.buttonBrowseDir_Click);
            // 
            // buttonTrgEnv
            // 
            this.buttonTrgEnv.Location = new System.Drawing.Point(6, 24);
            this.buttonTrgEnv.Name = "buttonTrgEnv";
            this.buttonTrgEnv.Size = new System.Drawing.Size(136, 28);
            this.buttonTrgEnv.TabIndex = 24;
            this.buttonTrgEnv.Text = "Target Enviornment";
            this.buttonTrgEnv.UseVisualStyleBackColor = true;
            this.buttonTrgEnv.Click += new System.EventHandler(this.buttonTrgEnv_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 19);
            this.label8.TabIndex = 29;
            this.label8.Text = "Source Enviornment";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(3, 222);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(647, 350);
            this.tabControl1.TabIndex = 27;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.linkLabel1);
            this.tabPage2.Controls.Add(this.buttonCompare);
            this.tabPage2.Controls.Add(this.labelSrcEnv);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(639, 320);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Compare";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelZipFile
            // 
            this.labelZipFile.AutoSize = true;
            this.labelZipFile.Cursor = System.Windows.Forms.Cursors.Default;
            this.labelZipFile.Location = new System.Drawing.Point(166, 98);
            this.labelZipFile.Name = "labelZipFile";
            this.labelZipFile.Size = new System.Drawing.Size(85, 19);
            this.labelZipFile.TabIndex = 36;
            this.labelZipFile.Text = "Not selected";
            // 
            // buttonZipFile
            // 
            this.buttonZipFile.Location = new System.Drawing.Point(6, 92);
            this.buttonZipFile.Name = "buttonZipFile";
            this.buttonZipFile.Size = new System.Drawing.Size(136, 28);
            this.buttonZipFile.TabIndex = 35;
            this.buttonZipFile.Text = "Select Zip File";
            this.buttonZipFile.UseVisualStyleBackColor = true;
            this.buttonZipFile.Click += new System.EventHandler(this.buttonZipFile_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DimGray;
            this.label1.Location = new System.Drawing.Point(14, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 19);
            this.label1.TabIndex = 34;
            this.label1.Text = "––––––– Or –––––––";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Enabled = false;
            this.linkLabel1.Location = new System.Drawing.Point(20, 241);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(101, 19);
            this.linkLabel1.TabIndex = 33;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "View last result";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // buttonCompare
            // 
            this.buttonCompare.Enabled = false;
            this.buttonCompare.Location = new System.Drawing.Point(18, 200);
            this.buttonCompare.Name = "buttonCompare";
            this.buttonCompare.Size = new System.Drawing.Size(134, 28);
            this.buttonCompare.TabIndex = 32;
            this.buttonCompare.Text = "&Compare";
            this.buttonCompare.UseVisualStyleBackColor = true;
            this.buttonCompare.Click += new System.EventHandler(this.buttonCompare_Click);
            // 
            // labelTrgEnv
            // 
            this.labelTrgEnv.AutoSize = true;
            this.labelTrgEnv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelTrgEnv.Location = new System.Drawing.Point(166, 30);
            this.labelTrgEnv.Name = "labelTrgEnv";
            this.labelTrgEnv.Size = new System.Drawing.Size(85, 19);
            this.labelTrgEnv.TabIndex = 31;
            this.labelTrgEnv.Text = "Not selected";
            this.labelTrgEnv.Click += new System.EventHandler(this.labelTrgEnv_Click);
            // 
            // labelSrcEnv
            // 
            this.labelSrcEnv.AutoSize = true;
            this.labelSrcEnv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.labelSrcEnv.Location = new System.Drawing.Point(176, 17);
            this.labelSrcEnv.Name = "labelSrcEnv";
            this.labelSrcEnv.Size = new System.Drawing.Size(85, 19);
            this.labelSrcEnv.TabIndex = 30;
            this.labelSrcEnv.Text = "Not selected";
            this.labelSrcEnv.Click += new System.EventHandler(this.labelSrcEnv_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxCreateZipFile);
            this.tabPage1.Controls.Add(this.buttonExport);
            this.tabPage1.Controls.Add(this.checkBoxDeleteDir);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.textBoxOutputDir);
            this.tabPage1.Controls.Add(this.buttonBrowseDir);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(639, 320);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Export";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxCreateZipFile
            // 
            this.checkBoxCreateZipFile.AutoSize = true;
            this.checkBoxCreateZipFile.Location = new System.Drawing.Point(133, 79);
            this.checkBoxCreateZipFile.Name = "checkBoxCreateZipFile";
            this.checkBoxCreateZipFile.Size = new System.Drawing.Size(113, 23);
            this.checkBoxCreateZipFile.TabIndex = 28;
            this.checkBoxCreateZipFile.Text = "Create zip file";
            this.checkBoxCreateZipFile.UseVisualStyleBackColor = true;
            // 
            // buttonExport
            // 
            this.buttonExport.Enabled = false;
            this.buttonExport.Location = new System.Drawing.Point(19, 118);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(70, 28);
            this.buttonExport.TabIndex = 27;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            this.buttonExport.Click += new System.EventHandler(this.buttonExport_Click);
            // 
            // groupBoxOutputLog
            // 
            this.groupBoxOutputLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxOutputLog.Controls.Add(this.panelProgress);
            this.groupBoxOutputLog.Controls.Add(this.richTextBox1);
            this.groupBoxOutputLog.Location = new System.Drawing.Point(5, 4);
            this.groupBoxOutputLog.Name = "groupBoxOutputLog";
            this.groupBoxOutputLog.Size = new System.Drawing.Size(527, 568);
            this.groupBoxOutputLog.TabIndex = 28;
            this.groupBoxOutputLog.TabStop = false;
            this.groupBoxOutputLog.Text = "Output log";
            this.groupBoxOutputLog.Visible = false;
            // 
            // panelProgress
            // 
            this.panelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProgress.Controls.Add(this.buttonAbort);
            this.panelProgress.Controls.Add(this.twoProgressStatusControl1);
            this.panelProgress.Location = new System.Drawing.Point(2, 406);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(523, 160);
            this.panelProgress.TabIndex = 4;
            this.panelProgress.Visible = false;
            // 
            // buttonAbort
            // 
            this.buttonAbort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAbort.Enabled = false;
            this.buttonAbort.Location = new System.Drawing.Point(4, 6);
            this.buttonAbort.Name = "buttonAbort";
            this.buttonAbort.Size = new System.Drawing.Size(75, 28);
            this.buttonAbort.TabIndex = 33;
            this.buttonAbort.Text = "Abort";
            this.buttonAbort.UseVisualStyleBackColor = true;
            this.buttonAbort.Click += new System.EventHandler(this.buttonAbort_Click);
            // 
            // twoProgressStatusControl1
            // 
            this.twoProgressStatusControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twoProgressStatusControl1.Location = new System.Drawing.Point(4, 57);
            this.twoProgressStatusControl1.Name = "twoProgressStatusControl1";
            this.twoProgressStatusControl1.ShowSingleProgress = false;
            this.twoProgressStatusControl1.Size = new System.Drawing.Size(515, 98);
            this.twoProgressStatusControl1.TabIndex = 34;
            progressStatusValue3.LabelCurrent = null;
            progressStatusValue3.LabelOverall = null;
            progressStatusValue3.ProgressCurrent = 0;
            progressStatusValue3.ProgressOverall = 0;
            this.twoProgressStatusControl1.Value1 = progressStatusValue3;
            progressStatusValue4.LabelCurrent = null;
            progressStatusValue4.LabelOverall = null;
            progressStatusValue4.ProgressCurrent = 0;
            progressStatusValue4.ProgressOverall = 0;
            this.twoProgressStatusControl1.Value2 = progressStatusValue4;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.White;
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.Location = new System.Drawing.Point(6, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(515, 375);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // groupBoxHelp
            // 
            this.groupBoxHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxHelp.Controls.Add(this.linkLabel2);
            this.groupBoxHelp.Location = new System.Drawing.Point(5, 4);
            this.groupBoxHelp.Name = "groupBoxHelp";
            this.groupBoxHelp.Size = new System.Drawing.Size(527, 568);
            this.groupBoxHelp.TabIndex = 1;
            this.groupBoxHelp.TabStop = false;
            this.groupBoxHelp.Text = "Help";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(6, 40);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(111, 23);
            this.linkLabel2.TabIndex = 0;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Click to view help";
            this.linkLabel2.UseCompatibleTextRendering = true;
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox2);
            this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxHelp);
            this.splitContainer1.Panel2.Controls.Add(this.groupBoxOutputLog);
            this.splitContainer1.Size = new System.Drawing.Size(1192, 575);
            this.splitContainer1.SplitterDistance = 653;
            this.splitContainer1.TabIndex = 34;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonTrgEnv);
            this.groupBox1.Controls.Add(this.labelZipFile);
            this.groupBox1.Controls.Add(this.labelTrgEnv);
            this.groupBox1.Controls.Add(this.buttonZipFile);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 47);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 133);
            this.groupBox1.TabIndex = 37;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target";
            // 
            // MyPluginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStripMenu);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MyPluginControl";
            this.Size = new System.Drawing.Size(1192, 602);
            this.Load += new System.EventHandler(this.MyPluginControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBoxOutputLog.ResumeLayout(false);
            this.panelProgress.ResumeLayout(false);
            this.groupBoxHelp.ResumeLayout(false);
            this.groupBoxHelp.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbSetting;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.CheckBox checkBoxDeleteDir;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxOutputDir;
        private System.Windows.Forms.Button buttonBrowseDir;
        private System.Windows.Forms.ListView listViewQueries;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonTrgEnv;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label labelSrcEnv;
        private System.Windows.Forms.Label labelTrgEnv;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonCompare;
        private System.Windows.Forms.GroupBox groupBoxOutputLog;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.GroupBox groupBoxHelp;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private TwoProgressStatusControl twoProgressStatusControl1;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button buttonAbort;
        private System.Windows.Forms.Button buttonQuerySetting;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonZipFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelZipFile;
        private System.Windows.Forms.CheckBox checkBoxCreateZipFile;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
