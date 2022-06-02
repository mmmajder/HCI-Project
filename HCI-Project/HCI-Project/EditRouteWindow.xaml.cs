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
    public partial class EditRouteWindow : Window
    {


        Point startPoint = new Point();

        public ObservableCollection<Station> routeStations { get; set; }
        public ObservableCollection<Station> allStations { get; set; }

        public string RouteName { get; set; }

        public EditRouteWindow(Route route)
        {
            InitializeComponent();
            this.DataContext = this;

            


            /*  Title = "Edit Route";

              List<Station> allRepoStations = StationRepo.GetStations();
              if(route != null)
              {
                  RouteName = "Route name: " + route.ToString();
                  routeStations = new ObservableCollection<Station>(route.Stations);
              }
              allStations = new ObservableCollection<Station>(allRepoStations);*/
        }
        /*
                private void ListView_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
                {
                    startPoint = e.GetPosition(null);
                }

                private void ListView_MouseMove(object sender, MouseEventArgs e)
                {
                    Point mousePos = e.GetPosition(null);
                    Vector diff = startPoint - mousePos;

                    if (e.LeftButton == MouseButtonState.Pressed &&
                        (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                        Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
                    {
                        // Get the dragged ListViewItem
                        ListView listView = sender as ListView;
                        ListViewItem listViewItem = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

                        // Find the data behind the ListViewItem
                        Station station = (Station)listView.ItemContainerGenerator.ItemFromContainer(listViewItem);

                        // Initialize the drag & drop operation
                        DataObject dragData = new DataObject("myFormat", station);
                        DragDrop.DoDragDrop(listViewItem, dragData, DragDropEffects.Move);
                    }
                }

                private static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
                {
                    do
                    {
                        if (current is T)
                        {
                            return (T)current;
                        }
                        current = VisualTreeHelper.GetParent(current);
                    }
                    while (current != null);
                    return null;
                }

                private void ListView_DragEnter(object sender, DragEventArgs e)
                {
                    if (!e.Data.GetDataPresent("myFormat") || sender == e.Source)
                    {
                        e.Effects = DragDropEffects.None;
                    }
                }

                private void ListView_Drop(object sender, DragEventArgs e)
                {
                    if (e.Data.GetDataPresent("myFormat"))
                    {
                        Station station = e.Data.GetData("myFormat") as Station;
                        routeStations.Add(station);
                    }
                }*/

/*
        private void TodoItem_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed &&
                sender is FrameworkElement frameworkElement)
            {
                object todoItem = frameworkElement.DataContext;

                DragDropEffects dragDropResult = DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, todoItem), DragDropEffects.Move);

                if (dragDropResult == DragDropEffects.None)
                {
                    AddTodoItem(todoItem);
                }
            }
        }

        private void TodoItem_DragOver(object sender, DragEventArgs e)
        {
            if (TodoItemInsertedCommand?.CanExecute(null) ?? false)
            {
                if (sender is FrameworkElement element)
                {
                    TargetTodoItem = element.DataContext;
                    InsertedTodoItem = e.Data.GetData(DataFormats.Serializable);

                    TodoItemInsertedCommand?.Execute(null);
                }
            }
        }

        private void TodoItemList_DragOver(object sender, DragEventArgs e)
        {
            object todoItem = e.Data.GetData(DataFormats.Serializable);
            AddTodoItem(todoItem);
        }

        private void AddTodoItem(object todoItem)
        {
            if (TodoItemDropCommand?.CanExecute(null) ?? false)
            {
                IncomingTodoItem = todoItem;
                TodoItemDropCommand?.Execute(null);
            }
        }

        private void TodoItemList_DragLeave(object sender, DragEventArgs e)
        {
            HitTestResult result = VisualTreeHelper.HitTest(lvItems, e.GetPosition(lvItems));

            if (result == null)
            {
                if (TodoItemRemovedCommand?.CanExecute(null) ?? false)
                {
                    RemovedTodoItem = e.Data.GetData(DataFormats.Serializable);
                    TodoItemRemovedCommand?.Execute(null);
                }
            }
        }*/
    }
}
