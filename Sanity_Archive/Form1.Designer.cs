namespace Sanity_Archive
{
    partial class Form1
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
            this.drive = new System.Windows.Forms.Label();
            this.drives_box = new System.Windows.Forms.ComboBox();
            this.fileFolder_box = new System.Windows.Forms.ListBox();
            this.encryption_bttn = new System.Windows.Forms.Button();
            this.compression_bttn = new System.Windows.Forms.Button();
            this.search_bttn = new System.Windows.Forms.Button();
            this.attributes_bttn = new System.Windows.Forms.Button();
            this.size_lbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // drive
            // 
            this.drive.AutoSize = true;
            this.drive.Location = new System.Drawing.Point(13, 13);
            this.drive.Name = "drive";
            this.drive.Size = new System.Drawing.Size(37, 13);
            this.drive.TabIndex = 0;
            this.drive.Text = "Drives";
            // 
            // drives_box
            // 
            this.drives_box.FormattingEnabled = true;
            this.drives_box.Location = new System.Drawing.Point(56, 10);
            this.drives_box.Name = "drives_box";
            this.drives_box.Size = new System.Drawing.Size(241, 21);
            this.drives_box.TabIndex = 1;
            // 
            // fileFolder_box
            // 
            this.fileFolder_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.fileFolder_box.FormattingEnabled = true;
            this.fileFolder_box.Location = new System.Drawing.Point(16, 43);
            this.fileFolder_box.Name = "fileFolder_box";
            this.fileFolder_box.Size = new System.Drawing.Size(281, 173);
            this.fileFolder_box.TabIndex = 2;
            // 
            // encryption_bttn
            // 
            this.encryption_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.encryption_bttn.Location = new System.Drawing.Point(13, 244);
            this.encryption_bttn.Name = "encryption_bttn";
            this.encryption_bttn.Size = new System.Drawing.Size(139, 23);
            this.encryption_bttn.TabIndex = 3;
            this.encryption_bttn.Text = "Encrypt/Decrypt";
            this.encryption_bttn.UseVisualStyleBackColor = true;
            // 
            // compression_bttn
            // 
            this.compression_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.compression_bttn.Location = new System.Drawing.Point(158, 244);
            this.compression_bttn.Name = "compression_bttn";
            this.compression_bttn.Size = new System.Drawing.Size(139, 23);
            this.compression_bttn.TabIndex = 4;
            this.compression_bttn.Text = "Compress/Decompress";
            this.compression_bttn.UseVisualStyleBackColor = true;
            // 
            // search_bttn
            // 
            this.search_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.search_bttn.Location = new System.Drawing.Point(158, 273);
            this.search_bttn.Name = "search_bttn";
            this.search_bttn.Size = new System.Drawing.Size(139, 23);
            this.search_bttn.TabIndex = 6;
            this.search_bttn.Text = "Search";
            this.search_bttn.UseVisualStyleBackColor = true;
            // 
            // attributes_bttn
            // 
            this.attributes_bttn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.attributes_bttn.Location = new System.Drawing.Point(13, 273);
            this.attributes_bttn.Name = "attributes_bttn";
            this.attributes_bttn.Size = new System.Drawing.Size(139, 23);
            this.attributes_bttn.TabIndex = 5;
            this.attributes_bttn.Text = "Attributes";
            this.attributes_bttn.UseVisualStyleBackColor = true;
            // 
            // size_lbl
            // 
            this.size_lbl.AutoSize = true;
            this.size_lbl.Location = new System.Drawing.Point(16, 225);
            this.size_lbl.Name = "size_lbl";
            this.size_lbl.Size = new System.Drawing.Size(169, 13);
            this.size_lbl.TabIndex = 7;
            this.size_lbl.Text = "0.0 kB/0.0 kB 0/0 files 0/0 folders";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 308);
            this.Controls.Add(this.size_lbl);
            this.Controls.Add(this.search_bttn);
            this.Controls.Add(this.attributes_bttn);
            this.Controls.Add(this.compression_bttn);
            this.Controls.Add(this.encryption_bttn);
            this.Controls.Add(this.fileFolder_box);
            this.Controls.Add(this.drives_box);
            this.Controls.Add(this.drive);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label drive;
        private System.Windows.Forms.ComboBox drives_box;
        private System.Windows.Forms.ListBox fileFolder_box;
        private System.Windows.Forms.Button encryption_bttn;
        private System.Windows.Forms.Button compression_bttn;
        private System.Windows.Forms.Button search_bttn;
        private System.Windows.Forms.Button attributes_bttn;
        private System.Windows.Forms.Label size_lbl;
    }
}

