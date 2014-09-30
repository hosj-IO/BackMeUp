using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BackMeUp
{
    public partial class FormMain : Form
    {
        private List<string> folderList;
        private string destination;

        public FormMain()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            labelStatus.Text = "";
            buttonStop.Enabled = false;

            folderList = new List<string>();
            LoadConfiguration();
            LoadData();
        }

        private void LoadData()
        {
            //throw new NotImplementedException();
        }

        private void LoadConfiguration()
        {
            //throw new NotImplementedException();
        }

        private void buttonSourceSelect_Click(object sender, EventArgs e)
        {
            using (var formSelectSource = new FormSelectSource())
            {
                if (formSelectSource.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    //get variable
                    LoadData();
                    UpdateGUI();
                }
            }
        }

        private void buttonBackupSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                destination = folderBrowserDialog.SelectedPath;
                SaveData();
                UpdateGUI();
            }
        }

        private void UpdateGUI()
        {
            //throw new NotImplementedException();
        }

        private void SaveData()
        {
            //throw new NotImplementedException();
        }
    }
}
