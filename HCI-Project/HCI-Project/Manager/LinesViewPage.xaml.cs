using DragDropDemo.ViewModels;
using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.Service;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for LinesViewPage.xaml
    /// </summary>
    public partial class LinesViewPage : Page
    {
        private List<Station> AllStations;
        private List<string> AllStationNames;
        private MapLinePage mapLinePage;
        private ManagerWindow managerWindow;

        private Route routeForDelete;
        public LinesViewPage(ManagerWindow managerWindow)
        {
            
            InitializeComponent();
            this.managerWindow = managerWindow;
            RefreshData();
            SetControlsVisible();
        }

        private void ComboBoxInit()
        {
            fromLocationCombobox.ItemsSource = AllStationNames;
            fromLocationCombobox.SelectedIndex = -1;
            fromLocationCombobox.IsTextSearchEnabled = true;
            fromLocationCombobox.IsEditable = true;
            fromLocationCombobox.IsTextSearchCaseSensitive = false;

            toLocationCombobox.ItemsSource = AllStationNames;
            toLocationCombobox.SelectedIndex = -1;
            toLocationCombobox.IsTextSearchEnabled = true;
            toLocationCombobox.IsEditable = true;
            toLocationCombobox.IsTextSearchCaseSensitive = false;

            routesCombobox.ItemsSource = null;
            routesCombobox.SelectedIndex = -1;
            routesCombobox.IsTextSearchEnabled = true;
            routesCombobox.IsEditable = true;
            routesCombobox.IsTextSearchCaseSensitive = false;

        }
       /* private void MouseRightButtonDownEvent(object sender, RoutedEventArgs e)
        {
            if (sender is Pushpin pushpin)
            {
                MessageBox.Show(string.Format("Pushpin {0} was clicked", pushpin.Content.ToString()));
            }
        }*/

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            TodoItemListingViewModel routeStations = new TodoItemListingViewModel();
            TodoItemListingViewModel allStations = RouteService.MapStations(AllStations);

            EditRouteWindow editRoute = new EditRouteWindow(null, Main, mapLinePage, managerWindow, this)
            {
                DataContext = new TodoViewModel(routeStations, allStations)
            };
            editRoute.Visibility = Visibility.Visible;
            Main.Content = editRoute;
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Route route = (Route)GetComboboxValue(routesCombobox);
            if (route == null)
            {
                return; // TODO: display error
            }


            TodoItemListingViewModel routeStations =  RouteService.MapStations(route.Stations);
            TodoItemListingViewModel allStations = RouteService.MapStations(AllStations);

            EditRouteWindow editRoute = new EditRouteWindow(route, Main, mapLinePage, managerWindow, this)
            {
                DataContext = new TodoViewModel(routeStations, allStations)
            };
            editRoute.Visibility = Visibility.Visible;
            Main.Content = editRoute;
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            
            RouteRepo.RemoveRoute(routeForDelete);

            CloseConfirmPopup(sender, e);
            Show_Click(sender, e);
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
            VisualiseChange();
        }

        public void VisualiseChange() 
        {
            object fromLocation = GetComboboxValue(fromLocationCombobox);
            object toLocation = GetComboboxValue(toLocationCombobox);
            routesCombobox.ItemsSource = null;

            if (fromLocation != null && toLocation != null)
            {
                List<Route> routes = RouteService.GetRoutes(fromLocation.ToString(), toLocation.ToString());
                mapLinePage.mapPage.DrawMapPolygon(routes);
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

        private void OpenConfirmPopup(object sender, RoutedEventArgs e)
        {
            Route clicked_route = (Route)GetComboboxValue(routesCombobox);

            if (clicked_route == null)
            {
                return;
            }
            routeForDelete = clicked_route;


            managerWindow.popup.YesButton.Click += Remove_Click;
            managerWindow.popup.YesButton.Click += CloseConfirmPopup;
            managerWindow.popup.NoButton.Click += CloseConfirmPopup;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete route " + clicked_route.ToString();

            managerWindow.host.ShowDialog(managerWindow.popup);
        }
        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= Remove_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopup;
        }

        private void EditStations_Click(object onj, RoutedEventArgs eventHandler)
        {
            StationsWindow sw = new StationsWindow(managerWindow, managerWindow.Main.Content, RefreshData);
            sw.Visibility = Visibility.Visible;
            managerWindow.Main.Content = sw;
        }

        public void RefreshCombobox()
        {
            /*AllStations = StationRepo.GetStations();
            AllStationNames = StationRepo.GetStationNames();
            fromLocationCombobox.ItemsSource = null;
            toLocationCombobox.ItemsSource = null;
            routesCombobox.ItemsSource = null;
            fromLocationCombobox.ItemsSource = AllStationNames;
            toLocationCombobox.ItemsSource = AllStationNames;*/

            AllStations = StationRepo.GetStations();
            AllStationNames = StationRepo.GetStationNames();

            ComboBoxInit();
        }

        public void RefreshData()
        {
            RefreshCombobox();

            mapLinePage = new MapLinePage(managerWindow, RefreshData);
            mapLinePage.mapPage.AddPushPins(AllStations);

            Main.Content = mapLinePage;
        }
    }
}
