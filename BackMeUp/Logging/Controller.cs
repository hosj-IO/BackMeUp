namespace BackMeUp.Logging
{
    public class Controller : IController
    {
        public event MessageDelegate ControllerEvent;

        public void InvokeControllerEvent(string message)
        {
            OnControllerEvent(new MessageEventArgs(message));
        }

        protected virtual void OnControllerEvent(MessageEventArgs messageEventArgs)
        {
            if (ControllerEvent != null)
            {
                ControllerEvent.Invoke(this, messageEventArgs);
            }
        }

    }
}
