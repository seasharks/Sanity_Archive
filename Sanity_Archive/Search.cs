﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Sanity_Archive
{
    public partial class Search : Form
    {
        private string _strSearchDirectory;
        public Search()
        {
            InitializeComponent();
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                _strSearchDirectory = fbd.SelectedPath;
                path_textbox.Text = _strSearchDirectory;
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {
            _strSearchDirectory = path_textbox.Text;
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            string strSearchString = filename_textbox.Text;
            search_result_box.Items.Clear();
            List<FileInfo> searchResult = FileSearch.Search(strSearchString, _strSearchDirectory);
            foreach (FileInfo curFile in searchResult)
            {
                search_result_box.Items.Add(curFile);

            }
            filename_textbox.Clear();
            filename_textbox.Focus();
        }
    }
}

