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
        public Enums.CarcassPassanger Carcass { get; set; }

        public Passanger(Guid id, string firm, string model,string registrationNumber, double weight, double fuelcost, int maxspeed, short countPassangers,Enums.CarcassPassanger carcass)
            : base(id, firm, model,registrationNumber , weight, fuelcost, maxspeed)
        {
            this.CountPassangers = countPassangers;
            this.Carcass = carcass;
        }
    }
}
