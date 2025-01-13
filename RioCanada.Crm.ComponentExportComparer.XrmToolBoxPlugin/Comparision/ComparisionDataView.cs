using RioCanada.Crm.ComponentExportComparer.Core.Extensions;
using System;
using System.Collections;
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
    partial class ComparisionDataView : UserControl
    {
        public delegate void OnOpenHandler(ComparisionLineItem item);
        public delegate void OnBackHandler();
        public delegate void OnContextHandler(ComparisionLineItem item, Control control, Point position);
        public event OnOpenHandler OnOpen;
        public event OnOpenHandler OnWindowOpen;
        public event OnBackHandler OnBack;
        public event OnContextHandler OnContextMenu;

        public bool ShowExternalLink { get; set; }
        private List<ComparisionLineItem> Data = new List<ComparisionLineItem>();
        private readonly int ROW_HEIGHT = 30;

        public ComparisionDataView()
        {
            InitializeComponent();

            // customize
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.MultiSelect = false;

            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            this.Resize += (ss, ee) =>
            {
                this.dataGridView1.Width = this.Width;
                this.dataGridView1.Height = this.Height;
            };
            //this.dataGridView1.Resize += (ss, ee) => Console.WriteLine($"Comparision Data View (grid) -> {this.dataGridView1.Width}, {this.dataGridView1.Height}");

            this.dataGridView1.Width = this.Width;
            this.dataGridView1.Height = this.Height;
            //this.dataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;

            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gainsboro;// this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor;

            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightYellow;
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dataGridView1.DefaultCellStyle.Padding = new Padding(12, 0, 15, 0);

            // columns
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 30, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.NotSortable,
            });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });

            this.HandleResize();
            this.dataGridView1.Resize += this.DataGridView1_Resize;
            this.dataGridView1.CellDoubleClick += this.DataGridView1_CellDoubleClick;
            this.dataGridView1.CellClick += this.DataGridView1_CellClick;
            this.dataGridView1.KeyDown += this.DataGridView1_KeyDown;
            this.dataGridView1.CellMouseMove += this.DataGridView1_CellMouseMove;
            this.dataGridView1.CellMouseClick += this.DataGridView1_CellMouseClick;
        }

        private void DataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) return;

            if (e.Button == MouseButtons.Right)
            {
                var item = this.Data[e.RowIndex];
                var cellPosition = this.dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                this.OnContextMenu?.Invoke(item, this.dataGridView1, new Point(cellPosition.Left + e.Location.X, cellPosition.Y + e.Location.Y));
            }
        }

        #region Events
        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                OnWindowOpen?.Invoke(this.Data[e.RowIndex]);
            }
        }

        private void DataGridView1_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 4 && e.RowIndex != -1)
            {
                Cursor = Cursors.Hand;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    OnOpen?.Invoke(this.Data[this.dataGridView1.SelectedRows[0].Index]);
                }
            }
            else if (e.KeyCode == Keys.Back)
            {
                e.SuppressKeyPress = true;
                OnBack?.Invoke();
            }
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;

            OnOpen?.Invoke(this.Data[e.RowIndex]);
        }

        private void DataGridView1_Resize(object sender, EventArgs e)
        {
            this.HandleResize();
        }
        #endregion

        private void HandleResize()
        {
            var fixedWidth = 0;
            foreach (var column in this.dataGridView1.Columns) fixedWidth += ((DataGridViewColumn)column).Width;

            fixedWidth -= this.dataGridView1.Columns[1].Width;
            this.dataGridView1.Columns[1].Width = this.dataGridView1.Width - fixedWidth - 19;
        }

        private DataGridViewRow GetRow(ComparisionLineItem item)
        {
            var dr = new DataGridViewRow { Height = ROW_HEIGHT };
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = item.GetIcon(),
                Style = new DataGridViewCellStyle { Padding = new Padding(5, 5, 0, 5) },
                ImageLayout = DataGridViewImageCellLayout.Zoom,
            });
            dr.Cells.Add(new DataGridViewTextBoxCell
            {
                Value = item.Name,
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (item.Status & ComparisionStatus.Unchanged) == ComparisionStatus.Unchanged ? AppResource.CheckGrey : AppResource.UncheckGrey,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (item.Status & ComparisionStatus.Modified) == ComparisionStatus.Modified ? AppResource.CheckOrange : AppResource.UncheckOrange,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (item.Status & ComparisionStatus.OnlyInSource) == ComparisionStatus.OnlyInSource ? AppResource.CheckGreen : AppResource.UncheckGreen,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });
            dr.Cells.Add(new DataGridViewImageCell
            {
                Value = (item.Status & ComparisionStatus.OnlyInTarget) == ComparisionStatus.OnlyInTarget ? AppResource.CheckRed : AppResource.UncheckRed,
                Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
            });

            return dr;
        }

        #region Public methods
        public void SetItems(List<ComparisionLineItem> items)
        {
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Rows.AddRange(items.Select(x => this.GetRow(x)).ToArray());
            this.Data = items;
        }

        public void SetSelectedItem(ComparisionLineItem item)
        {
            if (item == null) return;
            var index = this.Data.FindIndex(x => x.Key == item.Key);

            if (index == -1) return;

            this.dataGridView1.Rows[index].Selected = true;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[index].Cells[0];
        }
        #endregion
    }
}
