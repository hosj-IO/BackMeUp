using System;

namespace BackMeUp.Logging
{
    public delegate void MessageDelegate(Object sender, MessageEventArgs e);

    public class MessageEventArgs : EventArgs
    {
        public string Message;
        public MessageEventArgs(string message)
        {
            Message = message;
        }
    }

    public interface IController
    {
        event MessageDelegate ControllerEvent;
        void InvokeControllerEvent(string message);
    }
}
