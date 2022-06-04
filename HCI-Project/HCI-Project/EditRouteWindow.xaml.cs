using DragDropDemo.Commands;
using DragDropDemo.ViewModels;
using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for EditRouteWindow.xaml
    /// </summary>
    public partial class EditRouteWindow : Page
    {
        private readonly Frame main;
        private readonly MapPage myMap;
        private ManagerWindow managerWindow;

        public Route route { get; set; }

        public EditRouteWindow(ref Route route, Frame main, MapPage myMap, ManagerWindow managerWindow)
        {
            InitializeComponent();
            Title = "Edit Route";

            this.main = main;
            this.myMap = myMap;
            this.managerWindow = managerWindow;

            if (route != null)
            {
                routeName.Text = route.ToString();
                this.route = route;
            }

        }
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {

            List<Station> newRouteStations = new List<Station>();
            TodoViewModel model = (TodoViewModel)this.DataContext;
            TodoItemListingViewModel InProgressTodoItemListingViewModel = model.InProgressTodoItemListingViewModel;
            foreach (TodoItemViewModel a in InProgressTodoItemListingViewModel.TodoItemViewModels)
            {
                newRouteStations.Add(a.Station);
            }
            route.Stations = newRouteStations;
            main.Content = myMap;
        }

        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {
            main.Content = myMap;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Add_Click");
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Edit_Click");
        }
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Remove_Click");
            CloseConfirmPopup(sender, e);
        }

        private void OpenConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click += Remove_Click;
            managerWindow.popup.YesButton.Click += CloseConfirmPopup;
            managerWindow.popup.NoButton.Click += CloseConfirmPopup;
            managerWindow.popup.confirmMessage.Text = "Are you sure you want to delete this station?";
        }
        private void CloseConfirmPopup(object sender, RoutedEventArgs e)
        {
            managerWindow.popup.YesButton.Click -= Remove_Click;
            managerWindow.popup.NoButton.Click -= CloseConfirmPopup;
        }

    }
}
