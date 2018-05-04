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
        public double Price { get; }
        public double Weight { get; set; }
        public double FuelCost { get; set; }
        public int MaxSpeed { get; set; }
        public AbstractCar(Guid id, string firm, string model, string registrationNumber,double price , double weight, double fuelcost, int maxspeed)
        {
            this.Id = id;
            this.Firm = firm;
            this.Model = model;
            this.RegistrationNumber = registrationNumber;
            this.Price = price;
            this.Weight = weight;
            this.FuelCost = fuelcost;
            this.MaxSpeed = maxspeed;
        }
        public virtual string GetCharacteristics()
        {
            return "Weight: " + Weight + Environment.NewLine  + "Fuel сost: " + FuelCost + Environment.NewLine + "Max speed: " + MaxSpeed;
        }
    }
}
