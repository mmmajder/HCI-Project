using HCI_Project.Model;
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
            List<Station> stations1 = new List<Station> { allStations[0], allStations[1] };
            List<Station> stations2 = new List<Station> { allStations[0], allStations[3], allStations[2] };
            List<ScheduledRoute> allScheduledRoutes = ScheduledRouteRepo.GetScheduledRoutes();
            List<ScheduledRoute> scheduledRoutes1 = new List<ScheduledRoute> { allScheduledRoutes[0] };
            List<ScheduledRoute> scheduledRoutes2 = new List<ScheduledRoute> { allScheduledRoutes[1] };
            Routes.Add(new Route(stations1, scheduledRoutes1, "type 1"));
            Routes.Add(new Route(stations2, scheduledRoutes2, "type 2"));

        }
        public static void AddRoute(Route route)
        {
            Routes.Add(route);
        }

        public static List<Route> GetRoutes()
        {
            return Routes;
        }

        public static ObservableCollection<Route> GetRoutes(string from, string to, DateTime date)
        {
            ObservableCollection<Route> routes = new ObservableCollection<Route>();
            foreach (Route route in Routes)
            {
                bool isFrom = false;
                foreach (Station station in route.Stations)
                {
                    if (station.Name == from)
                    {
                        isFrom = true;
                    }
                    else if ((station.Name == to) && isFrom)
                    {
                        if (isAvailAbleRoute(route, date)) {
                            routes.Add(route);
                            continue;
                        }
                    }
                }
            }
            return routes;
        }

        public static bool isAvailAbleRoute(Route route, DateTime date)
        {
            foreach (ScheduledRoute scheduledRoute in route.ScheduledRoutes)
            {
                if (scheduledRoute.RepeatigDays.Contains((int) date.DayOfWeek))
                {
                    if (!scheduledRoute.NotWorkingDays.Contains(date))
                    {
                        return true;
                    }
                }
            }
            return false;

        }

    }
}
