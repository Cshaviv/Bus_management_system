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
        public AddBusWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            //busStatusCombo.ItemsSource = Enum.GetValues(typeof(BusStatus)).Cast<BusStatus>();
            //busStatusCombo.SelectedIndex = 0;
        }//yes
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }//yes
        private void AddBus_Click(object sender, RoutedEventArgs e)//yes
        {
            try
            {
                int licenseNum = int.Parse(licenseNumTextBox.Text);
                DateTime startDate = DateTime.Parse(startDateDatePicker.Text);
                DateTime lastDate = DateTime.Parse(dateLastTreatDatePicker.Text);
                double kmLastTreat = double.Parse(kmLastTreatTextBox.Text);
                double fuel = double.Parse(fuelTankTextBox.Text);
                double totalKm = double.Parse(totalKmTextBox.Text);
                //if (licenseNum = null || startDate = null || lastDate = null||)
                //BO.BusStatus status = (BO.BusStatus)Enum.Parse(typeof(BO.BusStatus), busStatusCombo.SelectedItem.ToString());
                BO.Bus b = new BO.Bus() { LicenseNum = licenseNum, FuelTank = fuel, StartDate = startDate, DateLastTreat = lastDate, StatusBus = BusStatus.Available, TotalKm = totalKm, KmLastTreat = kmLastTreat };
                if(b!=null)
                { 
                    bl.AddBus(b);
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.licenseNum, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                Exceptions(ex.num, ex.Message);          
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
       

        }
        private void Exceptions(int num,string massage)//yes
        {
            if (num == 1)
            {
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                licenseNumTextBox.BorderBrush = Brushes.Green;
                startDateDatePicker.BorderBrush = Brushes.Red;
                dateLastTreatDatePicker.BorderBrush = Brushes.Green;
                kmLastTreatTextBox.BorderBrush = Brushes.Green;
                fuelTankTextBox.BorderBrush = Brushes.Green;
                totalKmTextBox.BorderBrush = Brushes.Green;
            }
            if (num == 2)
            {
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                licenseNumTextBox.BorderBrush = Brushes.Green;
                startDateDatePicker.BorderBrush = Brushes.Green;
                dateLastTreatDatePicker.BorderBrush = Brushes.Red;
                kmLastTreatTextBox.BorderBrush = Brushes.Green;
                fuelTankTextBox.BorderBrush = Brushes.Green;
                totalKmTextBox.BorderBrush = Brushes.Green;
            }
            if (num == 3)
            {
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                licenseNumTextBox.BorderBrush = Brushes.Green;
                startDateDatePicker.BorderBrush = Brushes.Green;
                dateLastTreatDatePicker.BorderBrush = Brushes.Green;
                kmLastTreatTextBox.BorderBrush = Brushes.Green;
                fuelTankTextBox.BorderBrush = Brushes.Green;
                totalKmTextBox.BorderBrush = Brushes.Red;
            }
            if (num == 4)
            {
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                licenseNumTextBox.BorderBrush = Brushes.Green;
                startDateDatePicker.BorderBrush = Brushes.Green;
                dateLastTreatDatePicker.BorderBrush = Brushes.Green;
                kmLastTreatTextBox.BorderBrush = Brushes.Red;
                fuelTankTextBox.BorderBrush = Brushes.Green;
                totalKmTextBox.BorderBrush = Brushes.Green;
            }
            if (num == 5)
            {
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                licenseNumTextBox.BorderBrush = Brushes.Green;
                startDateDatePicker.BorderBrush = Brushes.Green;
                dateLastTreatDatePicker.BorderBrush = Brushes.Green;
                kmLastTreatTextBox.BorderBrush = Brushes.Green;
                fuelTankTextBox.BorderBrush = Brushes.Red;
                totalKmTextBox.BorderBrush = Brushes.Green;
            }
            if (num == 6)
            {
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                licenseNumTextBox.BorderBrush = Brushes.Red;
                startDateDatePicker.BorderBrush = Brushes.Green;
                dateLastTreatDatePicker.BorderBrush = Brushes.Green;
                kmLastTreatTextBox.BorderBrush = Brushes.Green;
                fuelTankTextBox.BorderBrush = Brushes.Green;
                totalKmTextBox.BorderBrush = Brushes.Green;
            }
        }
    }
}
