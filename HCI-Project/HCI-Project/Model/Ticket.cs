using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Ticket
    {
        public TicketStatus TicketStatus;
        public Route Route;

        public Ticket(TicketStatus ticketStatus, Route route)
        {
            TicketStatus = ticketStatus;
            Route = route;
        }
    }
}
