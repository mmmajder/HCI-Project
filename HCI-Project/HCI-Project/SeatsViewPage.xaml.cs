using HCI_Project.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for SeatsViewPage.xaml
    /// </summary>
    public partial class SeatsViewPage : Page
    {
        public List<WagonItem> wagonItems { get; set; }
        public int SelectedPlatformIndex { get; set; }
        public SeatsViewPage()
        {

            InitializeComponent();

            wagonItems = new List<WagonItem>();
            

            WagonTable table = new WagonTable();
            Seat seat1 = new Seat(SeatRotation.East, 2);
            Seat seat2 = new Seat(SeatRotation.West, 2);
            Seat seat3 = new Seat(SeatRotation.West, 2);
            Seat seat4 = new Seat(SeatRotation.West, 2);
            EmptySpace empty = new EmptySpace();

            wagonItems.Add(seat1);
            wagonItems.Add(table);
            wagonItems.Add(empty);
            wagonItems.Add(seat2);


            seats.ItemsSource = wagonItems;
            dataGrid.ItemsSource = wagonItems;

        }
    }
}
