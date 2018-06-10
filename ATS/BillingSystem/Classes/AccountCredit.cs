using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSLib.Classes;
using BillingSystem.Interfaces;
namespace BillingSystem.Classes
{
    public class AccountCredit : IAccount, ITariff
    {
        private ICollection<CallInfo> calls;
        public string FirstName { get; }

        public string SecondName { get; }

        public DateTime DateSignTariff { get; private set; }

        public Terminal Terminal { get; }

        public double Balance { get; private set; }

        public int FreeMinutes { get; private set; }

        public double PricePerSecond { get; private set; }

        public AccountCredit(Terminal terminal, string firstName, string secondName)
        {
            Terminal = terminal;
            FirstName = firstName;
            SecondName = secondName;
            DateSignTariff = DateTime.Now;
            calls = new List<CallInfo>();
        }
        public void SetTariff(double pricePerSecond, int freeMinutes)
        {
            PricePerSecond = pricePerSecond;
            FreeMinutes = freeMinutes;
        }
        public double CalculateCost(CallInfo call)
        {
           if (call.CallDuration>0)
            {
                if (FreeMinutes - call.CallDuration >= 0)
                {
                    FreeMinutes -= call.CallDuration;
                    return 0;
                }
                else
                {
                    int minutesForPay = call.CallDuration - FreeMinutes;
                    FreeMinutes = 0;
                    return minutesForPay * PricePerSecond;
                }
            }
            return 0;
        }
        public void EndCall(CallInfo call)
        {
            Balance -= CalculateCost(call);
            calls.Add(call);
        }
        public void PayToDeposit(double sum)
        {
            throw new NotImplementedException();
        }
    }
}
