using DragDropDemo.ViewModels;
using HCI_Project.Manager;
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
    public partial class TrainsViewPage : Page
    {
        private List<Wagon> AllWagons = WagonRepo.GetWagons();
        private List<string> trainTypes = TrainRepo.GetTrainTypeNames();
        private ManagerWindow managerWindow;

        private string trainTypeDelete;

        public TrainsViewPage(ManagerWindow managerWindow)
        {
            
            InitializeComponent();
            this.managerWindow = managerWindow;
            RefreshData();
        }

        private void ComboBoxInit()
        {
            trainTypesCombobox.ItemsSource = trainTypes;
            trainTypesCombobox.SelectedIndex = -1;
            trainTypesCombobox.IsTextSearchEnabled = true;
            trainTypesCombobox.IsEditable = true;
            trainTypesCombobox.IsTextSearchCaseSensitive = false;
        }
        
        private void AddTrainType_Click(object sender, RoutedEventArgs e)
        {
            WagonListingViewModel trainWagonList = new WagonListingViewModel();
            WagonListingViewModel allWagons = WagonService.MapWagons(AllWagons);

            EditTrainsPage editRoute = new EditTrainsPage(null, TrainCrudFrame, managerWindow, this)
            {
                DataContext = new WagonContext(trainWagonList, allWagons)
            };
            editRoute.Visibility = Visibility.Visible;
            TrainCrudFrame.Content = editRoute;
        }
        private void EditTrain_Click(object sender, RoutedEventArgs e)
        {
            string trainType = (string)GetComboboxValue(trainTypesCombobox);
            if (trainType == null)
            {
                return; // TODO: display error
            }

            WagonListingViewModel trainWagonList = WagonService.MapWagons(TrainRepo.GetWagons(trainType));
            WagonListingViewModel allWagons = WagonService.MapWagons(AllWagons);

            EditTrainsPage editRoute = new EditTrainsPage(trainType, TrainCrudFrame, managerWindow, this)
            {
                DataContext = new WagonContext(trainWagonList, allWagons)
            };
            editRoute.Visibility = Visibility.Visible;
            TrainCrudFrame.Content = editRoute;
        }

        private void DeleteTrainType(object sender, RoutedEventArgs e)
        {
            if (trainTypeDelete != null)
            {
                TrainRepo.RemoveTrainType(trainTypeDelete);
            }
            trainTypesCombobox.ItemsSource = null;
            trainTypesCombobox.ItemsSource = TrainRepo.GetTrainTypeNames();

            CloseConfirmPopupTrain(sender, e);
            TrainCrudFrame.Content = null;
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



        private void OpenConfirmPopupTrain(object sender, RoutedEventArgs e)
        {
            object trainTypeClicked = GetComboboxValue(trainTypesCombobox);

            if (trainTypeClicked == null)
            {
                return;
            }
            trainTypeDelete = trainTypeClicked.ToString();

            managerWindow.popup.YesButton.Click += DeleteTrainType;
            managerWindow.popup.YesButton.Click += CloseConfirmPopupTrain;
            managerWindow.popup.NoButton.Click += CloseConfirmPopupTrain;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete train " + trainTypeClicked + "?";

            managerWindow.host.ShowDialog(managerWindow.popup);
        }
        private void CloseConfirmPopupTrain(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= DeleteTrainType;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopupTrain;
        }

        private void EditWagons_Click(object onj, RoutedEventArgs eventHandler)
        {
            WagonSeatsWindow sw = new WagonSeatsWindow(managerWindow, managerWindow.Main.Content, RefreshData);
            sw.Visibility = Visibility.Visible;
            managerWindow.Main.Content = sw;
        }

        public void RefreshCombobox()
        {
            AllWagons = WagonRepo.GetWagons();
            trainTypes = TrainRepo.GetTrainTypeNames();
            ComboBoxInit();
        }

        public void RefreshData()
        {
            RefreshCombobox();
            TrainCrudFrame.Content = null;
        }
    }
}
