using HCI_Project.Exceptions;
using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    class TicketRepo
    {
        // unique Date + ScheduledRouteId
        private static Dictionary<string, List<Ticket>> TicketsMap = new Dictionary<string, List<Ticket>>();

        public static void addTicket(Ticket ticket)
        {
            string key = formTicketMapKey(ticket);

            if (TicketsMap.ContainsKey(key))
            {
                if (!doesFreeSeatExists())
                    throw new NoFreeSeatsException();
                if (!isChosenSeatFree())
                    throw new SeatTakenException();
                TicketsMap[key].Add(ticket);
            }
        }

        private static string formTicketMapKey(Ticket ticket)
        {
            string datePart = ticket.Date.ToString("dd_MM_yyyy");
            string routeIdPart = ticket.ScheduledRoute.id.ToString();

            return datePart + routeIdPart;
        }

        public static Boolean doesFreeSeatExists()
        {
            //TODO
            return true;
        }

        public static Boolean isChosenSeatFree(string seat="1A")
        {
            //TODO
            return true;
        }

        public static List<Ticket> getUserTickets(string username)
        {
            List<Ticket> userTickets = new List<Ticket>();

            foreach (List<Ticket> tickets in TicketsMap.Values)
                foreach(Ticket t in tickets)
                    if (t.Username.Equals(username) 
                        && (t.TicketStatus == TicketStatus.Payed || t.TicketStatus == TicketStatus.Reserved))
                        userTickets.Add(t);

            return userTickets;
        }

        public static int generateId()
        {
            int id = 1;

            foreach (List<Ticket> tickets in TicketsMap.Values)
                foreach (Ticket ticket in tickets)
                    id++;

            return id;
        }

        public static Ticket findTicket(int id)
        {
            foreach (List<Ticket> tickets in TicketsMap.Values)
                foreach (Ticket ticket in tickets)
                    if (ticket.Id == id)
                        return ticket;

            return null;
        }

        public static List<Ticket> getTickets(DateTime date, long routeId)
        {
            List<Ticket> tickets = new List<Ticket>();
            string key = formTicketMapKey(date, routeId);
            if (TicketsMap.ContainsKey(key))
                foreach (Ticket t in TicketsMap[key])
                    if (t.TicketStatus == TicketStatus.Payed || t.TicketStatus == TicketStatus.Reserved)
                        tickets.Add(t);

            return tickets;
        }

        private static string formTicketMapKey(DateTime date, long routeId)
        {
            string datePart = date.ToString("dd_MM_yyyy");
            string routeIdPart = routeId.ToString();

            return datePart + ":" + routeIdPart;
        }
    }
}
