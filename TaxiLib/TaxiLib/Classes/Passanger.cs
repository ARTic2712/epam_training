using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLib.Classes
{
    public class Passanger : AbstractCar, Interfaces.ICar, Interfaces.IOptionable
    {
        private Interfaces.ITaxi _taxi;
        public  Interfaces. ITaxi Taxi { get => _taxi; set => _taxi = value; }

        public short CountPassangers { get; } 
        private  Enums.CarcassPassanger Carcass { get;set; }
        public string GetCarcass()
        {
            {
                switch (Carcass)
                {
                    case Enums.CarcassPassanger.Sedan: return "Sedan";
                    case Enums.CarcassPassanger.Universal: return "Universal";
                    case Enums.CarcassPassanger.Hatchback : return "Hatchback";
                    case Enums.CarcassPassanger.Coupe : return "Coupe";
                    case Enums.CarcassPassanger.Cabriolet : return "Cabriolet";
                    case Enums.CarcassPassanger.PickUp : return "Pick-Up";
                    default:return "Unknown";
                }
            };
        }

        public Passanger(Guid id, string firm, string model,string registrationNumber,double price, double weight, double fuelcost, int maxspeed, short countPassangers,Enums.CarcassPassanger carcass)
            : base(id, firm, model,registrationNumber, price , weight, fuelcost, maxspeed)
        {
            this.CountPassangers = countPassangers;
            this.Carcass = carcass;
        }
        public override string GetCharacteristics()
        {
            return base.GetCharacteristics() + Environment.NewLine + "Count passangers: " + CountPassangers + Environment.NewLine + "Carcass:" + GetCarcass();
        }
    }
}
