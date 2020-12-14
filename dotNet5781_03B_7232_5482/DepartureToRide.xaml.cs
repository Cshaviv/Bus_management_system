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

namespace dotNet5781_03B_7232_5482
{
    /// <summary>
    /// Interaction logic for DepartureToRide.xaml
    /// </summary>
    public partial class DepartureToRide : Window
    {
        public Bus myBus { get; set; }
        public DepartureToRide()
        {
            InitializeComponent();
        }
        private void TakeForRide_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                double num;
                if (!double.TryParse(rideDisTextBox.Text, out num))
                {
                    ErrorDistance.Visibility = Visibility.Visible;
                    rideDisTextBox.BorderBrush = Brushes.Red;
                }
                else
                {
                    if (num < 0)
                    {
                        ErrorDistance.Visibility = Visibility.Visible;
                        rideDisTextBox.BorderBrush = Brushes.Red;
                    }
                    else if ((myBus.Kmafterrefueling + num) > 1200)
                        MessageBox.Show("There is no enough fuel for this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else if ((myBus.Kmaftertreat + num) > 20000)
                        MessageBox.Show("You need to take the bus for a treatment before this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                    {
                        myBus.Kmaftertreat += num;
                        myBus.Kmafterrefueling += num;
                        myBus.Km += num;
                        Close();
                    }
                }
            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
