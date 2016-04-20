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
    public partial class SanityArchive : Form
    {
        string currentPath;

        public SanityArchive()
        {
            InitializeComponent();
        }

        private void attributes_bttn_Click(object sender, EventArgs e)
        {
            if (fileFolder_box.Items.Count == 0)
            {
                currentPath = "C:/testfiles/";
                fileFolder_box.Items.Add("Week14A");
                fileFolder_box.Items.Add("81m.jpg");
                fileFolder_box.Items.Add("Morok.java");
                fileFolder_box.Items.Add("Morok2.java");
                return;
            }

            if (fileFolder_box.SelectedItems.Count > 1)
            {
                MessageBox.Show("There are more than one element to be selected");
            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                string path = currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);
                MessageBox.Show(((File.GetAttributes(path) & FileAttributes.ReadOnly).ToString() == "ReadOnly").ToString());
                      
                AttributesDialog attrDialog = new AttributesDialog();
                for (int i = 0; i < attrDialog.Controls.Count; i++)
                {
                    if (attrDialog.Controls[i].Name == "")
                    {

                    }
                }
                attrDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("There is nothing to be selected");
            }
        }
    }
}
