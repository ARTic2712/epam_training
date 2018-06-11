using System;

namespace BillingSystem.Interfaces
{
    public interface IAccount
    {
        string FirstName { get; }
        string SecondName { get; }
        DateTime DateSignTariff { get; }
        ATSLib.Classes.Terminal Terminal { get; }
        double Balance { get; }
        void PayToDeposit(double sum);
        void EndCall(ATSLib.Classes.CallInfo  call);
        void GetInfo(Func<ATSLib.Classes.CallInfo, bool> predicat);
    }
}
