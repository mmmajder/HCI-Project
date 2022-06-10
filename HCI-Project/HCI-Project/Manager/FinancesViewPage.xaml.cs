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
        }

        private void DateSelected_Click(object sender, RoutedEventArgs e)
        {
            DateTime? selectedD = datePicker.SelectedDate;
            IsDateSelected = true;
            DateRB.IsChecked = true;
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
        }

        private void DateRadioBtn_Check(object sender, RoutedEventArgs e)
        {
            monthNotChosenErrorLbl.Visibility = Visibility.Collapsed;
            if (datePicker.SelectedDate == null)
                dateNotChosenErrorLbl.Visibility = Visibility.Visible;
            else
                dateNotChosenErrorLbl.Visibility = Visibility.Collapsed;
        }

        private void GetReport_Click(object sender, RoutedEventArgs e)
        {
            if (routeCB.SelectedIndex == -1)
            {
                MessageBox.Show("To get a report you need first to choose a Route.");
                return;
            }
            if ( MonthRB.IsChecked == false && DateRB.IsChecked == false)
            {
                MessageBox.Show("To get a report you need first to choose Month or Date.");
                return;
            }
            if (DateRB.IsChecked == true && datePicker.SelectedDate == null)
            {
                dateNotChosenErrorLbl.Visibility = Visibility.Visible;
                return;
            }
            if (MonthRB.IsChecked == true && monthsCB.SelectedIndex == -1)
            {
                monthNotChosenErrorLbl.Visibility = Visibility.Visible;
                return;
            }

            List<Report> reports = new List<Report>();
            Report combinedReport = new Report();

            if (scheduleCB.SelectedIndex == -1)
            { // reports for Route
                if (MonthRB.IsChecked.Value)
                {
                    reports = TicketService.getReport((Route) routeCB.SelectedItem, getSelectedMonthIndex());
                    combinedReport = TicketService.combineReports(monthsCB.SelectedItem.ToString(), reports);
                }
                else
                {
                    combinedReport = TicketService.getReport((Route)routeCB.SelectedItem, datePicker.SelectedDate.Value);
                    reports = TicketService.getPastMonthsReports((Route)routeCB.SelectedItem, datePicker.SelectedDate.Value);
                }
            }
            else
            { // reports for ScheduledRoute
                if (MonthRB.IsChecked.Value)
                {
                    reports = TicketService.getReport((ScheduledRoute) scheduleCB.SelectedItem, getSelectedMonthIndex());
                    combinedReport = TicketService.combineReports(monthsCB.SelectedItem.ToString(), reports);
                } 
                else
                {
                    combinedReport = TicketService.getReport((ScheduledRoute)scheduleCB.SelectedItem, datePicker.SelectedDate.Value);
                    reports = TicketService.getPastMonthsReports((ScheduledRoute)scheduleCB.SelectedItem, datePicker.SelectedDate.Value);
                }
            }

            varShowingReportsForTB.Text = combinedReport.Group;
            setReportsCharts(reports);
            showCombinedReport(combinedReport);

            dateNotChosenErrorLbl.Visibility = Visibility.Collapsed;
            monthNotChosenErrorLbl.Visibility = Visibility.Collapsed;
        }

        private void setReportsCharts(List<Report> reports, int type)
        {
            if (type == 1) // for the month, line graph
            {

            }
            else // past months, barplot
            {

            }
        }

        private void showCombinedReport(Report report)
        {
            varIncomeLbl.Content = report.Income;
            varNumOfTicketsSoldLbl.Content = report.NumOfTickets;
            varSeatsSoldLbl.Content = report.Seats;
            reportGrid.Visibility = Visibility.Visible;
            return;
        }

        private int getSelectedMonthIndex()
        {
            return monthsCB.SelectedIndex + 1;
        }

        private void changeLblAndCBColors(Color color)
        {
            scheduleLbl.Foreground = new SolidColorBrush(color);
            scheduleCB.Background = new SolidColorBrush(color);
        }

    }
}
