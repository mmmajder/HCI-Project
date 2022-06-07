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

namespace HCI_Project.Popups
{
    /// <summary>
    /// Interaction logic for InputPopup.xaml
    /// </summary>
    public partial class InputStationNamePopup : Window
    {
        StationsWindow parentWindow;
        public string InputText = "";

        public InputStationNamePopup(string message, StationsWindow parentWindow)
        {
            InitializeComponent();

            inputMessage.Text = message;
            if (parentWindow != null)
            {
                this.parentWindow = parentWindow;
                parentWindow.IsEnabled = false;

                Canvas.SetLeft(this, parentWindow.Left + parentWindow.Width / 2.8);
                Canvas.SetTop(this, parentWindow.Top + parentWindow.Height / 2.3);
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Hide();
            if (parentWindow != null)
            {
                parentWindow.IsEnabled = true;
                parentWindow.AddPushpin(Input.Text);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            Hide();
            if (parentWindow != null)
            {
                parentWindow.IsEnabled = true;
            }
        }
    }
}
