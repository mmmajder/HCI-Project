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

        public string getRepeatingDays()
        {
            string str = "";
            List<int> dayInts = new List<int>();

            foreach (ScheduledRoute sc in ScheduledRoutes)
                foreach (int dayId in sc.RepeatigDays)
                    if (!dayInts.Contains(dayId))
                        dayInts.Add(dayId);

            dayInts.Sort();

            foreach (int dayInt in dayInts)
                str += getDay(dayInt) + " ";

            return str;
        }

        private string getDay(int day)
        {
            switch (day)
            {
                case 1: return "Mon";
                case 2: return "Tue";
                case 3: return "Wed";
                case 4: return "Thu";
                case 5: return "Fri";
                case 6: return "Sat";
                case 7: return "Sun";
                default: return "";
            }
        }
    }
}
