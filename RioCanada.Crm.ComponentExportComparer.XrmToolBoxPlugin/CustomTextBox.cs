using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    class CustomTextBox : TextBox
    {
        public CustomTextBox() : base()
        {
            this.Enter += this.CustomTextBox_Enter;
            this.Leave += this.CustomTextBox_Leave;
            this.TextChanged += this.CustomTextBox_TextChanged;

            this.Format();
        }

        private bool IsEmpty { get => (this.TextInternal == null || this.TextInternal.Length == 0); }
        private Color TextColor { get; set; }
        private string PlaceHolderInternal { get; set; }
        private string TextInternal { get; set; }
        private bool FocusedInternal { get; set; }

        public string PlaceHolder
        {
            get
            {
                return this.PlaceHolderInternal;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    this.PlaceHolderInternal = string.Empty;
                }
                else
                {
                    this.PlaceHolderInternal = value;
                }

                if (!this.FocusedInternal && this.IsEmpty)
                {
                    base.Text = this.PlaceHolderInternal;
                }

                this.Format();
            }
        }
        public new string Text
        {
            get
            {
                return this.TextInternal ?? string.Empty;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.TextInternal = string.Empty;
                }
                else
                {
                    this.TextInternal = value;
                }

                if (this.FocusedInternal || !this.IsEmpty)
                {
                    base.Text = this.TextInternal;
                }
                else
                {
                    base.Text = this.PlaceHolderInternal;
                }

                this.Format();
            }
        }

        public new Color ForeColor { get => this.TextColor; set { this.TextColor = value; this.Format(); } }

        private void CustomTextBox_Enter(object sender, EventArgs e)
        {
            this.FocusedInternal = true;
            base.Text = this.TextInternal;
            this.Format();
        }

        private void CustomTextBox_Leave(object sender, EventArgs e)
        {
            this.FocusedInternal = false;
            this.TextInternal = base.Text;
            if (this.IsEmpty)
            {
                base.Text = this.PlaceHolderInternal;
            }

            this.Format();
        }

        private void CustomTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.FocusedInternal)
            {
                this.TextInternal = base.Text;
            }
        }

        private void Format()
        {
            void _format()
            {
                if (!this.FocusedInternal && this.IsEmpty)
                {
                    base.ForeColor = Color.Gray;
                    base.Font = new Font(this.Font, FontStyle.Italic);
                }
                else
                {
                    base.ForeColor = this.TextColor;
                    base.Font = new Font(this.Font, FontStyle.Regular);
                }
            }

            Task.Run(() =>
            {
                if (this.IsDisposed) return;
                if (this.InvokeRequired)
                {
                    this.Invoke((MethodInvoker)delegate { _format(); });
                }
                else
                {
                    _format();
                }
            });
        }
    }
}
