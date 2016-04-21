using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sanity_Archive
{
    public partial class TextReader : Form
    {
        private SanityArchive mainForm = null;
        private string filePath;

        public TextReader()
        {
            InitializeComponent();
        }

        public TextReader(Form callingForm, string filePath)
        {
            mainForm = callingForm as SanityArchive;
            InitializeComponent();
            this.filePath = filePath;
        }

        private void TextReader_Load(object sender, EventArgs e)
        {
            try
            {
                StreamReader sr = new StreamReader(filePath, Encoding.UTF8);
                string str = sr.ReadToEnd();
                sr.Close();
                text_reader_box.Text = str;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
