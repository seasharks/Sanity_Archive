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

        public AttributesDialog()
        {
            InitializeComponent();
        }

        public AttributesDialog(string filePath)
        {
            InitializeComponent();
            _filePath = filePath;

            FileInfo finfo = new FileInfo(_filePath);
            attr_archive_checkbox.Checked = (File.GetAttributes(_filePath) & FileAttributes.Archive).ToString() == "Archive";
            attr_system_checkbox.Checked = (File.GetAttributes(_filePath) & FileAttributes.System).ToString() == "System";
            attr_readonly_checkbox.Checked = (File.GetAttributes(_filePath) & FileAttributes.ReadOnly).ToString() == "ReadOnly";
            attr_compressed_checkbox.Checked = (File.GetAttributes(_filePath) & FileAttributes.Compressed).ToString() == "Compressed";
            attr_encrypted_checkbox.Checked = (File.GetAttributes(_filePath) & FileAttributes.Encrypted).ToString() == "Encrypted";
        }
    }
}
