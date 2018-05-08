using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLib.Classes;
using TaxiLib.Interfaces;
namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityBuilder builder = new UnityBuilder(
                new List< ICar >(),
                new List<ITaxi >());

            Unity unity = builder.Build();
            Console.WriteLine("Sum of car prices is more than 5000:" + Taxi.GetSumPrice(Taxi.Find(unity.TaxiCollection,"TaxiCity").Items,a=>a.Price > 5000));
            Console.WriteLine("Sum of car prices with fuel cost less than 8:" + Taxi.GetSumPrice(Taxi.Find(unity.TaxiCollection, 5814444).Items, (IOptionable b) => b.FuelCost<8 ));
            Console.WriteLine(Taxi.Find(unity.TaxiCollection, "TaxiCity").ToString());
            IEnumerable<ICar> sortedCollectionCars = from car in Taxi.Find(unity.TaxiCollection, "TaxiCity").Items orderby (car as IOptionable).FuelCost select car;
            IEnumerable<ICar> selectedByMinSpeed = (Taxi.Find(unity.TaxiCollection, "TaxiCity") as Taxi).SelectBySpeed(170);
            IEnumerable<ICar> selectedByMinMaxSpeed = (Taxi.Find(unity.TaxiCollection, "TaxiCity") as Taxi).SelectBySpeed(170,180);
            //Taxi sortedCarsTaxi = new Taxi();
            //sortedCarsTaxi.Add ( sortedCollectionCars);
            //Console.WriteLine(sortedCarsTaxi.ToString());
        }
    }
}
