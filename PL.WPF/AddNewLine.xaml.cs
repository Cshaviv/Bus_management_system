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
    /// Interaction logic for AddNewLine.xaml
    /// </summary>
    public partial class AddNewLine : Window
    {
        IBL bl;
        public AddNewLine(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            List<BO.Station> station = bl.GetAllStations().ToList();
            firstStationComboBox.ItemsSource = station;
            firstStationComboBox.SelectedItem = "Code";
            //firstStationComboBox.DisplayMemberPath = "Code";       
            firstStationComboBox.SelectedIndex = 0;
            lastStationComboBox.ItemsSource = station;
            lastStationComboBox.SelectedItem = "Code";
            //lastStationComboBox.DisplayMemberPath = "Code";
            lastStationComboBox.SelectedIndex = 1;
            AreaComboBox.ItemsSource = Enum.GetValues(typeof(BO.Area));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource lineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("lineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // lineViewSource.Source = [generic data source]
        }

        private void AddLineClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station firstStation = (firstStationComboBox.SelectedItem) as BO.Station;
                BO.Station lastStation = (lastStationComboBox.SelectedItem) as BO.Station;
                if (firstStation == lastStation)
                    throw new Exception("error");
                int lineNum = int.Parse(lineNumTextBox.Text);
                double distance = double.Parse(distanceTextBox.Text);
                TimeSpan time = TimeSpan.FromMinutes(double.Parse(TimeTextBox.Text));
                BO.Area area = (BO.Area)Enum.Parse(typeof(BO.Area), AreaComboBox.SelectedItem.ToString());
                BO.Line newline = new BO.Line() { LineNum = lineNum, Area = area, FirstStation = firstStation.Code, LastStation = lastStation.Code };
                newline.Stations = new List<BO.StationInLine>();
                BO.StationInLine firstStat = new BO.StationInLine() { lineNum = lineNum, StationCode = firstStation.Code, Name = firstStation.Name, LineStationIndex = 0 , DistanceFromNext = distance, TimeFromNext = time};
                newline.Stations.Add(firstStat);
                BO.StationInLine lastStat = new BO.StationInLine() { lineNum = lineNum, StationCode = lastStation.Code, Name = lastStation.Name, LineStationIndex = 1, DistanceFromNext = 0, TimeFromNext = new TimeSpan(0,0,0) };
                newline.Stations.Add(lastStat);
                bl.AddNewLine(newline);
                MessageBox.Show("The line was added successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
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
