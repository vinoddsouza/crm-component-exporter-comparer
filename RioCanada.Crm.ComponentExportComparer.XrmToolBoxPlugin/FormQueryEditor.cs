using Microsoft.Xrm.Sdk;
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

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    partial class FormQueryEditor : Form
    {
        public string ResultQueryString { get; private set; }

        public FormQueryEditor(AutoCompleteStringCollection solutionSuggestion, string query = null)
        {
            InitializeComponent();
            this.InitializePlaceholder();

            if (!string.IsNullOrWhiteSpace(query))
            {
                var argumentQueryRequest = ArgumentQueryRequest.Parse(query);

                if (argumentQueryRequest.Solutions.Count > 0)
                {
                    textBoxSolution.Text = string.Join(",", argumentQueryRequest.Solutions);
                }

                if (argumentQueryRequest.EntityPatterns.Count > 0)
                {
                    textBoxEntity.Text = string.Join(",", argumentQueryRequest.EntityPatterns);
                }

                if (argumentQueryRequest.WebResourcePatterns.Count > 0)
                {
                    textBoxWebresource.Text = string.Join(",", argumentQueryRequest.WebResourcePatterns);
                }

                if (argumentQueryRequest.PluginStepPatterns.Count > 0)
                {
                    textBoxPluginstep.Text = string.Join(",", argumentQueryRequest.PluginStepPatterns);
                }

                if (argumentQueryRequest.OptionSetPatterns.Count > 0)
                {
                    textBoxOptionSet.Text = string.Join(",", argumentQueryRequest.OptionSetPatterns);
                }

                if (argumentQueryRequest.DashboardPatterns.Count > 0)
                {
                    textBoxDashboard.Text = string.Join(",", argumentQueryRequest.DashboardPatterns);
                }

                if (argumentQueryRequest.SiteMapPatterns.Count > 0)
                {
                    textBoxSiteMap.Text = string.Join(",", argumentQueryRequest.SiteMapPatterns);
                }

                if (argumentQueryRequest.SecurityRolePatterns.Count > 0)
                {
                    textBoxSecurityRole.Text = string.Join(",", argumentQueryRequest.SecurityRolePatterns);
                }

                if (argumentQueryRequest.WorkflowPatterns.Count > 0)
                {
                    textBoxWorkflow.Text = string.Join(",", argumentQueryRequest.WorkflowPatterns);
                }

                if (argumentQueryRequest.BusinessRulePatterns.Count > 0)
                {
                    textBoxBusinessRule.Text = string.Join(",", argumentQueryRequest.BusinessRulePatterns);
                }

                if (argumentQueryRequest.ActionPatterns.Count > 0)
                {
                    textBoxAction.Text = string.Join(",", argumentQueryRequest.ActionPatterns);
                }

                if (argumentQueryRequest.BusinessProcessFlowPatterns.Count > 0)
                {
                    textBoxBusinessProcessFlow.Text = string.Join(",", argumentQueryRequest.BusinessProcessFlowPatterns);
                }

                if (argumentQueryRequest.ModelDrivenAppPatterns.Count > 0)
                {
                    textBoxModelDrivenApp.Text = string.Join(",", argumentQueryRequest.ModelDrivenAppPatterns);
                }
            }

            Comparision.HotKeyManager.AddHotKey(this, AcceptChange, Keys.Enter, alt: true);

            if (solutionSuggestion != null)
            {
                this.textBoxSolution.AutoCompleteCustomSource = solutionSuggestion;
            }
        }

        private void InitializePlaceholder()
        {
            this.textBoxSolution.PlaceHolder = "Solution display name";
            this.textBoxEntity.PlaceHolder = "Comma seperate table schema name pattern";
            this.textBoxWebresource.PlaceHolder = "Comma seperate webresource schema name pattern";
            this.textBoxPluginstep.PlaceHolder = "Comma seperate plugin-step display name pattern";
            this.textBoxOptionSet.PlaceHolder = "Comma seperate optionset schema name pattern";
            this.textBoxDashboard.PlaceHolder = "Comma seperate dashboard display name pattern";
            this.textBoxSiteMap.PlaceHolder = "Comma seperate sitemap display name pattern";
            this.textBoxSecurityRole.PlaceHolder = "Comma seperate security role display name pattern";
            this.textBoxWorkflow.PlaceHolder = "Comma seperate workflow display name pattern";
            this.textBoxBusinessRule.PlaceHolder = "Comma seperate business rule display name pattern";
            this.textBoxAction.PlaceHolder = "Comma seperate action display name pattern";
            this.textBoxBusinessProcessFlow.PlaceHolder = "Comma seperate business process flow display name pattern";
            this.textBoxModelDrivenApp.PlaceHolder = "Comma seperate model-driven app display name pattern";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            AcceptChange();
        }

        private void AcceptChange()
        {
            List<string> queryItems = new List<string>();

            if (!string.IsNullOrWhiteSpace(textBoxSolution.Text))
            {
                if (textBoxSolution.Text.Split(',').Select(x => x.Trim()).Where(x => x == "*").Count() > 0)
                {
                    MessageBox.Show("\"*\" (All) not allowed for solution");
                    return;
                }

                queryItems.Add($"Solution={textBoxSolution.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxEntity.Text))
            {
                queryItems.Add($"Table={textBoxEntity.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxWebresource.Text))
            {
                queryItems.Add($"WebResource={textBoxWebresource.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxPluginstep.Text))
            {
                queryItems.Add($"PluginStep={textBoxPluginstep.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxOptionSet.Text))
            {
                queryItems.Add($"Choice={textBoxOptionSet.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxDashboard.Text))
            {
                queryItems.Add($"Dashboard={textBoxDashboard.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxSiteMap.Text))
            {
                queryItems.Add($"SiteMap={textBoxSiteMap.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxSecurityRole.Text))
            {
                queryItems.Add($"SecurityRole={textBoxSecurityRole.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxWorkflow.Text))
            {
                queryItems.Add($"Workflow={textBoxWorkflow.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxBusinessRule.Text))
            {
                queryItems.Add($"BusinessRule={textBoxBusinessRule.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxAction.Text))
            {
                queryItems.Add($"Action={textBoxAction.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxBusinessProcessFlow.Text))
            {
                queryItems.Add($"BusinessProcessFlow={textBoxBusinessProcessFlow.Text}");
            }

            if (!string.IsNullOrWhiteSpace(textBoxModelDrivenApp.Text))
            {
                queryItems.Add($"ModelDrivenApp={textBoxModelDrivenApp.Text}");
            }

            if (queryItems.Count == 0)
            {
                MessageBox.Show("Invalid data");
                return;
            }

            if (!string.IsNullOrWhiteSpace(textBoxSolution.Text) && queryItems.Count == 1)
            {
                queryItems.Add($"Table=*");
                queryItems.Add($"WebResource=*");
                queryItems.Add($"PluginStep=*");
                queryItems.Add($"Choice=*");
                queryItems.Add($"Dashboard=*");
                queryItems.Add($"SiteMap=*");
                queryItems.Add($"SecurityRole=*");
                queryItems.Add($"Workflow=*");
                queryItems.Add($"BusinessRule=*");
                queryItems.Add($"Action=*");
                queryItems.Add($"BusinessProcessFlow=*");
                queryItems.Add($"ModelDrivenApp=*");
            }

            this.ResultQueryString = string.Join(";", queryItems);
            this.DialogResult = DialogResult.OK;
        }

        private void FormQueryEditor_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            this.ShowHelp();
        }

        private void FormQueryEditor_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            this.ShowHelp();
        }

        private void ShowHelp()
        {
            System.Diagnostics.Process.Start("https://github.com/vinoddsouza/crm-component-comparer-exporter");
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxSolution.Text = string.Empty;
            textBoxEntity.Text = string.Empty;
            textBoxWebresource.Text = string.Empty;
            textBoxPluginstep.Text = string.Empty;
            textBoxOptionSet.Text = string.Empty;
            textBoxDashboard.Text = string.Empty;
            textBoxSiteMap.Text = string.Empty;
            textBoxSecurityRole.Text = string.Empty;
            textBoxWorkflow.Text = string.Empty;
            textBoxBusinessRule.Text = string.Empty;
            textBoxAction.Text = string.Empty;
            textBoxBusinessProcessFlow.Text = string.Empty;
            textBoxModelDrivenApp.Text = string.Empty;
        }
    }
}
