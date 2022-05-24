using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class ScheduledStation
    {
        public Station Station {get; set; }
        public TimeRange TimeRange { get; set; }

        public ScheduledStation(Station station, TimeRange timeRange)
        {
            Station = station;
            TimeRange = timeRange;
        }
    }
}
