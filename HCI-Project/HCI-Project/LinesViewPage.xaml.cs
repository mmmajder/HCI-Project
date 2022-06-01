using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.Service;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for LinesViewPage.xaml
    /// </summary>
    public partial class LinesViewPage : Page
    {        
        private readonly List<Station> AllStations = StationRepo.GetStations();
        private readonly List<string> AllStationNames = StationRepo.GetStationNames();
        public LinesViewPage()
        {
            InitializeComponent();
            MapInit();
            ComboBoxInit();

            MapService.AddPushPins(AllStations, MouseRightButtonDownEvent, myMap);
            SetControlsVisible();

        }

        private void ComboBoxInit()
        {
            //fromLocationCombobox.FilterMode = AutoCompleteFilterMode.Contains;
            fromLocationCombobox.ItemsSource = AllStationNames;

            toLocationCombobox.ItemsSource = AllStationNames;
            toLocationCombobox.SelectedIndex = -1;
            toLocationCombobox.IsTextSearchEnabled = true;
            toLocationCombobox.IsEditable = true;
            toLocationCombobox.IsTextSearchCaseSensitive = false;

        }


        private void MapInit()
        {
            myMap.Center = new Location(45.267136, 19.833549); // Novi Sad
            myMap.ZoomLevel = 11;
        }

        

        private void MouseRightButtonDownEvent(object sender, RoutedEventArgs e)
        {
            if (sender is Pushpin pushpin)
            {
                MessageBox.Show(string.Format("Pushpin {0} was clicked", pushpin.Content.ToString()));
            }
        }

        
        private void FromCombobox_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine(fromLocationCombobox.SelectedItem);
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine(fromLocationCombobox.SelectedItem);
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Route route = (Route)GetComboboxValue(routesCombobox);

            Console.WriteLine(route);
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {

            Console.WriteLine(fromLocationCombobox.SelectedItem);
        }

        private Object GetComboboxValue(ComboBox myCombobox)
        {
            if (myCombobox.SelectedIndex == -1)
            {
                return null;
            }
            int locationIndex = myCombobox.SelectedIndex;
            var selectedItem = myCombobox.Items[locationIndex];
            return selectedItem;
        }


        private void Show_Click(object sender, RoutedEventArgs e)
        {
            string fromLocation = GetComboboxValue(fromLocationCombobox).ToString();
            string toLocation = GetComboboxValue(toLocationCombobox).ToString();

            if (fromLocation != null && toLocation != null)
            {
                List<Route> routes = RouteService.GetRoutes(fromLocation, toLocation);
                MapService.DrawMapPolygon(routes, myMap);
                routesCombobox.ItemsSource = routes;
            }
        }

        private void SetControlsVisible()
        {
            bool isManagerLogged = true;
            if (isManagerLogged)
            {
                routesLbl.Visibility = Visibility.Visible;
                routesCombobox.Visibility = Visibility.Visible;

                addRoute.Visibility = Visibility.Visible;
                editRoute.Visibility = Visibility.Visible;
                removeRoute.Visibility = Visibility.Visible;
            }
        }

    }
}
