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
    partial class BreadcrumbControl : UserControl
    {
        public delegate void BreadcrumbClickHandler(BreadcrumbItem item);
        public event BreadcrumbClickHandler ItemClick;

        private readonly int LABEL_HEIGHT = 18;
        private int BreadcrumbCalculatedWidth { get; set; }
        private List<BreadcrumbItem> Items { get; set; }

        public BreadcrumbControl()
        {
            InitializeComponent();
            this.RenderControls();
        }

        protected override void OnParentFontChanged(EventArgs e)
        {
            base.OnParentFontChanged(e);
            this.Font = Parent.Font;
            this.RenderControls();
        }

        private void RenderControls()
        {
            this.Controls.Clear();
            this.BreadcrumbCalculatedWidth = 0;

            if (this.Items == null || this.Items.Count == 0)
            {
                this.BreadcrumbCalculatedWidth += this.RenderLabel("Root", this.BreadcrumbCalculatedWidth);
            }
            else
            {
                for (var i = 0; i < this.Items.Count - 1; i++)
                {
                    var item = this.Items[i];
                    this.BreadcrumbCalculatedWidth += this.RenderLabel(item.Text, this.BreadcrumbCalculatedWidth, item);
                    this.BreadcrumbCalculatedWidth += this.RenderDevider(this.BreadcrumbCalculatedWidth);
                }

                {
                    var item = this.Items[this.Items.Count - 1];
                    this.BreadcrumbCalculatedWidth += this.RenderLabel(item.Text, this.BreadcrumbCalculatedWidth);
                }
            }
        }

        private int RenderLabel(string text, int left, BreadcrumbItem item = null)
        {
            Label lbl;

            if (item == null)
            {
                lbl = new Label();
            }
            else
            {
                lbl = new LinkLabel();
                lbl.Click += (control, args) =>
                {
                    ItemClick?.Invoke(item);
                };
            }

            var width = MeasureDisplayStringWidth(text, this.Font);
            lbl.Text = text;
            lbl.Location = new Point(left, 0);
            lbl.Size = new Size(width, LABEL_HEIGHT);
            this.Controls.Add(lbl);
            return width;
        }

        private int RenderDevider(int left)
        {
            int deviderWidth = 15;
            Label lbl2 = new Label
            {
                Text = "⏵",
                Location = new Point(left, 0),
                Size = new Size(deviderWidth, LABEL_HEIGHT)
            };
            this.Controls.Add(lbl2);
            return deviderWidth;
        }

        private static int MeasureDisplayStringWidth(string text, Font font)
        {
            return TextRenderer.MeasureText(text, font).Width;
        }

        public void SetItems(List<BreadcrumbItem> items)
        {
            this.Items = items;
            this.RenderControls();
        }
    }
}
