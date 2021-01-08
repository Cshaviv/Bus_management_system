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
    /// Interaction logic for AddBusWindow.xaml
    /// </summary>
    public partial class AddBusWindow : Window
    {
        IBL bl;
        BO.Bus bus;
        public AddBusWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            busStatusCombo.ItemsSource = Enum.GetValues(typeof(BusStatus)).Cast<BusStatus>();
            busStatusCombo.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int licenseNum = int.Parse(licenseNumTextBox.Text);
                double fuel = double.Parse(fuelTankTextBox.Text);
                DateTime startDate = DateTime.Parse(startDateDatePicker.Text);
                DateTime lastDate = DateTime.Parse(dateLastTreatDatePicker.Text);
                double kmLastTreat = double.Parse(kmLastTreatTextBox.Text);
                BO.BusStatus status = (BO.BusStatus)Enum.Parse(typeof(BO.BusStatus), busStatusCombo.SelectedItem.ToString());
                double totalKm = double.Parse(totalKmTextBox.Text);
                BO.Bus b = new BO.Bus() { LicenseNum = licenseNum, kmAfterRefuling = fuel, StartDate = startDate, DateLastTreat = lastDate, StatusBus = status, TotalKm = totalKm, KmLastTreat = kmLastTreat };
                bl.AddBus(b);
                Close();
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.licenseNum, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                licenseNumTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (bus != null)
            {
                bl.AddBus(bus);
                Close();
            }

        }
    }
}
