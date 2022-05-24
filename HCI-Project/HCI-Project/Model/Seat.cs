using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Seat
    {
        public int Number { get; set; }
        public Coordinate Coordinate { get; set; }
        public SeatRotation SeatRotation { get; set; }
        public int SeatClass { get; set; }

        public Seat(int number, Coordinate coordinate, SeatRotation seatRotation, int seatClass)
        {
            Number = number;
            Coordinate = coordinate;
            SeatRotation = seatRotation;
            SeatClass = seatClass;
        }
    }
}
