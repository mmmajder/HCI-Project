using HCI_Project.Model;
using HCI_Project.Repo;
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
    /// Interaction logic for PricesPage.xaml
    /// </summary>
    public partial class PricesPage : Page
    {
        List<Route> Routes;
        Route SelectedRoute;
        Station SelectedStation;

        Stack<PriceUndoRedo> UndoStack;
        Stack<PriceUndoRedo> RedoStack;

        public static RoutedUICommand UndoCommand = new RoutedUICommand("UndoCommand", "UndoCommand",typeof(PricesPage));
        public static RoutedUICommand RedoCommand = new RoutedUICommand("RedoCommand", "RedoCommand", typeof(PricesPage));
        public static RoutedUICommand SaveChangeCommand = new RoutedUICommand("SaveChangeCommand", "SaveChangeCommand", typeof(PricesPage));


        public PricesPage()
        {
            InitializeComponent();
            fillRoutesData();
            UndoStack = new Stack<PriceUndoRedo>();
            RedoStack = new Stack<PriceUndoRedo>();

            UndoCommand.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
            RedoCommand.InputGestures.Add(new KeyGesture(Key.Y, ModifierKeys.Control));
            SaveChangeCommand.InputGestures.Add(new KeyGesture(Key.S, ModifierKeys.Control));
        }

        private void fillRoutesData()
        {
            Routes = RouteRepo.GetRoutes();
            routeCB.ItemsSource = Routes;
        }

        private void RouteSelected_Click(object sender, RoutedEventArgs e)
        {
            SelectedRoute = (Route)routeCB.SelectedItem;

            if (SelectedRoute == null)
                return;

            pricesPreviewer.Visibility = Visibility.Visible;
            fillPricesDataGrid();
            fillStations();
            stationCB.SelectedIndex = -1;
            priceTB.Text = "";
        }

        private void fillPricesDataGrid()
        {
            dgrMain.ItemsSource = SelectedRoute.getPrices().Where((v, i) => i != SelectedRoute.Stations.Count - 1).ToList();
        }

        private void fillStations()
        {
            stationCB.ItemsSource = SelectedRoute.Stations.Where((v, i) => i != SelectedRoute.Stations.Count - 1).ToList();
        }

        private void stationCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedStation = (Station) stationCB.SelectedItem;
            if (SelectedStation == null) return;

            stationErrorLbl.Visibility = Visibility.Collapsed;
            priceTB.Text = SelectedRoute.PriceCatalog[SelectedStation.Name].ToString();
        }

        private int getSelectedStationIndex()
        {
            int i = dgrMain.Items.IndexOf(dgrMain.SelectedItem);

            return i;
        }

        private void dgrMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int i = getSelectedStationIndex();
            if (i == -1) return;

            stationCB.SelectedIndex = i;
        }

        private void NewPrice_Changed(object sender, TextChangedEventArgs e)
        {
            double newPrice;
            if (!double.TryParse(priceTB.Text, out newPrice))
            {
                priceErrorLbl.Visibility = Visibility.Visible;
                ChangePriceBtn.Visibility = Visibility.Collapsed;
                return;
            }
            if (newPrice < 0)
            {
                priceErrorLbl.Visibility = Visibility.Visible;
                ChangePriceBtn.Visibility = Visibility.Collapsed;
                return;
            }



            priceErrorLbl.Visibility = Visibility.Collapsed;
            ChangePriceBtn.Visibility = Visibility.Visible;
        }

        private void ChangePriceBtn_Click(object sender, RoutedEventArgs e)
        {
            ChangePriceCompleteAction();
        }

        private void ChangePriceCompleteAction()
        {
            if (stationCB.SelectedItem == null)
            {
                ChangePriceBtn.Visibility = Visibility.Collapsed;
                stationErrorLbl.Visibility = Visibility.Visible;
                return;
            }
            stationErrorLbl.Visibility = Visibility.Collapsed;
            double newPrice;
            if (double.TryParse(priceTB.Text, out newPrice))
            {
                string stationName = stationCB.SelectedItem.ToString();
                double oldPrice = SelectedRoute.getPrice(stationName);
                if (newPrice != oldPrice)
                {
                    PriceUndoRedo priceMemo = new PriceUndoRedo(SelectedRoute.Id, stationName, SelectedRoute.getPrice(stationName), newPrice);
                    UndoStack.Push(priceMemo);
                    ChangePrice(SelectedRoute, stationName, newPrice);
                    RedoStack.Clear();
                    manageUndoRedoBtnsVisibility();
                }
                else
                    MessageBox.Show("The price for station " + stationName + " is already " + newPrice + ".");
            }
        }

        private void ChangePrice(Route route, string stationName, double price)
        {
            route.updatePrice(stationName, price);
            MessageBox.Show("Updated the Price for Station " + stationName + ". New price is " + price + ". On route: " + route);
            fillPricesDataGrid();
        }

        private void doUndo()
        {
            PriceUndoRedo priceMemo = UndoStack.Pop();
            Route route = RouteRepo.getRoute(priceMemo.RouteId);
            ChangePrice(route, priceMemo.StationName, priceMemo.OldPrice);
            RedoStack.Push(priceMemo);
        }

        private void doRedo()
        {
            PriceUndoRedo priceMemo = RedoStack.Pop();
            Route route = RouteRepo.getRoute(priceMemo.RouteId);
            ChangePrice(route, priceMemo.StationName, priceMemo.NewPrice);
            UndoStack.Push(priceMemo);
        }

        private void UndoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UndoStack.Count > 0)
                doUndo();
            manageUndoRedoBtnsVisibility();
        }

        private void RedoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (RedoStack.Count > 0)
                doRedo();
            manageUndoRedoBtnsVisibility();
        }

        private void manageUndoRedoBtnsVisibility()
        {
            if (UndoStack.Count == 0)
                UndoBtn.Visibility = Visibility.Hidden;
            else
                UndoBtn.Visibility = Visibility.Visible;

            if (RedoStack.Count == 0)
                RedoBtn.Visibility = Visibility.Hidden;
            else
                RedoBtn.Visibility = Visibility.Visible;
        }

        private void Undo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (UndoStack.Count > 0)
                doUndo();
            manageUndoRedoBtnsVisibility();
        }

        private void Redo_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (RedoStack.Count > 0)
                doRedo();
            manageUndoRedoBtnsVisibility();
        }

        private void SaveChange_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (pricesPreviewer.Visibility == Visibility.Visible)
                ChangePriceCompleteAction();
        }

        private void Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void Redo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void SaveChange_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        public void Help_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("PricesPage");
        }
    }
}
