using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class WagonType
    {
        public long Id { get; set; }
        public List<Seat> Seats { get; set; }

        public WagonType(long id, List<Seat> seats)
        {
            Id = id;
            Seats = seats;
        }
    }
}
