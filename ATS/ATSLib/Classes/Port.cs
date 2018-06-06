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
        public Enums.Mode Mode { get; private set; }
        public event Func<CallEventArgs, Enums.Mode> SetConnection;
        public Port(Terminal terminal)
        {
            Mode = Enums.Mode.Off;
            Terminal = terminal;
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
    }
}
