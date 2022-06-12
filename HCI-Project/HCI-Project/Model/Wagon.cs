using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Wagon
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public int RowCount { get; set; }
        public int ColCount { get; set; }

        public Wagon(long id, string name, int rowCount, int colCount)
        {
            Id = id;

            RowCount = rowCount;
            ColCount = colCount;

            Name = name;
        }

        public List<Seat> GetSeats()
        {
            int seatNum = RowCount * ColCount;
            List<Seat> seats = new List<Seat>();
            for (int i = 0; i < seatNum; i++)
            {
                seats.Add(new Seat(i + 1));
            }
            return seats;
        }
    }
}
