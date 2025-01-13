using RioCanada.Crm.ComponentExportComparer.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    partial class FormSetting : Form
    {
        public SettingConfiguration Configuration { get; set; }
        private Logger Logger { get; set; }

        public FormSetting(SettingConfiguration configuration, Logger logger)
        {
            InitializeComponent();

            comboBoxCompareTool.Items.Add("DiffMerge");

            comboBoxIDE.Items.Add("VS Code");
            comboBoxIDE.Items.Add("Notepad++");
            comboBoxIDE.Items.Add("Notepad");
            comboBoxIDE.Items.Add("Other");

            this.Logger = logger;
            this.Configuration = configuration;
            this.checkBoxIncludeAllMeta.Checked = configuration.IncludeAllProperty;
            this.checkBoxIncludeSysWebresource.Checked = configuration.IncludeSystemWebresource;
            this.checkBoxIncludeSysPluginStep.Checked = configuration.IncludeSystemPluginStep;
            this.checkBoxOpenDir.Checked = configuration.OpenDirectoryAfterExport;
            this.checkBoxSwapComparisonSide.Checked = configuration.SwapComparisonSide;
            this.checkBoxUseAsDefault.Checked = configuration.IsCompareToolDefault;
            this.textBoxExeFile.Text = configuration.CompareToolExecutablePath;
            this.checkBoxUseIDEAsDefault.Checked = configuration.IsIDEDefault;
            this.checkBoxVerifyTransformedData.Checked = configuration.VerifyTransformedData;
            this.checkBoxRunSimultaneously.Checked = configuration.RunSimultaneously;

            if (!string.IsNullOrWhiteSpace(configuration.CompareToolType))
            {
                this.comboBoxCompareTool.SelectedItem = configuration.CompareToolType;
            }

            if (configuration.DefaultIDE != null)
            {
                this.comboBoxIDE.SelectedItem = configuration.DefaultIDE.DisplayName;
            }
        }

        private void buttonBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog
            {
                Filter = "Executable file|*.exe"
            };
            if (opd.ShowDialog() == DialogResult.OK)
            {
                textBoxExeFile.Text = opd.FileName;
            }
        }

        private void buttonBrowseIDE_Click(object sender, EventArgs e)
        {
            OpenFileDialog opd = new OpenFileDialog
            {
                Filter = "Executable file|*.exe"
            };
            if (opd.ShowDialog() == DialogResult.OK)
            {
                textBoxIDEPath.Text = opd.FileName;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBoxExeFile.Text))
            {
                if (!System.IO.File.Exists(textBoxExeFile.Text))
                {
                    MessageBox.Show("Executable file not exists");
                    return;
                }

                if (System.IO.Path.GetExtension(textBoxExeFile.Text) != ".exe")
                {
                    MessageBox.Show("Invalid executable file");
                    return;
                }
            }

            try
            {
                this.Configuration.DefaultIDE = this.GetDefaultIDE();
            }
            catch
            {
                MessageBox.Show("Invalid executable file");
                return;
            }

            this.Configuration.IncludeAllProperty = checkBoxIncludeAllMeta.Checked;
            this.Configuration.IncludeSystemWebresource = checkBoxIncludeSysWebresource.Checked;
            this.Configuration.IncludeSystemPluginStep = checkBoxIncludeSysPluginStep.Checked;
            this.Configuration.OpenDirectoryAfterExport = checkBoxOpenDir.Checked;
            this.Configuration.SwapComparisonSide = checkBoxSwapComparisonSide.Checked;
            this.Configuration.CompareToolType = comboBoxCompareTool.SelectedItem as string;
            this.Configuration.CompareToolExecutablePath = textBoxExeFile.Text;
            this.Configuration.IsCompareToolDefault = checkBoxUseAsDefault.Checked;
            this.Configuration.IsIDEDefault = checkBoxUseIDEAsDefault.Checked;
            this.Configuration.VerifyTransformedData = checkBoxVerifyTransformedData.Checked;
            this.Configuration.RunSimultaneously = checkBoxRunSimultaneously.Checked;

            this.DialogResult = DialogResult.OK;
        }

        private IDE GetDefaultIDE()
        {
            if (!string.IsNullOrWhiteSpace(textBoxIDEPath.Text))
            {
                if (!System.IO.File.Exists(textBoxIDEPath.Text))
                {
                    throw new Exception("IDE file not exists");
                }

                if (System.IO.Path.GetExtension(textBoxIDEPath.Text) != ".exe")
                {
                    throw new Exception("Invalid IDE file");
                }
            }

            string selectedItem = this.comboBoxIDE.SelectedItem as string;

            if (selectedItem == "Other")
            {
                if (string.IsNullOrWhiteSpace(textBoxIDEPath.Text))
                {
                    throw new Exception("IDE file path is required");
                }

                return new IDE
                {
                    DisplayName = "Other",
                    ExecutablePath = textBoxIDEPath.Text,
                    Type = IDEType.Other,
                };
            }

            string path = string.IsNullOrWhiteSpace(textBoxIDEPath.Text) ? null : textBoxIDEPath.Text;

            if (selectedItem == "VS Code")
            {
                return new IDE
                {
                    DisplayName = "VS Code",
                    ExecutablePath = path,
                    Type = IDEType.VSCode,
                };
            }
            else if (selectedItem == "Notepad++")
            {
                return new IDE
                {
                    DisplayName = "Notepad++",
                    ExecutablePath = path,
                    Type = IDEType.NotepadPlus
                };
            }

            return new IDE
            {
                DisplayName = "Notepad",
                Type = IDEType.Notepad,
            };
        }

        private void buttonOpenLogFolder_Click(object sender, EventArgs e)
        {
            if (!System.IO.Directory.Exists(this.Logger.LogFolder))
            {
                MessageBox.Show("Logs are not generated yet. Compare or Export will generate log.");
                return;
            }

            System.Diagnostics.Process.Start("explorer.exe", this.Logger.LogFolder);
        }

        private void buttonSubmitBugReport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/vinoddsouza/crm-component-comparer-exporter/issues");
        }
    }
}
