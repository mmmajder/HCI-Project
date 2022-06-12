using HCI_Project.DTO;
using HCI_Project.Manager;
using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.Service;
using HelpSistem;
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
        public static List<ScheduledRoute> Routes = new List<ScheduledRoute>();
        public static ObservableCollection<RouteTableManagerDTO> TableData = new ObservableCollection<RouteTableManagerDTO>();
        public static string from;
        public static string to;
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
            from = GetLocationValue(fromLocationCombobox);
            to = GetLocationValue(toLocationCombobox);
            if (from != null && to != null)
            {
                FromLoc.Text = "From: " + from;
                ToLoc.Text = "To: " + to;
                TablePanel.Visibility = Visibility.Visible;
                Routes.Clear();
                Routes = RouteService.GetScheduledRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox));
                //dgrMain.ItemsSource = RouteService.GetRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox), date);
                TableData = RouteService.GetRoutesTableData(Routes, from, to);
                dgrMain.ItemsSource = TableData;
            } 
            else
            {
                MessageBox.Show("Please input valid values for start and end station");
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);
                ScheduledRoute slectedScheduledRoute = Routes[i];
                RouteTableManagerDTO selectedRow = TableData[i];

                EditRouteWindow.setSelectedRoute(slectedScheduledRoute);
                EditRouteWindow editRouteWindow = new EditRouteWindow();
                editRouteWindow.EditItem += new EditItemHandler(winEdit_EditItem);
                editRouteWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);
                ScheduledRoute slectedScheduledRoute = Routes[i];
                RouteTableManagerDTO selectedRow = TableData[i];
                TableData.Remove(selectedRow);
                Routes.Remove(slectedScheduledRoute);
                Route route = RouteService.FindRouteById(slectedScheduledRoute.RouteId);
                route.ScheduledRoutes.Remove(slectedScheduledRoute);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Select row to delete it");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddNewScheduledRoute addNewScheduledRoute = new AddNewScheduledRoute(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox));
            addNewScheduledRoute.AddItem += new AddItemHandler(winAdd_AddItem);
            addNewScheduledRoute.Show();
        }

        void winEdit_EditItem(object sender, ScheduledRoute itemToEdit)
        {
           // ScheduledRoute scheduledRoute = ScheduledRouteRepo.FindById(itemToEdit.id);
            for (int i=0; i<Routes.Count(); i++)
            {
                if (Routes[i].id == itemToEdit.id)
                {
                    Routes[i] = itemToEdit;
                    TableData[i] = new RouteTableManagerDTO { Depature=RouteService.GetDepature(itemToEdit, from).ToString("HH:mm"),
                        Arrival = RouteService.GetArrival(itemToEdit, to).ToString("HH:mm"),
                        Days =RouteService.GetDayNames(itemToEdit.RepeatigDays) };
                    break;
                }
            }
        }

        void winAdd_AddItem(object sender, ScheduledRoute itemToAdd)
        {
            string depature = RouteService.GetDepature(itemToAdd, from).ToString("HH:ss");
            string arrival = RouteService.GetArrival(itemToAdd, to).ToString("HH:ss");
            itemToAdd.RepeatigDays.Sort();
            string days = RouteService.GetDayNames(itemToAdd.RepeatigDays);
            RouteTableManagerDTO data = new RouteTableManagerDTO { Depature = depature, Arrival = arrival, Days = days };
            TableData.Add(data);
            Routes.Add(itemToAdd);
            dgrMain.ItemsSource = TableData;
        }
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("TimetableManager");
        }
    }
}
