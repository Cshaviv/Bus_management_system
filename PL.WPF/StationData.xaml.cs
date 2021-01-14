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
       ListBox stationsListBox;
        Rectangle IsDeletedRectangleStation;
        public StationData( IBL _bl, BO.Station _station , ListBox _stationsListBox /*Rectangle _IsDeletedRectangleStation*/) //, ListBox _stationsListBox)
        {
            InitializeComponent();
            bl = _bl;
            station = _station;
            stationsListBox = _stationsListBox;
           // IsDeletedRectangleStation = _IsDeletedRectangleStation;
            addressTextBox.Text= station.Address.ToString();
            nameTextBox.Text= station.Name.ToString();
            LineInStationListBox.ItemsSource = station.LinesInStation;
           // LineInStationListBox.DataContext = station.LinesInStation;
            LineInStationListBox.Visibility = Visibility.Visible;
            nameTextBox.Text = station.Name.ToString();
            addressTextBox.Text = station.Address.ToString();
            stationCodeTextBlock.Text = station.Code.ToString();
            
            
        }


        void RefreshAllStations()
        {
            stationsListBox.ItemsSource = bl.GetAllStations().ToList();
            
        }
        private void updateStation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string stationName = nameTextBox.Text;
                string stationAddress = addressTextBox.Text;
                int stationCode = int.Parse(stationCodeTextBlock.Text);
                BO.Station stat = new BO.Station() { Name = stationName, Address = stationAddress, Code = stationCode };
                bl.UpdateStation(stat);
                RefreshAllStations();
                MessageBox.Show("התחנה עודכנה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    

        private void deleteStation_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                MessageBoxResult res = MessageBox.Show("?אתה בטוח שאתה רוצה למחוק את התחנה", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.No)
                    return;
                int stationCode = int.Parse(stationCodeTextBlock.Text);
                bl.DeleteStation(stationCode);
                RefreshAllStations();
                Close();
                MessageBox.Show("התחנה נמחקה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LineListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }
    }
}
