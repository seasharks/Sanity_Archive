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
            if (new DirectoryInfo(path).Parent != null)
            {
                fileFolder_box.Items.Add("..");
            }

            foreach (DirectoryInfo dir in containedDirs)
            {
                if (dir.Attributes != FileAttributes.System
                && dir.Attributes != FileAttributes.Hidden
                && !dir.ToString().StartsWith("$"))
                    fileFolder_box.Items.Add(dir.ToString() + "\\");
            }

            FileInfo[] containedFiles = selectedDirectory.GetFiles();
            fileFolder_box.Items.AddRange(containedFiles);
        }

        private void SanityArchive_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            drives_box.Items.AddRange(drives);
            drives_box.SelectedIndex = 0;
        }

        private void drives_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillFileFolderBox(drives_box.Text);
                path_box.Text = currentPath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void fileFolder_box_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // checks if the doubleclicked area is surely an item of fileFolder_box
            // not sure how it works, found on stackoverflow
            int index = fileFolder_box.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                try
                {
                    // get the path of the doubleclicked folder or file
                    string clickedItemPath;
                    if (fileFolder_box.SelectedItem.ToString() == "..")
                    {
                        string currentPathWithoutEndingSlash = currentPath.Remove(currentPath.Length - 1);
                        DirectoryInfo parentOfCurrentDir = Directory.GetParent(currentPathWithoutEndingSlash);
                        string parentPath = parentOfCurrentDir.ToString();
                        clickedItemPath = parentPath.EndsWith("\\") ? parentPath : parentPath + "\\";
                    }
                    else 
                    { 
                        clickedItemPath = currentPath + fileFolder_box.SelectedItem.ToString();
                    }

                    // go to or open the doubleclicked folder or file
                    FileAttributes attr = File.GetAttributes(@clickedItemPath);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                    {
                        FillFileFolderBox(clickedItemPath);
                    }
                    else
                    {
                        // check if text file and open it in new window
                    }

                    // fill the pathBox with current path
                    path_box.Text = currentPath;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

#endregion

    }
}
