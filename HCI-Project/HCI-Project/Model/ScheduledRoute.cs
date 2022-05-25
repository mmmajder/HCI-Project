using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class ScheduledRoute
    {
        public List<ScheduledStation> Stations { get; set; }
        public long RouteId { get; set; }
        public List<int> RepeatigDays { get; set; } //days of week [1-7]
        public List<DateTime> NotWorkingDays { get; set;  }

        public ScheduledRoute(List<ScheduledStation> stations, long routeId, List<int> repeatigDays, List<DateTime> notWorkingDays)
        {
            Stations = stations;
            RouteId = routeId;
            RepeatigDays = repeatigDays;
            NotWorkingDays = notWorkingDays;
        }
    }
}
