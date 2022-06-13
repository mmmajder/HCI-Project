using DragDropDemo.ViewModels;
using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.Service;
using HelpSistem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HCI_Project.Manager
{
    /// <summary>
    /// Interaction logic for EditTrainsPage.xaml
    /// </summary>
    public partial class EditTrainsPage : Page
    {
        private Frame trainCrudFrame;
        private ManagerWindow managerWindow;
        private TrainsViewPage trainsViewPage;
        private string trainType;


        public EditTrainsPage(string trainType, Frame trainCrudFrame, ManagerWindow managerWindow, TrainsViewPage trainsViewPage)
        {
            InitializeComponent();

            this.trainCrudFrame = trainCrudFrame;
            this.managerWindow = managerWindow;
            this.trainsViewPage = trainsViewPage;
            this.trainType = trainType;
            if (trainType != null)
            {
                trainTypeLbl.Text = "Train Type";
                trainName.Text = trainType;
            }
            else
            {
                trainTypeLbl.Text = "New Train Type";
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

            List<Wagon> newTrainWagons = new List<Wagon>();
            WagonContext model = (WagonContext)this.DataContext;
            WagonListingViewModel InProgressTodoItemListingViewModel = model.InProgressWagonListingViewModel;
            foreach (WagonViewModel a in InProgressTodoItemListingViewModel.WagonViewModels)
            {
                newTrainWagons.Add(a.wagon);
            }

            if (SetNewWagons(newTrainWagons))
            {
                trainCrudFrame.Content = null;
                trainsViewPage.RefreshCombobox();
            }
        }

        private bool SetNewWagons(List<Wagon> newTrainWagons)
        {
            string input = trainName.Text;
            errorMessage.Visibility = Visibility.Hidden;
            errorMessageWagons.Visibility = Visibility.Hidden;
            MyMessageBox popup;

            if (trainType == input)
            {
                TrainRepo.SetWagons(trainType, newTrainWagons);
                popup = new MyMessageBox("Successfully edited train", this, true);
                popup.ShowDialog();
                return true;
            }

            if (!checkInput(input, newTrainWagons))
            {
                return false;
            }

            TrainRepo.AddTrainType(trainName.Text, newTrainWagons);

            if (trainType != null)
            {
                TrainRepo.RemoveTrainType(trainType);
                popup = new MyMessageBox("Successfully edited train", this, true);
                popup.ShowDialog();
                return true;
            }

            popup = new MyMessageBox("Successfully created new train", this, true);
            popup.ShowDialog();
            return true;
        }

        private bool checkInput(string input, List<Wagon> newTrainWagons)
        {
            bool valid = true;
            if (!IsValidInput(input))
            {
                errorMessage.Text = "Train Type can only contain letters, numbers and dash";
                errorMessage.Visibility = Visibility.Visible;
                valid = false;
            }

            if (newTrainWagons.Count < 1)
            {
                errorMessageWagons.Visibility = Visibility.Visible;
                valid = false;
            }

            if (TrainRepo.GetTrainTypeNames().Contains(input))
            {
                errorMessage.Text = "This train type already exists!";
                errorMessage.Visibility = Visibility.Visible;
                valid = false;
            }

            return valid;
        }

        private bool IsValidInput(string input)
        {
            if(input == null)
            {
                return false;
            }
            input = input.Trim();
            Regex re = new Regex(@"[a-zA-Z0-9 -]+$");

            return input != "" && re.IsMatch(input);
        }

        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {
            trainCrudFrame.Content = null;
        }

        private void EditStations_Click(object sender, RoutedEventArgs e)
        {
            WagonSeatsWindow sw = new WagonSeatsWindow(managerWindow, managerWindow.Main.Content, RefreshData);
            sw.Visibility = Visibility.Visible;
            managerWindow.Main.Content = sw;

        }

        public void RefreshData()
        {
            WagonContext model = (WagonContext)this.DataContext;
            WagonListingViewModel allWagons = WagonService.MapWagons(WagonRepo.GetWagons());
            model.CompletedWagonListingViewModel = allWagons;

            DataContext = new WagonContext(model.InProgressWagonListingViewModel, allWagons);
            //            this.DataContext = model;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            if (trainType != null)
            {
                HelpProvider.ShowHelp("EditTrain");
            }
            else
            {
                HelpProvider.ShowHelp("AddTrain");
            }
        }
    }
}
