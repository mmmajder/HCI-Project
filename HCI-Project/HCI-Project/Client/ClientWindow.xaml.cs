﻿using HCI_Project.Client;
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

        
    }
}
