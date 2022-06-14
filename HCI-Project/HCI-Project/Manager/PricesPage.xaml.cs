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

        public PricesPage()
        {
            InitializeComponent();
            fillRoutesData();
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
                SelectedRoute.PriceCatalog[stationCB.SelectedItem.ToString()] = newPrice;
                MessageBox.Show("You have successfully changed the Price to next station to " + newPrice + " for Station " + stationCB.SelectedItem.ToString());
                fillPricesDataGrid();
            }
                
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
