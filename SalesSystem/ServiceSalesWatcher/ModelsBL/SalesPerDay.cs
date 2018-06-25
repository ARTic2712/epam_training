using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSalesWatcher.ModelsBL
{
    public class SalesPerDay
    {
        public string ManagerFirstName { get; set; }
        public string ManagerSecondName { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }
        public DateTime DateSale { get; set; }
        public string ClientFirstName { get; set; }
        public string ClientSecondName { get; set; }
        public string Description { get; set; }
        public SalesPerDay (string managerFirstName,string managerSecondName)
        {
            ManagerFirstName = managerFirstName;
            ManagerSecondName = managerSecondName;
        }
    }
}
