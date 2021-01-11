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
            DistanceTextBox.Text= station.Distance.ToString();
            TimeTextBox.Text = station.Time.Hours.ToString();

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            double distance = double.Parse(DistanceTextBox.Text);
            TimeSpan time = TimeSpan.Parse(TimeTextBox.Text);
            BO.StationInLine stat = new BO.StationInLine() { StationCode = station.StationCode, Name = station.Name, DisabledAccess = station.DisabledAccess, LineStationIndex = station.LineStationIndex, Distance= distance, Time=time};
            try
            {
                bl.UpdateTimeAndDistance( stat, nextStation);
                Close();
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
    }


