using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSLib.Classes
{
    public class Terminal
    {
        public int Number { get; set; }
        public string Name { get; }
        public Port Port { get; set; }
        public event EventHandler<EventArgs> ActivateEvent;
        public event EventHandler<EventArgs> DeactivateEvent;
        public event EventHandler<EventArgs> ToAnswerCall;
        public event EventHandler<EventArgs> ToNoAnswerCall;
        public event EventHandler<EventArgs> ToRejectCall;
        public event EventHandler<EventArgs> EndCallEvent;

        private Boolean TimeRingEnd = false; 

        private System.Timers.Timer timer = new System.Timers.Timer(10000);
        public event Func<CallEventArgs, Enums.Mode> CallEvent;
        private System.Threading.Thread _ring;
        private Boolean IsActive { get; set; }
        public Terminal(string name,int number)
        {
            Name = name;
            Number = number;
            IsActive = false;
            timer.Elapsed += TimerEnd;
        }
        public void TurnOn()
        {
            if(!IsActive )
            {
                IsActive = true;
                ActivateEvent(this, null);
                if (Port != null) Port.IncomingCallEvent += IncomingCall;
            }
        }
        public void TurnOff()
        {
            if (IsActive)
            {
                IsActive = false;
                DeactivateEvent(this, null);
                if (Port != null) Port.IncomingCallEvent -= IncomingCall;

            }
        }
        public void Call(int phoneNumber)
        {
            if(IsActive )
            {
               switch( CallEvent(new CallEventArgs(phoneNumber, Number )))
                {
                    case Enums.Mode.Blocked: { Console.WriteLine("You are blocked!"); break; }
                    case Enums.Mode.Busy: { Console.WriteLine("Line is busy!"); break; }
                    case Enums.Mode.NotExist: { Console.WriteLine("This number does not exist."); break; }
                    case Enums.Mode.Ringing : { Console.WriteLine("You are calling now."); break; }

                }

            }
        }
        public void IncomingCall(object o,CallEventArgs call)
        {
            _ring = new System.Threading.Thread(()=> { Ringing(call.InPhoneNumber ); });
             _ring.Start();
        }
        private void Ringing(int  number)
        {
            Console.WriteLine(String.Format("Incoming call {0}, to answer press 1, to reject press 2", number));
            timer.Start();
            while (true)
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case '1': { if (!TimeRingEnd) ToAnswerCall(this, null); timer.Stop(); return; }
                    case '2': { if (!TimeRingEnd) ToRejectCall(this, null);timer.Stop(); return; }
                    default: { if (!TimeRingEnd) Console.WriteLine("You entered an invalid character. Please repeat."); break; }
                }
            }
        }

        private void TimerEnd(object o,EventArgs e)
        {
            TimeRingEnd = true;
            ToNoAnswerCall(this, null);
            timer.Stop();
        }
        public void EndCall()
        {
            if (ATS.GetStation().Ports.FirstOrDefault(x=>x.PhoneNumber==Number).Mode== Enums.Mode.Busy )
            {
                 EndCallEvent(this, null);
            }
        }

    }
}
