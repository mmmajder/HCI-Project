using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Ticket
    {
        public int Id;
        public TicketStatus TicketStatus;
        public ScheduledRoute ScheduledRoute;
        public DateTime Date;
        public String Username;
        public List<string> Seats;

        public Ticket(ScheduledRoute scheduledroute, DateTime date, String username, List<string> seats)
        {
            ScheduledRoute = scheduledroute;
            Date = date;
            Username = username;
            Seats = seats;
        }

        public Ticket(TicketStatus ticketStatus, ScheduledRoute scheduledroute, DateTime date, String username, List<string> seats)
        {
            TicketStatus = ticketStatus;
            ScheduledRoute = scheduledroute;
            Date = date;
            Username = username;
            Seats = seats;
        }
    }
}
