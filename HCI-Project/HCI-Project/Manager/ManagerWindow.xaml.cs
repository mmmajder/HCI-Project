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
        }

        private void LinesViewSelected(object sender, RoutedEventArgs e)
        {
            Main.Content = new LinesViewPage(this);
        }

        private void TrainsViewSelected(object sender, RoutedEventArgs e)
        {
            Main.Content = new TrainsViewPage();
        }

        private void HomeViewSelected(object sender, RoutedEventArgs e)
        {
            Main.Content = new HomeViewPage();
        }

        private void TimetableBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new TimetableViewPage();
        }

        private void FinancesBtn_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new FinancesViewPage();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();

        }
    }
}
