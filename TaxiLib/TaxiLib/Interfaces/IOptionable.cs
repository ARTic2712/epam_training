using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLib.Interfaces
{
    interface IOptionable
    {
        double Weight { get; }
        double FuelCost { get; set; }
        int MaxSpeed { get; set; }
    }
}
