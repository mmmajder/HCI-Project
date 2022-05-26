using HCI_Project.DTO;
using HCI_Project.Model;
using HCI_Project.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

            Routes = new ObservableCollection<RouteTableDTO>();

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

        public ObservableCollection<RouteTableDTO> Routes
        {
            get;
            set;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FromLoc.Text = "From: " + GetLocationValue(fromLocationCombobox);
            ToLoc.Text = "To: " + GetLocationValue(toLocationCombobox);
            TablePanel.Visibility = Visibility.Visible;
            DateTime? selectedDate = datePicker.SelectedDate;
            if (selectedDate.HasValue && GetLocationValue(fromLocationCombobox) != null && GetLocationValue(fromLocationCombobox)!=null)
            {
                Routes.Clear();
                DateTime date = selectedDate.Value;
                foreach (RouteTableDTO elem in RouteRepo.GetRoutes(GetLocationValue(fromLocationCombobox), GetLocationValue(toLocationCombobox), date))
                {
                    Routes.Add(elem);
                }
            }

        }
    }
}
