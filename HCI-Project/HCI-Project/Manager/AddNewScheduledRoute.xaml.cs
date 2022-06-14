using HCI_Project.DTO;
using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.Service;
using HelpSistem;
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

namespace HCI_Project.Manager
{
    /// <summary>
    /// Interaction logic for AddNewScheduledRoute.xaml
    /// </summary>
    /// 

    public delegate void AddItemHandler(object sender, ScheduledRoute itemToAdd);

    public partial class AddNewScheduledRoute : Window
    {
        private string fromLoc;
        private string toLoc;
        private List<Route> availableRoutes;
        private static List<int> selectedDays;
        private static List<ScheduledStation> editedScheduledStations;
        private static ScheduledStation selctedScheduledStation;
        private static Route selectedRoute;

        public static RoutedUICommand HelpCommand = new RoutedUICommand("HelpCommand", "HelpCommand", typeof(AddNewScheduledRoute));

        public event AddItemHandler AddItem;

        public AddNewScheduledRoute(string from, string to)
        {
            InitializeComponent();
            fromLoc = from;
            toLoc = to;
            FillCombobox();
            selectedDays = new List<int>();
            editedScheduledStations = new List<ScheduledStation>();
            //FillTable();
            HelpCommand.InputGestures.Add(new KeyGesture(Key.F1, ModifierKeys.Control));
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void FillTable(Route selectedRoute)
        {
            editedScheduledStations.Clear();
            List<EditRouteRowDTO> tableData = new List<EditRouteRowDTO>();

            foreach (Station station in selectedRoute.Stations)
            {
                tableData.Add(new EditRouteRowDTO { Station = station.Name, ArrivalTime = "00:00", DepatureTime = "00:00" });
                editedScheduledStations.Add(new ScheduledStation(station, new TimeRange()));
            }
            dgScheduledRoute.ItemsSource = tableData;
        }

        private void FillCombobox()
        {
            availableRoutes = RouteService.findRoutesOnPath(fromLoc, toLoc);
            routeCombobox.ItemsSource = RouteService.findRoutePaths(fromLoc, toLoc);
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("AddScheduledRoute");
        }

        private void Help_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void table_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = dgScheduledRoute.Items.IndexOf(dgScheduledRoute.SelectedItem);
            if (i>=0)
            {
                selctedScheduledStation = editedScheduledStations[i];
            }
        }

        public void CellChanged(object sender, DataGridCellEditEndingEventArgs e)
        {
            int ColumnIndex = e.Column.DisplayIndex;

            TextBox t = e.EditingElement as TextBox;
            string editedCellValue = t.Text.ToString();
            

            if (ColumnIndex == 0)
            {
                //error
            }
            try
            {
                if (ColumnIndex == 1)
                {
                    //int date = dayOfTrip(DateTime.Parse(editedCellValue));
                    selctedScheduledStation.TimeRange.Arrival = dayOfTrip(new DateTime(1, 1, 1) + DateTime.Parse(editedCellValue).TimeOfDay);
                }
                if (ColumnIndex == 2)
                {
                    selctedScheduledStation.TimeRange.Depature = dayOfTrip(new DateTime(1, 1, 1) + DateTime.Parse(editedCellValue).TimeOfDay);
                }
            }
            catch (Exception)
            {

            }

        }
        private DateTime dayOfTrip(DateTime dateTime)
        {
            int i = dgScheduledRoute.Items.IndexOf(dgScheduledRoute.SelectedItem);
            if (i>0)
            {
                DateTime previousDate = editedScheduledStations[i - 1].TimeRange.Depature;

                if (previousDate.TimeOfDay > dateTime.TimeOfDay)
                {
                    return new DateTime(previousDate.Year, previousDate.Month, previousDate.Day + 1) + dateTime.TimeOfDay;
                }
                return new DateTime(previousDate.Year, previousDate.Month, previousDate.Day) + dateTime.TimeOfDay;
            }
            return dateTime;
        }

        private Route GetRouteValue(ComboBox routeCombobox)
        {
            if (routeCombobox.SelectedIndex == -1)
            {
                return null;
            }
            int index = routeCombobox.SelectedIndex;
            return availableRoutes[index];
        }

        private void comboboxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedRoute = GetRouteValue(routeCombobox);
            FillTable(selectedRoute);
        }

        private void addDayOfWeek(int i)
        {
            if (!selectedDays.Contains(i))
            {
                selectedDays.Add(i);
            }
        }

        private void removeDayOfWeek(int i)
        {
            if (selectedDays.Contains(i))
            {
                selectedDays.Remove(i);
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
                ScheduledRoute scheduledRoute = new ScheduledRoute(ScheduledRouteRepo.GetNewScheduledRouteId(), editedScheduledStations, selectedRoute.Id, selectedDays, new List<DateTime>());
                ScheduledRouteRepo.AddScheduledRoute(scheduledRoute);
                selectedRoute.ScheduledRoutes.Add(scheduledRoute);
                AddItem(this, scheduledRoute);
                MyMessageBox popup = new MyMessageBox("You have successfully added scheduled route!", this, true);
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
            HelpProvider.ShowHelp("AddScheduledRoute");
        }
    }
}
