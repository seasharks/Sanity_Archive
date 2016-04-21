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
    public partial class AttributesDialog : Form
    {
        private string _filePath;
        private FileAttributes _attributes;

        public AttributesDialog()
        {
            InitializeComponent();
        }

        public AttributesDialog(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;

            _attributes = File.GetAttributes(_filePath);
            DateTime createdDate = File.GetCreationTime(_filePath);
            DateTime modifiedDate = File.GetLastWriteTime(_filePath);
            DateTime accessedDate = File.GetLastAccessTime(_filePath);

            attr_archive_checkbox.Checked = (_attributes & FileAttributes.Archive).ToString() == "Archive";
            attr_hidden_checkbox.Checked = (_attributes & FileAttributes.Hidden).ToString() == "Hidden";
            attr_system_checkbox.Checked = (_attributes & FileAttributes.System).ToString() == "System";
            attr_readonly_checkbox.Checked = (_attributes & FileAttributes.ReadOnly).ToString() == "ReadOnly";
            created_dateTimePicker.Value = createdDate;
            modified_dateTimePicker.Value = modifiedDate;
            accessed_dateTimePicker.Value = accessedDate;
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (attr_archive_checkbox.Checked && (_attributes & FileAttributes.Archive).ToString() != "Archive")
            {
                File.SetAttributes(_filePath, File.GetAttributes(_filePath) | FileAttributes.Archive);
            }
            else if (!attr_archive_checkbox.Checked && (_attributes & FileAttributes.Archive).ToString() == "Archive")
            {
                File.SetAttributes(_filePath, RemoveAttribute(File.GetAttributes(_filePath), FileAttributes.Archive));
            }

            if (attr_hidden_checkbox.Checked && (_attributes & FileAttributes.Hidden).ToString() != "Hidden")
            {
                File.SetAttributes(_filePath, File.GetAttributes(_filePath) | FileAttributes.Hidden);
            }
            else if (!attr_hidden_checkbox.Checked && (_attributes & FileAttributes.Hidden).ToString() == "Hidden")
            {
                File.SetAttributes(_filePath, RemoveAttribute(File.GetAttributes(_filePath), FileAttributes.Hidden));
            }

            if (attr_system_checkbox.Checked && (_attributes & FileAttributes.System).ToString() != "System")
            {
                File.SetAttributes(_filePath, File.GetAttributes(_filePath) | FileAttributes.System);
            }
            else if (!attr_system_checkbox.Checked && (_attributes & FileAttributes.System).ToString() == "System")
            {
                File.SetAttributes(_filePath, RemoveAttribute(File.GetAttributes(_filePath), FileAttributes.System));
            }
            
            if (attr_readonly_checkbox.Checked && (_attributes & FileAttributes.ReadOnly).ToString() != "ReadOnly")
            {
                File.SetAttributes(_filePath, File.GetAttributes(_filePath) | FileAttributes.ReadOnly);
            }
            else if (!attr_readonly_checkbox.Checked && (_attributes & FileAttributes.ReadOnly).ToString() == "ReadOnly")
            {
                File.SetAttributes(_filePath, RemoveAttribute(File.GetAttributes(_filePath), FileAttributes.ReadOnly));
            }
            File.SetCreationTime(_filePath, created_dateTimePicker.Value);
            File.SetLastWriteTime(_filePath, modified_dateTimePicker.Value);
            File.SetLastAccessTime(_filePath, accessed_dateTimePicker.Value);
            this.Close();
        }

        private void AttributesDialog_Load(object sender, EventArgs e)
        {

        }
    }
}
