using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.Comparision
{
    public class DataGridViewImageTextBoxCell : DataGridViewTextBoxCell
    {
        private Bitmap _icon { get; set; }
        public Bitmap Icon { get => this._icon; set { this._icon = value; this.DataGridView?.InvalidateCell(this); } }
        public string IconName { get; set; }

        protected override void Paint(
            Graphics graphics,
            Rectangle clipBounds,
            Rectangle cellBounds,
            int rowIndex,
            DataGridViewElementStates cellState,
            object value,
            object formattedValue,
            string errorText,
            DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            var imageSize = 12;
            var marginTop = (cellBounds.Height - imageSize) / 2;

            var orignalPadding = cellStyle.Padding;
            cellStyle.Padding = new Padding(orignalPadding.Left + imageSize + 8, orignalPadding.Top, orignalPadding.Right, orignalPadding.Bottom);

            // Call the base class method to paint the default cell appearance.
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState,
                value, formattedValue, errorText, cellStyle,
                advancedBorderStyle, paintParts);

            cellStyle.Padding = orignalPadding;

            if (this.Icon != null)
            {
                graphics.DrawImage(this.Icon, new RectangleF(cellStyle.Padding.Left + cellBounds.X + marginTop / 2, cellBounds.Y + marginTop, imageSize, imageSize));
            }
        }
    }

    public class DataGridViewImageTextBoxColumn : DataGridViewColumn
    {
        public DataGridViewImageTextBoxColumn()
        {
            this.CellTemplate = new DataGridViewImageTextBoxCell();
        }
    }
}
