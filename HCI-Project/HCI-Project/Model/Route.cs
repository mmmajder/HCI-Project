using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Route
    {
        public long Id { get; set; }
        public List<Station> Stations { get; set; }
        public List<ScheduledRoute> ScheduledRoutes { get; set; }
        // TODO PRICE
        public string TrainType { get; set; }

        public Route(long id, List<Station> stations, List<ScheduledRoute> scheduledRoutes, string trainType)
        {
            Id = id;
            Stations = stations;
            ScheduledRoutes = scheduledRoutes;
            TrainType = trainType;
        }

        public override string ToString()
        {
            string route = "";
            int length = Stations.Count;
            for (int i = 0; i < length; i++)
            {
                if (i == length - 1)
                {
                    route += Stations[i].Name;
                }
                else
                {
                    route += Stations[i].Name + "-";
                }
            }
            return route;
            
        }
    }
}
