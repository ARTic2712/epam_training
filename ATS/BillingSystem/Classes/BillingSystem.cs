using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BillingSystem.Interfaces;
namespace BillingSystem.Classes
{
    public class BillingSystem
    {
        private static BillingSystem _bs;
        public ICollection<IAccount> Accounts { get; }
        private BillingSystem()
        {
            Accounts = new List<IAccount>();
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
    }
}
