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
        public ICollection<ICar> Items { get;}

        public Taxi(Guid id, string name,int phoneNumber, ICollection<ICar> items)
        {
            this.Id = id;
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Items = items;

        }
        public Taxi()
        {
            this.Id = Guid.NewGuid() ;
            this.Name = "DefaultTaxi";
            this.PhoneNumber = 0;
            this.Items = new List<ICar>();

        }
        public void Add(IEnumerable<ICar> cars)
        {
            foreach(ICar car in cars)
            {
                Items.Add(car);
            }
        }
        public IEnumerable <ICar> SelectBySpeed(int minSpeed)
        {
            return from car in Items where (car as IOptionable).MaxSpeed > minSpeed select car;
        }
        public IEnumerable<ICar> SelectBySpeed(int minSpeed, int maxSpeed)
        {
            return from car in Items where (car as IOptionable).MaxSpeed > minSpeed && (car as IOptionable).MaxSpeed<maxSpeed  select car;
        }

        public static ITaxi Find(ICollection<ITaxi > taxiCollection,string name)
        {
            return taxiCollection.First(x => x.Name.Contains(name));
        }
        public static ITaxi Find(ICollection<ITaxi> taxiCollection, int phoneNumber)
        {
            return taxiCollection.First(x => x.PhoneNumber== phoneNumber);
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
        public override string ToString()
        {
            string TaxiStr="----------------------------------------------------------------" + Environment.NewLine;
            TaxiStr += Name + " Tel. number: " + PhoneNumber + Environment.NewLine;
            TaxiStr += "CARS:" + Environment.NewLine;
            foreach (ICar car in Items)
            {
                TaxiStr += "****************************************************************" + Environment.NewLine;
                TaxiStr += car.ToString() +Environment.NewLine ;

            }
            TaxiStr += "----------------------------------------------------------------";
            return TaxiStr;
        }
            }
}
