using HelpSistem;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace HCI_Project.Help
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        private JavaScriptControlHelper ch;
        public HelpViewer(string key)
        {
            InitializeComponent();
            string curDir = Directory.GetCurrentDirectory();
            string newPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(curDir, @"..\..\html\"));
            string path = String.Format("{0}{1}.htm", newPath, key);
            if (!File.Exists(path))
            {
                key = "error";
            }
            Uri u = new Uri(String.Format(path));

            ch = new JavaScriptControlHelper();

            
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}
