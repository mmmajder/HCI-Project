using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.ViewModels;
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
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    /// 
    public partial class StationsWindow : Window
    {
        private readonly List<Station> AllStations = StationRepo.GetStations();
        // private readonly ManagerWindow managerWindow;
        private List<DraggablePin> newPins = new List<DraggablePin>();

        public string StationNameInput;

        public StationsWindow(ManagerWindow managerWindow)
        {
            InitializeComponent();

            stations.ItemsSource = AllStations;
            // this.managerWindow = managerWindow;
        }

        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            InputStationNamePopup inputPopup = new InputStationNamePopup("Please input the name of new station.", this);
            inputPopup.Show();
        }

        public void AddPushpin(string name)
        {
            DraggablePin pin = mapPage.AddDragablePushPin(name);
            newPins.Add(pin);
        }

        private void DeleteStation_Click(object sender, RoutedEventArgs e)
        {

        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            foreach(DraggablePin pin in newPins)
            {
                Console.WriteLine(pin.Name);
                Console.WriteLine(pin.Location);
                Console.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
                StationRepo.AddStation(new Station(pin.Name, false, pin.Location));
            }
            
        }
/*
        private void OpenInpuPopup(object sender, RoutedEventArgs e)
        {
            popup.YesButton.Click += AddStation_Click;
            popup.YesButton.Click += CloseInpuPopup;
            popup.NoButton.Click += CloseInpuPopup;
            popup.confirmMessage.Text = "Are you sure you want to delete this station?";
        }

        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            popup.YesButton.Click -= DeleteStation_Click;
            popup.NoButton.Click -= CloseConfirmPopup;
        }
*/

        private void OpenConfirmPopup(object sender, RoutedEventArgs e)
        {
            popup.YesButton.Click += DeleteStation_Click;
            popup.YesButton.Click += CloseConfirmPopup;
            popup.NoButton.Click += CloseConfirmPopup;
            popup.confirmMessage.Text = "Are you sure you want to delete this station?";
        }
        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            popup.YesButton.Click -= DeleteStation_Click;
            popup.NoButton.Click -= CloseConfirmPopup;
        }
    }
}
