using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSLib.Classes
{
    public class CallEventArgs
    {
        public int OutPhoneNumber { get; set; }
        public int InPhoneNumber { get; set; }

        public CallEventArgs(int outNumber, int inNumber)
        {
            OutPhoneNumber = outNumber;
            InPhoneNumber = inNumber;

        }
    }
}
