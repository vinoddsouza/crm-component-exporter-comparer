
namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    partial class ProgressStatusControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBarOverall = new System.Windows.Forms.ProgressBar();
            this.labelProgressCurrent = new System.Windows.Forms.Label();
            this.progressBarCurrent = new System.Windows.Forms.ProgressBar();
            this.labelProgressOverall = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBarOverall
            // 
            this.progressBarOverall.Location = new System.Drawing.Point(3, 91);
            this.progressBarOverall.Maximum = 1000;
            this.progressBarOverall.Name = "progressBarOverall";
            this.progressBarOverall.Size = new System.Drawing.Size(403, 23);
            this.progressBarOverall.Step = 1;
            this.progressBarOverall.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarOverall.TabIndex = 38;
            // 
            // labelProgressCurrent
            // 
            this.labelProgressCurrent.AutoSize = true;
            this.labelProgressCurrent.Location = new System.Drawing.Point(2, 2);
            this.labelProgressCurrent.Name = "labelProgressCurrent";
            this.labelProgressCurrent.Size = new System.Drawing.Size(94, 17);
            this.labelProgressCurrent.TabIndex = 41;
            this.labelProgressCurrent.Text = "Label Current";
            // 
            // progressBarCurrent
            // 
            this.progressBarCurrent.Location = new System.Drawing.Point(3, 24);
            this.progressBarCurrent.Maximum = 1000;
            this.progressBarCurrent.Name = "progressBarCurrent";
            this.progressBarCurrent.Size = new System.Drawing.Size(403, 23);
            this.progressBarCurrent.Step = 1;
            this.progressBarCurrent.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBarCurrent.TabIndex = 40;
            // 
            // labelProgressOverall
            // 
            this.labelProgressOverall.AutoSize = true;
            this.labelProgressOverall.Location = new System.Drawing.Point(2, 65);
            this.labelProgressOverall.Name = "labelProgressOverall";
            this.labelProgressOverall.Size = new System.Drawing.Size(92, 17);
            this.labelProgressOverall.TabIndex = 39;
            this.labelProgressOverall.Text = "Label Overall";
            // 
            // ProgressStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBarOverall);
            this.Controls.Add(this.labelProgressCurrent);
            this.Controls.Add(this.progressBarCurrent);
            this.Controls.Add(this.labelProgressOverall);
            this.Name = "ProgressStatusControl";
            this.Size = new System.Drawing.Size(409, 117);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBarOverall;
        private System.Windows.Forms.Label labelProgressCurrent;
        private System.Windows.Forms.ProgressBar progressBarCurrent;
        private System.Windows.Forms.Label labelProgressOverall;
    }
}
