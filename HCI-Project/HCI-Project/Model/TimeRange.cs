using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class TimeRange
    {
        public DateTime Arrival { get; set; }
        public DateTime Depature { get; set; }

        public TimeRange(DateTime arrival, DateTime depature)
        {
            Arrival = arrival;
            Depature = depature;
        }
    }
}
