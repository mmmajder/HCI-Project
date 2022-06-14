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

namespace HCI_Project.Manager
{
    /// <summary>
    /// Interaction logic for EditTrainRoutPage.xaml
    /// </summary>
    public partial class EditTrainRoutPage : Page
    {
        private readonly Frame main;
        private readonly MapLinePage mapLinePage;
        private ManagerWindow managerWindow;
        private LinesViewPage linesPage;
        private List<string> trainTypes = TrainRepo.GetTrainTypeNames();

        public Route route { get; set; }

        
        public static RoutedUICommand SaveTrainRouteCommand = new RoutedUICommand("SaveTrainRouteCommand", "SaveTrainRouteCommand", typeof(EditTrainRoutPage));

        public EditTrainRoutPage(Route route, Frame main, MapLinePage mapLinePage, ManagerWindow managerWindow, LinesViewPage linesPage)
        {
            InitializeComponent();
            Title = "Edit Route";

            this.main = main;
            this.mapLinePage = mapLinePage;
            this.managerWindow = managerWindow;
            this.linesPage = linesPage;

            if (route != null)
            {
                routeNameLbl.Text = "Route Name:";
                routeName.Text = route.ToString();
                this.route = route;
            }
            else
            {
                routeNameLbl.Text = "New Route";
            }
            ComboBoxInit();
            SaveTrainRouteCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        }
        private void ComboBoxInit()
        {
            trainTypesCombobox.ItemsSource = trainTypes;
            trainTypesCombobox.IsTextSearchEnabled = true;
            trainTypesCombobox.IsEditable = true;
            trainTypesCombobox.IsTextSearchCaseSensitive = false;
            if (route != null)
            {
                trainTypesCombobox.SelectedItem = route.TrainType;
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

            List<Station> newRouteStations = new List<Station>();
            TodoViewModel model = (TodoViewModel)this.DataContext;
            TodoItemListingViewModel InProgressTodoItemListingViewModel = model.InProgressTodoItemListingViewModel;

            errorTrainType.Visibility = Visibility.Hidden;
            errorStations.Visibility = Visibility.Hidden;

            foreach (TodoItemViewModel a in InProgressTodoItemListingViewModel.TodoItemViewModels)
            {
                newRouteStations.Add(a.Station);
            }

            string trainType = GetTrainType();
            if(!ValidateInputs(trainType, newRouteStations))
            {
                return;
            }


            if (route != null)
            {
                route.Stations = newRouteStations;
                route.TrainType = trainType;

                MyMessageBox popup = new MyMessageBox("Successfully edited train route", this, true);
                popup.ShowDialog();
            }
            else
            {
                int id = RouteRepo.GetRoutes().Count + 1;
                Route route = new Route(id, newRouteStations, new List<ScheduledRoute>(), trainType);
                RouteRepo.AddRoute(route);
                MyMessageBox popup = new MyMessageBox("Successfully added new train route", this, true);
                popup.ShowDialog();
            }
            main.Content = mapLinePage;
            linesPage.RefreshCombobox();
            linesPage.VisualiseChange();
        }

        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {
            main.Content = mapLinePage;
        }

        private void EditStations_Click(object sender, RoutedEventArgs e)
        {
            StationsWindow sw = new StationsWindow(managerWindow, linesPage, RefreshData);
            sw.Visibility = Visibility.Visible;
            managerWindow.Main.Content = sw;
        }

        public void RefreshData()
        {
            TodoViewModel model = (TodoViewModel)this.DataContext;
            TodoItemListingViewModel allStations = RouteService.MapStations(StationRepo.GetStations());
            model.CompletedTodoItemListingViewModel = allStations;

            DataContext = new TodoViewModel(model.InProgressTodoItemListingViewModel, allStations);

            linesPage.RefreshCombobox();
        }

        public void Help_Click(object sender, RoutedEventArgs e)
        {
            if (route != null)
            {
                HelpProvider.ShowHelp("EditTrainRoute");
            }
            else
            {
                HelpProvider.ShowHelp("AddTrainRoute");
            }
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


        private bool ValidateInputs(string trainType, List<Station> newRouteStations)
        {
            bool valid = true;
            if (trainType == null)
            {
                errorTrainType.Visibility = Visibility.Visible;
                valid = false;
            }
            if (newRouteStations.Count < 2)
            {
                errorStations.Visibility = Visibility.Visible;
                valid = false;
            }
            return valid;
        }

        private string GetTrainType()
        {
            object trainTypeClicked = GetComboboxValue(trainTypesCombobox);

            if (trainTypeClicked == null)
            {
                return null;
            }
            return trainTypeClicked.ToString();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void SaveTrainRoute_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveChanges_Click(sender, e);
        }

        private void SaveTrainRoute_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        
    }

}
