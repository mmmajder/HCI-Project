using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    class Report
    {
        public int Income { get; set; }
        public int NumOfTickets { get; set; }
        public int Seats { get; set; }
        public string Group { get; set; }

        public Report() { }

        public Report(string group, int income, int numOfTickets, int seats) 
        {
            Group = group;
            Income = income;
            NumOfTickets = numOfTickets;
            Seats = seats;
        }
    }
}
