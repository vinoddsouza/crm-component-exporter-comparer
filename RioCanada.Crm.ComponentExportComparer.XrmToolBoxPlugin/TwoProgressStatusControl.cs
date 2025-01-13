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
    partial class TwoProgressStatusControl : UserControl
    {
        private readonly int PADDING_HORIZONTAL = 0;
        private readonly int PADDING_VERTICAL = 0;
        private readonly int GAP = 10;
        private bool single { get; set; }

        public TwoProgressStatusControl()
        {
            InitializeComponent();
        }

        public ProgressStatusValue Value1 { get => this.progressStatusControl1.Value; set => this.progressStatusControl1.Value = value; }
        public ProgressStatusValue Value2 { get => this.progressStatusControl2.Value; set => this.progressStatusControl2.Value = value; }

        public bool ShowSingleProgress
        {
            get => this.single; set
            {
                this.single = value;
                this.Redraw();
            }
        }

        protected override void OnResize(EventArgs e)
        {
            this.Redraw();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.Redraw();
        }

        protected override void OnParentFontChanged(EventArgs e)
        {
            base.OnParentFontChanged(e);
            this.Redraw();
        }

        private void Redraw()
        {
            var PROGRESS_STATUS_CONTROL_HEIGHT = this.Height - PADDING_VERTICAL * 2 - 2;

            this.progressStatusControl1.Height = PROGRESS_STATUS_CONTROL_HEIGHT;
            this.progressStatusControl2.Height = PROGRESS_STATUS_CONTROL_HEIGHT;

            this.progressStatusControl1.Left = PADDING_HORIZONTAL;

            if (this.single)
            {
                var PROGRESS_STATUS_CONTROL_WIDTH = (this.Width - PADDING_HORIZONTAL * 2);

                this.progressStatusControl2.Left = PROGRESS_STATUS_CONTROL_WIDTH;
                this.progressStatusControl1.Size = new Size(PROGRESS_STATUS_CONTROL_WIDTH, PROGRESS_STATUS_CONTROL_HEIGHT);
                this.progressStatusControl2.Size = new Size(0, PROGRESS_STATUS_CONTROL_HEIGHT);
            }
            else
            {
                var PROGRESS_STATUS_CONTROL_WIDTH = (this.Width - PADDING_HORIZONTAL * 2 - GAP) / 2;

                this.progressStatusControl2.Left = PADDING_HORIZONTAL + PROGRESS_STATUS_CONTROL_WIDTH + GAP;
                this.progressStatusControl1.Size = new Size(PROGRESS_STATUS_CONTROL_WIDTH, PROGRESS_STATUS_CONTROL_HEIGHT);
                this.progressStatusControl2.Size = new Size(PROGRESS_STATUS_CONTROL_WIDTH, PROGRESS_STATUS_CONTROL_HEIGHT);
            }
        }
    }
}
