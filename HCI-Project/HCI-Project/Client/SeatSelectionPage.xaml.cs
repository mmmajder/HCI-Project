using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
    /// Interaction logic for SeatSelectionPage.xaml
    /// </summary>
    public partial class SeatSelectionPage : Page
    {
        private readonly object PreviousPage;
        private Ticket Ticket;
        private List<Wagon> Wagons = new List<Wagon>();
        private Wagon Wagon;
        private List<string> TakenSeats;
        private ObservableCollection<string> SelectedSeats;
        private bool IsReservation;

        public SeatSelectionPage(object previousPage, Ticket ticket, bool isReservation = false)
        {
            InitializeComponent();
            PreviousPage = previousPage;
            Ticket = ticket;
            IsReservation = isReservation;
            getTakenSeats();
            fillWagons();
            ctor();
        }

        public void ctor()
        {
            SelectedSeats = new ObservableCollection<string>();
            SelectedSeats.CollectionChanged += listChanged;
        }

        private void listChanged(object sender, NotifyCollectionChangedEventArgs args)
        {
            dgrMain.ItemsSource = generateSeatsTable();

            if (SelectedSeats.Count > 0) 
            {
                AcceptSelectionBtn.Visibility = Visibility.Visible;
                stationErrorLbl.Visibility = Visibility.Collapsed;
            }
            else
            {
                AcceptSelectionBtn.Visibility = Visibility.Collapsed;
                stationErrorLbl.Visibility = Visibility.Visible;
            }
        }

        private List<WagonSeat> generateSeatsTable()
        {
            List<WagonSeat> wagonSeats = new List<WagonSeat>();

            foreach (string seat in SelectedSeats)
                wagonSeats.Add(new WagonSeat(seat));

            return wagonSeats;

        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            getCurrentClientWindow().Main.Content = PreviousPage;
        }

        private void AcceptSelection_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedSeats.Count > 0)
            {
                Ticket.TicketStatus = TicketStatus.Undefined;
                addSeatsToTicket();

                //if (IsReservation)
                //    Ticket.TicketStatus = TicketStatus.Reserved;
                //else
                //    Ticket.TicketStatus = TicketStatus.Payed;

                addPriceToTicket();
                openTicketPreview(Ticket);
                //TicketRepo.addTicket(Ticket);
                //GoBack();
            }
           else
            {
                AcceptSelectionBtn.Visibility = Visibility.Collapsed;
                stationErrorLbl.Visibility = Visibility.Visible;
            }
        }

        private void openTicketPreview(Ticket ticket)
        {
            TicketPreviewPage preview = new TicketPreviewPage(getCurrentClientWindow().Main.Content, ticket, true, IsReservation);
            preview.Visibility = Visibility.Visible;
            getCurrentClientWindow().Main.Content = preview;
        }

        private void addSeatsToTicket()
        {
            List<string> seats = new List<string>();
            seats.AddRange(SelectedSeats);
            Ticket.Seats = seats;
            Ticket.SeatsLength = seats.Count;
        }

        private void addPriceToTicket()
        {
            double price = RouteRepo.getRoute(Ticket.ScheduledRoute.RouteId).getPrice(Ticket.From, Ticket.To);
            price = price * Ticket.Seats.Count;
            Ticket.Price = price;
        }

        private ClientWindow getCurrentClientWindow()
        {
            return Application.Current.Windows.OfType<ClientWindow>().SingleOrDefault(x => x.IsActive);
        }


        private void fillWagons()
        {
            getWagons();
            List<int> wagonsI = new List<int>();

            int i = 1;
            foreach (Wagon w in Wagons)
            {
                wagonsI.Add(i);
                i++;
            }

            wagonCB.ItemsSource = wagonsI;
        }

        private void getWagons()
        {
            long routeId = Ticket.ScheduledRoute.RouteId;
            Route route = RouteRepo.getRoute(routeId);
            string trainType = route.TrainType;
            Wagons =  TrainRepo.GetWagons(trainType);
        }

        private void wagonCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (wagonCB.SelectedIndex != -1)
            {
                Wagon = Wagons[wagonCB.SelectedIndex];
                List<int> takenSeats = getTakenSeatsForWagon((int)wagonCB.SelectedItem);
                showSeats(takenSeats);
            }
        }

        private void getTakenSeats()
        {
            List<string> takenSeats = new List<string>();
            List<Ticket> tickets = TicketRepo.getTickets(Ticket.Date, Ticket.ScheduledRoute.id);

            foreach (Ticket t in tickets)
                takenSeats.AddRange(t.Seats);

            TakenSeats = takenSeats;
        }

        private List<int> getTakenSeatsForWagon(int wagonId)
        {
            List<int> seatNums = new List<int>();

            foreach (string seat in TakenSeats)
            {
                string[] splits = seat.Split('_');
                string wagon = splits[0];
                string seatNum = splits[1];

                if (int.Parse(wagon) == wagonId)
                    seatNums.Add(int.Parse(seatNum));

            }

            return seatNums;
        }

        private void showSeats(List<int> takenSeats)
        {
            SeatsView seatsView = new SeatsView(Wagon.RowCount, Wagon.ColCount, takenSeats, true, SelectedSeats, (int)wagonCB.SelectedItem);

            seatsContainter.Children.Clear();
            seatsContainter.Children.Add(seatsView);
            seatsContainter.Visibility = Visibility.Visible;
        }
    }
}
