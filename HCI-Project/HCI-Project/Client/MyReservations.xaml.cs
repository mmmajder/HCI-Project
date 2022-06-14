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

        public void RefreshData()
        {
            loadData();
        }

        private void PreviewBtn_Click(object sender, RoutedEventArgs e)
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

                openTicketPreview(selectedTicket);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void openTicketPreview(Ticket ticket)
        {
            TicketPreviewPage preview = new TicketPreviewPage(getCurrentClientWindow().Main.Content, ticket, true, false, RefreshData);
            preview.Visibility = Visibility.Visible;
            getCurrentClientWindow().Main.Content = preview;
        }

        private ClientWindow getCurrentClientWindow()
        {
            return Application.Current.Windows.OfType<ClientWindow>().SingleOrDefault(x => x.IsActive);
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

                selectedTicket.TicketStatus = TicketStatus.Payed;
                //TicketService.buyTicket(selectedTicket.Id);
                loadData();

                MyMessageBox popup = new MyMessageBox("Thank you for buying the ticket you have previously reserved.", this, true);
                popup.ShowDialog();
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

                //TicketService.cancelTicketUser(selectedTicket.Id);
                selectedTicket.TicketStatus = TicketStatus.UserCanceled;
                loadData();

                MyMessageBox popup = new MyMessageBox("You have canceled reservation for your ticket.", this, true);
                popup.ShowDialog();
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

            MyMessageBox popup = new MyMessageBox("Please, choose the table row first.", this, false);
            popup.ShowDialog();

            return null;
        }
        public void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("MyReservations");
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
