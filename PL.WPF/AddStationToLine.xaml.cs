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
    /// Interaction logic for AddStationToLine.xaml
    /// </summary>
    public partial class AddStationToLine : Window
    {
        IBL bl;
        BO.Line line;
        public AddStationToLine(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            List<BO.Station> station = bl.GetAllStations().ToList();
            List<BO.StationInLine> StationInLine = line.Stations.ToList();
            stationComboBox.ItemsSource = station;
            stationComboBox.SelectedIndex = 0;
            stationComboBox.SelectedItem = "Code";
            PrevstationComboBox.ItemsSource = StationInLine;
            PrevstationComboBox.SelectedItem = "Code";
            PrevstationComboBox.SelectedIndex = 0;

        }

        private void IfCheckedNo(object sender, RoutedEventArgs e)
        {
            if (YesCheckBox.IsChecked == true)
            {
                MessageBox.Show("לא ניתן ללחוץ על שתי הכפתורים בו זמנית, בטל אחד מהם", "WARNING", MessageBoxButton.OK, MessageBoxImage.Error);
               
            }
            else
            {
                PrevStatTextBlock.Visibility = Visibility.Visible;
                PrevstationComboBox.Visibility = Visibility.Visible;
            }
        }
        

       
private void IfCheckedYes(object sender, RoutedEventArgs e)
        {
           
            if(YesCheckBox.IsChecked == true)
            {

            }
           
           
        }

        private void AddNo_Click(object sender, RoutedEventArgs e)
        {
            if (YesCheckBox.IsChecked == true)
            {
                //Station in line
                BO.Station stat = (stationComboBox.SelectedItem) as BO.Station;
                BO.LineStation newStation = new BO.LineStation() { LineId = line.LineId, LineStationIndex = 1, StationCode = stat.Code };
                try
                {

                    bl.AddLineStation(newStation);
                }
                catch (Exception)
                {

                }
            }
            else
            {
                BO.StationInLine PrevStation = (PrevstationComboBox.SelectedItem) as BO.StationInLine;
                if (PrevStation == null)
                {
                    MessageBox.Show("Error", "Error", MessageBoxButton.OK, MessageBoxImage.Error);//להוסיף הערה
                    return;
                }
                 BO.Station newStat = (stationComboBox.SelectedItem) as BO.Station;
                 BO.LineStation newStation = new BO.LineStation() { LineId = line.LineId, LineStationIndex = PrevStation.LineStationIndex + 1, StationCode = newStat.Code };
                try
                {
                    bl.AddLineStation(newStation);
                }
                catch (Exception)
                {

                }
            }
            Close();
        }
    }
}
