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
            _unity.Add(new Passanger(Guid.NewGuid(), "Peugeot", "406", "8735 EB-4", 1500, 2560, 6.6, 180, 4, Enums.CarcassPassanger.Sedan));
            _unity.Add(new Passanger(Guid.NewGuid(), "Skoda", "Rapid", "2649 EB-4", 4250, 2350, 7.5, 175 , 4, Enums.CarcassPassanger.Sedan));
            _unity.Add(new Passanger(Guid.NewGuid(), "Citroen", "Picasso", "2553 EB-4", 5100, 2730, 7.8, 178, 6, Enums.CarcassPassanger.Universal));
            _unity.Add(new Truck(Guid.NewGuid(), "Iveco", "4910", "6752 EB-4", 9600, 3700, 9.5, 145, 4000, Enums.CarcassTruck.Awning, 20));

        }
        protected void CreateTaxi()
        {
            _unity.Add(new Taxi(Guid.NewGuid(), "TaxiCity", 5814444, new List<Interfaces.ICar> ()));
            _unity.Add(new Taxi(Guid.NewGuid(), "Bertel", 108, new List<Interfaces.ICar>()));

        }
        protected void FillTaxi()
        {
            _unity.TaxiCollection.First(x => x.Name.Contains("TaxiCity")).Items.Add(_unity.CarCollection.ElementAt(2));
            _unity.TaxiCollection.First(x=>x.Name.Contains("TaxiCity")).Items.Add(_unity.CarCollection.ElementAt(0));
            _unity.TaxiCollection.First(x => x.Name.Contains("TaxiCity")).Items.Add(_unity.CarCollection.ElementAt(3));
            _unity.TaxiCollection.First(x => x.Name.Contains("TaxiCity")).Items.Add(_unity.CarCollection.ElementAt(1));
            
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
