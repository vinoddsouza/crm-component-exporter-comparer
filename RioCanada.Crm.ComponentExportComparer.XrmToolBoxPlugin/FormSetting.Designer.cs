
namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    partial class FormSetting
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxIncludeSysPluginStep = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeSysWebresource = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeAllMeta = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBoxOpenDir = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxRunSimultaneously = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseAsDefault = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxCompareTool = new System.Windows.Forms.ComboBox();
            this.checkBoxSwapComparisonSide = new System.Windows.Forms.CheckBox();
            this.textBoxExeFile = new System.Windows.Forms.TextBox();
            this.buttonBrowseFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxUseIDEAsDefault = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonBrowseIDE = new System.Windows.Forms.Button();
            this.comboBoxIDE = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxIDEPath = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBoxVerifyTransformedData = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonOpenLogFolder = new System.Windows.Forms.Button();
            this.buttonSubmitBugReport = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(10, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(490, 432);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.checkBoxIncludeSysPluginStep);
            this.tabPage1.Controls.Add(this.checkBoxIncludeSysWebresource);
            this.tabPage1.Controls.Add(this.checkBoxIncludeAllMeta);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(482, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "General";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeSysPluginStep
            // 
            this.checkBoxIncludeSysPluginStep.AutoSize = true;
            this.checkBoxIncludeSysPluginStep.Location = new System.Drawing.Point(10, 69);
            this.checkBoxIncludeSysPluginStep.Name = "checkBoxIncludeSysPluginStep";
            this.checkBoxIncludeSysPluginStep.Size = new System.Drawing.Size(194, 23);
            this.checkBoxIncludeSysPluginStep.TabIndex = 28;
            this.checkBoxIncludeSysPluginStep.Text = "Include system plugin step";
            this.checkBoxIncludeSysPluginStep.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeSysWebresource
            // 
            this.checkBoxIncludeSysWebresource.AutoSize = true;
            this.checkBoxIncludeSysWebresource.Location = new System.Drawing.Point(10, 40);
            this.checkBoxIncludeSysWebresource.Name = "checkBoxIncludeSysWebresource";
            this.checkBoxIncludeSysWebresource.Size = new System.Drawing.Size(203, 23);
            this.checkBoxIncludeSysWebresource.TabIndex = 27;
            this.checkBoxIncludeSysWebresource.Text = "Include system webresource";
            this.checkBoxIncludeSysWebresource.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeAllMeta
            // 
            this.checkBoxIncludeAllMeta.AutoSize = true;
            this.checkBoxIncludeAllMeta.Location = new System.Drawing.Point(10, 11);
            this.checkBoxIncludeAllMeta.Name = "checkBoxIncludeAllMeta";
            this.checkBoxIncludeAllMeta.Size = new System.Drawing.Size(211, 23);
            this.checkBoxIncludeAllMeta.TabIndex = 26;
            this.checkBoxIncludeAllMeta.Text = "Include all metadata property";
            this.checkBoxIncludeAllMeta.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBoxOpenDir);
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(482, 402);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Export";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBoxOpenDir
            // 
            this.checkBoxOpenDir.AutoSize = true;
            this.checkBoxOpenDir.Location = new System.Drawing.Point(10, 11);
            this.checkBoxOpenDir.Name = "checkBoxOpenDir";
            this.checkBoxOpenDir.Size = new System.Drawing.Size(198, 23);
            this.checkBoxOpenDir.TabIndex = 27;
            this.checkBoxOpenDir.Text = "Open directory after export";
            this.checkBoxOpenDir.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxRunSimultaneously);
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(482, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Compare";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // checkBoxRunSimultaneously
            // 
            this.checkBoxRunSimultaneously.AutoSize = true;
            this.checkBoxRunSimultaneously.Location = new System.Drawing.Point(17, 16);
            this.checkBoxRunSimultaneously.Name = "checkBoxRunSimultaneously";
            this.checkBoxRunSimultaneously.Size = new System.Drawing.Size(277, 23);
            this.checkBoxRunSimultaneously.TabIndex = 36;
            this.checkBoxRunSimultaneously.Text = "Export source and target simultaneously";
            this.checkBoxRunSimultaneously.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBoxUseAsDefault);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.comboBoxCompareTool);
            this.groupBox2.Controls.Add(this.checkBoxSwapComparisonSide);
            this.groupBox2.Controls.Add(this.textBoxExeFile);
            this.groupBox2.Controls.Add(this.buttonBrowseFile);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(6, 65);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(470, 145);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Compare tool";
            // 
            // checkBoxUseAsDefault
            // 
            this.checkBoxUseAsDefault.AutoSize = true;
            this.checkBoxUseAsDefault.Location = new System.Drawing.Point(11, 100);
            this.checkBoxUseAsDefault.Name = "checkBoxUseAsDefault";
            this.checkBoxUseAsDefault.Size = new System.Drawing.Size(117, 23);
            this.checkBoxUseAsDefault.TabIndex = 29;
            this.checkBoxUseAsDefault.Text = "Use as default";
            this.checkBoxUseAsDefault.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tool";
            // 
            // comboBoxCompareTool
            // 
            this.comboBoxCompareTool.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCompareTool.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCompareTool.FormattingEnabled = true;
            this.comboBoxCompareTool.Location = new System.Drawing.Point(106, 21);
            this.comboBoxCompareTool.Name = "comboBoxCompareTool";
            this.comboBoxCompareTool.Size = new System.Drawing.Size(181, 25);
            this.comboBoxCompareTool.TabIndex = 0;
            // 
            // checkBoxSwapComparisonSide
            // 
            this.checkBoxSwapComparisonSide.AutoSize = true;
            this.checkBoxSwapComparisonSide.Location = new System.Drawing.Point(410, 100);
            this.checkBoxSwapComparisonSide.Name = "checkBoxSwapComparisonSide";
            this.checkBoxSwapComparisonSide.Size = new System.Drawing.Size(186, 23);
            this.checkBoxSwapComparisonSide.TabIndex = 28;
            this.checkBoxSwapComparisonSide.Text = "Reverse preview direction";
            this.checkBoxSwapComparisonSide.UseVisualStyleBackColor = true;
            this.checkBoxSwapComparisonSide.Visible = false;
            // 
            // textBoxExeFile
            // 
            this.textBoxExeFile.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxExeFile.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxExeFile.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxExeFile.Location = new System.Drawing.Point(106, 60);
            this.textBoxExeFile.Name = "textBoxExeFile";
            this.textBoxExeFile.Size = new System.Drawing.Size(326, 25);
            this.textBoxExeFile.TabIndex = 2;
            // 
            // buttonBrowseFile
            // 
            this.buttonBrowseFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseFile.Location = new System.Drawing.Point(438, 59);
            this.buttonBrowseFile.Name = "buttonBrowseFile";
            this.buttonBrowseFile.Size = new System.Drawing.Size(26, 24);
            this.buttonBrowseFile.TabIndex = 27;
            this.buttonBrowseFile.Text = "...";
            this.buttonBrowseFile.UseVisualStyleBackColor = true;
            this.buttonBrowseFile.Click += new System.EventHandler(this.buttonBrowseFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Executable file";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBoxUseIDEAsDefault);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonBrowseIDE);
            this.groupBox1.Controls.Add(this.comboBoxIDE);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxIDEPath);
            this.groupBox1.Location = new System.Drawing.Point(6, 216);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 134);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Default IDE";
            // 
            // checkBoxUseIDEAsDefault
            // 
            this.checkBoxUseIDEAsDefault.AutoSize = true;
            this.checkBoxUseIDEAsDefault.Location = new System.Drawing.Point(11, 94);
            this.checkBoxUseIDEAsDefault.Name = "checkBoxUseIDEAsDefault";
            this.checkBoxUseIDEAsDefault.Size = new System.Drawing.Size(117, 23);
            this.checkBoxUseIDEAsDefault.TabIndex = 34;
            this.checkBoxUseIDEAsDefault.Text = "Use as default";
            this.checkBoxUseIDEAsDefault.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 19);
            this.label3.TabIndex = 30;
            this.label3.Text = "IDE";
            // 
            // buttonBrowseIDE
            // 
            this.buttonBrowseIDE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonBrowseIDE.Location = new System.Drawing.Point(438, 55);
            this.buttonBrowseIDE.Name = "buttonBrowseIDE";
            this.buttonBrowseIDE.Size = new System.Drawing.Size(26, 24);
            this.buttonBrowseIDE.TabIndex = 33;
            this.buttonBrowseIDE.Text = "...";
            this.buttonBrowseIDE.UseVisualStyleBackColor = true;
            this.buttonBrowseIDE.Click += new System.EventHandler(this.buttonBrowseIDE_Click);
            // 
            // comboBoxIDE
            // 
            this.comboBoxIDE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxIDE.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxIDE.FormattingEnabled = true;
            this.comboBoxIDE.Location = new System.Drawing.Point(105, 21);
            this.comboBoxIDE.Name = "comboBoxIDE";
            this.comboBoxIDE.Size = new System.Drawing.Size(181, 25);
            this.comboBoxIDE.TabIndex = 29;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 19);
            this.label4.TabIndex = 32;
            this.label4.Text = "Executable file";
            // 
            // textBoxIDEPath
            // 
            this.textBoxIDEPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.textBoxIDEPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.textBoxIDEPath.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxIDEPath.Location = new System.Drawing.Point(105, 56);
            this.textBoxIDEPath.Name = "textBoxIDEPath";
            this.textBoxIDEPath.Size = new System.Drawing.Size(327, 25);
            this.textBoxIDEPath.TabIndex = 31;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.buttonSubmitBugReport);
            this.tabPage4.Controls.Add(this.buttonOpenLogFolder);
            this.tabPage4.Controls.Add(this.checkBoxVerifyTransformedData);
            this.tabPage4.Location = new System.Drawing.Point(4, 26);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(482, 402);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Debug";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // checkBoxVerifyTransformedData
            // 
            this.checkBoxVerifyTransformedData.AutoSize = true;
            this.checkBoxVerifyTransformedData.Location = new System.Drawing.Point(10, 11);
            this.checkBoxVerifyTransformedData.Name = "checkBoxVerifyTransformedData";
            this.checkBoxVerifyTransformedData.Size = new System.Drawing.Size(178, 23);
            this.checkBoxVerifyTransformedData.TabIndex = 28;
            this.checkBoxVerifyTransformedData.Text = "Verify Transformed Data";
            this.checkBoxVerifyTransformedData.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(430, 451);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(66, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(359, 451);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(66, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonOpenLogFolder
            // 
            this.buttonOpenLogFolder.Location = new System.Drawing.Point(10, 65);
            this.buttonOpenLogFolder.Name = "buttonOpenLogFolder";
            this.buttonOpenLogFolder.Size = new System.Drawing.Size(153, 27);
            this.buttonOpenLogFolder.TabIndex = 6;
            this.buttonOpenLogFolder.Text = "Open log folder";
            this.buttonOpenLogFolder.UseVisualStyleBackColor = true;
            this.buttonOpenLogFolder.Click += new System.EventHandler(this.buttonOpenLogFolder_Click);
            // 
            // buttonSubmitBugReport
            // 
            this.buttonSubmitBugReport.Location = new System.Drawing.Point(10, 98);
            this.buttonSubmitBugReport.Name = "buttonSubmitBugReport";
            this.buttonSubmitBugReport.Size = new System.Drawing.Size(153, 27);
            this.buttonSubmitBugReport.TabIndex = 29;
            this.buttonSubmitBugReport.Text = "Submit a bug report";
            this.buttonSubmitBugReport.UseVisualStyleBackColor = true;
            this.buttonSubmitBugReport.Click += new System.EventHandler(this.buttonSubmitBugReport_Click);
            // 
            // FormSetting
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(511, 490);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCompareTool;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxExeFile;
        private System.Windows.Forms.Button buttonBrowseFile;
        private System.Windows.Forms.CheckBox checkBoxIncludeAllMeta;
        private System.Windows.Forms.CheckBox checkBoxIncludeSysWebresource;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.CheckBox checkBoxOpenDir;
        private System.Windows.Forms.CheckBox checkBoxSwapComparisonSide;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxIDE;
        private System.Windows.Forms.Button buttonBrowseIDE;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxIDEPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxUseAsDefault;
        private System.Windows.Forms.CheckBox checkBoxIncludeSysPluginStep;
        private System.Windows.Forms.CheckBox checkBoxUseIDEAsDefault;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.CheckBox checkBoxVerifyTransformedData;
        private System.Windows.Forms.CheckBox checkBoxRunSimultaneously;
        private System.Windows.Forms.Button buttonSubmitBugReport;
        private System.Windows.Forms.Button buttonOpenLogFolder;
    }
}