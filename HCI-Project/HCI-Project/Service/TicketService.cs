using HCI_Project.Model;
using HCI_Project.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Service
{
    class TicketService
    {
        public static List<Ticket> getUserReservedTickets(string username)
        {
            return filterUserTickets(username, TicketStatus.Reserved);
        }

        public static List<Ticket> getUserPayedTickets(string username)
        {
            return filterUserTickets(username, TicketStatus.Payed);
        }

        private static List<Ticket> filterUserTickets(string username, TicketStatus status)
        {
            List<Ticket> tickets = new List<Ticket>();

            foreach (Ticket ticket in TicketRepo.getUserTickets(username))
                if (ticket.TicketStatus == status && ticket.Departure >= DateTime.Now)
                    tickets.Add(ticket);

            return tickets;
        }

        public static void reserveTicket(Ticket ticket)
        {
            firstTimeBuyTicket(ticket, TicketStatus.Reserved);
        }

        public static void buyTicket(Ticket ticket)
        {
            firstTimeBuyTicket(ticket, TicketStatus.Payed);
        }

        private static void firstTimeBuyTicket(Ticket ticket, TicketStatus status)
        {
            ticket.TicketStatus = status;
            setId(ticket);
            addTicket(ticket);
        }

        public static void buyTicket(int ticketId)
        {
            changeTicketStatus(ticketId, TicketStatus.Payed);
        }

        public static void cancelTicketUser(int ticketId)
        {
            changeTicketStatus(ticketId, TicketStatus.UserCanceled);
        }

        public static void cancelTicketService(int ticketId)
        {
            changeTicketStatus(ticketId, TicketStatus.ServiceCanceled);
        }

        private static void changeTicketStatus(int ticketId, TicketStatus status)
        {
            Ticket ticket = TicketRepo.findTicket(ticketId);

            if (ticket != null)
                ticket.TicketStatus = status;
        }

        private static void addTicket(Ticket ticket)
        {
            TicketRepo.addTicket(ticket);
        }

        public static bool doesFreeSeatExists(DateTime date, long routeId)
        {
            List<string> takenSeats = getTakenSeats(date, routeId);
            //TODO available seats

            return true;
        }

        public static List<string> getTakenSeats(DateTime date, long routeId)
        {
            List<string> seats = new List<string>();
            List<Ticket> tickets = TicketRepo.getTickets(date, routeId);

            foreach (Ticket t in tickets)
                seats.AddRange(t.Seats);

            return seats;
        }


        public static Boolean isSeatTaken(DateTime date, long routeId, string seat = "1A")
        {
            List<string> seats = getTakenSeats(date, routeId);

            foreach (string s in seats)
                if (s.Equals(seat))
                    return true;

            return false;
        }

        private static void setId(Ticket ticket)
        {
            ticket.Id = TicketRepo.generateId();
        }

        private static string formTicketMapKey(DateTime date, long routeId)
        {
            string datePart = date.ToString("dd_MM_yyyy");
            string routeIdPart = routeId.ToString();

            return datePart + ":" + routeIdPart;
        }

        public static List<string> getMonths()
        {
            List<string> months = new List<string> { "January", "February", "March", "April", "May", "June",
                                                      "July", "August", "Spetember", "November", "December"};

            return months;
        }

        public static List<Report> getReport(Route route, int month)
        {
            List<Report> reports = new List<Report>();
            List<string> scRoutesIds = new List<string>();

            foreach (ScheduledRoute sc in route.ScheduledRoutes)
                scRoutesIds.Add(sc.id.ToString());

            Dictionary<string, Report> reportsByDates = new Dictionary<string, Report>();
            List<string> dates = new List<string>();

            foreach (string key in TicketRepo.TicketsMap.Keys)
            {
                string[] splits = key.Split(':');
                string dateStr = splits[0];
                string scRouteIdStr = splits[1];
                string[] dateSplits = dateStr.Split('_');
                string monthStr = dateSplits[1];

                if (scRoutesIds.Contains(scRouteIdStr) && month == int.Parse(monthStr))
                {
                    string formatedDateStrKey = dateSplits[2] + "_" + dateSplits[1] + "_" + dateSplits[0];

                    if (!reportsByDates.ContainsKey(formatedDateStrKey))
                    {
                        reportsByDates[formatedDateStrKey] = new Report(dateStr.Replace('_', '.') + ".", 0, 0, 0);
                        dates.Add(formatedDateStrKey);
                    }

                    Report r = reportsByDates[formatedDateStrKey];

                    foreach (Ticket t in TicketRepo.getTicketsForReport(key))
                    {
                        r.Income += t.Price;
                        r.NumOfTickets += 1;
                        r.Seats += t.Seats.Count;
                    }
                }
            }

            dates.Sort();

            foreach (string d in dates)
                reports.Add(reportsByDates[d]);

            return reports;
        }

        public static List<Report> getReport(ScheduledRoute scRoute, int month)
        {
            List<Report> reports = new List<Report>();

            foreach (string key in TicketRepo.TicketsMap.Keys)
            {
                string[] splits = key.Split(':');
                string dateStr = splits[0];
                string scRouteIdStr = splits[1];
                string[] dateSplits = dateStr.Split('_');
                string monthStr = dateSplits[1];

                if (scRoute.id == long.Parse(scRouteIdStr) && month == int.Parse(monthStr))
                {
                    double income = 0;
                    int numOfTickets = 0;
                    int seats = 0;

                    foreach (Ticket t in TicketRepo.getTicketsForReport(key))
                    {
                        income += t.Price;
                        numOfTickets += 1;
                        seats += t.Seats.Count;
                    }

                    Report r = new Report(dateStr, income, numOfTickets, seats);
                    reports.Add(r);
                }
            }

            return reports;
        }

        public static Report getReport(Route route, DateTime date)
        {
            List<Report> reports = new List<Report>();
            List<string> scRoutesIds = new List<string>();

            foreach (ScheduledRoute sc in route.ScheduledRoutes)
            {
                Report r = getReport(sc, date);
                reports.Add(r);
            }

            return combineReports(date.ToString("dd.MM.yyyy."), reports);
        }

        public static List<Report> getPastMonthsReports(Route route, DateTime date)
        {
            List<Report> reports = new List<Report>();
            List<DateTime> dates = new List<DateTime> { date.AddMonths(-4), date.AddMonths(-3), date.AddMonths(-2), date.AddMonths(-1) };

            foreach (DateTime d in dates)
            {
                Report r = getReport(route, d);
                reports.Add(r);
            }

            reports.Add(getReport(route, date));

            return reports;
        }

        public static Report getReport(ScheduledRoute scRoute, DateTime date)
        {
            Report report = new Report(date.ToString("dd.MM.yyyy."), 0, 0, 0);

            foreach (Ticket t in TicketRepo.getPayedTickets(date, scRoute.id))
            {
                report.Income += t.Price;
                report.NumOfTickets += 1;
                report.Seats += t.Seats.Count;
            }

            return report;
        }

        public static List<Report> getPastMonthsReports(ScheduledRoute scRoute, DateTime date)
        {
            List<Report> reports = new List<Report>();
            List<DateTime> dates = new List<DateTime> { date.AddMonths(-4), date.AddMonths(-3), date.AddMonths(-2), date.AddMonths(-1) };

            foreach (DateTime d in dates)
            {
                Report r = getReport(scRoute, d);
                reports.Add(r);
            }

            reports.Add(getReport(scRoute, date));

            return reports;
        }

        public static Report combineReports(string group, List<Report> reports)
        {
            Report report = new Report(group, 0, 0, 0);

            foreach (Report r in reports)
            {
                report.Income += r.Income;
                report.NumOfTickets += r.NumOfTickets;
                report.Seats += r.Seats;
            }

            return report;
        }
    }
}
