using DragDropDemo.ViewModels;
using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Project.Service
{
    public class WagonService
    {
        public static WagonListingViewModel MapWagons(List<Wagon> wagons)
        {
            WagonListingViewModel mappedStations = new WagonListingViewModel();
            for (int i = 0; i < wagons.Count; i++)
            {
                mappedStations.AddWagon(new WagonViewModel(wagons[i], i));
            }
            return mappedStations;
        }
    }
}
