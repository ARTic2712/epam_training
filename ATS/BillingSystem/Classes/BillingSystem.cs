using System;
using System.Collections.Generic;
using System.Linq;
using BillingSystem.Interfaces;
namespace BillingSystem.Classes
{
    public class BillingSystem
    {
        public EventHandler<EventArgs> ChangeDateEvent;
        private static BillingSystem _bs;
        public DateTime CurrentDate { get; private set; }
        public ICollection<IAccount> Accounts { get; }
        private BillingSystem()
        {
            Accounts = new List<IAccount>();
            CurrentDate = DateTime.Now ;
        }
        public static BillingSystem Get()
        {
            if (_bs == null) _bs = new BillingSystem();
            return _bs;
        }
        public void ATSEndCall(object o, ATSLib.Classes.CallInfo callInfo)
        {
            Accounts.FirstOrDefault(x => x.Terminal.Number == callInfo.CallNumber).EndCall(callInfo);
            Accounts.FirstOrDefault(x => x.Terminal.Number == callInfo.ReceivingNumber).EndCall(callInfo);
        }
        public void ChangeDate(DateTime currentDate)
        {
            CurrentDate = currentDate;
            ChangeDateEvent?.Invoke(this, new EventArgs());
        }
    }
}
