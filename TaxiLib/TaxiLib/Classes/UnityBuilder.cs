using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiLib.Classes
{
    public class UnityBuilder
    {
        private Classes.Unity _unity;
        
        public UnityBuilder(ICollection <Interfaces.ICar > carCollection, ICollection<Interfaces.ITaxi > taxiCollection)
        {
            this._unity = new Unity(carCollection, taxiCollection);
        }

        protected void CreateCars()
        {
            
        }
        protected void CreateTaxi()
        {

        }
        protected void FillTaxi()
        {

        }
        public Classes.Unity Build()
        {
            CreateCars();
            CreateTaxi();
            FillTaxi();
            return _unity;
        }
    }
}
