using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public ScheduledRoute ScheduledRoute { get; set; }
        public String Username { get; set; }
        public List<string> Seats { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Price { get; set; }
        public DateTime Departure { get; set; }
        public string DepartureTimeStr { get; set; }
        public string DateStr { get; set; }

        public Ticket(ScheduledRoute scheduledroute, DateTime date, String username, List<string> seats, string from, string to, double price, DateTime departure)
        {
            ScheduledRoute = scheduledroute;
            Username = username;
            Seats = seats;
            From = from;
            To = to;
            Price = price;
            Departure = departure;
            DepartureTimeStr = Departure.ToString("HH:mm");
            DateStr = Departure.ToString("dd.MM.yyyy.");
        }
    }
}
