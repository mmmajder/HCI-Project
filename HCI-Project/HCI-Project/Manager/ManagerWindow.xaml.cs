using HCI_Project.Manager;
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
using System.Windows.Shapes;

namespace HCI_Project
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        public ManagerWindow()
        {
            InitializeComponent();
            Main.Content = new TimetableViewPage(this);
        }

        private void LinesViewSelected(object sender, RoutedEventArgs e)
        {
            Main.Content = new LinesViewPage(this);
        }

        private void TrainsViewSelected(object sender, RoutedEventArgs e)
        {
            Main.Content = new TrainsViewPage(this);
        }

        private void TimetableBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new TimetableViewPage(this);
        }

        private void FinancesBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new FinancesViewPage();
        }

        private void PricesBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new PricesPage();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();

        }
        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            DemoClient window = new DemoClient();
            window.Show();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                if (Main.Content is PricesPage)
                {
                    PricesPage pricesPage = new PricesPage();
                    pricesPage.Help_Click(sender, e);
                }
                if (Main.Content is LinesViewPage)
                {
                    LinesViewPage linesViewPage = new LinesViewPage(this);
                    linesViewPage.Help_Click(sender, e);
                }
                if (Main.Content is TrainsViewPage)
                {
                    TrainsViewPage trains = new TrainsViewPage(this);
                    trains.Help_Click(sender, e);
                }

                if (Main.Content is TimetableViewPage)
                {
                    TimetableViewPage pricesPage = new TimetableViewPage(this);
                    pricesPage.Help_Click(sender, e);
                }
                if (Main.Content is FinancesViewPage)
                {
                    FinancesViewPage linesViewPage = new FinancesViewPage();
                    linesViewPage.Help_Click(sender, e);
                }
            }
        }
    }
}
