using DragDropDemo.Commands;
using DragDropDemo.ViewModels;
using HCI_Project.Model;
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
        private Frame main;
        private MapPage myMap;

        private string RouteName { get; set; }
        public Route route { get; set; }

        public EditRouteWindow(ref Route route, Frame main, MapPage myMap)
        {
            InitializeComponent();
            Title = "Edit Route";

            this.main = main;
            this.myMap = myMap;

            if (route != null)
            {
                routeName.Text = "Route name: " + route.ToString();
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
            //this.Visibility = Visibility.Hidden;
            this.main.Content = myMap;
        }

        private void CancelChanges_Click(object sender, RoutedEventArgs e)
        {
            //this.Visibility = Visibility.Hidden;
            this.main.Content = myMap;
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
        }
    }
}
