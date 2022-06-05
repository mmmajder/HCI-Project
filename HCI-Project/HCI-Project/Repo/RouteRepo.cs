using HCI_Project.DTO;
using HCI_Project.Model;
using HCI_Project.util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    public class RouteRepo
    {
        private static List<Route> Routes = new List<Route>();

        public RouteRepo()
        {
            List<Station> allStations = StationRepo.GetStations();
            List<Station> stations1 = new List<Station> { allStations[0], allStations[1], allStations[2] };
            List<Station> stations2 = new List<Station> { allStations[0], allStations[3], allStations[2] };
            List<ScheduledRoute> allScheduledRoutes = ScheduledRouteRepo.GetScheduledRoutes();
            List<ScheduledRoute> scheduledRoutes1 = new List<ScheduledRoute> { allScheduledRoutes[0] };
            List<ScheduledRoute> scheduledRoutes2 = new List<ScheduledRoute> { allScheduledRoutes[1] };
            Routes.Add(new Route(1, stations1, scheduledRoutes1, "type 1"));
            Routes.Add(new Route(2, stations2, scheduledRoutes2, "type 2"));

        }
        public static void AddRoute(Route route)
        {
            Routes.Add(route);
        }

        public static List<Route> GetRoutes()
        {
            return Routes;
        }
        

    }
    
}
