using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Model
{
    public class WagonItem
    {
        public string ImageSource { get; set; }
        public string Name { get; set; }

        public WagonItem(string name, string img)
        {
            Name = name;
            ImageSource = img;
        }

        public WagonItem()
        {
        }
    }
}
