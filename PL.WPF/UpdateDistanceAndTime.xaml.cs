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
        BO.StationInLine station;
        BO.StationInLine nextStation;
        public UpdateDistanceAndTime(IBL _bl, BO.StationInLine _station, BO.StationInLine _nextStation)
        {
            InitializeComponent();
            bl = _bl;
            station = _station;
            nextStation = _nextStation;
            DistanceTextBox.Text= station.DistanceFromNext.ToString();
            TimeTextBox.Text = station.TimeFromNext.Minutes.ToString();

        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double distance = double.Parse(DistanceTextBox.Text);
            TimeSpan time = TimeSpan.FromMinutes(double.Parse(TimeTextBox.Text));
            BO.StationInLine stat = new BO.StationInLine() { StationCode = station.StationCode, Name = station.Name, DisabledAccess = station.DisabledAccess, LineStationIndex = station.LineStationIndex, DistanceFromNext= distance, TimeFromNext=time};
           
                bl.UpdateTimeAndDistance( stat, nextStation);
                MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }
    }
    }


