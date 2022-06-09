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

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for FinancesViewPage.xaml
    /// </summary>
    public partial class FinancesViewPage : Page
    {
        List<Route> Routes;
        Route SelectedRoute;

        List<ScheduledRoute> ScheduledRoutes;
        ScheduledRoute SelectedScheduledRoute;

        int SelectedMonth = -1;
        bool IsDateSelected = false;

        public FinancesViewPage()
        {
            InitializeComponent();
            fillRoutesData();
            fillMonths();
        }

        private void fillMonths()
        {
            List<String> months = TicketService.getMonths();
            monthsCB.ItemsSource = months;
        }

        private void fillRoutesData()
        {
            Routes = RouteRepo.GetRoutes();
            routeCB.ItemsSource = Routes;
        }

        private void fillScheduledRoutesData()
        {
            ScheduledRoutes = SelectedRoute.ScheduledRoutes;
            scheduleCB.ItemsSource = ScheduledRoutes;
        }

        private void emptyScheduledRoutesData()
        {
            ScheduledRoutes = null;
            scheduleCB.ItemsSource = null;
            scheduleCB.Items.Clear();
            changeLblAndCBColors(Colors.LightSlateGray);
            ClearScheduleBtn.Visibility = Visibility.Hidden;
        }

        private void RouteSelected_Click(object sender, RoutedEventArgs e)
        {
            SelectedRoute = (Route) routeCB.SelectedItem;
            ClearScheduleBtn.Visibility = Visibility.Hidden;

            if (SelectedRoute == null)
            {
                emptyScheduledRoutesData();
                return;
            }

            changeLblAndCBColors(Colors.White);
            fillScheduledRoutesData();
        }
        
        private void ScheduledRouteSelected_Click(object sender, RoutedEventArgs e)
        {
            ClearScheduleBtn.Visibility = Visibility.Visible;
        }
        

        private void MonthSelected_Click(object sender, RoutedEventArgs e)
        {
            SelectedMonth = monthsCB.SelectedIndex + 1;
            MonthRB.IsChecked = true;
            // TODO getData for that route/scheduledRoute for that month
        }

        private void DateSelected_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedD = datePicker.SelectedDate;
            IsDateSelected = true;
            DateRB.IsChecked = true;
            // TODO getData for that route/scheduledRoute for that date
        }

        private void ClearShcedule_Click(object sender, RoutedEventArgs e)
        {
            scheduleCB.SelectedIndex = -1;
            ClearScheduleBtn.Visibility = Visibility.Hidden;
        }

        private void MonthRadioBtn_Check(object sender, RoutedEventArgs e)
        {
            dateNotChosenErrorLbl.Visibility = Visibility.Collapsed;
            if (monthsCB.SelectedIndex == -1)
                monthNotChosenErrorLbl.Visibility = Visibility.Visible;
            else
                monthNotChosenErrorLbl.Visibility = Visibility.Collapsed;
            // TODO getData for that route/scheduledRoute for that month
        }

        private void DateRadioBtn_Check(object sender, RoutedEventArgs e)
        {
            monthNotChosenErrorLbl.Visibility = Visibility.Collapsed;
            if (datePicker.SelectedDate == null)
                dateNotChosenErrorLbl.Visibility = Visibility.Visible;
            else
                dateNotChosenErrorLbl.Visibility = Visibility.Collapsed;
            // TODO getData for that route/scheduledRoute for that date
        }

        private void changeLblAndCBColors(Color color)
        {
            scheduleLbl.Foreground = new SolidColorBrush(color);
            scheduleCB.Background = new SolidColorBrush(color);
        }

    }
}
