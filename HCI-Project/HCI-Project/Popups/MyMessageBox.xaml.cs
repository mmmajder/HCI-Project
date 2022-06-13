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
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MyMessageBox : Window
    {
        public Page parentWindow { get; set; }
        public MyMessageBox(string message, Page parentWindow, bool isSuccess)
        {
            InitializeComponent();
            txtMessage.Text = message;
            if (!isSuccess)
            {
                faIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Exclamation;
                Color color = (Color)ColorConverter.ConvertFromString("#cc1111");
                faIcon.Foreground = new SolidColorBrush(color);
            }
            if (parentWindow != null)
            {
                this.parentWindow = parentWindow;
                parentWindow.IsEnabled = false;

                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
            else
            {
                this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }
           
        }
        private void Ok_clicked(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            if (parentWindow != null)
            {
                parentWindow.IsEnabled = true;
            }

        }

    }
}
