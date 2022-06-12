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

            /*WagonTable table = new WagonTable();
            Seat seat1 = new Seat(SeatRotation.East, 2);
            Seat seat2 = new Seat(SeatRotation.West, 2);
            Seat seat3 = new Seat(SeatRotation.West, 2);
            Seat seat4 = new Seat(SeatRotation.West, 2);
            EmptySpace empty = new EmptySpace();*/

            // List<WagonItem> items = new List<WagonItem> { seat1, table, empty, seat2 };

            wagons.Add(new Wagon(0, "Wagon 1", 2, 2));
            wagons.Add(new Wagon(1, "Wagon 2", 9, 9));
        }

        public static List<Wagon> GetWagons()
        {
            return wagons;
        }

        public static void RemoveWagon(Wagon wagonForDelete)
        {
            wagons.Remove(wagonForDelete);
        }

        public static void SaveChanges(Wagon selectedWagon)
        {
            foreach(Wagon wagon in wagons)
            {
                if (wagon.Name.Equals(selectedWagon.Name))
                {
                    wagon.RowCount = selectedWagon.RowCount;
                    wagon.ColCount = selectedWagon.ColCount;
                }
            }
        }

        internal static void AddWagon(Wagon wagon)
        {
            wagons.Add(wagon);
        }

        public static bool NameAlreadyExists(string name)
        {
            foreach (Wagon wagon in wagons)
            {
                if (wagon.Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
