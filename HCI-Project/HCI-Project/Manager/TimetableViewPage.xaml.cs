using HCI_Project.DTO;
using HCI_Project.Manager;
using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
    /// Interaction logic for TimetableViewPage.xaml
    /// </summary>
    public partial class TimetableViewPage : Page
    {
        public static List<Route> Routes = new List<Route>();
        public TimetableViewPage()
        {
            InitializeComponent();
            FillData();
            DataContext = this;
        }

        private void FillData()
        {
            List<string> s = StationRepo.GetStationNames();
            fromLocationCombobox.ItemsSource = s;
            fromLocationCombobox.SelectedIndex = -1;

            toLocationCombobox.ItemsSource = StationRepo.GetStationNames();
            toLocationCombobox.SelectedIndex = -1;

        }


        private string GetLocationValue(ComboBox locationCombobox)
        {
            if (locationCombobox.SelectedIndex == -1)
            {
                return null;
            }
            int locationIndex = locationCombobox.SelectedIndex;
            var selectedItem = locationCombobox.Items[locationIndex];
            return selectedItem.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string from = GetLocationValue(fromLocationCombobox);
            string to = GetLocationValue(toLocationCombobox);
            FromLoc.Text = "From: " + from;
            ToLoc.Text = "To: " + to;
            TablePanel.Visibility = Visibility.Visible;
            Routes.Clear();
            if (from != null && to != null)
            {
                Routes = RouteService.GetRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox));
                //dgrMain.ItemsSource = RouteService.GetRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox), date);
                dgrMain.ItemsSource = RouteService.GetRoutesTableData(Routes, from, to);
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);
                Route slectedSRoute = Routes[i];
                EditRouteWindow editRouteWindow = new EditRouteWindow();
                editRouteWindow.Show();
              /*  ScheduledRouteWindow.setSelectedScheduledRoute(slectedScheduledRoute);
                ScheduledRouteWindow scheduledRouteWindow = new ScheduledRouteWindow();
                scheduledRouteWindow.Show();*/
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
