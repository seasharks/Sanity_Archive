#region File Header
/*[ Compilation unit ----------------------------------------------------------
      ​
         Component       : SanityArchive.cs
      ​
         Name            : sea-sharks
      ​
         Last Author     : Hunor Csaszar
      ​
         Language        : C#
      ​
         Creation Date   :  21.04.2016
      ​
         Description     : copy and move files
      ​
      ​
                     Copyright (C) Codecool Kft 2015-2016 All Rights Reserved
      ​
      -----------------------------------------------------------------------------*/
/*] END */
#endregion File Header
#region Used Namespaces ---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;
#endregion

namespace Sanity_Archive
{
    public partial class SanityArchive : Form
    {
        string currentPath;
        private string key = null;
        List<string> filePathsInClipBoard = new List<string>();
        private string path;
        private bool dataMovingInPogress = false;

        public SanityArchive()
        {
            InitializeComponent();

        }

#region Encrypt

        private void encryption_bttn_Click(object sender, EventArgs e)
        {
            // path = currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);

            if (!File.Exists("encryption.key")) key = GenerateKey();
            else
            {
                FileStream fsInput = new FileStream("encryption.key", FileMode.Open, FileAccess.Read);
                key = new StreamReader(fsInput).ReadToEnd();
                Console.WriteLine(key);
            }

            if (path.EndsWith(".enc"))
            {
                string pathOriginal = path;
                string decryptedFileName = path.Remove(path.Length - 4);
                DecryptFile(path, decryptedFileName, key);
                File.Delete(pathOriginal);
                FillFileFolderBox(currentPath);
            }
            else
            { 
                EncryptFile(path, path + ".enc", key);
                File.Delete(path);
                FillFileFolderBox(currentPath);
            }
        }

        string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider) DESCryptoServiceProvider.Create();
            string key = ASCIIEncoding.ASCII.GetString(desCrypto.Key);
            StreamWriter keyWriter = new StreamWriter("encryption.key");
            keyWriter.Write(key);
            keyWriter.Flush();
            keyWriter.Close();

            // Use the Automatically generated key for Encryption. 
            return key;
        }

        static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            // Filestreamek nyitása
            FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);
            //decryption technology meghatározsa
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //key és vektor beállítása
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            //obtain an encrypting object 
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            //output filera cryptostream nyitása
            CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);
            // input fájl olvasása
            byte[] bytearrayinput = new byte[fsInput.Length - 1];
            fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
            //cryptelt irás
            cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
            cryptostream.Close();
            fsInput.Close();
            fsEncrypted.Close();
        }

        static void DecryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //A 64 bit key and IV is required for this provider.

            //Set secret key For DES algorithm.
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
            fsread.Close();
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

#endregion

