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
        public List<WagonItem> Seats { get; set; }

        public int RowCount { get; set; }
        public int ColCount { get; set; }

        public Wagon(long id, int rowCount, int colCount, List<WagonItem> seats)
        {
            Id = id;
            Seats = seats;

            RowCount = rowCount;
            ColCount = colCount;
        }
    }
}
