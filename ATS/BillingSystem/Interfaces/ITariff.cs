using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Interfaces
{
    public interface ITariff
    {
        int FreeMinutes { get; }
        double PricePerSecond { get; }
        double CalculateCost(ATSLib.Classes.CallInfo call);
    }
}
