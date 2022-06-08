﻿using DragDropDemo.Commands;
using DragDropDemo.ViewModels;
using HCI_Project.Model;
using HCI_Project.Popups;
using HCI_Project.Repo;
using HCI_Project.Service;
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
        private readonly MapLinePage mapLinePage;
        private ManagerWindow managerWindow;
        private LinesViewPage linesPage;

        public Route route { get; set; }

        public EditRouteWindow(Route route, Frame main, MapLinePage mapLinePage, ManagerWindow managerWindow, LinesViewPage linesPage)
        {
            InitializeComponent();
            Title = "Edit Route";

            this.main = main;
            this.mapLinePage = mapLinePage;
            this.managerWindow = managerWindow;
            this.linesPage = linesPage;

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

            if (route != null)
            {
                route.Stations = newRouteStations;
            }
            else
            {
                string trainType = "";
                Route route = new Route(newRouteStations, new List<ScheduledRoute>(), trainType);
                RouteRepo.AddRoute(route);
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
//            this.DataContext = model;

            linesPage.RefreshCombobox();
        }


    }
}
