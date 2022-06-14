﻿using System;
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

        public static RoutedUICommand SaveChangeCommand = new RoutedUICommand("SaveChangeCommand", "SaveChangeCommand", typeof(InputStationNamePopup));

        public InputStationNamePopup(string message, string footNote, StationsWindow parentWindow)
        {
            InitializeComponent();

            inputMessage.Text = message;
            footNoteMessage.Text = footNote;
            this.parentWindow = parentWindow;
            this.parentWindow.IsEnabled = false;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            SaveChangeCommand.InputGestures.Add(new KeyGesture(Key.Enter));
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

        private void SaveChange_Executed(object sender, ExecutedRoutedEventArgs e)
        {
             Save_Click(sender, e);
        }

        private void SaveChange_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
