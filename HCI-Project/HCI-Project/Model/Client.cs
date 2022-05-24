using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Client : User
    {
        public List<Ticket> Tickets { get; set; }

        public Client(List<Ticket> tickets, UserType userType, string name, string surname, string username, string password) : base(userType, name, surname, username, password)
        {
            Tickets = tickets;
        }


    }
}
