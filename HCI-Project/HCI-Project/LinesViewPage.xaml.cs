using DragDropDemo.ViewModels;
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
        private MapPage myMap = new MapPage();
        private ManagerWindow managerWindow;
        public LinesViewPage(ManagerWindow managerWindow)
        {
            
            InitializeComponent();
            ComboBoxInit();

            myMap.AddPushPins(AllStations, MouseRightButtonDownEvent);
            SetControlsVisible();

            Main.Content = myMap;
            this.managerWindow = managerWindow;

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
            if (route == null)
            {
                return; // TODO: display error
            }


            TodoItemListingViewModel routeStations =  MapStations(route.Stations);
            TodoItemListingViewModel allStations = MapStations(AllStations);

            EditRouteWindow editRoute = new EditRouteWindow(ref route, Main, myMap, managerWindow)
            {
                DataContext = new TodoViewModel(routeStations, allStations)
            };
            editRoute.Visibility = Visibility.Visible;
            // myMap.Visibility = Visibility.Hidden;
            Main.Content = editRoute;
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(fromLocationCombobox.SelectedItem.ToString());
            CloseConfirmPopup(sender, e);
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
                myMap.DrawMapPolygon(routes);
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

        private TodoItemListingViewModel MapStations(List<Station> stations)
        {
            TodoItemListingViewModel mappedStations = new TodoItemListingViewModel();
            foreach (Station s in stations)
            {
                mappedStations.AddTodoItem(new TodoItemViewModel(s));
            }
            return mappedStations;
        }

        private void OpenConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click += Remove_Click;
            managerWindow.popup.YesButton.Click += CloseConfirmPopup;
            managerWindow.popup.NoButton.Click += CloseConfirmPopup;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete this route?";
        }
        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= Remove_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopup;
        }
    }
}
