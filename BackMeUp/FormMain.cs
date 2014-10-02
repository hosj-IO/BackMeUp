using System;
using System.Windows.Forms;
using BackMeUp.Properties;

namespace BackMeUp
{
    public partial class FormMain : Form
    {
        private BackupConfiguration BackupConfiguration;

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

            LoadConfiguration();
        }

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
            UpdateButtons(true);
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
            UpdateButtons(false);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.FormMain_aboutToolStripMenuItem_Click_Application_made_by_Sjoerd_Houben_2014_);
        }
    }
}
