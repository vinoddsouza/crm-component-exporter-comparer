using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Menees;
using Menees.Diffs;
using Menees.Diffs.Windows.Forms;
using Menees.Windows.Forms;
namespace RioCanada.Crm.ComponentExportComparer.Diff.Net
{
	partial class FileDiffForm
	{
		private Menees.Diffs.Windows.Forms.DiffControl DiffCtrl;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileDiffForm));
            this.DiffCtrl = new Menees.Diffs.Windows.Forms.DiffControl();
            this.SuspendLayout();
            // 
            // DiffCtrl
            // 
            this.DiffCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DiffCtrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.DiffCtrl.LineDiffHeight = 47;
            this.DiffCtrl.Location = new System.Drawing.Point(0, 30);
            this.DiffCtrl.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.DiffCtrl.Name = "DiffCtrl";
            this.DiffCtrl.OverviewWidth = 38;
            this.DiffCtrl.ShowWhiteSpaceInLineDiff = true;
            this.DiffCtrl.Size = new System.Drawing.Size(713, 558);
            this.DiffCtrl.TabIndex = 0;
            this.DiffCtrl.ViewFont = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DiffCtrl.LineDiffSizeChanged += new System.EventHandler(this.DiffCtrl_LineDiffSizeChanged);
            this.DiffCtrl.RecompareNeeded += new System.EventHandler(this.DiffCtrl_RecompareNeeded);
            // 
            // FileDiffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.ClientSize = new System.Drawing.Size(713, 588);
            this.Controls.Add(this.DiffCtrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimizeBox = false;
            this.Name = "FileDiffForm";
            this.Text = "File Comparison";
            this.Closed += new System.EventHandler(this.FileDiffForm_Closed);
            this.Load += new System.EventHandler(this.FileDiffForm_Load);
            this.Shown += new System.EventHandler(this.FileDiffForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}

