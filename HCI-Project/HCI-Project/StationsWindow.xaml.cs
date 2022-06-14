using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.ViewModels;
using HelpSistem;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        private readonly Action refreshData;

        public string StationNameInput;

        private Station stationForDelete;

        public static RoutedUICommand SaveChangeCommand = new RoutedUICommand("SaveChangeCommand", "SaveChangeCommand", typeof(StationsWindow));

        public StationsWindow(ManagerWindow managerWindow, object previousPage, Action refreshData)
        {
            InitializeComponent();

            stations.ItemsSource = AllStations;
            this.managerWindow = managerWindow;
            this.previousPage = previousPage;
            this.refreshData = refreshData;

            SaveChangeCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            InputStationNamePopup inputPopup = new InputStationNamePopup("Please input the name of new station.", "*Then you will be able to set it's location", this);
            inputPopup.ShowDialog();
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

             mapPage.RemovePushPins(new List<Pushpin> { getPushpinFromStation(stationForDelete) });
        }


        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if(newPins.Count <= 0)
            {
                return;
            }
            foreach(DraggablePin pin in newPins)
            {
                StationRepo.AddStation(new Station(pin.StationName, false, pin.Location));
            }

            MyMessageBox popup = new MyMessageBox("Successfully created new station", this, true);
            popup.ShowDialog();

            stations.ItemsSource = null;
            stations.ItemsSource = AllStations;
            mapPage.RemoveDraggablePins(newPins);
            newPins.Clear();
            if ((bool)showAll.IsChecked)
            {
                mapPage.RemovePushPins(allpushpins);
                ShowAll_Click(sender, e);
            }
        }
        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {
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
            if(newPins.Count > 0)
            {
                managerWindow.popup.YesButton.Click += SaveChanges_Click;
                managerWindow.popup.NoButton.Click += CancelChanges_Click;
                managerWindow.popup.YesButton.Click += CloseCloseConfirmPopup;
                managerWindow.popup.NoButton.Click += CloseCloseConfirmPopup;
                managerWindow.popup.confirmMessage.Text = "Would you like to save new stations?";

                managerWindow.host.ShowDialog(managerWindow.popup);
            }
            else
            {
                GoBack();
            }
            
        }
        private void GoBack()
        {
            managerWindow.Main.Content = previousPage;
            refreshData();
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
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete station " + cliecked_station.Name + "?";

            managerWindow.host.ShowDialog(managerWindow.popup);
        }
        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= DeleteStation_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopup;
        }

        private void CloseCloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= SaveChanges_Click;
            managerWindow.popup.NoButton.Click -= CancelChanges_Click;
            managerWindow.popup.YesButton.Click -= DeleteStation_Click;
            managerWindow.popup.NoButton.Click -= CloseCloseConfirmPopup;
            managerWindow.popup.YesButton.Click -= CloseCloseConfirmPopup;
            GoBack();
        }

        private Pushpin getPushpinFromStation(Station stationForDelete)
        {
            foreach(Pushpin pin in  allpushpins)
            {
                if (pin.Content.Equals(stationForDelete.Name))
                {
                    return pin;
                }
            }
            return null;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("Stations");
        }

        private void ShowSelectedStation_Click(object sender, SelectionChangedEventArgs e)
        {
            object selectedItem = stations.SelectedItem;
            if(selectedItem != null)
            {
                Station selectedStation = (Station)selectedItem;
                if (!(bool)showAll.IsChecked)
                {
                    mapPage.myMap.Children.Clear();
                    mapPage.AddPushPins(new List<Station> { selectedStation });
                }
                mapPage.SetCenter(selectedStation.Position);
            }
        }

        private void SaveChange_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveChanges_Click(sender, e);
        }

        private void SaveChange_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
