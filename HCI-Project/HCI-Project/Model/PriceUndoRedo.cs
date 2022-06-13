using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    class PriceUndoRedo
    {
        public long RouteId { get; set; }
        public string StationName { get; set; }
        public double OldPrice { get; set; }
        public double NewPrice { get; set; }

        public PriceUndoRedo() { }

        public PriceUndoRedo(long routeId, string stationNAme, double oldPrice, double newPrice) 
        {
            RouteId = routeId;
            StationName = stationNAme;
            OldPrice = oldPrice;
            NewPrice = newPrice;
        }
    }
}
