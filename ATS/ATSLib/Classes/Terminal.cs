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
        public event EventHandler<EventArgs> ActivateEvent;
        public event EventHandler<EventArgs> DeactivateEvent;

        public event Func<CallEventArgs, Enums.Mode> CallEvent;
        private Boolean IsActive { get; set; }
        public Terminal(string name,int number)
        {
            Name = name;
            Number = number;
            IsActive = false;
        }
        public void TurnOn()
        {
            if(!IsActive )
            {
                IsActive = true;
                ActivateEvent(this, null);
            }
        }
        public void TurnOff()
        {
            if (IsActive)
            {
                IsActive = false;
                DeactivateEvent(this, null);
            }
        }
        public void Call(int phoneNumber)
        {
            if(IsActive )
            {
               switch( CallEvent(new CallEventArgs(phoneNumber)))
                {
                    case Enums.Mode.Blocked: { Console.WriteLine("You are blocked!"); break; }
                    case Enums.Mode.Busy: { Console.WriteLine("Line is busy!"); break; }
                }

            }
        }
    }
}
