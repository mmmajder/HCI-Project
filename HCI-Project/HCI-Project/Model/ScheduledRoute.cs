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
    }
}