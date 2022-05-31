using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    public class ScheduledRouteRepo
    {
        private static List<ScheduledRoute> ScheduledRoutes = new List<ScheduledRoute>();

        public ScheduledRouteRepo()
        {
            List<ScheduledStation> stations = ScheduledStationRepo.GetScheduledStations();
            List<ScheduledStation> stations1 = new List<ScheduledStation> { stations[0], stations[1], stations[2] };
            List<ScheduledStation> stations2 = new List<ScheduledStation> { stations[3], stations[4], stations[5] };


            ScheduledRoutes.Add(new ScheduledRoute(stations1, 1, new List<int> { 1,2,3,4,5,6,7 }, new List<DateTime> ()));
            ScheduledRoutes.Add(new ScheduledRoute(stations2, 2, new List<int> { 3 }, new List<DateTime>()));
        }
        public static void AddScheduledRoute(ScheduledRoute scheduledRoute)
        {
            ScheduledRoutes.Add(scheduledRoute);
        }

        public static List<ScheduledRoute> GetScheduledRoutes()
        {
            return ScheduledRoutes;
        }

        
    }
}
