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
            string str= Id + " ";

            if (Stations.Count > 0)
                str += Stations[0].Name;

            if (Stations.Count > 2)
            {
                int middleInt = Stations.Count / 2;
                str += "-" + Stations[middleInt].Name;
            }

            if (Stations.Count > 1)
                str += "-" + Stations[Stations.Count-1].Name;

            return str;
        }
    }
}
