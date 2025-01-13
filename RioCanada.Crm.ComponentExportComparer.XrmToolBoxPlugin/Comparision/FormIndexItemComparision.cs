using RioCanada.Crm.ComponentExportComparer.Core.Models;
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
    public partial class FormIndexItemComparision : Form
    {
        private readonly Bitmap CheckGrey = AppResource.CheckGrey;
        private readonly Bitmap UncheckGrey = AppResource.UncheckGrey;
        private readonly Bitmap CheckOrange = AppResource.CheckOrange;
        private readonly Bitmap UncheckOrange = AppResource.UncheckOrange;
        private readonly Bitmap CheckGreen = AppResource.CheckGreen;
        private readonly Bitmap UncheckGreen = AppResource.UncheckGreen;
        private readonly Bitmap CheckRed = AppResource.CheckRed;
        private readonly Bitmap UncheckRed = AppResource.UncheckRed;

        private readonly int ROW_HEIGHT = 30;
        private readonly int SCROLL_WIDTH = 24;

        private string Source { get; set; }
        private string Target { get; set; }
        public Settings Settings { get; set; }

        private bool ViewAsList { get; set; }
        private List<ComparisionLineItem> ListViewData { get; set; }
        private Func<string, List<IndexLineItem>> DataGenerator { get; set; }

        public FormIndexItemComparision(Settings settings, string source, string target, Func<string, List<IndexLineItem>> generator, string title)
        {
            InitializeComponent();

            if (!string.IsNullOrEmpty(title))
            {
                this.Text = title;
            }

            this.Settings = settings;
            this.Source = source;
            this.Target = target;
            this.DataGenerator = generator;

            // INIT HOTKEYS
            InitHotkeys();
        }

        private void InitHotkeys()
        {
            // register the hotkeys with the form
            HotKeyManager.AddHotKey(this, this.Close, Keys.Escape);
        }

        private void FormIndexItemComparision_Load(object sender, EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += (ss, ee) =>
            {
                var sourceData = this.DataGenerator(this.Source);
                var targetData = this.DataGenerator(this.Target);

                this.ListViewData = ComparisionView.GetComparisionData(sourceData, targetData);
            };

            bgw.RunWorkerCompleted += (ss, ee) =>
            {
                bgw.Dispose();
                if (ee.Error != null)
                {
                    MessageBox.Show(ee.Error.Message);
                }
                else
                {
                    this.ReRenderGridView();
                    this.label1.Visible = false;
                }
            };

            bgw.RunWorkerAsync();
            this.ConfigureGrid();
        }

        #region Events
        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            this.HandleOpen(this.dataGridView1.Rows[e.RowIndex]);
        }

        private void DataGridView1_Resize(object sender, EventArgs e)
        {
            this.HandleResize();
        }

        private void DataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;

                if (this.dataGridView1.SelectedRows.Count > 0)
                {
                    this.HandleOpen(this.dataGridView1.SelectedRows[0]);
                }
            }
            else if (!this.ViewAsList && e.KeyCode == Keys.Left)
            {
                e.SuppressKeyPress = true;
            }
            else if (!this.ViewAsList && e.KeyCode == Keys.Right)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void comparisionStatusCheckboxFilter1_OnChange(object sender, EventArgs e)
        {
            this.ReRenderGridData();
        }
        #endregion

        #region Functions
        private void ConfigureGrid()
        {
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.MultiSelect = false;

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;

            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.ColumnHeadersHeight = 30;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 5, 10, 5);
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Gainsboro;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.Gainsboro;

            this.dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SunkenHorizontal;
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightYellow;
            this.dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            this.dataGridView1.DefaultCellStyle.Padding = new Padding(12, 0, 15, 0);

            this.dataGridView1.BackgroundColor = SystemColors.Control;
            this.dataGridView1.BorderStyle = BorderStyle.Fixed3D;

            this.dataGridView1.CellDoubleClick += this.DataGridView1_CellDoubleClick;
            this.dataGridView1.Resize += this.DataGridView1_Resize;
        }

        private void ReRenderGridView()
        {
            // Clean datagridview data and columns
            this.dataGridView1.Rows.Clear();
            this.dataGridView1.Columns.Clear();

            // Force garbage clean
            GC.Collect();

            this.ConfigureGridColumnAsListView();
            this.RenderListViewData();
        }

        private void ReRenderGridData()
        {
            // Clean datagridview data
            this.dataGridView1.Rows.Clear();

            // Force garbage clean
            GC.Collect();

            this.RenderListViewData();
        }

        private void ConfigureGridColumnAsListView()
        {
            this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Name", SortMode = DataGridViewColumnSortMode.Automatic });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });
            this.dataGridView1.Columns.Add(new DataGridViewImageColumn { HeaderText = "", Width = 32, SortMode = DataGridViewColumnSortMode.NotSortable });

            this.HandleResize();
        }

        private void RenderListViewData()
        {
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            this.ListViewData.ForEach(x =>
            {
                if ((x.Status & this.comparisionStatusCheckboxFilter1.Status) == 0) return;
                var item = x.DefaultItem;
                rows.Add(GetListViewRow(x, item.Name, x.Status));
            });
            dataGridView1.Rows.AddRange(rows.ToArray());
        }

        private void HandleResize()
        {
            if (this.dataGridView1.Columns.Count == 0) return;

            if (this.ViewAsList)
            {
                var fixedWidth = 0;
                foreach (var column in this.dataGridView1.Columns) fixedWidth += ((DataGridViewColumn)column).Width;

                fixedWidth -= this.dataGridView1.Columns[0].Width;
                //fixedWidth -= this.dataGridView1.Columns[1].Width;
                var availableWidth = this.dataGridView1.Width - fixedWidth - SCROLL_WIDTH;
                this.dataGridView1.Columns[0].Width = Convert.ToInt32(availableWidth * 0.50);
                //this.dataGridView1.Columns[1].Width = Convert.ToInt32(availableWidth * 0.25);
                this.dataGridView1.Columns[2].Width = Convert.ToInt32(availableWidth * 0.20);
                this.dataGridView1.Columns[3].Width = Convert.ToInt32(availableWidth * 0.30);
            }
            else
            {
                var fixedWidth = 0;
                foreach (var column in this.dataGridView1.Columns) fixedWidth += ((DataGridViewColumn)column).Width;

                fixedWidth -= this.dataGridView1.Columns[0].Width;
                this.dataGridView1.Columns[0].Width = this.dataGridView1.Width - fixedWidth - SCROLL_WIDTH;
            }
        }

        private void HandleOpen(DataGridViewRow row)
        {
            if (!(row.Tag is ComparisionLineItem item)) return;

            if (item.Status == ComparisionStatus.OnlyInTarget)
            {
                FormFileViewer.ShowContent(Settings, item.TargetItem.Content, item.Name);
            }
            else if (item.Status == ComparisionStatus.OnlyInSource)
            {
                FormFileViewer.ShowContent(Settings, item.SourceItem.Content, item.Name);
            }
            else
            {
                CompareContent(item.SourceItem.Content, item.TargetItem.Content);
            }
        }

        private void CompareContent(string contentA, string contentB)
        {
            var frmNew = new Diff.Net.FileDiffForm();
            frmNew.ShowDifferences(new Diff.Net.ShowDiffArgs(contentA, contentB, Diff.Net.DiffType.Text));
            frmNew.WindowState = FormWindowState.Maximized;
            frmNew.ShowDialog();
        }

        private DataGridViewRow GetListViewRow(ComparisionLineItem item, string displayName, ComparisionStatus status)
        {
            var dr = new DataGridViewRow
            {
                Height = ROW_HEIGHT,
                Tag = item,
            };
            dr.Cells.AddRange(
                new DataGridViewTextBoxCell
                {
                    Value = displayName,
                },
                new DataGridViewImageCell
                {
                    Value = (status & ComparisionStatus.Unchanged) == ComparisionStatus.Unchanged ? CheckGrey : UncheckGrey,
                    Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                },
                    new DataGridViewImageCell
                    {
                        Value = (status & ComparisionStatus.Modified) == ComparisionStatus.Modified ? CheckOrange : UncheckOrange,
                        Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                    },
                    new DataGridViewImageCell
                    {
                        Value = (status & ComparisionStatus.OnlyInSource) == ComparisionStatus.OnlyInSource ? CheckGreen : UncheckGreen,
                        Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                    },
                    new DataGridViewImageCell
                    {
                        Value = (status & ComparisionStatus.OnlyInTarget) == ComparisionStatus.OnlyInTarget ? CheckRed : UncheckRed,
                        Style = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Padding = new Padding(1, 1, 1, 1) },
                    }
                );

            return dr;
        }
        #endregion
    }
}
