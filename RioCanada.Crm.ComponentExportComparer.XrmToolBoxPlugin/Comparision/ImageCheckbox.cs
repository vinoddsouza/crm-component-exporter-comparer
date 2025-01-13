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
    public partial class ImageCheckbox : UserControl
    {
        public event EventHandler CheckedChanged;
        public ImageCheckbox()
        {
            InitializeComponent();
        }

        private Image _CheckedImage { get; set; }
        private Image _UncheckedImage { get; set; }
        private Image _HoverCheckedImage { get; set; }
        private Image _HoverUncheckedImage { get; set; }
        private bool _Checked { get; set; }
        private bool _IsHover { get; set; }

        public Image CheckedImage { get => this._CheckedImage; set { this._CheckedImage = value; this.RenderImage(); } }
        public Image UncheckedImage { get => this._UncheckedImage; set { this._UncheckedImage = value; this.RenderImage(); } }
        public Image HoverCheckedImage { get => this._HoverCheckedImage; set { this._HoverCheckedImage = value; this.RenderImage(); } }
        public Image HoverUncheckedImage { get => this._HoverUncheckedImage; set { this._HoverUncheckedImage = value; this.RenderImage(); } }
        public bool Checked { get => this._Checked; set { this._Checked = value; this.RenderImage(); } }
        public string Label { get => this.label1.Text; set => this.label1.Text = value; }
        private bool IsHover { get => this._IsHover; set { this._IsHover = value; this.RenderImage(); } }
        private Image CurrentImage { get; set; }

        private void RenderImage()
        {
            Image image;

            if (this.Checked)
            {
                image = CheckedImage;

                if (this.IsHover && HoverCheckedImage != null)
                {
                    image = HoverCheckedImage;
                }
            }
            else
            {
                image = UncheckedImage;

                if (this.IsHover && HoverUncheckedImage != null)
                {
                    image = HoverUncheckedImage;
                }
            }

            if (this.CurrentImage != image)
            {
                this.pictureBox1.Image = image;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Checked = !this.Checked;
            this.CheckedChanged?.Invoke(this, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Checked = !this.Checked;
            this.CheckedChanged?.Invoke(this, e);
        }

        private void ImageCheckbox_Click(object sender, EventArgs e)
        {
            this.Checked = !this.Checked;
            this.CheckedChanged?.Invoke(this, e);
        }

        private void ImageCheckbox_MouseHover(object sender, EventArgs e)
        {
            this.IsHover = true;
        }

        private void ImageCheckbox_MouseLeave(object sender, EventArgs e)
        {
            this.IsHover = false;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.IsHover = true;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            this.IsHover = false;
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            this.IsHover = true;
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.IsHover = false;
        }
    }
}
