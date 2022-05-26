using HCI_Project.DTO;
using HCI_Project.Model;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for ScheduledRouteWindow.xaml
    /// </summary>
    public partial class ScheduledRouteWindow : Window
    {
        public static ScheduledRoute SelectedScheduledRoute { get; set; }

        public ScheduledRouteWindow()
        {
            InitializeComponent();
            dgUsers.ItemsSource = FillTable();
        }

        private static List<ScheduledListItemDTO> FillTable()
        {
            List<ScheduledListItemDTO> tableData = new List<ScheduledListItemDTO>();
            foreach (ScheduledStation scheduledStation in SelectedScheduledRoute.Stations)
            {
                tableData.Add(new ScheduledListItemDTO {Station = scheduledStation.Station.Name, Arrival = scheduledStation.TimeRange.Arrival.ToString("HH:mm"), Depature = scheduledStation.TimeRange.Depature.ToString("HH:mm") });
            }
            return tableData;
        }

        public static void setSelectedScheduledRoute(ScheduledRoute scheduled)
        {
            SelectedScheduledRoute = scheduled;
        }
    }
}
