using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Repo
{
    public class WagonRepo
    {
        private static List<Wagon> wagons = new List<Wagon>();
        public WagonRepo()
        {
            // List<Seat> allSeats = SeatRepo.GetSeats();

            WagonTable table = new WagonTable();
            Seat seat1 = new Seat(SeatRotation.East, 2);
            Seat seat2 = new Seat(SeatRotation.West, 2);
            Seat seat3 = new Seat(SeatRotation.West, 2);
            Seat seat4 = new Seat(SeatRotation.West, 2);
            EmptySpace empty = new EmptySpace();

            List<WagonItem> items = new List<WagonItem> { seat1, table, empty, seat2 };

            wagons.Add(new Wagon(0, 2, 2, items));
        }

        public static List<Wagon> GetWagons()
        {
            return wagons;
        }
    }
}