#region Directory and File Browser
        
        private void SanityArchive_Load(object sender, EventArgs e)
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                drives_box.Items.Add(drive.Name + " " + drive.DriveType.ToString());
            }
            drives_box.SelectedIndex = 0;
        }

        private void drives_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillFileFolderBox(drives_box.Text.Substring(0, 3));
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
                OpenListBoxItem();
            }
        }

        private void fileFolder_box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            { 
                OpenListBoxItem();
            }
        }

        private void path_box_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    HandleFileOrFolder(path_box.Text);
                }
                catch (FileNotFoundException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        
        private void OpenListBoxItem()
        // collects double-clicked or entered file or folder path and call HandleFileOrFolder with it
        {
            try
            {
                string launchedItemPath;
                if (fileFolder_box.SelectedItem.ToString() == "..")
                {
                    string currentPathWithoutEndingSlash = currentPath.Remove(currentPath.Length - 1);
                    DirectoryInfo parentOfCurrentDir = Directory.GetParent(currentPathWithoutEndingSlash);
                    string parentPath = parentOfCurrentDir.ToString();
                    launchedItemPath = parentPath.EndsWith("\\") ? parentPath : parentPath + "\\";
                }
                else
                {
                    launchedItemPath = currentPath + fileFolder_box.SelectedItem.ToString();
                }

                HandleFileOrFolder(launchedItemPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HandleFileOrFolder(string path)
        // decides whether the parameter path is file or folder and calls corresponding methods to handle them
        {
            FileAttributes attr = File.GetAttributes(@path);
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
            {
                FillFileFolderBox(path);
            }
            else
            {
                try
                {
                    TextReader textReader = new TextReader(this, @path);
                    textReader.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }

            // fill the pathBox with current path
            path_box.Text = currentPath;
        }

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

#endregion

        private void compression_bttn_Click(object sender, EventArgs e)
        {

        }

        private void search_bttn_Click(object sender, EventArgs e)
        {
            Search s = new Search(currentPath);
            s.Show();
        }
        
        private void size_lbl_Click(object sender, EventArgs e)
        {

        }

#region Copy, Move and Paste
        
        private void copy_button_Click(object sender, EventArgs e)
        {
            PutSelectedItemsInClipboard();
            dataMovingInPogress = false;
        }

        private void move_button_Click(object sender, EventArgs e)
        {
            PutSelectedItemsInClipboard();
            dataMovingInPogress = true;
        }

        private void paste_button_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsFileDropList())
            {
                StringCollection pathsFromClipBoard = Clipboard.GetFileDropList();
                foreach (string sourcePath in pathsFromClipBoard)
                {
                    try
                    {
                        FileAttributes attr = File.GetAttributes(sourcePath);
                        if (attr.HasFlag(FileAttributes.Directory)) RelocateFolder(sourcePath, currentPath, dataMovingInPogress);
                        else RelocateFile(sourcePath, currentPath, dataMovingInPogress);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (Clipboard.ContainsText())
            {
                string sourcePath = Clipboard.GetText();
                try
                {
                    FileAttributes attr = File.GetAttributes(sourcePath);
                    if (attr.HasFlag(FileAttributes.Directory)) RelocateFolder(sourcePath, currentPath, dataMovingInPogress);
                    else RelocateFile(sourcePath, currentPath, dataMovingInPogress);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            FillFileFolderBox(currentPath);
            dataMovingInPogress = false;
        }

        private void PutSelectedItemsInClipboard()
        {
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                StringCollection pathsInClipBoard = new StringCollection();
                foreach (string path in filePathsInClipBoard)
                {
                    pathsInClipBoard.Add(path);
                }
                Clipboard.SetFileDropList(pathsInClipBoard);
            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                Clipboard.SetText(path);
            }
            else
            {
                MessageBox.Show("Nothing is selected", "Failure", MessageBoxButtons.OK);
            }
        }

        private void RelocateFile(string sourceFilePath, string destinationFolderPath, bool movingNotCopying)
        {
            FileInfo fileInClipBoard = new FileInfo(sourceFilePath);
            string destinationPath = destinationFolderPath + fileInClipBoard.Name;
            if (File.Exists(destinationPath))
            {
                string msg = String.Format("{0} already exists. Would you like to overwrite?", destinationPath);
                MessageBoxButtons bttns = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(msg, "File already exists", bttns);
                if (result == DialogResult.No) return;
                else File.Delete(destinationPath);
            }
            if (movingNotCopying) fileInClipBoard.MoveTo(destinationPath);
            else fileInClipBoard.CopyTo(destinationPath);
        }

        private void RelocateFolder(string sourceFolderPath, string destinationFolderPath, bool movingNotCopying)
        {
            DirectoryInfo folderInClipBoard = new DirectoryInfo(sourceFolderPath);
            string destinationPath = destinationFolderPath + folderInClipBoard.Name;
            if (Directory.Exists(destinationPath))
            {
                string msg = String.Format("{0} already exists. Would you like to overwrite?", destinationPath);
                MessageBoxButtons bttns = MessageBoxButtons.YesNo;
                DialogResult result = MessageBox.Show(msg, "Folder already exists", bttns);
                if (result == DialogResult.No) return;
                else Directory.Delete(destinationPath, true);
            }
            Directory.CreateDirectory(destinationPath);

            DirectoryInfo[] folders = folderInClipBoard.GetDirectories();
            FileInfo[] files = folderInClipBoard.GetFiles();

            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destinationPath, file.Name);
                file.CopyTo(temppath, false);
            }

            foreach (DirectoryInfo subFolder in folders)
            {
                destinationPath += "\\";
                RelocateFolder(subFolder.FullName, destinationPath, movingNotCopying);
            }

            if (movingNotCopying) folderInClipBoard.Delete(true);
        }

        #endregion

        #region Attributes handler
        private void attributes_bttn_Click(object sender, EventArgs e)
        {
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                AttributesDialog attrDialog = new AttributesDialog(filePathsInClipBoard);
                attrDialog.ShowDialog();
            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                string path = currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);

                AttributesDialog attrDialog = new AttributesDialog(path);
                attrDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("There is nothing to be selected");
            }
        }
        #endregion

        private void fileFolder_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                filePathsInClipBoard.Clear();
                foreach (object item in fileFolder_box.SelectedItems)
                {
                    string fileName = item.ToString();
                    filePathsInClipBoard.Add(currentPath + fileName);
                }

            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                path = currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);

                if (fileFolder_box.SelectedItem.ToString() == "..")
                {
                    size_lbl.Text = "0,00 KB";
                    return;
                }
            }

            CalculateSize();

        }

