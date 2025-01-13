using McTools.Xrm.Connection;
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
    partial class FormConnectionDetail : Form
    {
        private ConnectionDetail ConnectionDetail;
        public FormConnectionDetail(ConnectionDetail connectionDetail)
        {
            InitializeComponent();

            this.ConnectionDetail = connectionDetail;
            this.labelConnectionName.Text = connectionDetail.ConnectionName;
            this.labelServerName.Text = connectionDetail.ServerName;
            this.labelOrganization.Text = connectionDetail.Organization;
            this.labelOrganizationName.Text = connectionDetail.OrganizationFriendlyName;
            this.labelUserName.Text = connectionDetail.UserName;
            this.labelVersionName.Text = connectionDetail.OrganizationVersion;

            var compatibility = GetCompatibility(connectionDetail);

            switch (compatibility)
            {
                case Compatibility.NotCompatible:
                    this.labelMsg.Text = "⚠ This version is not compatible.";
                    this.labelMsg.ForeColor = Color.OrangeRed;
                    break;
                case Compatibility.NotSupported:
                    this.labelMsg.Text = "✗ This version is not supported.";
                    this.labelMsg.ForeColor = Color.Red;
                    break;
                default:
                    this.labelMsg.Text = "✓ This version is compatible.";
                    this.labelMsg.ForeColor = Color.Green;
                    break;
            }
        }

        public static Compatibility GetCompatibility(ConnectionDetail connectionDetail)
        {
            if (connectionDetail.OrganizationMajorVersion < 9)
            {
                return Compatibility.NotSupported;
            }
            else if (connectionDetail.OrganizationMajorVersion == 9 && connectionDetail.OrganizationMinorVersion == 2)
            {
                return Compatibility.Compatible;
            }
            else if (connectionDetail.OrganizationMajorVersion == 9 && connectionDetail.OrganizationMinorVersion < 2)
            {
                return Compatibility.NotCompatible;
            }
            else
            {
                return Compatibility.NotCompatible;
            }
        }

        public enum Compatibility
        {
            NotSupported,
            Compatible,
            NotCompatible,
        }

        private void buttonPowerApp_Click(object sender, EventArgs e)
        {
            UriGenerator.Open(UriGenerator.PowerApps(this.ConnectionDetail.ServiceClient.EnvironmentId));
        }

        private void buttonDynamics365_Click(object sender, EventArgs e)
        {
            UriGenerator.Open(UriGenerator.Dynamics365(this.ConnectionDetail.ServerName));
        }
    }
}
