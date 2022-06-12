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
using Prism.Commands;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace HCI_Project.Styles
{
    /// <summary>
    /// Interaction logic for IntegerUpDown.xaml
    /// </summary>
    public partial class IntegerUpDown : UserControl
    {
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(IntegerUpDown), new PropertyMetadata(0, ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = (IntegerUpDown)d;
            var newValue = (int)e.NewValue;

            if (control != null)
            {
                control.Value = newValue;
            }
        }

        public DelegateCommand UpValueCommand { get; set; }
        public DelegateCommand DownValueCommand { get; set; }

        public IntegerUpDown()
        {
            InitializeComponent();
            UpValueCommand = new DelegateCommand(UpValue);
            DownValueCommand = new DelegateCommand(DownValue);

            TopContainer.DataContext = this;
        }

        public void UpValue()
        {
            Value += 1;
        }

        public void DownValue()
        {
            if(Value > 1)
            {
                Value -= 1;
            } 
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
