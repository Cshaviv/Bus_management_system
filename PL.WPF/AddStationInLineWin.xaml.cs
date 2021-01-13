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
    /// Interaction logic for AddStationInLineWin.xaml
    /// </summary>
    public partial class AddStationInLineWin : Window
    {
        IBL bl;
        BO.Line line;
        public AddStationInLineWin(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            List<BO.Station> station = bl.GetAllStations().ToList();
            List<BO.StationInLine> StationInLine = line.Stations.ToList();
            stationComboBox.ItemsSource = station;
            stationComboBox.SelectedIndex = 0;
        }

        private void rbFirst_Checked(object sender, RoutedEventArgs e)
        {
            PrevStatTextBlock.Visibility = Visibility.Hidden;
            PrevstationComboBox.Visibility = Visibility.Hidden;
            PrevDistanceTextBlock.Visibility = Visibility.Hidden;
            PrevDistanceTextBox.Visibility = Visibility.Hidden;
            PrevTimeTextBlock.Visibility = Visibility.Hidden;
            PrevTimeTextBox.Visibility = Visibility.Hidden;
            NextDistanceTextBlock.Visibility = Visibility.Visible;
            NextDistanceTextBox.Visibility = Visibility.Visible;
            NextTimeTextBlock.Visibility = Visibility.Visible;
            NextTimeTextBox.Visibility = Visibility.Visible;
            AddLast.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Hidden;
            AddFirst.Visibility = Visibility.Visible;
        }

        private void rbLast_Checked(object sender, RoutedEventArgs e)
        {
            PrevStatTextBlock.Visibility = Visibility.Hidden;
            PrevstationComboBox.Visibility = Visibility.Hidden;
            PrevDistanceTextBlock.Visibility = Visibility.Visible;
            PrevDistanceTextBox.Visibility = Visibility.Visible;
            PrevTimeTextBlock.Visibility = Visibility.Visible;
            PrevTimeTextBox.Visibility = Visibility.Visible;
            NextDistanceTextBlock.Visibility = Visibility.Hidden;
            NextDistanceTextBox.Visibility = Visibility.Hidden;
            NextTimeTextBlock.Visibility = Visibility.Hidden;
            NextTimeTextBox.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Hidden;
            AddFirst.Visibility = Visibility.Hidden;
            AddLast.Visibility = Visibility.Visible;

        }

        private void rbMiddle_Checked(object sender, RoutedEventArgs e)
        {

            List<BO.StationInLine> StationInLine = line.Stations.ToList();
            PrevstationComboBox.ItemsSource = StationInLine;
            PrevstationComboBox.SelectedItem = "Code";
            PrevstationComboBox.SelectedIndex = 0;
            PrevStatTextBlock.Visibility = Visibility.Visible;
            PrevstationComboBox.Visibility = Visibility.Visible;
            PrevDistanceTextBlock.Visibility = Visibility.Visible;
            PrevDistanceTextBox.Visibility = Visibility.Visible;
            PrevTimeTextBlock.Visibility = Visibility.Visible;
            PrevTimeTextBox.Visibility = Visibility.Visible;
            NextDistanceTextBlock.Visibility = Visibility.Visible;
            NextDistanceTextBox.Visibility = Visibility.Visible;
            NextTimeTextBlock.Visibility = Visibility.Visible;
            NextTimeTextBox.Visibility = Visibility.Visible;
            AddFirst.Visibility = Visibility.Hidden;
            AddLast.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Visible;  

        }

        private void AddFirstClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station stat = (stationComboBox.SelectedItem) as BO.Station;

               // BO.StationInLine stat = (stationComboBox.SelectedItem) as BO.StationInLine;
                double distance = double.Parse(NextDistanceTextBox.Text);
                TimeSpan time = TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text));
                bl.AddStationInLine(stat.Code, line.LineId, 0,line.Stations[0].StationCode,0, distance, time, 0, new TimeSpan(0, 0, 0));
                MessageBox.Show("successfull", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddLastClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station stat = (stationComboBox.SelectedItem) as BO.Station;
                double distance = double.Parse(PrevDistanceTextBox.Text);
                TimeSpan time = TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text));
                bl.AddStationInLine(stat.Code, line.LineId, line.Stations.Count, 0, line.Stations[line.Stations.Count-1].StationCode, 0, new TimeSpan(0, 0, 0), distance, time);
                MessageBox.Show("successfull", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddMiddleClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station stat = (stationComboBox.SelectedItem) as BO.Station;
                BO.StationInLine prevStat = (PrevstationComboBox.SelectedItem) as BO.StationInLine;
                double distanceNext = double.Parse(NextDistanceTextBox.Text);
                TimeSpan timeNext = TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text));
                double distancePrev = double.Parse(PrevDistanceTextBox.Text);
                TimeSpan timePrev = TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text));
                bl.AddStationInLine(stat.Code, line.LineId, prevStat.LineStationIndex + 1, line.Stations[prevStat.LineStationIndex + 1].StationCode , prevStat.StationCode,  distanceNext, timeNext, distancePrev, timePrev);
                MessageBox.Show("successfull", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        
               catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
