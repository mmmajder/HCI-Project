using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project.Client
{
    /// <summary>
    /// Interaction logic for TimetableViewClientPage.xaml
    /// </summary>
    public partial class TimetableViewClientPage : Page
    {
        public static List<ScheduledRoute> Routes = new List<ScheduledRoute>();
        private static DateTime SearchedDate;
        private static string SearchedFrom;
        private static string SearchedTo;

        public TimetableViewClientPage()
        {
            InitializeComponent();
            FillData();
            DataContext = this;
        }

        private void FillData()
        {
            List<string> s = StationRepo.GetStationNames();
            fromLocationCombobox.ItemsSource = s;
            fromLocationCombobox.SelectedIndex = -1;

            toLocationCombobox.ItemsSource = StationRepo.GetStationNames();
            toLocationCombobox.SelectedIndex = -1;

        }


        private string GetLocationValue(ComboBox locationCombobox)
        {
            if (locationCombobox.SelectedIndex == -1)
            {
                return null;
            }
            int locationIndex = locationCombobox.SelectedIndex;
            var selectedItem = locationCombobox.Items[locationIndex];
            return selectedItem.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FromLoc.Text = "From: " + GetLocationValue(fromLocationCombobox);
            ToLoc.Text = "To: " + GetLocationValue(toLocationCombobox);
            TablePanel.Visibility = Visibility.Visible;
            DateTime? selectedDate = datePicker.SelectedDate;
            Routes.Clear();
            if (selectedDate.HasValue && GetLocationValue(fromLocationCombobox) != null && GetLocationValue(fromLocationCombobox) != null)
            {
                DateTime date = selectedDate.Value;
                SearchedDate = date;
                SearchedFrom = GetLocationValue(fromLocationCombobox);
                SearchedTo = GetLocationValue(toLocationCombobox);
                Routes = RouteService.GetScheduledRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox), date);
                dgrMain.ItemsSource = RouteService.GetRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox), date);
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);
                ScheduledRoute selectedScheduledRoute = Routes[i];
                ScheduledRouteWindow.setSelectedScheduledRoute(selectedScheduledRoute);
                ScheduledRouteWindow scheduledRouteWindow = new ScheduledRouteWindow();
                scheduledRouteWindow.Show();
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void firstBuyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScheduledRoute selectedScheduledRoute = getSelectedScheduledRoute();
                if (selectedScheduledRoute == null) return;

                Ticket ticket = createTicket(selectedScheduledRoute);
                if (ticket == null) return;

                TicketService.buyTicket(ticket);
                MessageBoxResult result = MessageBox.Show("You have succesfully bought ticket");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void firstResBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ScheduledRoute selectedScheduledRoute = getSelectedScheduledRoute();
                if (selectedScheduledRoute == null) return;

                Ticket ticket = createTicket(selectedScheduledRoute);
                if (ticket == null) return;

                TicketService.reserveTicket(ticket);
                MessageBox.Show("You have succesfully reserved ticket.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private Ticket createTicket(ScheduledRoute selectedScheduledRoute)
        {
            string seat = "1A"; //

            DateTime departureTime = selectedScheduledRoute.getDepartureTime(SearchedFrom).Value;
            DateTime departure = SearchedDate.AddHours(departureTime.Hour).AddMinutes(departureTime.Minute);

            if (!BuyResValidations(departure, selectedScheduledRoute, seat))
                return null;

            List<string> seats = new List<string>(); //
            seats.Add(seat); //
            double price = RouteRepo.getRoute(selectedScheduledRoute.RouteId).getPrice(SearchedFrom, SearchedTo); //
            User u = UserRepo.getLogged();

            return new Ticket(selectedScheduledRoute, SearchedDate, u.Username, seats, SearchedFrom, SearchedTo, price, departure);
        }

        private bool BuyResValidations(DateTime departure, ScheduledRoute selectedScheduledRoute, string seat = "1A")
        {
            if (departure > DateTime.Now.AddDays(5))
            {
                MessageBox.Show("You can buy/reserve tickets no more than 5 days in advance.", "Buying error");
            }
            else if (departure < DateTime.Now)
            {
                MessageBox.Show("You can not buy/reserve tickets for the trains that have left already.");
            }
            else if (!TicketService.doesFreeSeatExists(SearchedDate, selectedScheduledRoute.id))
            {
                MessageBox.Show("You can buy/reserve tickets no more than 5 days in advance.");
            }
            else if (TicketService.isSeatTaken(SearchedDate, selectedScheduledRoute.id, seat))
            {
                MessageBox.Show("Chosen seat is already taken. Please choose another one.");
            }
            else
                return true;

            return false;
        }

        private ScheduledRoute getSelectedScheduledRoute()
        {
            int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);
            
            if (i != -1)
                return Routes[i];

            MessageBox.Show("Please, choose the table row first.");

            return null;
        }
    }
}
