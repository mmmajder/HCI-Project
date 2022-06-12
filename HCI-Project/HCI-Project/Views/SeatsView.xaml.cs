using HCI_Project.ViewModels;
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

namespace HCI_Project.Views
{
    /// <summary>
    /// Interaction logic for SeatsView.xaml
    /// </summary>
    public partial class SeatsView : UserControl
    {
        public int ColumnCount;
        public int RowCount;

        public SeatsView(int RowCount, int ColumnCount)
        {
            InitializeComponent();
            this.ColumnCount = RowCount;
            this.RowCount = ColumnCount;
            DrawSeats();

            GridHelpers.SetColumnCount(gridMain, ColumnCount);
            GridHelpers.SetRowCount(gridMain, RowCount);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            string seatNumber = (string)b.Content;
            Console.WriteLine(seatNumber);
        }

        private void DrawSeats()
        {
            int count = 1;


            for (int i = 0; i < ColumnCount; i++)
            {
                for (int j = 0; j < RowCount; j++)
                {
                    Button MyControl1 = new Button();
                    MyControl1.Content = count.ToString();
                    MyControl1.Name = "Button" + count.ToString();
                    MyControl1.Click += Button_Click;
                    MyControl1.Margin = new Thickness(5);

                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    gridMain.Children.Add(MyControl1);

                    count++;
                }

            }
        }
    }
}
