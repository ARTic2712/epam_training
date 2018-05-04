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
            Console.WriteLine("Сумма автомобилей стоимостью выше 5000:" + Taxi.GetSumPrice(unity.TaxiCollection.First(x => x.Name.Contains("TaxiCity")).Items,a=>a.Price > 5000));
            Console.WriteLine("Сумма автомобилей с расходом меньше 8:" + Taxi.GetSumPrice(unity.TaxiCollection.First(x => x.Name.Contains("TaxiCity")).Items, (IOptionable b) => b.FuelCost<8 ));
            Console.WriteLine(unity.CarCollection.ElementAt(0).GetCharacteristics());
            // IEnumerable<IOptionable > Taxicars = (unity.TaxiCollection.First(x => x.Name.Contains("TaxiCity")).Items.OrderBy((IOptionable c) => c.FuelCost);
            //  Console.WriteLine( ((List<ICar>)unity.TaxiCollection.First(x => x.Name.Contains("TaxiCity")).Items).FindAll(x => x.Price > 5000).Count );

        }
    }
}
