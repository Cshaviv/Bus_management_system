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
    /// Interaction logic for StationDataUser.xaml
    /// </summary>
    public partial class StationDataUser : Window
    {
        IBL BL;
        BO.Station stat;
        ListBox stationsListBox;
        public StationDataUser(IBL _bl, Station _stat, ListBox _stationsListBox)
        {
            InitializeComponent();
            BL = _bl;
            stat = _stat;
             stationsListBox = _stationsListBox;
            // IsDeletedRectangleStation = _IsDeletedRectangleStation;
            addressTextBlock.Text = stat.Address.ToString();
            nameTextBlock.Text = stat.Name.ToString();
            LineInStationListBox.ItemsSource = stat.LinesInStation;
            // LineInStationListBox.DataContext = station.LinesInStation;
            LineInStationListBox.Visibility = Visibility.Visible;
             stationCodeTextBlock.Text = stat.Code.ToString();

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SimulateOneStationWindow win = new SimulateOneStationWindow(BL,stat);
            win.ShowDialog();
        }
    }
}
