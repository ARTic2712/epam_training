using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLib.Interfaces
{
    public interface ITaxi
    {
        Guid Id { get; }
        string Name { get; set; }
        int PhoneNumber { get; set; }
        ICollection<ICar> Items { get;}
    }
}
