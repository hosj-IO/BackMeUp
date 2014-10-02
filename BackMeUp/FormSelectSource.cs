using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BackMeUp.Properties;

namespace BackMeUp
{
    public partial class FormSelectSource : Form
    {
        private readonly List<string> FolderList;

        public FormSelectSource()
        {
            InitializeComponent();
            var backupConfiguration = Core.DeserializeConfig(typeof(BackupConfiguration), Attributes.FileName) as BackupConfiguration;
            FolderList = new List<string>();
            if (backupConfiguration != null) FolderList = backupConfiguration.SourceDirectories;
            foreach (string folder in FolderList)
            {
                listBoxOverview.Items.Add(folder);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                var selectedFolder = folderBrowserDialog.SelectedPath;
                FolderList.Add(selectedFolder);
                listBoxOverview.Items.Add(selectedFolder);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var item = listBoxOverview.SelectedItem;
            if (item != null)
            {
                listBoxOverview.Items.Remove(item);
                FolderList.Remove(item.ToString());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveToData();
            Close();
        }

        private void SaveToData()
        {
            var backupConfiguration =
                Core.DeserializeConfig(typeof(BackupConfiguration), Attributes.FileName) as BackupConfiguration;
            if (backupConfiguration != null)
            {
                backupConfiguration.SourceDirectories = FolderList;
                Core.SerializeConfig(backupConfiguration, typeof(BackupConfiguration), Attributes.FileName);
                DialogResult = DialogResult.OK;
            }
        }

        private void FormSelectSource_Load(object sender, EventArgs e)
        {
            Text = Resources.FormSelectSource_FormSelectSource_Load_BackMeUp____Sources;
            FormBorderStyle = FormBorderStyle.FixedSingle;
        }
    }
}
