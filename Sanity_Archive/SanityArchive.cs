#region File Header
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
using System.Runtime.InteropServices;
using System.Threading;


namespace Sanity_Archive
{
    public partial class SanityArchive : Form
    {
        string currentPath;
        private string key=null;

        public SanityArchive()
        {
            InitializeComponent();
        }

        private void attributes_bttn_Click(object sender, EventArgs e)
        {
           
        }

#region Encrypt

        private void encryption_bttn_Click(object sender, EventArgs e)
        {
            string path;
            if (fileFolder_box.SelectedItems.Count > 1)
            {
                MessageBox.Show("There are more than one element to be selected");
            }
            else if (fileFolder_box.SelectedItems.Count == 1)
            {
                path = currentPath + fileFolder_box.GetItemText(fileFolder_box.SelectedItem);
 
                if(key==null)key = GenerateKey();

                if (path.EndsWith(".enc"))
                {
                    string pathOriginal = path;
                    string decryptedFileName = path.Remove(path.Length-4);
                    DecryptFile(path, decryptedFileName, key);
                    File.Delete(pathOriginal);
                    FillFileFolderBox(currentPath);
                }
                else
                { 
                   EncryptFile(path, path+".enc", key);
                   File.Delete(path);
                    FillFileFolderBox(currentPath);
                }
            }

        }

        string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            //FileStream saveKey = new FileStream();

            // Use the Automatically generated key for Encryption. 
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            // Filestreamek nyitása
            FileStream fsInput = new FileStream(sInputFilename,FileMode.Open,FileAccess.Read);
            FileStream fsEncrypted = new FileStream(sOutputFilename,FileMode.Create,FileAccess.Write);
            //decryption technology meghatározsa
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            //key és vektor beállítása
            DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            //obtain an encrypting object 
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            //output filera cryptostream nyitása
            CryptoStream cryptostream = new CryptoStream(fsEncrypted,desencrypt,CryptoStreamMode.Write);
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
                // check if text file and open it in new window
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
            Search s = new Search();
            s.Show();
        }

        private void size_lbl_Click(object sender, EventArgs e)
        {

        }

        #endregion


    }
}
