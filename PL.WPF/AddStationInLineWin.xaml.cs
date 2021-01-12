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
            AddLast.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Hidden;
            AddFirst.Visibility = Visibility.Visible;
            PrevStatTextBlock.Visibility = Visibility.Hidden;
            PrevstationComboBox.Visibility = Visibility.Hidden;
        }

        private void rbLast_Checked(object sender, RoutedEventArgs e)
        {
            AddFirst.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Hidden;
            AddLast.Visibility = Visibility.Visible;
            PrevStatTextBlock.Visibility = Visibility.Hidden;
            PrevstationComboBox.Visibility = Visibility.Hidden;

        }

        private void rbMiddle_Checked(object sender, RoutedEventArgs e)
        {
            AddFirst.Visibility = Visibility.Hidden;
            AddLast.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Visible;
            PrevStatTextBlock.Visibility = Visibility.Visible;
            PrevstationComboBox.Visibility = Visibility.Visible;
            List<BO.StationInLine> StationInLine = line.Stations.ToList();
            PrevstationComboBox.ItemsSource = StationInLine;
            PrevstationComboBox.SelectedItem = "Code";
            PrevstationComboBox.SelectedIndex = 0;

        }

        private void AddMiddleClick(object sender, RoutedEventArgs e)
        {
            BO.StationInLine stat = (stationComboBox.SelectedItem) as BO.StationInLine;
            //BO.LineStation newStation = new BO.LineStation() { LineId = line.LineId, LineStationIndex = 1, StationCode = stat.Code };
            try
            {
                if (!bl.IsExistAdjacentStations(stat.StationCode, line.Stations[1].StationCode))
                {
                    DistanceTextBlock.Visibility = Visibility.Visible;
                    TimeTextBlock.Visibility = Visibility.Visible;
                    DistanceTextBox.Visibility = Visibility.Visible;
                    TimeTextBox.Visibility = Visibility.Visible;
                    double distance = double.Parse(DistanceTextBox.Text);
                    TimeSpan time = TimeSpan.Parse(TimeTextBox.Text);
                    //BO.StationInLine newStat = new BO.StationInLine() { StationCode = stat.StationCode, Name = station.Name, DisabledAccess = station.DisabledAccess, LineStationIndex = station.LineStationIndex, Distance = distance, Time = time };

                }

                //bl.AddLineStation(newStation);
            }
            catch (Exception)
            {

            }
        }
    }
}
