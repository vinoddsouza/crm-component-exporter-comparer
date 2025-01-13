
namespace RioCanada.Crm.ComponentExportComparer.XrmToolBoxPlugin
{
    partial class FormQuerySetting
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.checkBoxIncludeSysWebresource = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeAllMeta = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxIncludeEntityDashboard = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeEntityRelationship = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeEntityColumn = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeEntityRibbon = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeEntityView = new System.Windows.Forms.CheckBox();
            this.checkBoxIncludeEntityForm = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.checkBoxIncludeSysPluginStep = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBoxRoleById = new System.Windows.Forms.CheckBox();
            this.checkBoxReplaceEmptyStringByNull = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxIncludeSysWebresource
            // 
            this.checkBoxIncludeSysWebresource.AutoSize = true;
            this.checkBoxIncludeSysWebresource.Location = new System.Drawing.Point(6, 53);
            this.checkBoxIncludeSysWebresource.Name = "checkBoxIncludeSysWebresource";
            this.checkBoxIncludeSysWebresource.Size = new System.Drawing.Size(194, 21);
            this.checkBoxIncludeSysWebresource.TabIndex = 27;
            this.checkBoxIncludeSysWebresource.Text = "Include system webresource";
            this.checkBoxIncludeSysWebresource.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeAllMeta
            // 
            this.checkBoxIncludeAllMeta.AutoSize = true;
            this.checkBoxIncludeAllMeta.Location = new System.Drawing.Point(6, 24);
            this.checkBoxIncludeAllMeta.Name = "checkBoxIncludeAllMeta";
            this.checkBoxIncludeAllMeta.Size = new System.Drawing.Size(202, 21);
            this.checkBoxIncludeAllMeta.TabIndex = 26;
            this.checkBoxIncludeAllMeta.Text = "Include all metadata property";
            this.checkBoxIncludeAllMeta.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(279, 464);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(66, 27);
            this.buttonCancel.TabIndex = 5;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOk.Location = new System.Drawing.Point(208, 464);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(66, 27);
            this.buttonOk.TabIndex = 4;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBoxIncludeEntityDashboard);
            this.groupBox1.Controls.Add(this.checkBoxIncludeEntityRelationship);
            this.groupBox1.Controls.Add(this.checkBoxIncludeEntityColumn);
            this.groupBox1.Controls.Add(this.checkBoxIncludeEntityRibbon);
            this.groupBox1.Controls.Add(this.checkBoxIncludeEntityView);
            this.groupBox1.Controls.Add(this.checkBoxIncludeEntityForm);
            this.groupBox1.Location = new System.Drawing.Point(12, 165);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 202);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Table";
            // 
            // checkBoxIncludeEntityDashboard
            // 
            this.checkBoxIncludeEntityDashboard.AutoSize = true;
            this.checkBoxIncludeEntityDashboard.Location = new System.Drawing.Point(6, 140);
            this.checkBoxIncludeEntityDashboard.Name = "checkBoxIncludeEntityDashboard";
            this.checkBoxIncludeEntityDashboard.Size = new System.Drawing.Size(145, 21);
            this.checkBoxIncludeEntityDashboard.TabIndex = 34;
            this.checkBoxIncludeEntityDashboard.Text = "Include dashboards";
            this.checkBoxIncludeEntityDashboard.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeEntityRelationship
            // 
            this.checkBoxIncludeEntityRelationship.AutoSize = true;
            this.checkBoxIncludeEntityRelationship.Location = new System.Drawing.Point(6, 53);
            this.checkBoxIncludeEntityRelationship.Name = "checkBoxIncludeEntityRelationship";
            this.checkBoxIncludeEntityRelationship.Size = new System.Drawing.Size(149, 21);
            this.checkBoxIncludeEntityRelationship.TabIndex = 33;
            this.checkBoxIncludeEntityRelationship.Text = "Include relationships";
            this.checkBoxIncludeEntityRelationship.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeEntityColumn
            // 
            this.checkBoxIncludeEntityColumn.AutoSize = true;
            this.checkBoxIncludeEntityColumn.Location = new System.Drawing.Point(6, 24);
            this.checkBoxIncludeEntityColumn.Name = "checkBoxIncludeEntityColumn";
            this.checkBoxIncludeEntityColumn.Size = new System.Drawing.Size(123, 21);
            this.checkBoxIncludeEntityColumn.TabIndex = 32;
            this.checkBoxIncludeEntityColumn.Text = "Include columns";
            this.checkBoxIncludeEntityColumn.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeEntityRibbon
            // 
            this.checkBoxIncludeEntityRibbon.AutoSize = true;
            this.checkBoxIncludeEntityRibbon.Location = new System.Drawing.Point(6, 169);
            this.checkBoxIncludeEntityRibbon.Name = "checkBoxIncludeEntityRibbon";
            this.checkBoxIncludeEntityRibbon.Size = new System.Drawing.Size(114, 21);
            this.checkBoxIncludeEntityRibbon.TabIndex = 31;
            this.checkBoxIncludeEntityRibbon.Text = "Include ribbon";
            this.checkBoxIncludeEntityRibbon.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeEntityView
            // 
            this.checkBoxIncludeEntityView.AutoSize = true;
            this.checkBoxIncludeEntityView.Location = new System.Drawing.Point(6, 82);
            this.checkBoxIncludeEntityView.Name = "checkBoxIncludeEntityView";
            this.checkBoxIncludeEntityView.Size = new System.Drawing.Size(106, 21);
            this.checkBoxIncludeEntityView.TabIndex = 30;
            this.checkBoxIncludeEntityView.Text = "Include views";
            this.checkBoxIncludeEntityView.UseVisualStyleBackColor = true;
            // 
            // checkBoxIncludeEntityForm
            // 
            this.checkBoxIncludeEntityForm.AutoSize = true;
            this.checkBoxIncludeEntityForm.Location = new System.Drawing.Point(6, 111);
            this.checkBoxIncludeEntityForm.Name = "checkBoxIncludeEntityForm";
            this.checkBoxIncludeEntityForm.Size = new System.Drawing.Size(109, 21);
            this.checkBoxIncludeEntityForm.TabIndex = 29;
            this.checkBoxIncludeEntityForm.Text = "Include forms";
            this.checkBoxIncludeEntityForm.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.checkBoxReplaceEmptyStringByNull);
            this.groupBox2.Controls.Add(this.checkBoxIncludeSysPluginStep);
            this.groupBox2.Controls.Add(this.checkBoxIncludeSysWebresource);
            this.groupBox2.Controls.Add(this.checkBoxIncludeAllMeta);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(336, 140);
            this.groupBox2.TabIndex = 32;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "General";
            // 
            // checkBoxIncludeSysPluginStep
            // 
            this.checkBoxIncludeSysPluginStep.AutoSize = true;
            this.checkBoxIncludeSysPluginStep.Location = new System.Drawing.Point(6, 82);
            this.checkBoxIncludeSysPluginStep.Name = "checkBoxIncludeSysPluginStep";
            this.checkBoxIncludeSysPluginStep.Size = new System.Drawing.Size(184, 21);
            this.checkBoxIncludeSysPluginStep.TabIndex = 28;
            this.checkBoxIncludeSysPluginStep.Text = "Include system plugin step";
            this.checkBoxIncludeSysPluginStep.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.checkBoxRoleById);
            this.groupBox3.Location = new System.Drawing.Point(12, 382);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(336, 64);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Security Role";
            // 
            // checkBoxRoleById
            // 
            this.checkBoxRoleById.AutoSize = true;
            this.checkBoxRoleById.Location = new System.Drawing.Point(6, 24);
            this.checkBoxRoleById.Name = "checkBoxRoleById";
            this.checkBoxRoleById.Size = new System.Drawing.Size(274, 21);
            this.checkBoxRoleById.TabIndex = 26;
            this.checkBoxRoleById.Text = "Compare security role by id instead name";
            this.checkBoxRoleById.UseVisualStyleBackColor = true;
            // 
            // checkBoxReplaceEmptyStringByNull
            // 
            this.checkBoxReplaceEmptyStringByNull.AutoSize = true;
            this.checkBoxReplaceEmptyStringByNull.Location = new System.Drawing.Point(6, 109);
            this.checkBoxReplaceEmptyStringByNull.Name = "checkBoxReplaceEmptyStringByNull";
            this.checkBoxReplaceEmptyStringByNull.Size = new System.Drawing.Size(195, 21);
            this.checkBoxReplaceEmptyStringByNull.TabIndex = 29;
            this.checkBoxReplaceEmptyStringByNull.Text = "Replace empty string by null";
            this.checkBoxReplaceEmptyStringByNull.UseVisualStyleBackColor = true;
            // 
            // FormQuerySetting
            // 
            this.AcceptButton = this.buttonOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(360, 503);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormQuerySetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Query Options";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.CheckBox checkBoxIncludeAllMeta;
        private System.Windows.Forms.CheckBox checkBoxIncludeSysWebresource;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBoxIncludeEntityRibbon;
        private System.Windows.Forms.CheckBox checkBoxIncludeEntityView;
        private System.Windows.Forms.CheckBox checkBoxIncludeEntityForm;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxIncludeEntityRelationship;
        private System.Windows.Forms.CheckBox checkBoxIncludeEntityColumn;
        private System.Windows.Forms.CheckBox checkBoxIncludeSysPluginStep;
        private System.Windows.Forms.CheckBox checkBoxIncludeEntityDashboard;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBoxRoleById;
        private System.Windows.Forms.CheckBox checkBoxReplaceEmptyStringByNull;
    }
}