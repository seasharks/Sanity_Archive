using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Security.Cryptography;
using System.IO;
using System.Runtime.InteropServices;


namespace Sanity_Archive
{
    public partial class SanityArchive : Form
    {
        string currentPath;
        private string key;

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

                //string path = @"C:\Workspace\new\text.txt";
                FileAttributes attributes = File.GetAttributes(path);
                key = GenerateKey();

                if (path.EndsWith(".enc"))
                {
                    // Decrypt the file.
                    File.SetAttributes(path, File.GetAttributes(path) | FileAttributes.Encrypted);
                    Console.WriteLine("The {0} file is decrypted.", path);
                    DecryptFile(path, @"C:\Workspace\new\decrypted.txt", key);

                }
                else
                {
                    // Encrypt the file.    
                    attributes = RemoveAttribute(attributes, FileAttributes.Encrypted);
                    File.SetAttributes(path, attributes);
                    Console.WriteLine("The {0} file is encrypted.", path);
                    EncryptFile(path, @"C:\Workspace\new\text.txt.enc", key);
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
            //Set initialization vector.
            DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            //Create a file stream to read the encrypted file back.
            FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
            //Create a DES decryptor from the DES instance.
            ICryptoTransform desdecrypt = DES.CreateDecryptor();
            //Create crypto stream set to read and do a 
            //DES decryption transform on incoming bytes.
            CryptoStream cryptostreamDecr = new CryptoStream(fsread,
                                                         desdecrypt,
                                                         CryptoStreamMode.Read);
            //Print the contents of the decrypted file.
            StreamWriter fsDecrypted = new StreamWriter(sOutputFilename);
            fsDecrypted.Write(new StreamReader(cryptostreamDecr).ReadToEnd());
            fsDecrypted.Flush();
            fsDecrypted.Close();
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        #endregion


    }
}
