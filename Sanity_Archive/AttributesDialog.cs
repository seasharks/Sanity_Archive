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
                    attr_archive_checkbox.CheckState = CheckState.Indeterminate;
                }
                if (attr_hidden_checkbox.Checked != ((_attributesOfFiles[i] & FileAttributes.Hidden).ToString() == "Hidden"))
                {
                    attr_hidden_checkbox.ThreeState = true;
                    attr_hidden_checkbox.CheckState = CheckState.Indeterminate;
                }
                if (attr_readonly_checkbox.Checked != ((_attributesOfFiles[i] & FileAttributes.ReadOnly).ToString() == "ReadOnly"))
                {
                    attr_readonly_checkbox.ThreeState = true;
                    attr_readonly_checkbox.CheckState = CheckState.Indeterminate;
                }
                if (attr_system_checkbox.Checked != ((_attributesOfFiles[i] & FileAttributes.System).ToString() == "System"))
                {
                    attr_system_checkbox.ThreeState = true;
                    attr_system_checkbox.CheckState = CheckState.Indeterminate;
                }
            }
            created_dateTimePicker.Value = _createdDate;
            modified_dateTimePicker.Value = _modifiedDate;
            accessed_dateTimePicker.Value = _accessedDate;
        }

        private void AddGivenAttributeToAllFile(FileAttributes attribute)
        {
            for (int i = 0; i < _attributesOfFiles.Count; i++)
                File.SetAttributes(_filePaths[i], File.GetAttributes(_filePaths[i]) | attribute);
        }

        private void RemoveGivenAttributeFromAllFile(FileAttributes attribute)
        {
            for (int i = 0; i < _attributesOfFiles.Count; i++)
                File.SetAttributes(_filePaths[i], RemoveAttribute(File.GetAttributes(_filePaths[i]), attribute));
        }

        private static FileAttributes RemoveAttribute(FileAttributes attributes, FileAttributes attributesToRemove)
        {
            return attributes & ~attributesToRemove;
        }

        private void ok_button_Click(object sender, EventArgs e)
        {
            if (_attributesOfFiles.Count > 1)
            {
                ok_button_Click_MultipleFileSelected();
            }
            else
            {
                ok_button_Click_SingleFileSelected();
            }
        }

        private void ok_button_Click_SingleFileSelected()
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

                if ((_attributes & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    if (_createdDate != created_dateTimePicker.Value)
                        Directory.SetCreationTime(_filePath, created_dateTimePicker.Value);
                    if (_modifiedDate != modified_dateTimePicker.Value)
                        Directory.SetLastWriteTime(_filePath, modified_dateTimePicker.Value);
                    if (_accessedDate != accessed_dateTimePicker.Value)
                        Directory.SetLastAccessTime(_filePath, accessed_dateTimePicker.Value);
                }
                else
                {
                    bool isReadOnly = (File.GetAttributes(_filePath) & FileAttributes.ReadOnly).ToString() == "ReadOnly";
                    if (isReadOnly)
                        File.SetAttributes(_filePath, RemoveAttribute(File.GetAttributes(_filePath), FileAttributes.ReadOnly));

                    if (_createdDate != created_dateTimePicker.Value)
                        File.SetCreationTime(_filePath, created_dateTimePicker.Value);
                    if (_modifiedDate != modified_dateTimePicker.Value)
                        File.SetLastWriteTime(_filePath, modified_dateTimePicker.Value);
                    if (_accessedDate != accessed_dateTimePicker.Value)
                        File.SetLastAccessTime(_filePath, accessed_dateTimePicker.Value);

                    if (isReadOnly)
                        File.SetAttributes(_filePath, File.GetAttributes(_filePath) | FileAttributes.ReadOnly);
                }
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

        private void ok_button_Click_MultipleFileSelected()
        {
            try
            {
                if (attr_archive_checkbox.ThreeState)
                {
                    if (attr_archive_checkbox.CheckState == CheckState.Checked)
                        AddGivenAttributeToAllFile(FileAttributes.Archive);
                    else if (attr_archive_checkbox.CheckState == CheckState.Unchecked)
                        RemoveGivenAttributeFromAllFile(FileAttributes.Archive);
                }
                else
                {
                    if (attr_archive_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.Archive).ToString() != "Archive")
                        AddGivenAttributeToAllFile(FileAttributes.Archive);
                    else if (!attr_archive_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.Archive).ToString() == "Archive")
                        RemoveGivenAttributeFromAllFile(FileAttributes.Archive);
                }

                if (attr_hidden_checkbox.ThreeState)
                {
                    if (attr_hidden_checkbox.CheckState == CheckState.Checked)
                        AddGivenAttributeToAllFile(FileAttributes.Hidden);
                    else if (attr_hidden_checkbox.CheckState == CheckState.Unchecked)
                        RemoveGivenAttributeFromAllFile(FileAttributes.Hidden);
                }
                else
                {
                    if (attr_hidden_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.Hidden).ToString() != "Hidden")
                        AddGivenAttributeToAllFile(FileAttributes.Hidden);
                    else if (!attr_hidden_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.Hidden).ToString() == "Hidden")
                        RemoveGivenAttributeFromAllFile(FileAttributes.Hidden);
                }

                if (attr_readonly_checkbox.ThreeState)
                {
                    if (attr_readonly_checkbox.CheckState == CheckState.Checked)
                        AddGivenAttributeToAllFile(FileAttributes.ReadOnly);
                    else if (attr_readonly_checkbox.CheckState == CheckState.Unchecked)
                        RemoveGivenAttributeFromAllFile(FileAttributes.ReadOnly);
                }
                else
                {
                    if (attr_readonly_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.ReadOnly).ToString() != "ReadOnly")
                        AddGivenAttributeToAllFile(FileAttributes.ReadOnly);
                    else if (!attr_readonly_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.ReadOnly).ToString() == "ReadOnly")
                        RemoveGivenAttributeFromAllFile(FileAttributes.ReadOnly);
                }

                if (attr_system_checkbox.ThreeState)
                {
                    if (attr_system_checkbox.CheckState == CheckState.Checked)
                        AddGivenAttributeToAllFile(FileAttributes.System);
                    else if (attr_system_checkbox.CheckState == CheckState.Unchecked)
                        RemoveGivenAttributeFromAllFile(FileAttributes.System);
                }
                else
                {
                    if (attr_system_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.System).ToString() != "System")
                        AddGivenAttributeToAllFile(FileAttributes.System);
                    else if (!attr_system_checkbox.Checked && (_attributesOfFiles[0] & FileAttributes.System).ToString() == "System")
                        RemoveGivenAttributeFromAllFile(FileAttributes.System);
                }
                
                if (_createdDate != created_dateTimePicker.Value)
                {
                    for (int i = 0; i < _filePaths.Count; i++)
                    {
                        if ((_attributesOfFiles[i] & FileAttributes.Directory) == FileAttributes.Directory)
                            Directory.SetCreationTime(_filePaths[i], created_dateTimePicker.Value);
                        else
                        {
                            bool isReadOnly = (File.GetAttributes(_filePaths[i]) & FileAttributes.ReadOnly).ToString() == "ReadOnly";
                            if (isReadOnly)
                                File.SetAttributes(_filePaths[i], RemoveAttribute(File.GetAttributes(_filePaths[i]), FileAttributes.ReadOnly));
                            File.SetCreationTime(_filePaths[i], created_dateTimePicker.Value);
                            if (isReadOnly)
                                File.SetAttributes(_filePaths[i], File.GetAttributes(_filePaths[i]) | FileAttributes.ReadOnly);
                        }
                    }
                }
                if (_modifiedDate != modified_dateTimePicker.Value)
                {
                    for (int i = 0; i < _filePaths.Count; i++)
                    {
                        if ((_attributesOfFiles[i] & FileAttributes.Directory) == FileAttributes.Directory)
                            Directory.SetLastWriteTime(_filePaths[i], modified_dateTimePicker.Value);
                        else
                        {
                            bool isReadOnly = (File.GetAttributes(_filePaths[i]) & FileAttributes.ReadOnly).ToString() == "ReadOnly";
                            if (isReadOnly)
                                File.SetAttributes(_filePaths[i], RemoveAttribute(File.GetAttributes(_filePaths[i]), FileAttributes.ReadOnly));
                            File.SetLastWriteTime(_filePaths[i], modified_dateTimePicker.Value);
                            if (isReadOnly)
                                File.SetAttributes(_filePaths[i], File.GetAttributes(_filePaths[i]) | FileAttributes.ReadOnly);
                        }
                    }
                }
                if (_accessedDate != accessed_dateTimePicker.Value)
                {
                    for (int i = 0; i < _filePaths.Count; i++)
                    {
                        if ((_attributesOfFiles[i] & FileAttributes.Directory) == FileAttributes.Directory)
                            Directory.SetLastAccessTime(_filePaths[i], accessed_dateTimePicker.Value);
                        else
                        {
                            bool isReadOnly = (File.GetAttributes(_filePaths[i]) & FileAttributes.ReadOnly).ToString() == "ReadOnly";
                            if (isReadOnly)
                                File.SetAttributes(_filePaths[i], RemoveAttribute(File.GetAttributes(_filePaths[i]), FileAttributes.ReadOnly));
                            File.SetLastAccessTime(_filePaths[i], accessed_dateTimePicker.Value);
                            if (isReadOnly)
                                File.SetAttributes(_filePaths[i], File.GetAttributes(_filePaths[i]) | FileAttributes.ReadOnly);
                        }
                    }
                }
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

        private void created_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (created_dateTimePicker.CustomFormat == " ")
            {
                created_dateTimePicker.CustomFormat = "yyyy.MMMMdd.  HH:mm:ss";
            }
        }

        private void modified_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (modified_dateTimePicker.CustomFormat == " ")
            {
                modified_dateTimePicker.CustomFormat = "yyyy.MMMMdd.  HH:mm:ss";
            }
        }

        private void accessed_dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (accessed_dateTimePicker.CustomFormat == " ")
            {
                accessed_dateTimePicker.CustomFormat = "yyyy.MMMMdd.  HH:mm:ss";
            }
        }
    }
}
