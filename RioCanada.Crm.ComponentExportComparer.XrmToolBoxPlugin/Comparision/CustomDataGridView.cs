using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.Comparision
{
    class CustomDataGridView : DataGridView
    {
        private const int CAPTIONHEIGHT = 1;
        private const int BORDERWIDTH = 1;

        public CustomDataGridView() : base()
        {
            VerticalScrollBar.VisibleChanged += this.VerticalScrollBar_VisibleChanged;
            this.ShowScrollBars();
        }

        private void VerticalScrollBar_VisibleChanged(object sender, EventArgs e)
        {
            this.ShowScrollBars();
        }

        private void ShowScrollBars()
        {
            if (!VerticalScrollBar.Visible)
            {
                int width = VerticalScrollBar.Width;

                VerticalScrollBar.Location =
                new Point(ClientRectangle.Width - width - BORDERWIDTH, CAPTIONHEIGHT);

                VerticalScrollBar.Size =
                new Size(width, ClientRectangle.Height - CAPTIONHEIGHT - BORDERWIDTH);

                VerticalScrollBar.Show();
            }
        }
    }
}
