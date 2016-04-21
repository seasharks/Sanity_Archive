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
            this.attr_hidden_checkbox = new System.Windows.Forms.CheckBox();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.created_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.created_label = new System.Windows.Forms.Label();
            this.modified_label = new System.Windows.Forms.Label();
            this.accessed_label = new System.Windows.Forms.Label();
            this.modified_dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.accessed_dateTimePicker = new System.Windows.Forms.DateTimePicker();
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
            this.attr_system_checkbox.Location = new System.Drawing.Point(28, 95);
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
            // attr_hidden_checkbox
            // 
            this.attr_hidden_checkbox.AutoSize = true;
            this.attr_hidden_checkbox.Location = new System.Drawing.Point(28, 49);
            this.attr_hidden_checkbox.Name = "attr_hidden_checkbox";
            this.attr_hidden_checkbox.Size = new System.Drawing.Size(60, 17);
            this.attr_hidden_checkbox.TabIndex = 3;
            this.attr_hidden_checkbox.Text = "Hidden";
            this.attr_hidden_checkbox.UseVisualStyleBackColor = true;
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(28, 207);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(90, 23);
            this.ok_button.TabIndex = 5;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancel_button.Location = new System.Drawing.Point(170, 207);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(90, 23);
            this.cancel_button.TabIndex = 6;
            this.cancel_button.Text = "Cancel";
            this.cancel_button.UseVisualStyleBackColor = true;
            // 
            // created_label
            // 
            this.created_label.AutoSize = true;
            this.created_label.Location = new System.Drawing.Point(25, 124);
            this.created_label.Name = "created_label";
            this.created_label.Size = new System.Drawing.Size(35, 13);
            this.created_label.TabIndex = 8;
            this.created_label.Text = "Created:";
            // 
            // modified_label
            // 
            this.modified_label.AutoSize = true;
            this.modified_label.Location = new System.Drawing.Point(25, 152);
            this.modified_label.Name = "modified_label";
            this.modified_label.Size = new System.Drawing.Size(35, 13);
            this.modified_label.TabIndex = 9;
            this.modified_label.Text = "Modified:";
            // 
            // accessed_label
            // 
            this.accessed_label.AutoSize = true;
            this.accessed_label.Location = new System.Drawing.Point(25, 180);
            this.accessed_label.Name = "accessed_label";
            this.accessed_label.Size = new System.Drawing.Size(35, 13);
            this.accessed_label.TabIndex = 10;
            this.accessed_label.Text = "Accessed:";
            // 
            // created_dateTimePicker
            // 
            this.created_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.created_dateTimePicker.CustomFormat = "yyyy.MMMMdd.  hh:mm";
            this.created_dateTimePicker.Location = new System.Drawing.Point(85, 118);
            this.created_dateTimePicker.Name = "dateTimePicker1";
            this.created_dateTimePicker.Size = new System.Drawing.Size(175, 20);
            this.created_dateTimePicker.TabIndex = 7;
            // 
            // modified_dateTimePicker
            // 
            this.modified_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.modified_dateTimePicker.CustomFormat = "yyyy.MMMMdd.  hh:mm";
            this.modified_dateTimePicker.Location = new System.Drawing.Point(85, 146);
            this.modified_dateTimePicker.Name = "dateTimePicker2";
            this.modified_dateTimePicker.Size = new System.Drawing.Size(175, 20);
            this.modified_dateTimePicker.TabIndex = 11;
            // 
            // accessed_dateTimePicker
            // 
            this.accessed_dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.accessed_dateTimePicker.CustomFormat = "yyyy.MMMMdd.  hh:mm";
            this.accessed_dateTimePicker.Location = new System.Drawing.Point(85, 174);
            this.accessed_dateTimePicker.Name = "dateTimePicker3";
            this.accessed_dateTimePicker.Size = new System.Drawing.Size(175, 20);
            this.accessed_dateTimePicker.TabIndex = 12;
            // 
            // AttributesDialog
            // 
            this.AcceptButton = this.ok_button;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancel_button;
            this.ClientSize = new System.Drawing.Size(283, 242);
            this.Controls.Add(this.accessed_dateTimePicker);
            this.Controls.Add(this.modified_dateTimePicker);
            this.Controls.Add(this.accessed_label);
            this.Controls.Add(this.modified_label);
            this.Controls.Add(this.created_label);
            this.Controls.Add(this.created_dateTimePicker);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.attr_hidden_checkbox);
            this.Controls.Add(this.attr_readonly_checkbox);
            this.Controls.Add(this.attr_system_checkbox);
            this.Controls.Add(this.attr_archive_checkbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "AttributesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AttributesDialog";
            this.Load += new System.EventHandler(this.AttributesDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox attr_archive_checkbox;
        private System.Windows.Forms.CheckBox attr_system_checkbox;
        private System.Windows.Forms.CheckBox attr_readonly_checkbox;
        private System.Windows.Forms.CheckBox attr_hidden_checkbox;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.DateTimePicker created_dateTimePicker;
        private System.Windows.Forms.Label created_label;
        private System.Windows.Forms.Label modified_label;
        private System.Windows.Forms.Label accessed_label;
        private System.Windows.Forms.DateTimePicker modified_dateTimePicker;
        private System.Windows.Forms.DateTimePicker accessed_dateTimePicker;
    }
}