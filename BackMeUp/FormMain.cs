using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using BackMeUp.Logging;
using BackMeUp.Properties;

namespace BackMeUp
{
    public partial class FormMain : Form
    {
        private BackupConfiguration BackupConfiguration;
        private IController MessageController;
        //private int MessageCount;

        public FormMain()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            labelStatus.Text = string.Empty;
            buttonStop.Enabled = false;

            MessageController = new Controller();
            //MessageCount = 0;

            
            //MessageController.ControllerEvent += msgController_ControllerEvent;

            LoadConfiguration();
        }

        //void msgController_ControllerEvent(object sender, MessageEventArgs e)
        //{
        //    MessageCount += 1;
        //    Text = e.Message + Resources.single_space + MessageCount.ToString();
        //}

        private void LoadConfiguration()
        {
            BackupConfiguration = new BackupConfiguration();
            buttonStart.Enabled = false;

            BackupConfiguration = Core.DeserializeConfig(typeof(BackupConfiguration), Attributes.FileName) as BackupConfiguration;
            if (BackupConfiguration != null)
            {
                if (!string.IsNullOrWhiteSpace(BackupConfiguration.Destination) &&
                    BackupConfiguration.SourceDirectories.Count > 0)
                {
                    //Configuration is completed for the program to work.
                    buttonStart.Enabled = true;
                }
            }
            MessageController.InvokeControllerEvent("Configuration has been loaded.");
        }

        private void buttonSourceSelect_Click(object sender, EventArgs e)
        {
            using (var formSelectSource = new FormSelectSource())
            {
                formSelectSource.ShowDialog();
                if (formSelectSource.DialogResult == DialogResult.OK)
                {

                    //Check if the changed folders have not been emptied
                    LoadConfiguration();
                }
            }
        }

        private void buttonBackupSelect_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK
                    && !String.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    var backupConfiguration = Core.DeserializeConfig(typeof(BackupConfiguration), Attributes.FileName) as BackupConfiguration ??
                                              new BackupConfiguration();
                    backupConfiguration.Destination = folderBrowserDialog.SelectedPath;
                    Core.SerializeConfig(backupConfiguration, typeof(BackupConfiguration), Attributes.FileName);
                    LoadConfiguration();
                }
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            MessageController.InvokeControllerEvent("Backup process started.");
            UpdateButtons(true);
            StartProcess();
        }

        private void StartProcess()
        {
            Dictionary<string, string> temporaryIndex = IndexFiles(BackupConfiguration.SourceDirectories);
            CompareIndex(temporaryIndex, temporaryIndex);
            AddFileSystemWatchers();
        }

        private void AddFileSystemWatchers()
        {
            foreach (string sourceDirectory in BackupConfiguration.SourceDirectories)
            {
                var fileSystemWatcher = new FileSystemWatcher(sourceDirectory);

            }
        }

        private void CompareIndex(Dictionary<string, string> temporaryIndex, Dictionary<string, string> fileHashTable)
        {
            foreach (KeyValuePair<string, string> temporaryFileHashPair in temporaryIndex)
            {
                string value;

                //Check if hash exists in current configuration
                if (fileHashTable.TryGetValue(temporaryFileHashPair.Key, out value))
                {
                    if (!value.Equals(temporaryFileHashPair.Value))
                    {
                        UpdateFileHashTable();
                    }
                }
            }
        }

        private void UpdateFileHashTable()
        {

        }

        private Dictionary<string, string> IndexFiles(List<string> sourceDirectories)
        {
            return null;
        }

        private void UpdateButtons(bool isRunning)
        {
            buttonStart.Enabled = !isRunning;
            buttonStop.Enabled = isRunning;
            buttonSourceSelect.Enabled = !isRunning;
            buttonBackupSelect.Enabled = !isRunning;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            MessageController.InvokeControllerEvent("Backup process stopped.");
            UpdateButtons(false);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.FormMain_aboutToolStripMenuItem_Click_Application_made_by_Sjoerd_Houben_2014_);
        }

        private void loggingScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formLogging = new FormLogging(MessageController);
            formLogging.Show();
        }
    }
}
