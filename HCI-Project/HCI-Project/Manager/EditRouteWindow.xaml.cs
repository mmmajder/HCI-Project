using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using HCI_Project.DTO;
using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.util;
using Syncfusion.Windows.Controls.Input;

namespace HCI_Project.Manager
{
    /// <summary>
    /// Interaction logic for EditRouteWindow.xaml
    /// </summary>
    public partial class EditRouteWindow : Window
    {
        public static ScheduledRoute SelectedScheduledRoute { get; set; }
        public static ScheduledStation SelectedScheduledStation { get; set; }
        public static DateTime start;
        public static DateTime to;
        //public SfTimePicker DepatureTime;
        // public TimeSpan DepatureTime;
        /*private TimeSpan _DepatureTime;
        public TimeSpan DepatureTime
        {
            get { return _DepatureTime; }
            set
            {
                if (_DepatureTime != value)
                {
                    _DepatureTime = value;
                    OnPropertyChanged("DepatureTime");
                }
            }
        }
        */
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public EditRouteWindow()
        {
            InitializeComponent();
            FillData();
        }

        private void FillData()
        {
            FillCheckboxes();
            FillTable();
        }

        private void FillTable()
        {
            List<EditRouteRowDTO> tableData = new List<EditRouteRowDTO>();
            foreach (ScheduledStation scheduledStation in SelectedScheduledRoute.Stations)
            {
                tableData.Add(new EditRouteRowDTO { Station = scheduledStation.Station.Name, ArrivalTime = scheduledStation.TimeRange.Arrival.ToString("HH:mm"), DepatureTime = scheduledStation.TimeRange.Depature.ToString("HH:mm") });
            }
            dgRoute.ItemsSource = tableData;
        }

        internal static void setSelectedRoute(ScheduledRoute slectedSRoute)
        {
            SelectedScheduledRoute = slectedSRoute;
        }


        private void FillCheckboxes()
        {
            if (SelectedScheduledRoute.RepeatigDays.Contains(1))
                Mon.IsChecked = true;
            if (SelectedScheduledRoute.RepeatigDays.Contains(2))
                Tue.IsChecked = true;
            if (SelectedScheduledRoute.RepeatigDays.Contains(3))
                Wed.IsChecked = true;
            if (SelectedScheduledRoute.RepeatigDays.Contains(4))
                Thu.IsChecked = true;
            if (SelectedScheduledRoute.RepeatigDays.Contains(5))
                Fri.IsChecked = true;
            if (SelectedScheduledRoute.RepeatigDays.Contains(6))
                Sat.IsChecked = true;
            if (SelectedScheduledRoute.RepeatigDays.Contains(7))
                Sun.IsChecked = true;
        }

        /*public void setTimeRange(string depature, string arrival)
        {
            DepatureTime.Value = DateTimeUtils.getDateTime(depature);
            ArrivalTime.Value = DateTimeUtils.getDateTime(arrival);
            //to = DateTimeUtils.getDateTime(arrival);

            //ArrivalTime.SetValue(new TimeSpan(04, 45, 00));
            //  DepatureTime.SetValue(DateTimeUtils.getDateTime(depature));

            //        StartTime =;

        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
                if (station != null)
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
