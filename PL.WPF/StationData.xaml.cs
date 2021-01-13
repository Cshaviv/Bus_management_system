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
    /// Interaction logic for StationData.xaml
    /// </summary>
    public partial class StationData : Window
    {
        IBL bl;
        BO.Station station;
       // ListBox stationsListBox;
        public StationData( IBL _bl, BO.Station _station) //, ListBox _stationsListBox)
        {
            InitializeComponent();
            bl = _bl;
            station = _station;
            // LineListBox.ItemsSource = station.Lines.ToList();
            LineInStationListBox.DataContext = station.LinesInStation;
            LineInStationListBox.Visibility = Visibility.Visible;
            stationNameTextBlock.Text = station.Name.ToString();
            AddressTextBlock.Text = station.Address.ToString();
            stationCodeTextBlock.Text = station.Code.ToString();
        }

        private void updateStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteStation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    }
}
