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
    /// Interaction logic for TicketPreviewPage.xaml
    /// </summary>
    public partial class TicketPreviewPage : Page
    {
        private Ticket Ticket { get; set; }
        private readonly object PreviousPage;
        private bool IsClient;
        private bool IsReservation;
        private readonly Action RefreshData;

        public TicketPreviewPage(object previousPage, Ticket ticket, bool isClient=true, bool isReservation = false, Action refreshData = null)
        {
            InitializeComponent();
            Ticket = ticket;
            PreviousPage = previousPage;
            IsClient = isClient;
            IsReservation = isReservation;
            RefreshData = refreshData;
            fillData();
            fillSeatsTable();
            setButtonsVisibility();
        }

        private void setButtonsVisibility()
        {
            if (IsClient)
            {
                if (Ticket.TicketStatus != TicketStatus.Payed && Ticket.TicketStatus != TicketStatus.Reserved)
                {
                    confirmBtn.Visibility = Visibility.Visible;
                }
                else if (Ticket.TicketStatus == TicketStatus.Reserved)
                {
                    buyBtn.Visibility = Visibility.Visible;
                }

                if (Ticket.TicketStatus == TicketStatus.Payed || Ticket.TicketStatus == TicketStatus.Reserved)
                {
                    cancelBtn.Visibility = Visibility.Visible;
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (Ticket.TicketStatus == TicketStatus.Reserved)
            {
                Ticket.TicketStatus = TicketStatus.UserCanceled;
                //TicketService.cancelTicketUser(Ticket.Id);
                MessageBox.Show("You have canceled reservation for your ticket.");
                GoBack();
            } 
            else if (Ticket.TicketStatus == TicketStatus.Payed)
            {
                if (Ticket.Departure < DateTime.Now.AddDays(1))
                {
                    MessageBox.Show("It is not possible to cancel the ticket less than 1 day prior to departure time.");
                    return;
                }
                Ticket.TicketStatus = TicketStatus.UserCanceled;
                //TicketService.cancelTicketUser(Ticket.Id);
                MessageBox.Show("You have canceled your ticket. We will refund your money in the shortest possible time.");
                GoBack();
            }

            
            return;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (IsReservation)
            {
                TicketService.reserveTicket(Ticket);
                MessageBox.Show("You have succesfully reserved ticket.");
                getCurrentClientWindow().Main.Content = new MyReservations();
            }
            else
            {
                TicketService.buyTicket(Ticket);
                MessageBox.Show("Thank you for buying the ticket you have previously bought. You can see all your bought tickets in \"MY TICKETS\" tab.");
                getCurrentClientWindow().Main.Content = new MyTicketsPage();
            }
        }

        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            if (Ticket.TicketStatus == TicketStatus.Reserved)
            {
                Ticket.TicketStatus = TicketStatus.Payed;
                //TicketService.buyTicket(Ticket.Id);
                MessageBox.Show("Thank you for buying the ticket you have previously bought. You can see all your bought tickets in \"MY TICKETS\" tab.");
                GoBack();
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            if (IsClient)
                getCurrentClientWindow().Main.Content = PreviousPage;
            else
                getCurrentManagertWindow().Main.Content = PreviousPage;

            if (RefreshData != null)
                RefreshData();
        }

        private ClientWindow getCurrentClientWindow()
        {
            return Application.Current.Windows.OfType<ClientWindow>().SingleOrDefault(x => x.IsActive);
        }

        private ManagerWindow getCurrentManagertWindow()
        {
            return Application.Current.Windows.OfType<ManagerWindow>().SingleOrDefault(x => x.IsActive);
        }

        private void fillData()
        {
            varDateTB.Text = Ticket.DateStr;
            varDepartureTB.Text = Ticket.Departure.ToString("HH:mm");
            varFromTB.Text = Ticket.From;
            varLastameTB.Text = UserRepo.getUser(Ticket.Username).Surname;
            varNameTB.Text = UserRepo.getUser(Ticket.Username).Name;
            varNumOfSeatsTB.Text = Ticket.Seats.Count.ToString();
            varPriceTB.Text = Ticket.Price.ToString();
            varStatusTB.Text = Ticket.TicketStatus.ToString();
            varToTB.Text = Ticket.To;
        }

        private void fillSeatsTable()
        {
            List<WagonSeat> wagonSeats = new List<WagonSeat>();

            foreach (string seat in Ticket.Seats)
                wagonSeats.Add(new WagonSeat(seat));

            dgrMain.ItemsSource = wagonSeats;
        }

        private void Page_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                GoBack();
            }
        }
    }
}
