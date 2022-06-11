using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.DTO
{
    public class ScheduledStationDTO
    {
        public Station Station { get; set; }
        public TimeRange TimeRange { get; set; }
        public Boolean IsError { get; set; }

        public ScheduledStationDTO(Station station, TimeRange timeRange, bool isError)
        {
            Station = station;
            TimeRange = timeRange;
            IsError = isError;
        }
        public ScheduledStationDTO(ScheduledStation scheduledStation, bool isError)
        {
            Station = scheduledStation.Station;
            TimeRange = scheduledStation.TimeRange;
            IsError = isError;
        }
    }
}
