using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLib.Interfaces;

namespace TaxiLib.Classes
{
    class Taxi : Interfaces.ITaxi
    {
        public Guid Id { get; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        ICollection<ICar> ITaxi.Items { get; }
    }
}
