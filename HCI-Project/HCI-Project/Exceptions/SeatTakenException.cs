using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Exceptions
{
    public class SeatTakenException : Exception
    {
        public SeatTakenException() {}
    }

    public class NoFreeSeatsException : Exception
    {
        public NoFreeSeatsException() { }
    }
}
