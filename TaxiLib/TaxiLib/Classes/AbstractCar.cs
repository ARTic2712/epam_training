using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLib.Classes
{
    public abstract class AbstractCar : Interfaces.ICar, Interfaces.IOptionable
    {
        public Guid Id { get; }
        public string Firm { get; }
        public string Model { get;}
        public string RegistrationNumber { get; }
        public double Weight { get; set; }
        public double FuelCost { get; set; }
        public int MaxSpeed { get; set; }
        public AbstractCar(Guid id, string firm, string model, string registrationNumber, double weight, double fuelcost, int maxspeed)
        {
            this.Id = id;
            this.Firm = firm;
            this.Model = model;
            this.RegistrationNumber = registrationNumber;
            this.Weight = weight;
            this.FuelCost = fuelcost;
            this.MaxSpeed = maxspeed;
        }
    }
}
