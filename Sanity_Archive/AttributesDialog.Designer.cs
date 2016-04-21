namespace Sanity_Archive
{
    partial class AttributesDialog
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
            this.attr_archive_checkbox = new System.Windows.Forms.CheckBox();
            this.attr_system_checkbox = new System.Windows.Forms.CheckBox();
            this.attr_readonly_checkbox = new System.Windows.Forms.CheckBox();
            this.attr_compressed_checkbox = new System.Windows.Forms.CheckBox();
            this.attr_encrypted_checkbox = new System.Windows.Forms.CheckBox();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // attr_archive_checkbox
            // 
            this.attr_archive_checkbox.AutoSize = true;
            this.attr_archive_checkbox.Location = new System.Drawing.Point(28, 26);
            this.attr_archive_checkbox.Name = "attr_archive_checkbox";
            this.attr_archive_checkbox.Size = new System.Drawing.Size(62, 17);
            this.attr_archive_checkbox.TabIndex = 0;
            this.attr_archive_checkbox.Text = "Archive";
            this.attr_archive_checkbox.UseVisualStyleBackColor = true;
            // 
            // attr_system_checkbox
            // 
            this.attr_system_checkbox.AutoSize = true;
            this.attr_system_checkbox.Location = new System.Drawing.Point(28, 49);
            this.attr_system_checkbox.Name = "attr_system_checkbox";
            this.attr_system_checkbox.Size = new System.Drawing.Size(60, 17);
            this.attr_system_checkbox.TabIndex = 1;
            this.attr_system_checkbox.Text = "System";
            this.attr_system_checkbox.UseVisualStyleBackColor = true;
            // 
            // attr_readonly_checkbox
            // 
            this.attr_readonly_checkbox.AutoSize = true;
            this.attr_readonly_checkbox.Location = new System.Drawing.Point(28, 72);
            this.attr_readonly_checkbox.Name = "attr_readonly_checkbox";
            this.attr_readonly_checkbox.Size = new System.Drawing.Size(74, 17);
            this.attr_readonly_checkbox.TabIndex = 2;
            this.attr_readonly_checkbox.Text = "Read-only";
            this.attr_readonly_checkbox.UseVisualStyleBackColor = true;
            // 
            // attr_compressed_checkbox
            // 
            this.attr_compressed_checkbox.AutoSize = true;
            this.attr_compressed_checkbox.Enabled = false;
            this.attr_compressed_checkbox.Location = new System.Drawing.Point(28, 95);
            this.attr_compressed_checkbox.Name = "attr_compressed_checkbox";
            this.attr_compressed_checkbox.Size = new System.Drawing.Size(84, 17);
            this.attr_compressed_checkbox.TabIndex = 3;
            this.attr_compressed_checkbox.Text = "Compressed";
            this.attr_compressed_checkbox.UseVisualStyleBackColor = true;
            // 
            // attr_encrypted_checkbox
            // 
            this.attr_encrypted_checkbox.AutoSize = true;
            this.attr_encrypted_checkbox.Enabled = false;
            this.attr_encrypted_checkbox.Location = new System.Drawing.Point(28, 118);
            this.attr_encrypted_checkbox.Name = "attr_encrypted_checkbox";
            this.attr_encrypted_checkbox.Size = new System.Drawing.Size(74, 17);
            this.attr_encrypted_checkbox.TabIndex = 4;
            this.attr_encrypted_checkbox.Text = "Encrypted";
            this.attr_encrypted_checkbox.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(28, 153);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(84, 23);
            this.ok_button.TabIndex = 5;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(133, 153);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(84, 23);
            this.cancel_button.TabIndex = 6;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // AttributesDialog
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(239, 188);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.attr_encrypted_checkbox);
            this.Controls.Add(this.attr_compressed_checkbox);
            this.Controls.Add(this.attr_readonly_checkbox);
            this.Controls.Add(this.attr_system_checkbox);
            this.Controls.Add(this.attr_archive_checkbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AttributesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AttributesDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox attr_archive_checkbox;
        private System.Windows.Forms.CheckBox attr_system_checkbox;
        private System.Windows.Forms.CheckBox attr_readonly_checkbox;
        private System.Windows.Forms.CheckBox attr_compressed_checkbox;
        private System.Windows.Forms.CheckBox attr_encrypted_checkbox;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
    }
}