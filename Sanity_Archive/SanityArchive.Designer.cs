﻿namespace Sanity_Archive
{
    partial class SanityArchive
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
            this.drive_lbl = new System.Windows.Forms.Label();
            this.drives_box = new System.Windows.Forms.ComboBox();
            this.fileFolder_box = new System.Windows.Forms.ListBox();
            this.encryption_bttn = new System.Windows.Forms.Button();
            this.compression_bttn = new System.Windows.Forms.Button();
            this.search_bttn = new System.Windows.Forms.Button();
            this.attributes_bttn = new System.Windows.Forms.Button();
            this.size_lbl = new System.Windows.Forms.Label();
            this.path_box = new System.Windows.Forms.TextBox();
            this.path_lbl = new System.Windows.Forms.Label();
            this.copy_button = new System.Windows.Forms.Button();
            this.move_button = new System.Windows.Forms.Button();
            this.paste_button = new System.Windows.Forms.Button();
            this.exctract_bttn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // drive_lbl
            // 
            this.drive_lbl.AutoSize = true;
            this.drive_lbl.Location = new System.Drawing.Point(13, 13);
            this.drive_lbl.Name = "drive_lbl";
            this.drive_lbl.Size = new System.Drawing.Size(37, 13);
            this.drive_lbl.TabIndex = 0;
            this.drive_lbl.Text = "Drives";
            // 
            // drives_box
            // 
            this.drives_box.FormattingEnabled = true;
            this.drives_box.Location = new System.Drawing.Point(56, 10);
            this.drives_box.Name = "drives_box";
            this.drives_box.Size = new System.Drawing.Size(241, 21);
            this.drives_box.TabIndex = 1;
            this.drives_box.SelectedIndexChanged += new System.EventHandler(this.drives_box_SelectedIndexChanged);
            // 
            // fileFolder_box
            // 
            this.fileFolder_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileFolder_box.FormattingEnabled = true;
            this.fileFolder_box.Location = new System.Drawing.Point(16, 69);
            this.fileFolder_box.Name = "fileFolder_box";
            this.fileFolder_box.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.fileFolder_box.Size = new System.Drawing.Size(282, 147);
            this.fileFolder_box.TabIndex = 2;
            this.fileFolder_box.SelectedIndexChanged += new System.EventHandler(this.fileFolder_box_SelectedIndexChanged);
            this.fileFolder_box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.fileFolder_box_KeyDown);
            this.fileFolder_box.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.fileFolder_box_MouseDoubleClick);
            // 
            // encryption_bttn
            // 
            this.encryption_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.encryption_bttn.Location = new System.Drawing.Point(16, 255);
            this.encryption_bttn.Name = "encryption_bttn";
            this.encryption_bttn.Size = new System.Drawing.Size(139, 23);
            this.encryption_bttn.TabIndex = 3;
            this.encryption_bttn.Text = "Encrypt/Decrypt";
            this.encryption_bttn.UseVisualStyleBackColor = true;
            this.encryption_bttn.Click += new System.EventHandler(this.encryption_bttn_Click);
            // 
            // compression_bttn
            // 
            this.compression_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.compression_bttn.Location = new System.Drawing.Point(158, 255);
            this.compression_bttn.Name = "compression_bttn";
            this.compression_bttn.Size = new System.Drawing.Size(68, 23);
            this.compression_bttn.TabIndex = 4;
            this.compression_bttn.Text = "Compress";
            this.compression_bttn.UseVisualStyleBackColor = true;
            this.compression_bttn.Click += new System.EventHandler(this.compression_bttn_Click);
            // 
            // search_bttn
            // 
            this.search_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.search_bttn.Location = new System.Drawing.Point(158, 284);
            this.search_bttn.Name = "search_bttn";
            this.search_bttn.Size = new System.Drawing.Size(139, 23);
            this.search_bttn.TabIndex = 6;
            this.search_bttn.Text = "Search";
            this.search_bttn.UseVisualStyleBackColor = true;
            this.search_bttn.Click += new System.EventHandler(this.search_bttn_Click);
            // 
            // attributes_bttn
            // 
            this.attributes_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.attributes_bttn.Location = new System.Drawing.Point(16, 284);
            this.attributes_bttn.Name = "attributes_bttn";
            this.attributes_bttn.Size = new System.Drawing.Size(139, 23);
            this.attributes_bttn.TabIndex = 5;
            this.attributes_bttn.Text = "Attributes";
            this.attributes_bttn.UseVisualStyleBackColor = true;
            this.attributes_bttn.Click += new System.EventHandler(this.attributes_bttn_Click);
            // 
            // size_lbl
            // 
            this.size_lbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.size_lbl.AutoSize = true;
            this.size_lbl.Location = new System.Drawing.Point(15, 236);
            this.size_lbl.Name = "size_lbl";
            this.size_lbl.Size = new System.Drawing.Size(38, 13);
            this.size_lbl.TabIndex = 7;
            this.size_lbl.Text = "0.0 kB";
            // 
            // path_box
            // 
            this.path_box.Location = new System.Drawing.Point(56, 38);
            this.path_box.Name = "path_box";
            this.path_box.Size = new System.Drawing.Size(241, 20);
            this.path_box.TabIndex = 8;
            this.path_box.KeyDown += new System.Windows.Forms.KeyEventHandler(this.path_box_KeyDown);
            // 
            // path_lbl
            // 
            this.path_lbl.AutoSize = true;
            this.path_lbl.Location = new System.Drawing.Point(15, 41);
            this.path_lbl.Name = "path_lbl";
            this.path_lbl.Size = new System.Drawing.Size(29, 13);
            this.path_lbl.TabIndex = 9;
            this.path_lbl.Text = "Path";
            // 
            // copy_button
            // 
            this.copy_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.copy_button.Location = new System.Drawing.Point(16, 313);
            this.copy_button.Name = "copy_button";
            this.copy_button.Size = new System.Drawing.Size(87, 23);
            this.copy_button.TabIndex = 10;
            this.copy_button.Text = "Copy";
            this.copy_button.UseVisualStyleBackColor = true;
            this.copy_button.Click += new System.EventHandler(this.copy_button_Click);
            // 
            // move_button
            // 
            this.move_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.move_button.Location = new System.Drawing.Point(109, 313);
            this.move_button.Name = "move_button";
            this.move_button.Size = new System.Drawing.Size(87, 23);
            this.move_button.TabIndex = 11;
            this.move_button.Text = "Move";
            this.move_button.UseVisualStyleBackColor = true;
            this.move_button.Click += new System.EventHandler(this.move_button_Click);
            // 
            // paste_button
            // 
            this.paste_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.paste_button.Location = new System.Drawing.Point(202, 313);
            this.paste_button.Name = "paste_button";
            this.paste_button.Size = new System.Drawing.Size(95, 23);
            this.paste_button.TabIndex = 12;
            this.paste_button.Text = "Paste";
            this.paste_button.UseVisualStyleBackColor = true;
            this.paste_button.Click += new System.EventHandler(this.paste_button_Click);
            // 
            // exctract_bttn
            // 
            this.exctract_bttn.Location = new System.Drawing.Point(229, 255);
            this.exctract_bttn.Name = "exctract_bttn";
            this.exctract_bttn.Size = new System.Drawing.Size(68, 23);
            this.exctract_bttn.TabIndex = 13;
            this.exctract_bttn.Text = "Extract";
            this.exctract_bttn.UseVisualStyleBackColor = true;
            this.exctract_bttn.Click += new System.EventHandler(this.exctract_bttn_Click);
            // 
            // SanityArchive
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(315, 354);
            this.Controls.Add(this.exctract_bttn);
            this.Controls.Add(this.paste_button);
            this.Controls.Add(this.move_button);
            this.Controls.Add(this.copy_button);
            this.Controls.Add(this.path_lbl);
            this.Controls.Add(this.path_box);
            this.Controls.Add(this.size_lbl);
            this.Controls.Add(this.search_bttn);
            this.Controls.Add(this.attributes_bttn);
            this.Controls.Add(this.compression_bttn);
            this.Controls.Add(this.encryption_bttn);
            this.Controls.Add(this.fileFolder_box);
            this.Controls.Add(this.drives_box);
            this.Controls.Add(this.drive_lbl);
            this.MaximumSize = new System.Drawing.Size(331, 393);
            this.Name = "SanityArchive";
            this.Text = "Sanity Archive";
            this.Load += new System.EventHandler(this.SanityArchive_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label drive_lbl;
        private System.Windows.Forms.ComboBox drives_box;
        private System.Windows.Forms.ListBox fileFolder_box;
        private System.Windows.Forms.Button encryption_bttn;
        private System.Windows.Forms.Button compression_bttn;
        private System.Windows.Forms.Button search_bttn;
        private System.Windows.Forms.Button attributes_bttn;
        private System.Windows.Forms.Label size_lbl;
        private System.Windows.Forms.TextBox path_box;
        private System.Windows.Forms.Label path_lbl;
        private System.Windows.Forms.Button copy_button;
        private System.Windows.Forms.Button move_button;
        private System.Windows.Forms.Button paste_button;
        private System.Windows.Forms.Button exctract_bttn;
    }
}

