using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLib.Classes
{
   public class Unity
    {
        private ICollection<Interfaces.ICar> _carCollection;
        private ICollection<Interfaces.ITaxi> _taxiCollection;

        public Unity(ICollection<Interfaces.ICar> carCollection, ICollection <Interfaces.ITaxi > taxiCollection)
        {
            this._carCollection = carCollection;
            this._taxiCollection = taxiCollection;
        }

        public void Add(Interfaces.ICar item)
        {
            _carCollection.Add(item);
        }

        public void Add(Interfaces.ITaxi  item)
        {
            _taxiCollection .Add(item);
        }
    }
}
