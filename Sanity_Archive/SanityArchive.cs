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

        }

        #region Directory and File Browser

        private void SanityArchive_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            drives_box.Items.AddRange(drives);
        }

        private void drives_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                fileFolder_box.Items.Clear();
                DirectoryInfo selectedDirectory = new DirectoryInfo(drives_box.Text);

                DirectoryInfo[] containedDirs = selectedDirectory.GetDirectories();
                FileInfo[] containedFiles = selectedDirectory.GetFiles();

                fileFolder_box.Items.AddRange(containedDirs);
                fileFolder_box.Items.AddRange(containedFiles);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
