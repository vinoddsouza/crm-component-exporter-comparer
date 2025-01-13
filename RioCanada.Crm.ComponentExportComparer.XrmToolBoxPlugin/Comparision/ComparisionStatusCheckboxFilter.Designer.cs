
namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.Comparision
{
    partial class ComparisionStatusCheckboxFilter
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
            this.imageCheckboxTarget = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ImageCheckbox();
            this.imageCheckboxSource = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ImageCheckbox();
            this.imageCheckboxModified = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ImageCheckbox();
            this.imageCheckboxUnchanged = new RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.ImageCheckbox();
            this.SuspendLayout();
            // 
            // imageCheckboxTarget
            // 
            this.imageCheckboxTarget.Checked = true;
            this.imageCheckboxTarget.CheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.CheckRed;
            this.imageCheckboxTarget.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageCheckboxTarget.HoverCheckedImage = null;
            this.imageCheckboxTarget.HoverUncheckedImage = null;
            this.imageCheckboxTarget.Label = "Only in target";
            this.imageCheckboxTarget.Location = new System.Drawing.Point(363, 3);
            this.imageCheckboxTarget.Name = "imageCheckboxTarget";
            this.imageCheckboxTarget.Size = new System.Drawing.Size(125, 18);
            this.imageCheckboxTarget.TabIndex = 23;
            this.imageCheckboxTarget.UncheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.UncheckRedDark;
            this.imageCheckboxTarget.CheckedChanged += new System.EventHandler(this.imageCheckboxTarget_CheckedChanged);
            // 
            // imageCheckboxSource
            // 
            this.imageCheckboxSource.Checked = true;
            this.imageCheckboxSource.CheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.CheckGreen;
            this.imageCheckboxSource.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageCheckboxSource.HoverCheckedImage = null;
            this.imageCheckboxSource.HoverUncheckedImage = null;
            this.imageCheckboxSource.Label = "Only in source";
            this.imageCheckboxSource.Location = new System.Drawing.Point(223, 3);
            this.imageCheckboxSource.Name = "imageCheckboxSource";
            this.imageCheckboxSource.Size = new System.Drawing.Size(125, 18);
            this.imageCheckboxSource.TabIndex = 22;
            this.imageCheckboxSource.UncheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.UncheckGreenDark;
            this.imageCheckboxSource.CheckedChanged += new System.EventHandler(this.imageCheckboxSource_CheckedChanged);
            // 
            // imageCheckboxModified
            // 
            this.imageCheckboxModified.Checked = true;
            this.imageCheckboxModified.CheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.CheckOrange;
            this.imageCheckboxModified.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageCheckboxModified.HoverCheckedImage = null;
            this.imageCheckboxModified.HoverUncheckedImage = null;
            this.imageCheckboxModified.Label = "Modified";
            this.imageCheckboxModified.Location = new System.Drawing.Point(121, 3);
            this.imageCheckboxModified.Name = "imageCheckboxModified";
            this.imageCheckboxModified.Size = new System.Drawing.Size(104, 18);
            this.imageCheckboxModified.TabIndex = 21;
            this.imageCheckboxModified.UncheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.UncheckOrangeDark;
            this.imageCheckboxModified.CheckedChanged += new System.EventHandler(this.imageCheckboxModified_CheckedChanged);
            // 
            // imageCheckboxUnchanged
            // 
            this.imageCheckboxUnchanged.Checked = false;
            this.imageCheckboxUnchanged.CheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.CheckGrey;
            this.imageCheckboxUnchanged.Cursor = System.Windows.Forms.Cursors.Hand;
            this.imageCheckboxUnchanged.HoverCheckedImage = null;
            this.imageCheckboxUnchanged.HoverUncheckedImage = null;
            this.imageCheckboxUnchanged.Label = "Unchanged";
            this.imageCheckboxUnchanged.Location = new System.Drawing.Point(3, 3);
            this.imageCheckboxUnchanged.Name = "imageCheckboxUnchanged";
            this.imageCheckboxUnchanged.Size = new System.Drawing.Size(104, 18);
            this.imageCheckboxUnchanged.TabIndex = 20;
            this.imageCheckboxUnchanged.UncheckedImage = global::RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin.AppResource.UncheckGreyDark;
            this.imageCheckboxUnchanged.CheckedChanged += new System.EventHandler(this.imageCheckboxUnchanged_CheckedChanged);
            // 
            // ComparisionStatusCheckboxFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.imageCheckboxTarget);
            this.Controls.Add(this.imageCheckboxSource);
            this.Controls.Add(this.imageCheckboxModified);
            this.Controls.Add(this.imageCheckboxUnchanged);
            this.Name = "ComparisionStatusCheckboxFilter";
            this.Size = new System.Drawing.Size(503, 28);
            this.ResumeLayout(false);

        }

        #endregion

        private ImageCheckbox imageCheckboxTarget;
        private ImageCheckbox imageCheckboxSource;
        private ImageCheckbox imageCheckboxModified;
        private ImageCheckbox imageCheckboxUnchanged;
    }
}
