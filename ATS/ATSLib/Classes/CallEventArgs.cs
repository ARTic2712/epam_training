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
