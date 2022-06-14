using HCI_Project.Client;
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

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void LinesViewSelected(object sender, RoutedEventArgs e)
        {
            Main.Content = new LinesViewPage(null);
        }

        private void HomeViewSelected(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomeViewPage();
        }

        private void TimetableBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new TimetableViewClientPage();
        }

        private void MyTicketsBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MyTicketsPage();
        }
        
        private void MyReservationsBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new MyReservations();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();

        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            DemoClient window = new DemoClient();
            window.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                if (Main.Content is MyReservations)
                {
                    MyReservations pricesPage = new MyReservations();
                    pricesPage.Help_Click(sender, e);
                }
                if (Main.Content is MyTicketsPage)
                {
                    MyTicketsPage linesViewPage = new MyTicketsPage();
                    linesViewPage.Help_Click(sender, e);
                }
                if (Main.Content is ScheduledRouteWindow)
                {
                    ScheduledRouteWindow trains = new ScheduledRouteWindow();
                    trains.Help_Click(sender, e);
                }

                if (Main.Content is TimetableViewClientPage)
                {
                    TimetableViewClientPage pricesPage = new TimetableViewClientPage();
                    pricesPage.Help_Click(sender, e);
                }
            }
        }

    }
}
