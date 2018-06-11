using System;
using System.Collections.Generic;
using System.Linq;
using ATSLib.Classes;
using BillingSystem.Interfaces;

namespace BillingSystem.Classes
{
    public class AccountCredit : IAccount, ITariff
    {
        private ICollection<CallInfo> Calls { get; }
        public event EventHandler<EventArgs> BlockForDebtEvent;
        public event EventHandler<EventArgs> UnBlockEvent;

        public string FirstName { get; }

        public string SecondName { get; }

        public DateTime DateSignTariff { get; private set; }

        public Terminal Terminal { get; }

        public double Balance { get; private set; }

        public int FreeMinutes { get; private set; }

        public double PricePerSecond { get; private set; }
        private double LastMonthDebt { get; set; }

        public AccountCredit(Terminal terminal, string firstName, string secondName)
        {
            Terminal = terminal;
            FirstName = firstName;
            SecondName = secondName;
            Calls = new List<CallInfo>();
            BillingSystem.Get().ChangeDateEvent += CheckBalance;
            BlockForDebtEvent += Terminal.TerminalBlock;
            UnBlockEvent += Terminal.TerminalUnBlock ;
        }
        public void SetTariff(double pricePerSecond, int freeMinutes)
        {
           if ((BillingSystem.Get().CurrentDate-DateSignTariff).Days>30)
            {
                PricePerSecond = pricePerSecond;
                FreeMinutes = freeMinutes;
                DateSignTariff = BillingSystem.Get().CurrentDate;
            }
           else
            {
                Console.WriteLine("You have already changed tarif this month!");
            }
            
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
            call.CallPrice = CalculateCost(call);
            Balance -= call.CallPrice;
            CallInfo c = new CallInfo(call.CallNumber, call.ReceivingNumber, call.BeginCall, call.CallingAnswerType);
            c.CallPrice = call.CallPrice;
            c.EndCall = call.EndCall;
            Calls.Add(c);
            
        }
        public void PayToDeposit(double sum)
        {
            Balance += sum;
            if (sum>LastMonthDebt)
            {
                LastMonthDebt = 0;
                UnBlockEvent?.Invoke(this, null);
            }
        }
        private void CheckBalance(object o, EventArgs e)
        {
            if (BillingSystem.Get().CurrentDate.Day==1)
            {
                DateTime LastMonth = BillingSystem.Get().CurrentDate.AddMonths(-1);
                LastMonthDebt = SumForPeriod(new DateTime(LastMonth.Year, LastMonth.Month, 1), new DateTime(LastMonth.Year, LastMonth.Month, BillingSystem.Get().CurrentDate.AddDays(-1).Day));
            }
            if (BillingSystem.Get().CurrentDate.Day == 25 && LastMonthDebt>0)
            {
                BlockForDebtEvent?.Invoke(this, e);
            }
        }
        private double SumForPeriod(DateTime DateBegin, DateTime DateEnd)
        {
            return Calls.Where(c => c.BeginCall >= DateBegin && c.BeginCall <= DateEnd).Sum((x) => x.CallPrice);
        }
        public void GetInfo(Func <CallInfo,bool> predicate )
        {
            IEnumerable<CallInfo> calls = Calls.Where(predicate);
            foreach(CallInfo call in calls )
            {
                Console.WriteLine("Call {0} - {1}; Duration: {2}; Price: {3}; Number: {4};",call.BeginCall.ToString("dd.MM.yyyy HH:mm:ss"), call.EndCall .ToString("dd.MM.yyyy HH:mm:ss"),call.CallDuration,call.CallPrice,call.ReceivingNumber );
            }
        }
    }
}
