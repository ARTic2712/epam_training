using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSLib.Classes
{
    public class Port
    {
        private Terminal  Terminal  { get; set; }
        public Enums.Mode Mode { get; set; }
        public event Func<CallEventArgs, Enums.Mode> SetConnection;
        public event Func<CallEventArgs, Enums.Answer> IncomingCallEvent;
        public int PhoneNumber
        {
            get { return Terminal.Number; }
        }
        public Port(Terminal terminal)
        {
            Mode = Enums.Mode.Off;
            Terminal = terminal;
            Terminal.Port = this;
            Terminal.ActivateEvent  += OnTerminalActivate;
        }
        public void OnTerminalActivate(object o, EventArgs  e)
        {
            if (Mode == Enums.Mode.Off)
            {
                Mode = Enums.Mode.Free;
                Console.WriteLine(String.Format("Terminal {0} is activated", Terminal.Number ));
                Terminal.DeactivateEvent += OnTerminalDeactivate;
                Terminal.ActivateEvent -= OnTerminalActivate;
                Terminal.CallEvent += OnTerminalCall;
            }
        }
        public void OnTerminalDeactivate(object o, EventArgs e)
        {
            if (Mode != Enums.Mode.Off)
            {
                Mode = Enums.Mode.Off ;
                Console.WriteLine(String.Format("Terminal {0} is deactivated", Terminal.Number));
                Terminal.ActivateEvent += OnTerminalActivate;
                Terminal.CallEvent -= OnTerminalCall;
            }
        }
        public Enums.Mode OnTerminalCall(CallEventArgs call)
        {
            switch (Mode)
            {
                case Enums.Mode.Blocked : { return Enums.Mode.Blocked; }
                case Enums.Mode.Busy: { return Enums.Mode.Busy; }
                case Enums.Mode.Free: { return SetConnection(call); }
                default: throw new Exception("Call error");
            }
        }
        public Enums.Answer  IncomingCall(int InPhoneNumber)
        {
            return IncomingCallEvent(new CallEventArgs(Terminal.Number, InPhoneNumber));
        }
    }
}
