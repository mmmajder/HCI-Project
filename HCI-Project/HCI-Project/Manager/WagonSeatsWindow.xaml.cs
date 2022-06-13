using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.ViewModels;
using HCI_Project.Views;
using HelpSistem;
using MaterialDesignThemes.Wpf;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    /// 
    public partial class WagonSeatsWindow : Page
    {
        private List<Wagon> AllWagons = WagonRepo.GetWagons();
        private readonly ManagerWindow managerWindow;
        private readonly object previousPage;
        private readonly Action refreshData;

        public string StationNameInput;
        private Wagon selectedWagon;
        private Wagon wagonForDelete;
        private SeatsView seatsView;

        private bool isNew;

        public WagonSeatsWindow(ManagerWindow managerWindow, object previousPage, Action refreshData)
        {
            InitializeComponent();

            allWagonsList.ItemsSource = AllWagons;
            this.managerWindow = managerWindow;
            this.previousPage = previousPage;
            this.refreshData = refreshData;
        }

        private void AddWagon_Click(object sender, RoutedEventArgs e)
        {
            wagonNameLbl.Content = "New wagon name: ";
            isNew = true;
            ShowSeats();
        }


        private void DeleteWagon_Click(object sender, RoutedEventArgs e)
        {
            if(wagonForDelete != null)
            {
                WagonRepo.RemoveWagon(wagonForDelete);
            }
            allWagonsList.ItemsSource = null;
            allWagonsList.ItemsSource = AllWagons;

            seatsViewContainter.Visibility = Visibility.Hidden;
        }

        private void ShowSeats_Click(object sender, SelectionChangedEventArgs e)
        {
            wagonNameLbl.Content = "Wagon name: ";
            isNew = false;
            ShowSeats();
        }

        private void ShowSeats()
        {
            errorMessage.Visibility = Visibility.Hidden;
            var selectedItem = allWagonsList.SelectedItem;
            if (selectedItem != null && !isNew)
            {
                selectedWagon = (Wagon)selectedItem;
                seatsView = new SeatsView(selectedWagon.RowCount, selectedWagon.ColCount);

                RowCountPicker.Value = selectedWagon.RowCount;
                ColumnCountPicker.Value = selectedWagon.ColCount;

                wagonName.Text = selectedWagon.Name;
            }
            else if(isNew)
            {
                wagonName.Text = "";

                seatsView = new SeatsView(1, 1);
                RowCountPicker.Value = 1;
                ColumnCountPicker.Value = 1;
            }

            seatsContainter.Children.Clear();
            seatsContainter.Children.Add(seatsView);
            seatsViewContainter.Visibility = Visibility.Visible;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string inputName = wagonName.Text;
            int inputRowCount = RowCountPicker.Value;
            int inputColCount = ColumnCountPicker.Value;

            if (!isNew && selectedWagon.Name == inputName)
            {
                selectedWagon.RowCount = inputRowCount;
                selectedWagon.ColCount = inputColCount;

                WagonRepo.SaveChanges(selectedWagon);

                MyMessageBox popup = new MyMessageBox("Successfully edited this wagon", this, true);
                popup.ShowDialog();
            }
            else
            {
                int id = WagonRepo.GetWagons().Count + 1;
                Wagon wagon = new Wagon(id, inputName, inputRowCount, inputColCount);
                if (!SaveNew(wagon)) { return; }

                MyMessageBox popup = new MyMessageBox("Successfully added new wagon", this, true);
                popup.ShowDialog();
            }

            allWagonsList.ItemsSource = null;
            allWagonsList.ItemsSource = AllWagons;

            seatsContainter.Children.Clear();
            seatsViewContainter.Visibility = Visibility.Hidden;
            errorMessage.Visibility = Visibility.Hidden;
        }

        private bool SaveNew(Wagon wagon)
        {
            string input = wagonName.Text;
            errorMessage.Visibility = Visibility.Hidden;

            if (!IsValidInput(input))
            {
                errorMessage.Content = "Wagon name can only contain letters, numbers and dash";
                errorMessage.Visibility = Visibility.Visible;
                return false;
            }
            else
            {
                if (WagonRepo.NameAlreadyExists(input) && isNew)
                {
                    errorMessage.Content = "This train type already exists!";
                    errorMessage.Visibility = Visibility.Visible;
                    return false;
                }

                WagonRepo.AddWagon(wagon);
                if (!isNew)
                {
                    WagonRepo.RemoveWagon(selectedWagon);
                }
            }
            return true;
        }


        private void PreviewSeats_Click(object sender, RoutedEventArgs e)
        {
            seatsView = new SeatsView(RowCountPicker.Value, ColumnCountPicker.Value);
            seatsContainter.Children.Clear();
            seatsContainter.Children.Add(seatsView);
            seatsViewContainter.Visibility = Visibility.Visible;
        }


        private void GoBack(object sender, RoutedEventArgs e)
        {
            managerWindow.Main.Content = previousPage;
            refreshData();
        }


        private void OpenConfirmPopup(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Wagon cliecked_wagon = button.DataContext as Wagon;
            
            if(cliecked_wagon == null)
            {
                return;
            }
            wagonForDelete = cliecked_wagon;

            managerWindow.popup.YesButton.Click += DeleteWagon_Click;
            managerWindow.popup.YesButton.Click += CloseConfirmPopup;
            managerWindow.popup.NoButton.Click += CloseConfirmPopup;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete wagon " + cliecked_wagon.Name + "?";

            managerWindow.host.ShowDialog(managerWindow.popup);
        }
        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= DeleteWagon_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopup;
        }



        /* private void Close_Click(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click += SaveChanges_Click;
            managerWindow.popup.YesButton.Click += CloseCloseConfirmPopup;
            managerWindow.popup.NoButton.Click += CloseCloseConfirmPopup;
            managerWindow.popup.confirmMessage.Text = "Would you like to save new wagons?";

            managerWindow.host.ShowDialog(managerWindow.popup);            
        }
         private void CloseCloseConfirmPopup(object sender, RoutedEventArgs e)
         {
             managerWindow.popup.YesButton.Click -= SaveChanges_Click;
             managerWindow.popup.YesButton.Click -= DeleteWagon_Click;
             managerWindow.popup.NoButton.Click -= CloseCloseConfirmPopup;
             managerWindow.popup.YesButton.Click -= CloseCloseConfirmPopup;
             GoBack(sender, e);
         }
*/
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("Wagons");
        }


        private bool IsValidInput(string input)
        {
            if (input == null)
            {
                return false;
            }
            input = input.Trim();
            Regex re = new Regex(@"[a-zA-Z0-9 -]+$");

            return input != "" && re.IsMatch(input);
        }


    }
}
