using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiLib.Interfaces;

namespace TaxiLib.Classes
{
   public class Unity
    {
        private ICollection<Interfaces.ICar> _carCollection;
        private ICollection<Interfaces.ITaxi> _taxiCollection;

        public Unity(ICollection<Interfaces.ICar> carCollection, ICollection <Interfaces.ITaxi > taxiCollection)
        {
            this.CarCollection = carCollection;
            this.TaxiCollection = taxiCollection;
        }

        public ICollection<ICar> CarCollection { get; private set; }
        public ICollection<ITaxi> TaxiCollection { get; private set; }

        public void Add(Interfaces.ICar item)
        {
            CarCollection.Add(item);
        }

        public void Add(Interfaces.ITaxi  item)
        {
            TaxiCollection .Add(item);
        }
    }
}
