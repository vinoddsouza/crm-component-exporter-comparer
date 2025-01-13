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
    partial class ProgressStatusControl : UserControl
    {
        private readonly int PADDING_HORIZONTAL = 0;
        private readonly int PADDING_VERTICAL = 0;
        private readonly int LABEL_PROGRESS_GAP = 4;
        private readonly int CURRENT_OVERALL_GAP = 20;
        private readonly int LABEL_HEIGHT = 13;

        private ProgressStatusValue val { get; set; } = new ProgressStatusValue();
        public ProgressStatusControl()
        {
            InitializeComponent();
        }

        protected override void OnResize(EventArgs e)
        {
            var PROGRESS_WIDTH = this.Width - PADDING_HORIZONTAL * 2;
            var PROGRESS_HEIGHT = (this.Height - PADDING_VERTICAL * 2 - LABEL_HEIGHT * 2 - LABEL_PROGRESS_GAP * 2 - CURRENT_OVERALL_GAP) / 2;

            this.progressBarCurrent.Size = new Size(PROGRESS_WIDTH, PROGRESS_HEIGHT);
            this.progressBarOverall.Size = new Size(PROGRESS_WIDTH, PROGRESS_HEIGHT);

            this.progressBarCurrent.Left = PADDING_HORIZONTAL;
            this.progressBarOverall.Left = PADDING_HORIZONTAL;
            this.labelProgressCurrent.Left = PADDING_HORIZONTAL;
            this.labelProgressOverall.Left = PADDING_HORIZONTAL;

            this.labelProgressOverall.Top = PADDING_VERTICAL + LABEL_HEIGHT + LABEL_PROGRESS_GAP + PROGRESS_HEIGHT + CURRENT_OVERALL_GAP;
            this.progressBarOverall.Top = PADDING_VERTICAL + LABEL_HEIGHT + LABEL_PROGRESS_GAP + PROGRESS_HEIGHT + CURRENT_OVERALL_GAP + LABEL_HEIGHT + LABEL_PROGRESS_GAP;
        }

        public ProgressStatusValue Value
        {
            get
            {
                return this.val;
            }
            set
            {
                this.val = value ?? new ProgressStatusValue();

                this.labelProgressCurrent.Text = val.LabelCurrent ?? string.Empty;
                this.labelProgressOverall.Text = val.LabelOverall ?? string.Empty;
                this.progressBarCurrent.Value = Helper.GetProgressValue(0, 1000, val.ProgressCurrent);
                this.progressBarOverall.Value = Helper.GetProgressValue(0, 1000, val.ProgressOverall);
            }
        }
    }

    class ProgressStatusValue
    {
        public string LabelCurrent { get; set; }
        public string LabelOverall { get; set; }
        public int ProgressCurrent { get; set; }
        public int ProgressOverall { get; set; }
    }
}
