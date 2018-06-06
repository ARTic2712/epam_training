using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSLib.Classes
{
    public class CallEventArgs
    {
        public int PhoneNumber { get; set; }
        public CallEventArgs(int number)
        {
            PhoneNumber = number;
        }
    }
}
