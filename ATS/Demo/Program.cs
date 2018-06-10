using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATSLib.Classes;
using BillingSystem.Classes;
namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            ATS.GetStation().EndCallEvent += BillingSystem.Classes.BillingSystem.Get().ATSEndCall;
            Terminal t = new Terminal("Xiaomi redmi note 4", 1111111);
            Terminal t2 = new Terminal("Xiaomi redmi 5", 2442425);
            Terminal t3 = new Terminal("Xiaomi redmi 3", 233);
            Terminal t4 = new Terminal("Samsung S4", 787878);

            AccountCredit ac1 = new AccountCredit(t, "Вася", "Петров");
            AccountCredit ac2 = new AccountCredit(t2, "Аня", "Васечкина");
            AccountCredit ac3 = new AccountCredit(t3, "Павел", "Анютин");
            AccountCredit ac4 = new AccountCredit(t4, "Александр", "Павловский");
            ac1.SetTariff(2.6, 1);
            BillingSystem.Classes.BillingSystem.Get().Accounts.Add(ac1);
            BillingSystem.Classes.BillingSystem.Get().Accounts.Add(ac2);
            BillingSystem.Classes.BillingSystem.Get().Accounts.Add(ac3);
            BillingSystem.Classes.BillingSystem.Get().Accounts.Add(ac4);


            ATS.GetStation().AddPort(new Port(t));
            ATS.GetStation().AddPort(new Port(t2));
            ATS.GetStation().AddPort(new Port(t3));
            ATS.GetStation().AddPort(new Port(t4));


            ac1.Terminal.TurnOn();
            ac1.Terminal.Call(2442425);
            ac2.Terminal.TurnOn();
            ac1.Terminal.Call(2442425);
            System.Threading.Thread.Sleep(1000);
            ac3.Terminal .TurnOn();
            ac3.Terminal.Call(2442425);
            ac3.Terminal.Call(1111111);
            System.Threading.Thread.Sleep(3000);
            ac2.Terminal.EndCall();
            ac4.Terminal .TurnOn();
            ac3.Terminal.Call(787878);
            System.Threading.Thread.Sleep(2000);
            ac3.Terminal.EndCall();
            System.Threading.Thread.Sleep(1000000);
        }
    }
}
