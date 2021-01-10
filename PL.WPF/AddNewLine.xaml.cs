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
            firstStationComboBox.DisplayMemberPath = "Name";
            firstStationComboBox.SelectedIndex=0;
            lastStationComboBox.ItemsSource = station;
            lastStationComboBox.DisplayMemberPath = "Name";
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

            BO.Station firstStation = (firstStationComboBox.SelectedItem) as BO.Station;
            BO.Station lastStation = (lastStationComboBox.SelectedItem) as BO.Station;
            if (bl.IsExistAdjacentStations(firstStation.Code, lastStation.Code))
            {
                int lineNum = int.Parse(lineNumTextBox.Text);
                BO.Area area = (BO.Area)Enum.Parse(typeof(BO.Area), AreaComboBox.SelectedItem.ToString());
                BO.Line newline = new BO.Line() { LineId = -1, LineNum = lineNum, Area = area };
                BO.StationInLine temp1 = new BO.StationInLine() { DisabledAccess = firstStation.DisabledAccess, Name = firstStation.Name, LineStationIndex = 1, StationCode = firstStation.Code };
                newline.Stations.Add(temp1);
                BO.StationInLine temp2 = new BO.StationInLine() { DisabledAccess = lastStation.DisabledAccess, Name = lastStation.Name, LineStationIndex = 2, StationCode = lastStation.Code };
                newline.Stations.Add(temp2);
                bl.AddNewLine(newline);
                MessageBox.Show("The line was added successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            else
            {
             
                int lineNum = int.Parse(lineNumTextBox.Text);
                BO.Area area = (BO.Area)Enum.Parse(typeof(BO.Area), AreaComboBox.SelectedItem.ToString());
                BO.Line newline = new BO.Line() { LineId = -1, LineNum = lineNum, Area = area };
                TimeSpan time = TimeSpan.Parse(travelTimeTextBox.Text);
                double distance = double.Parse(travelDistanceTextBox.Text);
                BO.StationInLine temp1 = new BO.StationInLine() { Distance = distance, Time = time, DisabledAccess = firstStation.DisabledAccess, Name = firstStation.Name, LineStationIndex = 1, StationCode = firstStation.Code };
                newline.Stations = new List<BO.StationInLine>();
                newline.Stations.Add(temp1);
                BO.StationInLine temp2 = new BO.StationInLine() { DisabledAccess = lastStation.DisabledAccess, Name = lastStation.Name, LineStationIndex = 2, StationCode = lastStation.Code };
                newline.Stations.Add(temp2);
                bl.AddNewLine(newline);
                MessageBox.Show("The line was added successfully", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                
            }
        }
}
