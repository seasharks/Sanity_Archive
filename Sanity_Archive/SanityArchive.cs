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

        private void FillFileFolderBox(string path)
        // Fill fileFolder_box with folder and file items found under the given path
        {
            fileFolder_box.Items.Clear();
            DirectoryInfo selectedDirectory = new DirectoryInfo(path);
            currentPath = selectedDirectory.ToString();

            DirectoryInfo[] containedDirs = selectedDirectory.GetDirectories();
            if (path.Length > 3)
            {
                fileFolder_box.Items.Add("..");
            }
            FileInfo[] containedFiles = selectedDirectory.GetFiles();

            fileFolder_box.Items.AddRange(containedDirs);
            fileFolder_box.Items.AddRange(containedFiles);
        }

        private void SanityArchive_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            drives_box.Items.AddRange(drives);
        }

        private void drives_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillFileFolderBox(drives_box.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void fileFolder_box_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = fileFolder_box.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                string clickedItemPath = currentPath + "/" + fileFolder_box.SelectedItem.ToString();
                FileAttributes attr = File.GetAttributes(@clickedItemPath);
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    try
                    {
                        FillFileFolderBox(clickedItemPath);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    // check if text file and open it in new window
                }
            }
        }

        #endregion

    }
}
