using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class StationPrice
    {
        public string StationName { get; set; }
        public double Price { get; set; }

        public StationPrice() {}
        public StationPrice(string name, double price) 
        {
            StationName = name;
            Price = price;
        }
    }
}
