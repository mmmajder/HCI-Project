using HCI_Project.DTO;
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
    /// Interaction logic for TimetableViewClientPage.xaml
    /// </summary>
    public partial class TimetableViewClientPage : Page
    {
        public static List<ScheduledRoute> Routes = new List<ScheduledRoute>();
        public TimetableViewClientPage()
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
            FromLoc.Text = "From: " + GetLocationValue(fromLocationCombobox);
            ToLoc.Text = "To: " + GetLocationValue(toLocationCombobox);
            DateTime? selectedDate = datePicker.SelectedDate;
            Routes.Clear();
            if (selectedDate.HasValue && GetLocationValue(fromLocationCombobox) != null && GetLocationValue(fromLocationCombobox) != null)
            {
                TablePanel.Visibility = Visibility.Visible;
                DateTime date = selectedDate.Value;
                Routes = RouteService.GetScheduledRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox), date);
                List<RouteTableDTO> data = RouteService.GetRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox), date);
                dgrMain.ItemsSource = data;
                if (data.Count==0)
                {
                    MessageBox.Show("There are no scheduled routes betweeen these stations on selected date!");
                }
            } else
            {
                MessageBox.Show("Please input valid values for start and end station and date");
            }
        }

        private void btnView_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);
                ScheduledRoute slectedScheduledRoute = Routes[i];
                ScheduledRouteWindow.setSelectedScheduledRoute(slectedScheduledRoute);
                ScheduledRouteWindow scheduledRouteWindow = new ScheduledRouteWindow();
                scheduledRouteWindow.Show();
                //This is the code which will show the button click row data. Thank you.
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("TimetableClient");
        }
    }
}
