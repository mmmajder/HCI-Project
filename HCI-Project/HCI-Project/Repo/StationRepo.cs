using HCI_Project.Model;
using Microsoft.Maps.MapControl.WPF;
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
            Stations.Add(new Station("Novi Sad", true, new Location(45.265366, 19.829557)));
            Stations.Add(new Station("Zrenjanin", true, new Location(45.380917, 20.376678)));
            Stations.Add(new Station("Glamoc", true, new Location(44.045818, 16.856857)));
            Stations.Add(new Station("Backa Topola", true, new Location(45.819782, 19.629997)));
        }
        public static void AddStation(Station station)
        {
            Stations.Add(station);
        }

        public static void RemoveStation(Station station)
        {
            Stations.Remove(station);
        }

        public static ref List<Station> GetStations()
        {
            return ref Stations;
        }

        public static List<String> GetStationNames()
        {
            return Stations.Select(o => o.Name).ToList();
        }

        public static Station GetStationByName(string name)
        {
            try
            {
                return Stations.Single(s => s.Name == name);
            } 
            catch
            {
                return null;
            }
        }
    }
}
