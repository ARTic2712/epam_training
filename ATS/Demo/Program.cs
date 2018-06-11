using System;
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
            ac1.SetTariff(3, 4);

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
            ac3.Terminal .TurnOn();
            //ac3.Terminal.Call(2442425);
            //ac3.Terminal.Call(1111111);
            System.Threading.Thread.Sleep(4000);
            ac2.Terminal.EndCall();
            ac1.Terminal.Call(233);
            System.Threading.Thread.Sleep(2000);
            ac1.Terminal.EndCall();
            BillingSystem.Classes.BillingSystem.Get().ChangeDate(new DateTime(2018, 7, 1));
            ac1.Terminal.Call(233);
            System.Threading.Thread.Sleep(6000);
            ac1.Terminal.EndCall();
            BillingSystem.Classes.BillingSystem.Get().ChangeDate(new DateTime(2018, 7, 25));
            ac1.Terminal.Call(233);
            //ac4.Terminal .TurnOn();
            //ac3.Terminal.Call(787878);
            //System.Threading.Thread.Sleep(2000);
            //ac3.Terminal.EndCall();
            ac1.PayToDeposit(8);
            ac1.Terminal.Call(233);
            ac1.PayToDeposit(20);
            ac1.Terminal.Call(233);
            ac1.GetInfo(x => true);
            Console.WriteLine("---------------------------------------------------------------");
            ac1.GetInfo(x=>x.CallPrice>3);
            Console.WriteLine("---------------------------------------------------------------");
            ac1.GetInfo(x => x.BeginCall >= new DateTime(2018, 6, 1) && x.BeginCall <= new DateTime(2018, 6, 30));
            Console.WriteLine("---------------------------------------------------------------");
            ac1.GetInfo(x =>x.ReceivingNumber ==233);
            Console.WriteLine("---------------------------------------------------------------");
            System.Threading.Thread.Sleep(1000000);
        }
    }
}
