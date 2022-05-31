using HCI_Project.DTO;
using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Service
{
    public class RouteService
    {
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
            foreach (Route route in RouteRepo.GetRoutes())
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
                        routes.Add(new RouteTableDTO { From = date.ToString("dd.MM.yyyy"), Depature = startTime.ToString("HH:mm"), To = endDate.ToString("dd.MM.yyyy"), Arrival = endTime.ToString("HH:mm"), Time = (endTime - startTime).ToString() });
                    }
                }
            }
            return routes;
        }

        public static List<ScheduledRoute> GetScheduledRoutes(string from, string to, DateTime date)
        {
            List<ScheduledRoute> routes = new List<ScheduledRoute>();
            foreach (Route route in RouteRepo.GetRoutes())
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

        public static List<Route> GetRoutes(string from, string to)
        {
            List<Route> routes = new List<Route>();
            foreach (Route route in RouteRepo.GetRoutes())
            {
                if (isGoodRoute(route, from, to))
                {
                    routes.Add(route);
                }
            }
            return routes;
        }

        public static List<RouteTableManagerDTO> GetRoutesTableData(List<Route> Routes, string from, string to) 
        {
            List<RouteTableManagerDTO> tableData = new List<RouteTableManagerDTO>();
            foreach (Route route in Routes)
            {
                foreach (ScheduledRoute scheduledRoute in route.ScheduledRoutes)
                {
                    DateTime depature = GetDepature(scheduledRoute, from);
                    DateTime arrival = GetArrival(scheduledRoute, to);
                    string days = GetDayNames(scheduledRoute.RepeatigDays);

                    tableData.Add(new RouteTableManagerDTO { Depature = depature.ToString("HH:mm"), Arrival = arrival.ToString("HH:mm"), Days=days });
                }

            }
            return tableData;
        }

        public static string GetDayNames(List<int> days)
        {
            string dayString = "";
            foreach(int day in days)
            {
                switch (day)
                {
                    case 1:
                        dayString += "Mon, ";
                        break;
                    case 2:
                        dayString += "Tue, ";
                        break;
                    case 3:
                        dayString += "Wed, ";
                        break;
                    case 4:
                        dayString += "Thu, ";
                        break;
                    case 5:
                        dayString += "Fri, ";
                        break;
                    case 6:
                        dayString += "Sat, ";
                        break;
                    case 7:
                        dayString += "Sun, ";
                        break;
                    default:
                        break;
                }
            }
            dayString = dayString.Substring(0, dayString.Length - 2);
            return dayString;
        }

        public static DateTime GetDepature(ScheduledRoute scheduledRoute, string location)
        {
            foreach (ScheduledStation scheduled in scheduledRoute.Stations)
            {
                if (scheduled.Station.Name==location)
                {
                    return scheduled.TimeRange.Depature;
                }
            }
            return new DateTime();
        }

        public static DateTime GetArrival(ScheduledRoute scheduledRoute, string location)
        {
            foreach (ScheduledStation scheduled in scheduledRoute.Stations)
            {
                if (scheduled.Station.Name == location)
                {
                    return scheduled.TimeRange.Arrival;
                }
            }
            return new DateTime();
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
