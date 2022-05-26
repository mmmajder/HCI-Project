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
        public static List<ScheduledRoute> getAvailAbleRoutes(Route route, DateTime date)
        {
            List<ScheduledRoute> scheduledRoutes = new List<ScheduledRoute>();
            foreach (ScheduledRoute scheduledRoute in route.ScheduledRoutes)
            {
                if (scheduledRoute.RepeatigDays.Contains((int)date.DayOfWeek))
                {
                    if (!scheduledRoute.NotWorkingDays.Contains(date))
                    {
                        scheduledRoutes.Add(scheduledRoute);
                    }
                }
            }
            return scheduledRoutes;

        }

        public static List<RouteTableDTO> GetRoutes(string from, string to, DateTime date)
        {
            List<RouteTableDTO> routes = new List<RouteTableDTO>();
            foreach (Route route in Routes)
            {
                if (isGoodRoute(route, from, to))
                {
                    foreach (ScheduledRoute scheduledRoute in getAvailAbleRoutes(route, date))
                    {
                        DateTime startTime = DateTime.Now;
                        DateTime endTime = DateTime.Now;
                        foreach (ScheduledStation scheduledStation in scheduledRoute.Stations)
                        {
                            if (scheduledStation.Station.Name == from)
                            {
                                startTime = scheduledStation.TimeRange.Depature;
                            }
                            if (scheduledStation.Station.Name == to)
                            {
                                endTime = scheduledStation.TimeRange.Arrival;
                            }
                        }
                        DateTime endDate = DateTimeUtils.calculateEndDate(endTime, date);
                        routes.Add(new RouteTableDTO{ From = date.ToString("dd.MM.yyyy"), Depature = startTime.ToString("HH:mm"), To = endDate.ToString("dd.MM.yyyy"), Arrival = endTime.ToString("HH:mm"), Time = (endTime - startTime).ToString() });
                    }
                }
            }
            return routes;
        }

        public static List<ScheduledRoute> GetScheduledRoutes(string from, string to, DateTime date)
        {
            List<ScheduledRoute> routes = new List<ScheduledRoute>();
            foreach (Route route in Routes)
            {
                if (isGoodRoute(route, from, to))
                {
                    foreach (ScheduledRoute scheduledRoute in getAvailAbleRoutes(route, date))
                    {
                       routes.Add(scheduledRoute);
                    }
                }
            }
            return routes;
        }


        private static bool isGoodRoute(Route route, string from, string to)
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
                    return true;
                }
            }
            return false;
        }

        

    }
    
}
