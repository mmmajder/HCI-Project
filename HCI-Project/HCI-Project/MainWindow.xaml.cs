using HCI_Project.Repo;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string username;
        private string password;
        public MainWindow()
        {
            new UserRepo();
            InitializeComponent();
            DataContext = this;
        }

        private void doLogin(object sender, RoutedEventArgs e)
        {
            username = txtUsername.Text;
            User user = UserRepo.Login(username, password);
            if (user != null)
            {
                WrongInput.Visibility = Visibility.Hidden;
                Window window = new Window();
                window.Show();
                this.Close();
            }
            else
            {
                WrongInput.Visibility = Visibility.Visible;
            }
        }
       

        public void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            password = txtPassword.Password.ToString();
        }

    }
}
