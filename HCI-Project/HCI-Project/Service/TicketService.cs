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
    }
}
