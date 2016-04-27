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

#region Used Namespaces -------------------------------------------------------------------------------------------------------------

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
//using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Threading;
using System.Collections.Specialized;

#endregion Used Namespaces ----------------------------------------------------------------------------------------------------------

namespace Sanity_Archive
{
    public partial class SanityArchive : Form
    {
        private string _currentPath;
        private string _key;
        public List<string> FilePathsInClipBoard { get; } = new List<string>();
        private string _path;
        private bool _dataMovingInPogress;

        public SanityArchive()
        {
            InitializeComponent();

        }

        private void search_bttn_Click(object sender, EventArgs e)
        {
            Search s = new Search(_currentPath);
            s.Show();
        }

        private void fileFolder_box_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                FilePathsInClipBoard.Clear();
                foreach (object item in fileFolder_box.SelectedItems)
                {
                    string fileName = item.ToString();
                    FilePathsInClipBoard.Add(_currentPath + fileName);
                }

            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                _path = _currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);

                if (fileFolder_box.SelectedItem.ToString() == "..")
                {
                    size_lbl.Text = "0,00 KB";
                    return;
                }
            }

            CalculateSize();

        }

        #region Encrypt/Decrypt -----------------------------------------------------------------------------------------------------

        private void encryption_bttn_Click(object sender, EventArgs e)
        {
            // _path = _currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);

                if (!File.Exists("encryption._key")) _key = GenerateKey();
                else
                {
                    FileStream fsInput = new FileStream("encryption._key", FileMode.Open, FileAccess.Read);
                    _key = new StreamReader(fsInput).ReadToEnd();
                    Console.WriteLine(_key);
                }

                if (_path.EndsWith(".enc"))
                {
                    string pathOriginal = _path;
                    string decryptedFileName = _path.Remove(_path.Length - 4);
                    DecryptFile(_path, decryptedFileName, _key);
                    File.Delete(pathOriginal);
                    FillFileFolderBox(_currentPath);
                }
                else
                { 
                    EncryptFile(_path, _path + ".enc", _key);
                   File.Delete(_path);
                    FillFileFolderBox(_currentPath);
                }
        }

        string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider) DESCryptoServiceProvider.Create();
            string key = ASCIIEncoding.ASCII.GetString(desCrypto.Key);
            StreamWriter keyWriter = new StreamWriter("encryption._key");
            keyWriter.Write(key);
            keyWriter.Flush();
            keyWriter.Close();

            // Use the Automatically generated _key for Encryption. 
            return key;
        }

        static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            // Filestreamek nyitása
            FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write);
            //decryption technology meghatározsa
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //_key és vektor beállítása
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
            //A 64 bit _key and IV is required for this provider.

            //Set secret _key For DES algorithm.
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

        #endregion Encrypt/Decrypt --------------------------------------------------------------------------------------------------

        #region Directory and File Browser ------------------------------------------------------------------------------------------

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
                path_box.Text = _currentPath;
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
        // collects double-clicked or entered file or folder _path and call HandleFileOrFolder with it
        {
            try
            {
                string launchedItemPath;
                if (fileFolder_box.SelectedItem.ToString() == "..")
                {
                    string currentPathWithoutEndingSlash = _currentPath.Remove(_currentPath.Length - 1);
                    DirectoryInfo parentOfCurrentDir = Directory.GetParent(currentPathWithoutEndingSlash);
                    string parentPath = parentOfCurrentDir.ToString();
                    launchedItemPath = parentPath.EndsWith("\\") ? parentPath : parentPath + "\\";
                }
                else
                {
                    launchedItemPath = _currentPath + fileFolder_box.SelectedItem.ToString();
                }

                HandleFileOrFolder(launchedItemPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void HandleFileOrFolder(string path)
        // decides whether the parameter _path is file or folder and calls corresponding methods to handle them
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

            // fill the pathBox with current _path
            path_box.Text = _currentPath;
        }

        private void FillFileFolderBox(string path)
        // Fill fileFolder_box with folder and file items found under the given _path
        {
            fileFolder_box.Items.Clear();
            DirectoryInfo selectedDirectory = new DirectoryInfo(path);
            _currentPath = selectedDirectory.ToString();

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

        #endregion Directory and File Browser ---------------------------------------------------------------------------------------

        #region Copy-Move-Paste -----------------------------------------------------------------------------------------------------
        
        private void copy_button_Click(object sender, EventArgs e)
        {
            PutSelectedItemsInClipboard();
            _dataMovingInPogress = false;
        }

        private void move_button_Click(object sender, EventArgs e)
        {
            PutSelectedItemsInClipboard();
            _dataMovingInPogress = true;
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
                        if (attr.HasFlag(FileAttributes.Directory)) RelocateFolder(sourcePath, _currentPath, _dataMovingInPogress);
                        else RelocateFile(sourcePath, _currentPath, _dataMovingInPogress);
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
                    if (attr.HasFlag(FileAttributes.Directory)) RelocateFolder(sourcePath, _currentPath, _dataMovingInPogress);
                    else RelocateFile(sourcePath, _currentPath, _dataMovingInPogress);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            FillFileFolderBox(_currentPath);
            _dataMovingInPogress = false;
        }

        private void PutSelectedItemsInClipboard()
        {
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                StringCollection pathsInClipBoard = new StringCollection();
                foreach (string path in FilePathsInClipBoard)
                {
                    pathsInClipBoard.Add(path);
                }
                Clipboard.SetFileDropList(pathsInClipBoard);
            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                Clipboard.SetText(_path);
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

        #endregion Copy-Move-Paste --------------------------------------------------------------------------------------------------

        #region Attributes handler --------------------------------------------------------------------------------------------------

        private void attributes_bttn_Click(object sender, EventArgs e)
        {
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                AttributesDialog attrDialog = new AttributesDialog(FilePathsInClipBoard);
                attrDialog.ShowDialog();
            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                string path = _currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);

                AttributesDialog attrDialog = new AttributesDialog(path);
                attrDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("There is nothing to be selected");
            }
        }
        #endregion Attributes handler -----------------------------------------------------------------------------------------------

        #region Calculate Size ------------------------------------------------------------------------------------------------------

        public void CalculateSize()
        {
            size_lbl.Text = "Loading size. . .";
            long size = 0;
            //FOR ONE SELECTED ITEM
            if (fileFolder_box.SelectedItems.Count == 1)
            {
                FileAttributes attr = File.GetAttributes(_path);
                //CHECK IF DIRECTORY
                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        try
                        {
                            if (!_path.EndsWith(".."))
                            {
                                DirectoryInfo directoryInfo = new DirectoryInfo(_path);
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
                    if (!_path.EndsWith(".."))
                    {
                        double fileSize = new FileInfo(_path).Length/1024.0;
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
                    foreach (string filePath in FilePathsInClipBoard)
                    {
                        FileAttributes attr = File.GetAttributes(_path);
                        //CHECK IF DIRECTORY
                        if (!_path.EndsWith(".."))
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

        # endregion Calculate Size --------------------------------------------------------------------------------------------------

        #region Compression/Decompression -------------------------------------------------------------------------------------------

        #region Compression ---------------------------------------------------------------------------------------------------------

        private void compression_bttn_Click(object sender, EventArgs e)
        {
            try
            {
                Compress(_path, FilePathsInClipBoard);

                if (fileFolder_box.SelectedItems.Count == 1)
                {
                    MessageBox.Show(@"Arhive file successfully created!");
                }
                else if (fileFolder_box.SelectedItems.Count > 1)
                {
                    MessageBox.Show(@"Arhive file successfully created from multiple files!");
                }
                FillFileFolderBox(_currentPath);
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show(@"You can not add directories to the archive!");
            }
        }

        private void Compress(string singlePath, List<string> pathsList)
        {
            string dirParentPath = String.Empty;
            string dirParent = String.Empty;
            int i = 0;

            //In case of single selecting. ------------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 1)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(singlePath);
                    //Just the name of the file without extension.

                dirParentPath = Directory.GetParent(singlePath).FullName;
                    //Absolute filepath of the files contained in the list.

                string zipFilePath = dirParentPath + @"\" + fileNameWithoutExtension + @".zip";
                    //Path and name of the destination zip file.

                while (File.Exists(zipFilePath))
                {
                    i += 1;
                    zipFilePath = dirParentPath + @"\" + fileNameWithoutExtension + i + @".zip";
                }

                string fileName = Path.GetFileName(singlePath); //Just the name of the file with extension.

                using (ZipArchive createZipFile = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
                {
                    createZipFile.CreateEntryFromFile(singlePath, fileName);
                }
            }

            //In case of multiple selecting. ----------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count > 1)
            {
                DirectoryInfo dirP = new DirectoryInfo(pathsList[0]);

                if (dirP.Parent != null)
                {
                    dirParent = dirP.Parent.Name; //Name of the list's parent directory.
                    dirParentPath = dirP.Parent.FullName; //Absolute filepath of the files contained in the list.
                }

                string zipFilePaths = dirParentPath + @"\" + dirParent + @".zip";
                    //Path and name of the destination zip file.

                while (File.Exists(zipFilePaths))
                {
                    i += 1;
                    zipFilePaths = dirParentPath + @"\" + dirParent + i + @".zip";
                }

                foreach (var fileToCompressPath in pathsList)
                {
                    DirectoryInfo pathInfo = new DirectoryInfo(fileToCompressPath); //Full paths and names of the files to be compressed.
                    string fileNames = pathInfo.Name; //Just the name of the file with extension.

                    using (ZipArchive createZipFile = ZipFile.Open(zipFilePaths, ZipArchiveMode.Update))
                    {
                        createZipFile.CreateEntryFromFile(fileToCompressPath, fileNames);
                    }
                }
            }

            //In case of selection is ZERO. -----------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"No items selected!");
            }
        }

        #endregion Compression ------------------------------------------------------------------------------------------------------

        #region Decompression/Extract -----------------------------------------------------------------------------------------------

        private void exctract_bttn_Click(object sender, EventArgs e)
        {
            try
            {
                Decompression(_path, FilePathsInClipBoard);

                if (fileFolder_box.SelectedItems.Count == 1)
                {
                    MessageBox.Show(@"File(s) has been extracted to separate folder.");
                }
                else if (fileFolder_box.SelectedItems.Count > 1)
                {
                    MessageBox.Show(@"File(s) has been extracted to separate folders.");
                }
                FillFileFolderBox(_currentPath);
            }
            catch (Exception)
            {
                MessageBox.Show(@"You extracted once these files. This operation is not supported.");
            }
        }

        private void Decompression(string singlePath, List<string> pathsList)
        {
            string zipExtractionPath = "";

            //In case of single selecting. ------------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 1)
            {
                if (Path.GetExtension(singlePath) == ".zip")
                {
                    using (ZipArchive archive = ZipFile.Open(singlePath, ZipArchiveMode.Update))
                    {
                        string parentPath = Directory.GetParent(singlePath).FullName;
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(singlePath);
                        zipExtractionPath = parentPath + @"\" + fileNameWithoutExtension;

                        archive.ExtractToDirectory(zipExtractionPath);
                    }
                }
                else
                {
                    MessageBox.Show(@"Please choose an arhive file to decompress!");
                }
            }

            //In case of multiple selecting. ----------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count > 1)
            {
                DirectoryInfo dirP = new DirectoryInfo(pathsList[0]);

                foreach (string zipPath in pathsList)
                {
                    if (Path.GetExtension(zipPath) == ".zip")
                    {
                        using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Update))
                        {
                            if (dirP.Parent != null)
                            {
                                string zipSourcePath = dirP.Parent.FullName; //Absolute filepath of the files contained in the list.
                                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(zipPath);
                                zipExtractionPath = zipSourcePath + @"\" + fileNameWithoutExtension;
                            }
                                archive.ExtractToDirectory(zipExtractionPath);
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please choose an arhive file to decompress!");
                    }
                }
            }

            //In case of selection is ZERO. -----------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"No items selected!");
            }
        }

        #endregion Decompression/Extract --------------------------------------------------------------------------------------------

        #endregion Compression/Decompression ----------------------------------------------------------------------------------------
    }


}
