using System;

namespace ATSLib.Classes
{
    public class CallInfo
    {
        public  int CallNumber { get;}
        public int ReceivingNumber { get;}
        public DateTime BeginCall { get;}
        public DateTime EndCall { get; set; }
        public double CallPrice { get; set; }
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
