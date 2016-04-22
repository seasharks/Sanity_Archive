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
        List<string> _filePaths = new List<string>();
        private FileAttributes _attributes;
        private List<FileAttributes> _attributesOfFiles = new List<FileAttributes>();
        private DateTime _createdDate;
        private DateTime _modifiedDate;
        private DateTime _accessedDate;

        public AttributesDialog()
        {
            InitializeComponent();
        }

        public AttributesDialog(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;

            _attributes = File.GetAttributes(_filePath);
            _createdDate = File.GetCreationTime(_filePath);
            _modifiedDate = File.GetLastWriteTime(_filePath);
            _accessedDate = File.GetLastAccessTime(_filePath);

            attr_archive_checkbox.Checked = (_attributes & FileAttributes.Archive).ToString() == "Archive";
            attr_hidden_checkbox.Checked = (_attributes & FileAttributes.Hidden).ToString() == "Hidden";
            attr_system_checkbox.Checked = (_attributes & FileAttributes.System).ToString() == "System";
            attr_readonly_checkbox.Checked = (_attributes & FileAttributes.ReadOnly).ToString() == "ReadOnly";
            created_dateTimePicker.Value = _createdDate;
            modified_dateTimePicker.Value = _modifiedDate;
            accessed_dateTimePicker.Value = _accessedDate;
        }

        public AttributesDialog(List<string> filePaths)
        {
            InitializeComponent();
            _filePaths = filePaths;

            warning_label.Visible = true;
            warning_text_label.Visible = true;

            for (int i = 0; i < _filePaths.Count; i++)
            {
                _attributesOfFiles.Add(File.GetAttributes(_filePaths[i]));
            }
            
            _createdDate = File.GetCreationTime(_filePaths[0]);
            _modifiedDate = File.GetLastWriteTime(_filePaths[0]);
            _accessedDate = File.GetLastAccessTime(_filePaths[0]);
            attr_archive_checkbox.Checked = (_attributesOfFiles[0] & FileAttributes.Archive).ToString() == "Archive";
            attr_hidden_checkbox.Checked = (_attributesOfFiles[0] & FileAttributes.Hidden).ToString() == "Hidden";
            attr_system_checkbox.Checked = (_attributesOfFiles[0] & FileAttributes.System).ToString() == "System";
            attr_readonly_checkbox.Checked = (_attributesOfFiles[0] & FileAttributes.ReadOnly).ToString() == "ReadOnly";

            for (int i = 1; i <filePaths.Count; i++)
            {
                if (_createdDate != File.GetCreationTime(_filePaths[i]))
                {
                    created_dateTimePicker.CustomFormat = " ";
                    _createdDate = DateTime.Now;
                }
                if (_modifiedDate != File.GetLastWriteTime(_filePaths[i]))
                {
                    modified_dateTimePicker.CustomFormat = " ";
                    _modifiedDate = DateTime.Now;
                }
                if (_accessedDate != File.GetLastAccessTime(_filePaths[i]))
                {
                    accessed_dateTimePicker.CustomFormat = " ";
                    _accessedDate = DateTime.Now;
                }

                if (attr_archive_checkbox.Checked != ((_attributesOfFiles[i] & FileAttributes.Archive).ToString() == "Archive"))
                {
                    attr_archive_checkbox.ThreeState = true;
                    attr_archive_checkbox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
                }
                if (attr_hidden_checkbox.Checked != ((_attributesOfFiles[i] & FileAttributes.Hidden).ToString() == "Hidden"))
                {
                    attr_hidden_checkbox.ThreeState = true;
                    attr_hidden_checkbox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
                }
                if (attr_readonly_checkbox.Checked != ((_attributesOfFiles[i] & FileAttributes.ReadOnly).ToString() == "ReadOnly"))
                {
                    attr_readonly_checkbox.ThreeState = true;
                    attr_readonly_checkbox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
                }
                if (attr_system_checkbox.Checked != ((_attributesOfFiles[i] & FileAttributes.System).ToString() == "System"))
                {
                    attr_system_checkbox.ThreeState = true;
                    attr_system_checkbox.CheckState = System.Windows.Forms.CheckState.Indeterminate;
                }
            }
            created_dateTimePicker.Value = _createdDate;
            modified_dateTimePicker.Value = _modifiedDate;
            accessed_dateTimePicker.Value = _accessedDate;
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            try
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
            }
            catch (UnauthorizedAccessException)
            {
                // It raises when user checks "ReadOnly" attribute
            }
            finally
            {
                this.Close();
            }
        }
    }
}
