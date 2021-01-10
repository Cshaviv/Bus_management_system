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
                return;
            }
            else
            {
                PrevStatTextBlock.Visibility = Visibility.Visible;
                PrevstationComboBox.Visibility = Visibility.Visible;
            }
        }
        

       
private void IfCheckedYes(object sender, RoutedEventArgs e)
        {
            if (NoCheckBox.IsChecked == true)
            {
                MessageBox.Show("לא ניתן ללחוץ על שתי הכפתורים בו זמנית, בטל אחד מהם", "WARNING", MessageBoxButton.OK, MessageBoxImage.Error);

            }
           
        }
    }
}
