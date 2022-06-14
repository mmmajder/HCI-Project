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
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.util;
using HelpSistem;
using Syncfusion.Windows.Controls.Input;

namespace HCI_Project.Manager
{
    /// <summary>
    /// Interaction logic for EditRouteWindow.xaml
    /// </summary>
    /// 

    public delegate void EditItemHandler(object sender, ScheduledRoute itemToEdit);
    public partial class EditRouteWindow : Window
    {
        public static ScheduledRoute SelectedScheduledRoute { get; set; }
        public static ScheduledStation SelectedScheduledStation { get; set; }
        public static DateTime start;
        public static DateTime to;

        public static ScheduledRoute EditedValue { get; set; }

        public event EditItemHandler EditItem;


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
            setEditedValue();
        }

        private void setEditedValue()
        {
            List<ScheduledStation> scheduledStations = new List<ScheduledStation>();
            foreach (ScheduledStation scheduledStation in SelectedScheduledRoute.Stations)
            {
                scheduledStations.Add(scheduledStation);
            }
            EditedValue = new ScheduledRoute(SelectedScheduledRoute.id, scheduledStations, SelectedScheduledRoute.RouteId, SelectedScheduledRoute.RepeatigDays, SelectedScheduledRoute.NotWorkingDays);

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

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = dgRoute.Items.IndexOf(dgRoute.SelectedItem);
            SelectedScheduledStation = EditedValue.Stations[i];
        }

        public void CellChanged(object sender, DataGridCellEditEndingEventArgs e)
        {
            //TODO set new STATION
            int ColumnIndex = e.Column.DisplayIndex;

            TextBox t = e.EditingElement as TextBox;
            string editedCellValue = t.Text.ToString();

            if (ColumnIndex == 0)
            {
                Station newStation = StationRepo.GetStationByName(editedCellValue);
                if (newStation != null)
                {
                    SelectedScheduledStation.Station = newStation;
                /*    foreach (ScheduledStation scheduledStation in EditedValue.Stations)
                    {
                        if (scheduledStation.Station.Name == SelectedScheduledStation.Station.Name)
                        {
                            scheduledStation.Station = newStation;
                        }
                    }*/
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
                    SelectedScheduledStation.TimeRange.Arrival = dayOfTrip(new DateTime(1, 1, 1) + DateTime.Parse(editedCellValue).TimeOfDay);
                }
                if (ColumnIndex == 2)
                {
                    SelectedScheduledStation.TimeRange.Depature = dayOfTrip(new DateTime(1, 1, 1) + DateTime.Parse(editedCellValue).TimeOfDay);
                }
            }
            catch (Exception)
            {

            }

        }

        private DateTime dayOfTrip(DateTime dateTime)
        {
            int i = dgRoute.Items.IndexOf(dgRoute.SelectedItem);
            if (i > 0)
            {
                DateTime previousDate = EditedValue.Stations[i - 1].TimeRange.Depature;

                if (previousDate.TimeOfDay > dateTime.TimeOfDay)
                {
                    return new DateTime(previousDate.Year, previousDate.Month, previousDate.Day + 1) + dateTime.TimeOfDay;
                }
                return new DateTime(previousDate.Year, previousDate.Month, previousDate.Day) + dateTime.TimeOfDay;
            }
            return dateTime;
        }

        private void addDayOfWeek(int i)
        {
            if (!SelectedScheduledRoute.RepeatigDays.Contains(i))
            {
                SelectedScheduledRoute.RepeatigDays.Add(i);
            }
        }

        private void removeDayOfWeek(int i)
        {
            if (SelectedScheduledRoute.RepeatigDays.Contains(i))
            {
                SelectedScheduledRoute.RepeatigDays.Remove(i);
            }
        }

        private void MonChecked(object sender, RoutedEventArgs e)
        {
            addDayOfWeek(1);
        }

        private void MonUnchecked(object sender, RoutedEventArgs e)
        {
            removeDayOfWeek(1);
        }
        private void TueChecked(object sender, RoutedEventArgs e)
        {
            addDayOfWeek(2);
        }

        private void TueUnchecked(object sender, RoutedEventArgs e)
        {
            removeDayOfWeek(2);
        }
        private void WedChecked(object sender, RoutedEventArgs e)
        {
            addDayOfWeek(3);
        }

        private void WedUnchecked(object sender, RoutedEventArgs e)
        {
            removeDayOfWeek(3);
        }
        private void ThuChecked(object sender, RoutedEventArgs e)
        {
            addDayOfWeek(4);
        }

        private void ThuUnchecked(object sender, RoutedEventArgs e)
        {
            removeDayOfWeek(4);
        }
        private void FriChecked(object sender, RoutedEventArgs e)
        {
            addDayOfWeek(5);
        }

        private void FriUnchecked(object sender, RoutedEventArgs e)
        {
            removeDayOfWeek(5);
        }
        private void SatChecked(object sender, RoutedEventArgs e)
        {
            addDayOfWeek(6);
        }

        private void SatUnchecked(object sender, RoutedEventArgs e)
        {
            removeDayOfWeek(6);
        }
        private void SunChecked(object sender, RoutedEventArgs e)
        {
            addDayOfWeek(7);
        }

        private void SunUnchecked(object sender, RoutedEventArgs e)
        {
            removeDayOfWeek(7);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SelectedScheduledRoute = EditedValue;
                EditItem(this, SelectedScheduledRoute);
                MyMessageBox popup = new MyMessageBox("You have successfully editted scheduled route!", this, true);
                popup.ShowDialog();
                this.Close();
            }
            catch
            {
                MyMessageBox popup = new MyMessageBox("Please fill all necessary fields!", this, false);
                popup.ShowDialog();
            }
        }

        public void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("EditScheduledRouteManager");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
    public class DrAmtValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string input = value.ToString();
            try
            {
                DateTimeUtils.getTime(input);
            }
            catch
            {
                return new ValidationResult(false, "Invalid input.");
            }
            return new ValidationResult(true, null);
        }
    }
}
