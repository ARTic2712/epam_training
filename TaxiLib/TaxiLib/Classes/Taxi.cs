using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLib.Interfaces;

namespace TaxiLib.Classes
{
    public class Taxi : Interfaces.ITaxi
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public ICollection<ICar> Items { get; }

        public Taxi(Guid id, string name,int phoneNumber, ICollection<ICar> items)
        {
            this.Id = id;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Items = items;

        }
        public static double GetSumPrice(ICollection<ICar> cars,Predicate<ICar> predicate)
        {
            Double summa = 0;
            foreach(ICar car in cars)
            {
                if (predicate(car))
                {
                    summa += car.Price;
                }
            }
            return summa;
        }
        public static double GetSumPrice(ICollection<ICar> cars, Predicate<IOptionable> predicate)
        {
            Double summa = 0;
            foreach (ICar car in cars)
            {
                if (predicate((IOptionable)car))
                {
                    summa += car.Price;
                }
            }
            return summa;
        }
    }
}
