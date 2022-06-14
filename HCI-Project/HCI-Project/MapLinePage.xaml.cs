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

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for MapLinePage.xaml
    /// </summary>
    public partial class MapLinePage : Page
    {
        private ManagerWindow managerWindow;
        private Action refreshData;
        public MapLinePage(ManagerWindow managerWindow, Action refreshData)
        {
            InitializeComponent();
            this.managerWindow = managerWindow;
            if (managerWindow != null)
            {
                editStation.Visibility = Visibility.Visible;
            }
            this.refreshData = refreshData;
        }

        private void EditStations_Click(object obj, RoutedEventArgs eventHandler)
        {
            StationsWindow sw = new StationsWindow(managerWindow, managerWindow.Main.Content, refreshData);
            sw.Visibility = Visibility.Visible;
            managerWindow.Main.Content = sw;
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            if (managerWindow != null)
            {
                HelpProvider.ShowHelp("MapLine");
            }
            else
            {
                HelpProvider.ShowHelp("MapLineClient");
            }
        }
    }
}

