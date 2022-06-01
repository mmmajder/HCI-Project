using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class Station
    {
        public string Name { get; set; }
        public Boolean IsActive { get; set; }
        public Location Position { get; set; }

        public Station(string name, bool isActive, Location position)
        {
            Name = name;
            IsActive = isActive;
            Position = position;
        }
    }
}
