
namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    partial class TwoProgressStatusControl
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
            RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue progressStatusValue1 = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue();
            RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue progressStatusValue2 = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusValue();
            this.progressStatusControl2 = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusControl();
            this.progressStatusControl1 = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ProgressStatusControl();
            this.SuspendLayout();
            // 
            // progressStatusControl2
            // 
            this.progressStatusControl2.Location = new System.Drawing.Point(265, 3);
            this.progressStatusControl2.Name = "progressStatusControl2";
            this.progressStatusControl2.Size = new System.Drawing.Size(259, 126);
            this.progressStatusControl2.TabIndex = 1;
            this.progressStatusControl2.Value = progressStatusValue1;
            // 
            // progressStatusControl1
            // 
            this.progressStatusControl1.Location = new System.Drawing.Point(0, 3);
            this.progressStatusControl1.Name = "progressStatusControl1";
            this.progressStatusControl1.Size = new System.Drawing.Size(259, 126);
            this.progressStatusControl1.TabIndex = 0;
            this.progressStatusControl1.Value = progressStatusValue2;
            // 
            // TwoProgressStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressStatusControl2);
            this.Controls.Add(this.progressStatusControl1);
            this.Name = "TwoProgressStatusControl";
            this.Size = new System.Drawing.Size(526, 131);
            this.ResumeLayout(false);

        }

        #endregion

        private ProgressStatusControl progressStatusControl1;
        private ProgressStatusControl progressStatusControl2;
    }
}
