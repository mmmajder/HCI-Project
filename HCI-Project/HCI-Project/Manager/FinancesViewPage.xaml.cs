using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.Service;
using LiveCharts;
using LiveCharts.Wpf;
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

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }

        private List<Report> ReportsForGraph;
        private int GraphType;


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
            ClearRouteBtn.Visibility = Visibility.Visible;

            if (SelectedRoute == null)
            {
                emptyScheduledRoutesData();
                return;
            }

            changeLblAndCBColors(Colors.White);
            fillScheduledRoutesData();
            setRouteIsRunningOnDays();
        }
        
        private void ScheduledRouteSelected_Click(object sender, RoutedEventArgs e)
        {
            ClearScheduleBtn.Visibility = Visibility.Visible;
            setRouteIsRunningOnDays();
        }

        private void setRouteIsRunningOnDays()
        {
            routeIsRunningTB.Visibility = Visibility.Hidden;
            varRouteIsRunningTB.Visibility = Visibility.Hidden;

            if (routeCB.SelectedIndex != -1)
            {
                varRouteIsRunningTB.Text = ((Route)routeCB.SelectedItem).getRepeatingDays();
                routeIsRunningTB.Visibility = Visibility.Visible;
                varRouteIsRunningTB.Visibility = Visibility.Visible;
            }
            

            if (scheduleCB.SelectedIndex != -1)
                varRouteIsRunningTB.Text = ((ScheduledRoute)scheduleCB.SelectedItem).getRepeatingDays();
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

        private void ClearRoute_Click(object sender, RoutedEventArgs e)
        {
            scheduleCB.SelectedIndex = -1;
            ClearScheduleBtn.Visibility = Visibility.Hidden;
            routeCB.SelectedIndex = -1;
            ClearRouteBtn.Visibility = Visibility.Collapsed;
            emptyScheduledRoutesData();
            setRouteIsRunningOnDays();
        }

        private void ClearShcedule_Click(object sender, RoutedEventArgs e)
        {
            scheduleCB.SelectedIndex = -1;
            ClearScheduleBtn.Visibility = Visibility.Hidden;
            setRouteIsRunningOnDays();
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

            reportGrid.Visibility = Visibility.Visible;

            List<Report> reports = new List<Report>();
            Report combinedReport = new Report();
            int chartType = 1; // line graph

            if (routeCB.SelectedIndex == -1)
            { // reports for all
                if (MonthRB.IsChecked.Value)
                {
                    reports = TicketService.getReport(getSelectedMonthIndex());
                    combinedReport = TicketService.combineReports(monthsCB.SelectedItem.ToString(), reports);
                    dgrMain.ItemsSource = TicketService.getReportTickets(getSelectedMonthIndex());
                }
                else
                {
                    combinedReport = TicketService.getReport(datePicker.SelectedDate.Value);
                    reports = TicketService.getPastMonthsReports(datePicker.SelectedDate.Value);
                    chartType = 2;
                    dgrMain.ItemsSource = TicketService.getReportTickets(datePicker.SelectedDate.Value);
                }
            }
            else if (scheduleCB.SelectedIndex == -1)
            { // reports for Route
                if (MonthRB.IsChecked.Value)
                {
                    reports = TicketService.getReport((Route) routeCB.SelectedItem, getSelectedMonthIndex());
                    combinedReport = TicketService.combineReports(monthsCB.SelectedItem.ToString(), reports);
                    dgrMain.ItemsSource = TicketService.getReportTickets((Route)routeCB.SelectedItem, getSelectedMonthIndex());
                }
                else
                {
                    combinedReport = TicketService.getReport((Route)routeCB.SelectedItem, datePicker.SelectedDate.Value);
                    reports = TicketService.getPastMonthsReports((Route)routeCB.SelectedItem, datePicker.SelectedDate.Value);
                    chartType = 2;
                    dgrMain.ItemsSource = TicketService.getReportTickets((Route)routeCB.SelectedItem, datePicker.SelectedDate.Value);
                }
            }
            else
            { // reports for ScheduledRoute
                if (MonthRB.IsChecked.Value)
                {
                    reports = TicketService.getReport((ScheduledRoute) scheduleCB.SelectedItem, getSelectedMonthIndex());
                    combinedReport = TicketService.combineReports(monthsCB.SelectedItem.ToString(), reports);
                    dgrMain.ItemsSource = TicketService.getReportTickets((ScheduledRoute)scheduleCB.SelectedItem, getSelectedMonthIndex());
                } 
                else
                {
                    combinedReport = TicketService.getReport((ScheduledRoute)scheduleCB.SelectedItem, datePicker.SelectedDate.Value);
                    reports = TicketService.getPastMonthsReports((ScheduledRoute)scheduleCB.SelectedItem, datePicker.SelectedDate.Value);
                    chartType = 2;
                    dgrMain.ItemsSource = TicketService.getReportTickets((ScheduledRoute)scheduleCB.SelectedItem, datePicker.SelectedDate.Value);
                }
            }

            setShowingDataFor(combinedReport.Group);
            setGraphTitle(chartType);
            setReportsCharts(reports, chartType);
            showCombinedReport(combinedReport);

            dateNotChosenErrorLbl.Visibility = Visibility.Collapsed;
            monthNotChosenErrorLbl.Visibility = Visibility.Collapsed;
        }

        private void setShowingDataFor(string group)
        {
            string showingForStr = group;

            if (routeCB.SelectedIndex != -1)
                showingForStr += " - " + routeCB.SelectedItem;

            if (scheduleCB.SelectedIndex != -1)
                showingForStr += " - " + scheduleCB.SelectedItem;

            varShowingReportsForTB.Text = showingForStr;
        }

        private void setGraphTitle(int chartType)
        {
            if (chartType == 1)
                GraphTitleText.Text = "Progression for " + monthsCB.SelectedItem;
            else
                GraphTitleText.Text = "Three months comparison";
        }

        // chartssssssssssss vvvv
        //
        private void setReportsCharts(List<Report> reports, int type)
        {
            ReportsForGraph = reports;
            GraphType = type;
            setIncomeGraph();
        }

        private void setIncomeGraph()
        {
            List<int> values = new List<int>();
            List<string> labels = new List<string>();

            foreach (Report r in ReportsForGraph)
            {
                values.Add(r.Income);
                labels.Add(r.Group);
            }

            SetGraph("Income", values, labels);
        }

        private void setTicketsGraph()
        {
            List<int> values = new List<int>();
            List<string> labels = new List<string>();

            foreach (Report r in ReportsForGraph)
            {
                values.Add(r.NumOfTickets);
                labels.Add(r.Group);
            }

            SetGraph("Sold tickets", values, labels);
        }

        private void setSeatsGraph()
        {
            List<int> values = new List<int>();
            List<string> labels = new List<string>();

            foreach (Report r in ReportsForGraph)
            {
                values.Add(r.Seats);
                labels.Add(r.Group);
            }

            SetGraph("Sold seats", values, labels);
        }

        private void SetGraph(string title, List<int> numericValues, List<string> labels)
        {
            if (GraphType == 1)
                SetLineGraph(title, numericValues, labels);
            else
                SetBarGraph(title, numericValues, labels);
        }

        private void SetLineGraph(string title, List<int> numericValues, List<string> labels)
        {
            DataContext = null;

            ChartValues<int> vals = new ChartValues<int>();
            vals.AddRange(numericValues);

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = title,
                    Values = vals
                }
            };

            Labels = labels.ToArray();
            YFormatter = value => value.ToString("N");

            DataContext = this;
        }

        private void SetBarGraph(string title, List<int> numericValues, List<string> labels)
        {
            DataContext = null;

            ChartValues<int> vals = new ChartValues<int>();
            vals.AddRange(numericValues);

            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = title,
                    Values = vals
                }
            };

            Labels = labels.ToArray();
            YFormatter = value => value.ToString("N");

            DataContext = this;
        }

        // chartssss ^^^^

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

        private void IncomeBtn_Click(object sender, RoutedEventArgs e)
        {
            setIncomeGraph();
        }

        private void TicketsBtn_Click(object sender, RoutedEventArgs e)
        {
            setTicketsGraph();
        }

        private void SeatsBtn_Click(object sender, RoutedEventArgs e)
        {
            setSeatsGraph();
        }
    }
}