#region Calculate Size
        public void CalculateSize()
        {
            size_lbl.Text = "Loading size. . .";
            long size = 0;
            //FOR ONE SELECTED ITEM
            if (fileFolder_box.SelectedItems.Count == 1)
            {
                FileAttributes attr = File.GetAttributes(path);
                //CHECK IF DIRECTORY
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        try
                        {
                            if (!path.EndsWith(".."))
                            {
                                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                                double dirSize = DirSize(directoryInfo)/1024.0;
                                RefreshSizeLabel(dirSize);
                            }
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("You don't have permission to access some elements in that directory!");
                            size_lbl.Text = "Denied";
                        }
                    }).Start();
                }
                else
                {
                    if (!path.EndsWith(".."))
                    {
                        double fileSize = new FileInfo(path).Length/1024.0;
                        RefreshSizeLabel(fileSize);
                    }
                }
            }
            //FOR MULTIPLE SELECTION
            else
            {
                new Thread(() =>
                {
                    //Thread.CurrentThread.IsBackground = true;
                    foreach (string filePath in filePathsInClipBoard)
                    {
                        FileAttributes attr = File.GetAttributes(path);
                        //CHECK IF DIRECTORY
                        if (!path.EndsWith(".."))
                        {
                            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                            {
                                    try
                                    {
                                            DirectoryInfo directoryInfo = new DirectoryInfo(filePath);
                                            size += DirSize(directoryInfo);
                                    }
                                    catch (Exception)
                                    {
                                    MessageBox.Show("You don't have permission to access some elements in that directory!");
                                    size_lbl.Text = "Denied";
                                    }  
                            }
                            else
                            {
                                size += new FileInfo(filePath).Length;
                            }
                        }

                    }
                    double sizeOfFiles = size / 1024.0;
                    RefreshSizeLabel(sizeOfFiles); 
                }).Start();
            }
            
        }
        private long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            try
            {
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }
                // Add subdirectory sizes.
                DirectoryInfo[] dis = d.GetDirectories();
                foreach (DirectoryInfo di in dis)
                {
                    size += DirSize(di);
                }
            }
            catch { size_lbl.Text = "Loading size. . .  something went wrong"; }
            return size;
        }
        private void RefreshSizeLabel(double size)
        {
            if (size < 1024) size_lbl.Text = $"{size:F2} KB";
            else if (size < 1048576) size_lbl.Text = $"{size / 1024:F2} MB";
            else size_lbl.Text = $"{size / 1048576:F2} GB";
        }
#endregion
    }
}
