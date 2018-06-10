using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATSLib.Classes
{
    public class CallInfo
    {
        public  int CallNumber { get;}
        public int ReceivingNumber { get;}
        private DateTime BeginCall { get;}
        private DateTime EndCall { get; set; }
        public Enums.AnswerType CallingAnswerType { get; set; }
        public CallInfo(int callNumber, int receivingNumber, DateTime beginCall, Enums.AnswerType callingAnswerType)
        {
            CallNumber = callNumber;
            ReceivingNumber = receivingNumber;
            BeginCall = beginCall;
            CallingAnswerType = callingAnswerType;
        }
        public void FinishCall(DateTime endCall)
        {
            if (CallingAnswerType==Enums.AnswerType.Answered) EndCall = endCall;
        }
        public int CallDuration
        {
            get
            {
                if (EndCall != null) return (EndCall - BeginCall).Seconds ;
                return 0;
            }
        }
    }
}
