using HCI_Project.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HCI_Project.Client
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Page
    {
        List<Ticket> Tickets = new List<Ticket>();
        public MyReservations()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            Tickets = TicketService.getUserReservedTickets(UserRepo.getLogged().Username);
            dgrMain.ItemsSource = Tickets;
        }

        private void BuyBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ticket selectedTicket = getSelectedTicket();
                if (selectedTicket == null)
                {
                    ticketNotChoosenLbl.Visibility = Visibility.Visible;
                    return;
                }
                ticketNotChoosenLbl.Visibility = Visibility.Hidden;

                TicketService.buyTicket(selectedTicket.Id);
                loadData();

                MessageBox.Show("Thank you for buying the ticket you have previously bought. You can see all your bought tickets in \"MY TICKETS\" tab.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Ticket selectedTicket = getSelectedTicket();
                if (selectedTicket == null)
                {
                    ticketNotChoosenLbl.Visibility = Visibility.Visible;
                    return;
                }
                ticketNotChoosenLbl.Visibility = Visibility.Hidden;

                TicketService.cancelTicketUser(selectedTicket.Id);
                loadData();

                MessageBox.Show("You have canceled reservation for your ticket.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private Ticket getSelectedTicket()
        {
            int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);

            if (i != -1)
                return Tickets[i];

            MessageBox.Show("Please, choose the table row first.");

            return null;
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("MyReservations");
        }
    }
}
