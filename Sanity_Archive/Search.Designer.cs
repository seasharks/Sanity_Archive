namespace Sanity_Archive
{
    partial class Search
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
            this.filename_textbox = new System.Windows.Forms.TextBox();
            this.path = new System.Windows.Forms.Label();
            this.search_button = new System.Windows.Forms.Button();
            this.path_textbox = new System.Windows.Forms.TextBox();
            this.search_result_box = new System.Windows.Forms.ListBox();
            this.filename = new System.Windows.Forms.Label();
            this.brows_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filename_textbox
            // 
            this.filename_textbox.Location = new System.Drawing.Point(70, 12);
            this.filename_textbox.Name = "filename_textbox";
            this.filename_textbox.Size = new System.Drawing.Size(202, 20);
            this.filename_textbox.TabIndex = 0;
            // 
            // path
            // 
            this.path.AutoSize = true;
            this.path.Location = new System.Drawing.Point(12, 46);
            this.path.Name = "path";
            this.path.Size = new System.Drawing.Size(32, 13);
            this.path.TabIndex = 1;
            this.path.Text = "Path:";
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(278, 9);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(75, 23);
            this.search_button.TabIndex = 2;
            this.search_button.Text = "Search";
            this.search_button.UseVisualStyleBackColor = true;
            this.search_button.Click += new System.EventHandler(this.search_button_Click);
            // 
            // path_textbox
            // 
            this.path_textbox.Location = new System.Drawing.Point(70, 45);
            this.path_textbox.Name = "path_textbox";
            this.path_textbox.ReadOnly = true;
            this.path_textbox.Size = new System.Drawing.Size(202, 20);
            this.path_textbox.TabIndex = 3;
            // 
            // search_result_box
            // 
            this.search_result_box.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.search_result_box.FormattingEnabled = true;
            this.search_result_box.Location = new System.Drawing.Point(12, 71);
            this.search_result_box.Name = "search_result_box";
            this.search_result_box.Size = new System.Drawing.Size(341, 173);
            this.search_result_box.TabIndex = 4;
            // 
            // filename
            // 
            this.filename.AutoSize = true;
            this.filename.Location = new System.Drawing.Point(12, 14);
            this.filename.Name = "filename";
            this.filename.Size = new System.Drawing.Size(52, 13);
            this.filename.TabIndex = 5;
            this.filename.Text = "Filename:";
            // 
            // brows_button
            // 
            this.brows_button.Location = new System.Drawing.Point(278, 43);
            this.brows_button.Name = "brows_button";
            this.brows_button.Size = new System.Drawing.Size(75, 23);
            this.brows_button.TabIndex = 6;
            this.brows_button.Text = "Browse";
            this.brows_button.UseVisualStyleBackColor = true;
            this.brows_button.Click += new System.EventHandler(this.Browse_Click);
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 255);
            this.Controls.Add(this.brows_button);
            this.Controls.Add(this.filename);
            this.Controls.Add(this.search_result_box);
            this.Controls.Add(this.path_textbox);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.path);
            this.Controls.Add(this.filename_textbox);
            this.MinimumSize = new System.Drawing.Size(381, 294);
            this.Name = "Search";
            this.Text = "Search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filename_textbox;
        private System.Windows.Forms.Label path;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.TextBox path_textbox;
        private System.Windows.Forms.ListBox search_result_box;
        private System.Windows.Forms.Label filename;
        private System.Windows.Forms.Button brows_button;
    }
}