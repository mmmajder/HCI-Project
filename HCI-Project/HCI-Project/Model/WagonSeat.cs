using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    class WagonSeat
    {
        public string Name { get; set; }
        public int WagonNum { get; set; }
        public int SeatNum { get; set; }

        public WagonSeat() { }

        public WagonSeat(string name) 
        {
            Name = name;
            string[] splits = name.Split('_');
            WagonNum = int.Parse(splits[0]);
            SeatNum = int.Parse(splits[1]);
        }
    }
}
