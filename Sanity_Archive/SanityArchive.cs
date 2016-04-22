﻿#region File Header
/*[ Compilation unit ----------------------------------------------------------
      ​
         Component       : SanityArchive.cs
      ​
         Name            : sea-sharks
      ​
         Last Author     : Andras Kesik
      ​
         Language        : C#
      ​
         Creation Date   :  20.04.2016
      ​
         Description     : file and folder browsing feature
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
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Threading;


namespace Sanity_Archive
{
    public partial class SanityArchive : Form
    {
        string currentPath;
        private string key = null;
        List<string> filePathsInClipBoard = new List<string>();
        private string path;

        public SanityArchive()
        {
            InitializeComponent();

        }

#region Encrypt

        private void encryption_bttn_Click(object sender, EventArgs e)
        {
            //string path;
            //if (fileFolder_box.SelectedItems.Count > 1)
            //{
            //    MessageBox.Show("There are more than one element to be selected");
            //}
            //else if (fileFolder_box.SelectedItems.Count == 1)
            //{
            //    path = currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);

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
            //}

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

        private void search_bttn_Click(object sender, EventArgs e)
        {
            Search s = new Search();
            s.Show();
        }

        private void copy_button_Click(object sender, EventArgs e)
        {

        }

        private void size_lbl_Click(object sender, EventArgs e)
        {

        }


        #endregion

        #region Attributes handler
        private void attributes_bttn_Click(object sender, EventArgs e)
        {
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                MessageBox.Show("There are more than one element to be selected");
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
            }
        }


        #region Compression/Extract -----------------------------------------------------------------------------------------------------------

        #region Compression ------------------------------------------------------------------------------------------------------------------

        private void compression_bttn_Click(object sender, EventArgs e)
        {
            Compress(path, filePathsInClipBoard);
        }

        private void Compress(string singlePath, List<string> pathsList)
        {
            string dirParentPath = "";
            string dirParent = "";
            int i = 0;

            //In case of single selecting. ----------------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 1)
            {
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(singlePath); //Just the name of the file without extension.

                dirParentPath = Directory.GetParent(singlePath).FullName; //Absolute filepath of the files contained in the list.

                string zipFilePath = dirParentPath + @"\" + fileNameWithoutExtension + @".zip"; //Path and name of the destination zip file.

                while (File.Exists(zipFilePath))
                {
                    i += 1;
                    zipFilePath = dirParentPath + @"\" + fileNameWithoutExtension + i + @".zip";
                }

                string fileName = Path.GetFileName(singlePath); //Just the name of the file with extension.

                using (ZipArchive createZipFile = ZipFile.Open(zipFilePath, ZipArchiveMode.Update))
                {
                    try
                    {
                        createZipFile.CreateEntryFromFile(singlePath, fileName);
                        MessageBox.Show(@"Arhive file successfully created!");
                    }
                    catch (DirectoryNotFoundException)
                    {
                        MessageBox.Show(@"You can not add directories to the archive!");
                    }
                }
            }

            //In case of multiple selecting. ---------------------------------------------------------------------------------------------------

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
                        try
                        {
                            createZipFile.CreateEntryFromFile(fileToCompressPath, fileNames);
                            MessageBox.Show(@"Arhive file successfully created from multiple files!");
                        }
                        catch (DirectoryNotFoundException)
                        {
                            MessageBox.Show(@"You can not add directories to the archive!");
                        }
                    }
                }
            }

            //In case of selection is ZERO. ---------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"No items selected!");
            }
        }

        #endregion Compression ---------------------------------------------------------------------------------------------------------------

        #region Decompression/Extract ------------------------------------------------------------------------------------------------------

        private void exctract_bttn_Click(object sender, EventArgs e)
        {
            Decompression(path, filePathsInClipBoard);
        }

        private void Decompression(string singlePath, List<string> pathsList)
        {
            string zipExtractionPath = "";

            //In case of single selecting. ----------------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 1)
            {
                if (Path.GetExtension(singlePath) == ".zip")
                {
                    using (ZipArchive archive = ZipFile.Open(singlePath, ZipArchiveMode.Update))
                    {
                        string parentPath = Directory.GetParent(singlePath).FullName;
                        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(singlePath);
                        zipExtractionPath = parentPath + @"\" + fileNameWithoutExtension;

                        try
                        {
                            archive.ExtractToDirectory(zipExtractionPath);
                            MessageBox.Show(@"Files has been extracted to separate folders.");
                        }
                        catch (Exception )
                        {
                            MessageBox.Show(@"You extracted once these files. This operation is not supported.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show(@"Please choose an arhive file to decompress!");
                }
            }

            //In case of multiple selecting. ---------------------------------------------------------------------------------------------------

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
                            try
                            {
                                archive.ExtractToDirectory(zipExtractionPath);
                                MessageBox.Show(@"Files has been extracted to separate folders.");
                            }
                            catch (Exception)
                            {
                                MessageBox.Show(@"You extracted once these files. This operation is not supported.");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Please choose an arhive file to decompress!");
                    }
                }
            }

            //In case of selection is ZERO. ---------------------------------------------------------------------------------------

            if (fileFolder_box.SelectedItems.Count == 0)
            {
                MessageBox.Show(@"No items selected!");
            }
        }

        #endregion Decompression/Extract ------------------------------------------------------------------------------------------------------

        #endregion Compression/Extract --------------------------------------------------------------------------------------------------------
    }
}
