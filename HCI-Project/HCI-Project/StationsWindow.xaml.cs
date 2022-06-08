using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.ViewModels;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    /// 
    public partial class StationsWindow : Page
    {
        private List<Station> AllStations = StationRepo.GetStations();
        private readonly ManagerWindow managerWindow;
        private readonly object previousPage;
        private List<DraggablePin> newPins = new List<DraggablePin>();
        private List<Pushpin> allpushpins = new List<Pushpin>();

        public string StationNameInput;

        private Station stationForDelete;

        public StationsWindow(ManagerWindow managerWindow, object previousPage)
        {
            InitializeComponent();

            stations.ItemsSource = AllStations;
            this.managerWindow = managerWindow;
            this.previousPage = previousPage;
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            InputStationNamePopup inputPopup = new InputStationNamePopup("Please input the name of new station.", "*Then you will be able to set it's location", this);
            inputPopup.Show();
        }

        public void AddPushpin(string name)
        {
            DraggablePin pin = mapPage.AddDragablePushPin(name);
            newPins.Add(pin);
        }

        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {
            if(stationForDelete != null)
            {
                StationRepo.RemoveStation(stationForDelete);
            }
            stations.ItemsSource = null;
            stations.ItemsSource = AllStations;

//            mapPage.RemoveDraggablePins()
        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            
            foreach(DraggablePin pin in newPins)
            {
                StationRepo.AddStation(new Station(pin.StationName, false, pin.Location));
            }

            stations.ItemsSource = null;
            stations.ItemsSource = AllStations;
            mapPage.RemoveDraggablePins(newPins);
            newPins.Clear();
        }

        private void ShowAll_Click(object sender, RoutedEventArgs e)
        {
            if((bool)showAll.IsChecked)
            {
                allpushpins =  mapPage.AddPushPins(AllStations);
            }
            else
            {
                mapPage.RemovePushPins(allpushpins);
            }

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            foreach (DraggablePin pin in newPins)
            {
                StationRepo.AddStation(new Station(pin.StationName, false, pin.Location));
            }

            managerWindow.Main.Content = previousPage;
        }


        private void OpenConfirmPopup(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Station cliecked_station = button.DataContext as Station;
            
            if(cliecked_station == null)
            {
                return;
            }
            stationForDelete = cliecked_station;

            managerWindow.popup.YesButton.Click += DeleteStation_Click;
            managerWindow.popup.YesButton.Click += CloseConfirmPopup;
            managerWindow.popup.NoButton.Click += CloseConfirmPopup;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete station?";

            managerWindow.host.ShowDialog(managerWindow.popup);
        }
        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= DeleteStation_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopup;
        }



        private DraggablePin FindPinByStation(Station station)
        {
            return null;
        }
    }
}
