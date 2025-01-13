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
    public partial class ComparisionStatusCheckboxFilter : UserControl
    {
        public event EventHandler OnChange;
        public bool IncludeUnchanged { get => this.imageCheckboxUnchanged.Checked; }
        public bool IncludeOnlyInSource { get => this.imageCheckboxSource.Checked; }
        public bool IncludeOnlyInTarget { get => this.imageCheckboxTarget.Checked; }
        public bool IncludeModified { get => this.imageCheckboxModified.Checked; }
        public ComparisionStatus Status
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

        public ComparisionStatusCheckboxFilter()
        {
            InitializeComponent();
        }

        private void imageCheckboxUnchanged_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange?.Invoke(this, e);
        }

        private void imageCheckboxModified_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange?.Invoke(this, e);
        }

        private void imageCheckboxSource_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange?.Invoke(this, e);
        }

        private void imageCheckboxTarget_CheckedChanged(object sender, EventArgs e)
        {
            this.OnChange?.Invoke(this, e);
        }
    }
}
