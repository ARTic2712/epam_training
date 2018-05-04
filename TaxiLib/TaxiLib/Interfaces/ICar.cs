using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLib.Interfaces
{
    public interface ICar
    {
        Guid Id { get; }
        string Firm { get; }
        string Model { get; }
        string RegistrationNumber { get; }
        double Price { get; }
        string GetCharacteristics();
    }
}
