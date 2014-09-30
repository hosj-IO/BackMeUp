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
    public partial class FormSelectSource : Form
    {
        private List<string> folderList;

        public FormSelectSource()
        {
            InitializeComponent();
            folderList = new List<string>();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var selectedFolder = folderBrowserDialog.SelectedPath;
                folderList.Add(selectedFolder);
                listBoxOverview.Items.Add(selectedFolder);
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var item = listBoxOverview.SelectedItem;
            if (item != null)
            {
                listBoxOverview.Items.Remove(item);
                folderList.Remove(item.ToString());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
