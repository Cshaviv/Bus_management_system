using BLAPI;
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

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for UpdateDistanceAndTime.xaml
    /// </summary>
    public partial class UpdateDistanceAndTime : Window
    {
        IBL bl;
        BO.StationInLine first;
        BO.StationInLine second;
        public UpdateDistanceAndTime(IBL _bl, BO.StationInLine _first, BO.StationInLine _second)
        {
            InitializeComponent();
            bl = _bl;
            first = _first;
            second = _second;
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TimeSpan time = TimeSpan.Parse(TextBoxTime.Text);
                double distance = double.Parse(TextBoxDistance.Text);
                if (distance < 0)
                {
                    MessageBox.Show("invalid distance", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                //BO.AdjacentStation adj = new BO.AdjacentStation() { StationCode1 = first.StationCode, StationCode2 = second.StationCode, Distance = distance, Time = time }; 
                first.Distance = distance;
                first.Time = time;
                bl.UpdateTimeAndDistance(first, second);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

        }
    }
}
