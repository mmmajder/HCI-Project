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
    /// Interaction logic for MyTicketsPage.xaml
    /// </summary>
    public partial class MyTicketsPage : Page
    {
        List<Ticket> Tickets = new List<Ticket>();
        public MyTicketsPage()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            Tickets = TicketService.getUserPayedTickets(UserRepo.getLogged().Username);
            dgrMain.ItemsSource = Tickets;
        }

        public void RefreshData()
        {
            loadData();
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

                if (selectedTicket.Departure < DateTime.Now.AddDays(1))
                {
                    MessageBox.Show("It is not possible to cancel the ticket less than 1 day prior to departure time.");
                    return;
                }

                selectedTicket.TicketStatus = TicketStatus.UserCanceled;
                //TicketService.cancelTicketUser(selectedTicket.Id);
                loadData();

                MessageBox.Show("You have canceled your ticket. We will refund your money in the shortest possible time.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
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

        private Ticket getSelectedTicket()
        {
            int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);

            if (i != -1)
                return Tickets[i];

            MessageBox.Show("Please, choose the table row first.");

            return null;
        }
    }
}
