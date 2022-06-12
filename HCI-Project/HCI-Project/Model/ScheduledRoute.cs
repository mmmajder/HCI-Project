using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class ScheduledRoute
    {
        public long id { get; set; }
        public List<ScheduledStation> Stations { get; set; }
        public long RouteId { get; set; }
        public List<int> RepeatigDays { get; set; } //days of week [1-7]
        public List<DateTime> NotWorkingDays { get; set; }

        public ScheduledRoute(long id, List<ScheduledStation> stations, long routeId, List<int> repeatigDays, List<DateTime> notWorkingDays)
        {
            this.id = id;
            Stations = stations;
            RouteId = routeId;
            RepeatigDays = repeatigDays;
            NotWorkingDays = notWorkingDays;
        }

        public DateTime ?getDepartureTime(string stationName)
        {
            foreach (ScheduledStation s in Stations)
                if (s.Station.Name.Equals(stationName))
                    return s.TimeRange.Depature;

            return null;
        }

        public override string ToString()
        {
            string str = "";

            if (Stations.Count > 0)
                str += Stations[0].TimeRange.Depature.ToString("HH:mm");

            if (Stations.Count > 1)
                str += " - " + Stations[Stations.Count - 1].TimeRange.Depature.ToString("HH:mm");

            str += getRepeatingDays();

            return str;
        }

        public string getRepeatingDays()
        {
            string str = "";

            foreach (int i in RepeatigDays)
                str += " " + getDay(i);

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