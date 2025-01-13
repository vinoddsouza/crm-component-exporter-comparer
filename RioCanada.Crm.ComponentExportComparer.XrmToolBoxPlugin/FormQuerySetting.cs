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
    partial class FormQuerySetting : Form
    {
        public SettingConfiguration Configuration { get; set; }
        public FormQuerySetting(SettingConfiguration configuration)
        {
            InitializeComponent();

            this.Configuration = configuration;
            this.checkBoxIncludeAllMeta.Checked = configuration.IncludeAllProperty;
            this.checkBoxIncludeSysWebresource.Checked = configuration.IncludeSystemWebresource;
            this.checkBoxIncludeSysPluginStep.Checked = configuration.IncludeSystemPluginStep;

            this.checkBoxRoleById.Checked = configuration.RoleById;

            this.checkBoxIncludeEntityColumn.Checked = configuration.IncludeEntityColumn;
            this.checkBoxIncludeEntityRelationship.Checked = configuration.IncludeEntityRelationship;
            this.checkBoxIncludeEntityForm.Checked = configuration.IncludeEntityForm;
            this.checkBoxIncludeEntityDashboard.Checked = configuration.IncludeEntityDashboard;
            this.checkBoxIncludeEntityView.Checked = configuration.IncludeEntityView;
            this.checkBoxIncludeEntityRibbon.Checked = configuration.IncludeEntityRibbon;
            this.checkBoxReplaceEmptyStringByNull.Checked = configuration.ReplaceEmptyStringByNull;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Configuration.IncludeAllProperty = checkBoxIncludeAllMeta.Checked;
            this.Configuration.IncludeSystemWebresource = checkBoxIncludeSysWebresource.Checked;
            this.Configuration.IncludeSystemPluginStep = checkBoxIncludeSysPluginStep.Checked;

            this.Configuration.RoleById = checkBoxRoleById.Checked;

            this.Configuration.IncludeEntityColumn = checkBoxIncludeEntityColumn.Checked;
            this.Configuration.IncludeEntityRelationship = checkBoxIncludeEntityRelationship.Checked;
            this.Configuration.IncludeEntityForm = checkBoxIncludeEntityForm.Checked;
            this.Configuration.IncludeEntityDashboard = checkBoxIncludeEntityDashboard.Checked;
            this.Configuration.IncludeEntityView = checkBoxIncludeEntityView.Checked;
            this.Configuration.IncludeEntityRibbon = checkBoxIncludeEntityRibbon.Checked;
            this.Configuration.ReplaceEmptyStringByNull = checkBoxReplaceEmptyStringByNull.Checked;

            this.DialogResult = DialogResult.OK;
        }
    }
}
