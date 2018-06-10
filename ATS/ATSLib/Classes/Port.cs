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
        public event EventHandler <CallEventArgs> IncomingCallEvent;
        public event EventHandler<EventArgs> AnswerEvent;
        public event EventHandler<EventArgs> NoAnswerEvent;
        public event EventHandler<EventArgs> RejectEvent;
        public event EventHandler<EventArgs> EndCallEventOnPort;
        
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
                case Enums.Mode.Ringing : { return Enums.Mode.Ringing; }
                case Enums.Mode.Free:
                    {
                        return SetConnection(call);
                    }
                default: throw new Exception("Call error");
            }
        }
        public void  IncomingCall(int InPhoneNumber)
        {
            Terminal.ToAnswerCall += Answered;
            Terminal.ToRejectCall  += Rejected;
            Terminal.ToNoAnswerCall += NoAnswered;
            

            IncomingCallEvent(this,new CallEventArgs(Terminal.Number, InPhoneNumber));
        }
        public void Answered(object o, EventArgs e)
        {
            Terminal.ToAnswerCall -= Answered;
            Terminal.ToRejectCall -= Rejected;
            Terminal.ToNoAnswerCall -= NoAnswered;
            Mode = Enums.Mode.Busy;
            AnswerEvent(this, null);
            Terminal.EndCallEvent += EndCallOnPort;

        }
        public void Rejected(object o, EventArgs e)
        {
            Terminal.ToAnswerCall -= Answered;
            Terminal.ToRejectCall -= Rejected;
            Terminal.ToNoAnswerCall -= NoAnswered;
            Mode = Enums.Mode.Free ;
            RejectEvent (this, null);

        }
        public void NoAnswered(object o, EventArgs e)
        {
            Terminal.ToAnswerCall -= Answered;
            Terminal.ToRejectCall -= Rejected;
            Terminal.ToNoAnswerCall -= NoAnswered;
            Mode = Enums.Mode.Free;
            NoAnswerEvent(this, null);
        }
        public void OutCallAnswered(object o, CallEventArgs  e)
        {
            if (e.InPhoneNumber == PhoneNumber)
            {
                Mode = Enums.Mode.Busy;
                ATS.GetStation().TerminalAnswered -= OutCallAnswered;
                ATS.GetStation().TerminalNoAnswered -= OutCallNoAnswered;
                ATS.GetStation().TerminalRejected -= OutCallRejected;
                ATS.GetStation().EndCallEvent += EndCallOnPort;
                Console.WriteLine("Call are accepted");
                Terminal.EndCallEvent += EndCallOnPort;

            }
        }
        public void OutCallNoAnswered(object o, CallEventArgs e)
        {
            if (e.InPhoneNumber == PhoneNumber)
            {
                Mode = Enums.Mode.Free;
                ATS.GetStation().TerminalAnswered -= OutCallAnswered;
                ATS.GetStation().TerminalNoAnswered -= OutCallNoAnswered;
                ATS.GetStation().TerminalRejected -= OutCallRejected;
                Console.WriteLine("Subscriber does not answer");
            }
        }
        public  void OutCallRejected(object o, CallEventArgs e)
        {
            if (e.InPhoneNumber == PhoneNumber)
            {
                Mode = Enums.Mode.Free;
                ATS.GetStation().TerminalAnswered -= OutCallAnswered;
                ATS.GetStation().TerminalNoAnswered -= OutCallNoAnswered;
                ATS.GetStation().TerminalRejected -= OutCallRejected;
                Console.WriteLine("Call rejected");

            }
        }
        private void EndCallOnPort(object o,EventArgs e)
        {
            Mode = Enums.Mode.Free;
            Terminal.EndCallEvent -= EndCallOnPort;
            EndCallEventOnPort(this, null);
        }
        private void EndCallOnPort(object o, CallInfo e)
        {
            Mode = Enums.Mode.Free;
            Terminal.EndCallEvent -= EndCallOnPort;
        }
    }
}
