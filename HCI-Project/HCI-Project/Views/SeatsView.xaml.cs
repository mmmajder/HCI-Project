using HCI_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public List<int> TakenSeats;
        public bool IsClient;
        public int WagonInd;
        private ObservableCollection<string> SelectedSeats;

        public SeatsView(int RowCount, int ColumnCount, List<int> takenSeats=null, bool isClient=false, ObservableCollection<string> selectedSeats = null, int wagonInd = 0)
        {
            InitializeComponent();
            this.ColumnCount = RowCount;
            this.RowCount = ColumnCount;
            TakenSeats = takenSeats;
            SelectedSeats = selectedSeats;
            IsClient = isClient;
            WagonInd = wagonInd;

            DrawSeats();

            GridHelpers.SetColumnCount(gridMain, ColumnCount);
            GridHelpers.SetRowCount(gridMain, RowCount);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsClient)
            {
                Button b = (Button)sender;
                if (!TakenSeats.Contains(int.Parse((string)b.Content)))
                {
                    string seatStr = WagonInd + "_" + b.Content;
                    if (!b.Background.Equals(Brushes.GreenYellow)) //not yet selected
                    {
                        b.Background = Brushes.GreenYellow;
                        SelectedSeats.Add(seatStr);
                    }
                    else // already selected
                    {
                        b.Background = Brushes.DeepSkyBlue;
                        List<string> newSelectedSeats = new List<string>();

                        foreach (string seat in SelectedSeats)
                            if (!seat.Equals(seatStr))
                                newSelectedSeats.Add(seat);

                        SelectedSeats.Clear();
                        foreach (string seat in newSelectedSeats)
                            SelectedSeats.Add(seat);
                    }
                    
                }
            }
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
                    MyControl1.Background = Brushes.DeepSkyBlue;

                    if (IsClient && TakenSeats.Contains(count))
                        MyControl1.Background = new SolidColorBrush(Color.FromArgb(0xCC, 0x11, 0x11, 0));
                    else if (IsClient && SelectedSeats.Contains(WagonInd + "_" + MyControl1.Content))
                        MyControl1.Background = Brushes.GreenYellow;

                    Grid.SetColumn(MyControl1, j);
                    Grid.SetRow(MyControl1, i);
                    gridMain.Children.Add(MyControl1);

                    count++;
                }

            }
        }
    }
}
