using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Seat : WagonItem
    {
        // public int Number { get; set; }
        //public Coordinate Coordinate { get; set; }
        public SeatRotation SeatRotation { get; set; }
        public int SeatClass { get; set; }


        public Seat(SeatRotation seatRotation, int seatClass) : base("seat", "/images/seat_icon_left.png")
        {
            // Number = number;
            // Coordinate = coordinate;
            SeatRotation = seatRotation;
            SeatClass = seatClass;
        }
    }
    
    public class WagonTable : WagonItem
    {
        public WagonTable() : base("table", "/images/seat_icon_left.png")
        {

        }
    }

    public class EmptySpace : WagonItem
    {
        public EmptySpace() : base("empty", "/images/seat_icon_left.png")
        {

        }
    }
}
