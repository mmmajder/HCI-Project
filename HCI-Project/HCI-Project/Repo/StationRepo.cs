using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    public class StationRepo
    {
        private static List<Station> Stations = new List<Station>();

        public StationRepo()
        {
            Stations.Add(new Station("Novi Sad", true, new Position(54, 45)));
            Stations.Add(new Station("Zrenjanin", true, new Position(45, 55)));
            Stations.Add(new Station("Glamoc", true, new Position(52, 35)));
            Stations.Add(new Station("Backa Topola", true, new Position(24, 75)));
        }
        public static void AddStation(Station station)
        {
            Stations.Add(station);
        }

        public static List<Station> GetStations()
        {
            return Stations;
        }

        public static List<String> GetStationNames()
        {
            return Stations.Select(o => o.Name).ToList();
        }
    }
}
