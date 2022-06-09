using HCI_Project.Model;
using HCI_Project.Repo;
using HCI_Project.ViewModels;
using HCI_Project.Views;
using MaterialDesignThemes.Wpf;
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

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for TrainsViewPage.xaml
    /// </summary>
    public partial class TrainsViewPage : Page
    {
        private SeatsView seatsView;
        private List<string> trainTypes = TrainRepo.GetTrainTypeNames();
        private ManagerWindow managerWindow;
        private string trainTypeDelete;
        private Wagon wagonForeDelete;
        private Wagon selectedWagon;

        public TrainsViewPage(ManagerWindow managerWindow)
        {
            InitializeComponent();
            this.managerWindow = managerWindow;
           
            trainTypesCombobox.ItemsSource = trainTypes;
        }

        private void AddTrainType_Click(object sender, RoutedEventArgs e)
        {
            // open for name
            // chose wagons
        }
        private void AddWagon_Click(object sender, RoutedEventArgs e)
        {
            // open for name
            // row count, col count, id
        }
        
        private void ShowWagons(object sender, RoutedEventArgs e)
        {
            object trainTypeClicked = GetComboboxValue(trainTypesCombobox);

            if (trainTypeClicked == null)
            {
                return;
            }

            wagonsList.ItemsSource = TrainRepo.GetWagons(trainTypeClicked.ToString());
            wagonsContainer.Visibility = Visibility.Visible;
        }

        private void ShowSeats(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = wagonsList.SelectedItem;
            if (selectedItem != null)
            {
                selectedWagon = (Wagon)selectedItem;
                seatsView = new SeatsView(selectedWagon.RowCount, selectedWagon.ColCount);
                seatsContainter.Children.Clear();
                seatsContainter.Children.Add(seatsView);
                seatsViewContainter.Visibility = Visibility.Visible;

                RowCountPicker.Value = selectedWagon.RowCount;
                ColumnCountPicker.Value = selectedWagon.ColCount;
            }
        }
        private void SaveSeats_Click(object sender, RoutedEventArgs e)
        {   
            seatsView = new SeatsView(RowCountPicker.Value, ColumnCountPicker.Value);
            seatsContainter.Children.Clear();
            seatsContainter.Children.Add(seatsView);
            seatsViewContainter.Visibility = Visibility.Visible;

            selectedWagon.RowCount = RowCountPicker.Value;
            selectedWagon.ColCount = ColumnCountPicker.Value;

            WagonRepo.SaveChanges(selectedWagon);
        }
        


        private void OpenConfirmPopupTrain(object sender, RoutedEventArgs e)
        {
            object trainTypeClicked = GetComboboxValue(trainTypesCombobox);

            if (trainTypeClicked == null)
            {
                return;
            }
            trainTypeDelete = trainTypeClicked.ToString();

            managerWindow.popup.YesButton.Click += DeleteTrainType_Click;
            managerWindow.popup.YesButton.Click += CloseConfirmPopupTrain;
            managerWindow.popup.NoButton.Click += CloseConfirmPopupTrain;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete train " + trainTypeClicked + "?";

            managerWindow.host.ShowDialog(managerWindow.popup);
        }
        private void OpenConfirmPopupWagon(object sender, RoutedEventArgs e)
        {

            var wagonClicked = wagonsList.SelectedItem;
            if (wagonClicked != null)
            {
                return;
            }
            wagonForeDelete = (Wagon)wagonClicked;

            managerWindow.popup.YesButton.Click += DeleteWagon;
            managerWindow.popup.YesButton.Click += CloseConfirmPopupWagon;
            managerWindow.popup.NoButton.Click += CloseConfirmPopupWagon;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete wagon " + wagonForeDelete.Name + "?";

            managerWindow.host.ShowDialog(managerWindow.popup);
        }
        private void CloseConfirmPopupWagon(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= DeleteTrainType_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopupWagon;
        }

        private void CloseConfirmPopupTrain(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= DeleteTrainType_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopupTrain;
        }

        private void DeleteTrainType_Click(object sender, RoutedEventArgs e)
        {
            if (trainTypeDelete != null)
            {
                TrainRepo.RemoveTrainType(trainTypeDelete);
            }
            trainTypesCombobox.ItemsSource = null;
            trainTypesCombobox.ItemsSource = trainTypes;

            // TODO: delete previewed wagons and seats
        }

        private void DeleteWagon(object sender, RoutedEventArgs e)
        {
            if (wagonForeDelete != null)
            {
                WagonRepo.RemoveWagon(wagonForeDelete);
            }

            // TODO: delete previewed wagons and seats
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


    }
}
