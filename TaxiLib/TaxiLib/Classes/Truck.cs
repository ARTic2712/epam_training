﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLib.Interfaces;

namespace TaxiLib.Classes
{
    public class Truck:AbstractCar,Interfaces.ICar, Interfaces.IOptionable 
    {
        private Interfaces.ITaxi _taxi;
        public ITaxi Taxi { get => _taxi; set => _taxi = value; }
        public decimal Carrying { get; set; }
        public decimal Volume { get; set; }
        public Enums.CarcassTruck Carcass { get; set; }
        public Truck(Guid id, string firm, string model,string registrationNumber, double price, double weight, double fuelcost, int maxspeed, decimal carrying, Enums.CarcassTruck carcass, decimal volume)
            : base(id,firm,model,registrationNumber,price ,weight,fuelcost,maxspeed)
        {
            this.Carrying = carrying;
            this.Carcass = carcass;
            this.Volume = volume;
        }
        public override string GetCharacteristics()
        {
            return base.GetCharacteristics() + Environment.NewLine + "Carrying: " + Carrying + Environment.NewLine +"Useful volume: " + Volume + Environment.NewLine  + "Carcass:" + GetCarcass();
        }
        public string GetCarcass()
        {
            {
                switch (Carcass)
                {
                    case Enums.CarcassTruck.Awning  : return "Awning";
                    case Enums.CarcassTruck.Board : return "Board";
                    case Enums.CarcassTruck.Fridge : return "Fridge";
                    case Enums.CarcassTruck.Wagon: return "Wagon";
                    default: return "Unknown";
                }
            };
        }
        public override string ToString()
        {
            return base.ToString() + GetCharacteristics();
        }
    }
}
