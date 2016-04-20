namespace Sanity_Archive
{
    partial class TextReader
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
            this.text_reader_box = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // text_reader_box
            // 
            this.text_reader_box.Location = new System.Drawing.Point(12, 12);
            this.text_reader_box.Name = "text_reader_box";
            this.text_reader_box.Size = new System.Drawing.Size(260, 237);
            this.text_reader_box.TabIndex = 0;
            this.text_reader_box.Text = "";
            // 
            // TextReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.text_reader_box);
            this.Name = "TextReader";
            this.Text = "TextReader";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox text_reader_box;
    }
}