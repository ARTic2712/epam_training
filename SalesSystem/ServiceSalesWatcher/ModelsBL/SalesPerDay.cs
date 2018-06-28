using System;

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
        public void ParseLine(string line)
        {
            var lineContent = line.Split(',');
            ClientFirstName = lineContent[0].Trim();
            ClientSecondName = lineContent[1].Trim();
            Product = lineContent[2].Trim();
            Price = Convert.ToDouble(lineContent[3]);
            Description = lineContent[4].Trim();
            DateSale = Convert.ToDateTime(lineContent[5]);
        }
    }
}
