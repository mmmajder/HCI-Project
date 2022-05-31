using HCI_Project.DTO;
using HCI_Project.Model;
using HCI_Project.Repo;
using System;
using System.Collections.Generic;
using System.Data;
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
        public static ScheduledStation SelectedScheduledStation { get; set; }

        public static List<String> ComboboxData { get; set; }

        public ScheduledRouteWindow()
        {
            InitializeComponent();
            dgRoute.ItemsSource = FillTable();
            ComboboxData = StationRepo.GetStationNames();
            DataContext = this;
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

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = dgRoute.Items.IndexOf(dgRoute.SelectedItem);
            SelectedScheduledStation = SelectedScheduledRoute.Stations[i];
        }

        public void CellChanged(object sender, DataGridCellEditEndingEventArgs e)
        {
            //TODO set new STATION
            int ColumnIndex = e.Column.DisplayIndex;

            TextBox t = e.EditingElement as TextBox;
            string editedCellValue = t.Text.ToString();

            if (ColumnIndex == 0)
            {
                Station station = StationRepo.GetStationByName(editedCellValue);
                if (station!=null)
                {
                    SelectedScheduledStation.Station = station;
                }
                else
                {
                    ///eror
                }
            }

            try
            {
                if (ColumnIndex == 1)
                {
                    SelectedScheduledStation.TimeRange.Arrival = DateTime.Parse(editedCellValue);
                }
                if (ColumnIndex == 2)
                {
                    SelectedScheduledStation.TimeRange.Depature = DateTime.Parse(editedCellValue);
                }
            }
            catch (Exception)
            {

            }
            
        }
    }
}
