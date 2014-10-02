using System;
using System.Windows.Forms;
using BackMeUp.Logging;
using BackMeUp.Properties;

namespace BackMeUp
{
    public partial class FormLogging : Form
    {
        readonly IController MessageController;
        private int MessageCount;

        public FormLogging(IController controller)
        {
            InitializeComponent();
            MessageController = controller;
        }

        private void FormLogging_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Text = Resources.FormLogging_FormLogging_Load_BackMeUp____Logging;
            MessageController.ControllerEvent += msgController_ControllerEvent;
        }

        void msgController_ControllerEvent(object sender, MessageEventArgs e)
        {
            MessageCount += 1;
            listBoxLog.Items.Add(MessageCount.ToString() + ": " + e.Message);
        }
    }
}
