using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    public class ScheduledStationRepo
    {
        private static List<ScheduledStation> ScheduledStations = new List<ScheduledStation>();

        public ScheduledStationRepo()
        {
            List<Station> allStations = StationRepo.GetStations();
            //ns -> zr -> gl rep
            ScheduledStations.Add(new ScheduledStation(allStations[0], new TimeRange(new DateTime(2022, 6, 1, 8, 30, 0), new DateTime(2022, 6, 1, 8, 35, 0))));
            ScheduledStations.Add(new ScheduledStation(allStations[1], new TimeRange(new DateTime(2022, 6, 1, 9, 0, 0), new DateTime(2022, 6, 1, 9, 5, 0))));
            ScheduledStations.Add(new ScheduledStation(allStations[2], new TimeRange(new DateTime(2022, 6, 2, 9, 0, 0), new DateTime(2022, 6, 2, 9, 5, 0))));


            //ns -> bt -> gl rep
            ScheduledStations.Add(new ScheduledStation(allStations[0], new TimeRange(new DateTime(2022, 5, 8, 10, 30, 0), new DateTime(2022, 5, 8, 8, 35, 0))));
            ScheduledStations.Add(new ScheduledStation(allStations[3], new TimeRange(new DateTime(2022, 5, 15, 11, 0, 0), new DateTime(2022, 5, 15, 11, 5, 0))));
            ScheduledStations.Add(new ScheduledStation(allStations[2], new TimeRange(new DateTime(2022, 5, 16, 15, 30, 0), new DateTime(2022, 5, 16, 15, 35, 0))));
        }
        public static void AddScheduledStation(ScheduledStation station)
        {
            ScheduledStations.Add(station);
        }

        public static List<ScheduledStation> GetScheduledStations()
        {
            return ScheduledStations;
        }
    }
}
