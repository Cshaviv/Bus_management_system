using BLAPI;
using BO;
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
            firstStationComboBox.SelectedIndex = 0;
            lastStationComboBox.ItemsSource = station;
            lastStationComboBox.SelectedItem = "Code";
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
                int lineNum = int.Parse(lineNumTextBox.Text);
                if (!Int32.TryParse(lineNumTextBox.Text, out int licenseNum))
                    throw new BadInputException(1, "מחרוזת קלט לא היתה בתבנית הנכונה");
                BO.Station firstStation = (firstStationComboBox.SelectedItem) as BO.Station;
                BO.Station lastStation = (lastStationComboBox.SelectedItem) as BO.Station;
                if (firstStation == lastStation)
                    throw new BO.BadLineIdException("לא ניתן להכניס שוב את אותה תחנה");
                if (!Double.TryParse(distanceTextBox.Text, out double distance))
                    throw new BadInputException(2, "מחרוזת קלט לא היתה בתבנית הנכונה");
                if (!Double.TryParse(TimeTextBox.Text, out double time_))
                    throw new BadInputException(3, "מחרוזת קלט לא היתה בתבנית הנכונה");
                TimeSpan time = TimeSpan.FromMinutes(double.Parse(TimeTextBox.Text));
                BO.Area area = (BO.Area)Enum.Parse(typeof(BO.Area), AreaComboBox.SelectedItem.ToString());
                BO.Line newline = new BO.Line() { LineNum = lineNum, Area = area, FirstStation = firstStation.Code, LastStation = lastStation.Code };
                newline.Stations = new List<BO.StationInLine>();
                BO.StationInLine firstStat = new BO.StationInLine() { lineNum = lineNum, StationCode = firstStation.Code, Name = firstStation.Name, LineStationIndex = 0, DistanceFromNext = distance, TimeFromNext = time };
                newline.Stations.Add(firstStat);
                BO.StationInLine lastStat = new BO.StationInLine() { lineNum = lineNum, StationCode = lastStation.Code, Name = lastStation.Name, LineStationIndex = 1, DistanceFromNext = 0, TimeFromNext = new TimeSpan(0, 0, 0) };
                newline.Stations.Add(lastStat);
                bl.AddNewLine(newline);
                lineNumTextBox.BorderBrush = Brushes.Gray;
                TimeTextBox.BorderBrush = Brushes.Gray;
                distanceTextBox.BorderBrush = Brushes.Gray;
                MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BadLineIdException ex)
            {

                lineNumTextBox.BorderBrush = Brushes.Red;
                TimeTextBox.BorderBrush = Brushes.Gray;
                distanceTextBox.BorderBrush = Brushes.Gray;
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                Exceptions(ex.num, ex.Message);
            }
        }
        private void Exceptions(int num, string message)
        {
            if (num == 1)
            {
                lineNumTextBox.BorderBrush = Brushes.Red;
                TimeTextBox.BorderBrush = Brushes.Gray;
                distanceTextBox.BorderBrush = Brushes.Gray;
                MessageBox.Show(message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (num == 2)
            {
                lineNumTextBox.BorderBrush = Brushes.Gray;
                TimeTextBox.BorderBrush = Brushes.Gray;
                distanceTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show(message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (num == 3)
            {
                lineNumTextBox.BorderBrush = Brushes.Gray;
                TimeTextBox.BorderBrush = Brushes.Red;
                distanceTextBox.BorderBrush = Brushes.Gray;
                MessageBox.Show(message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }
    }
}
