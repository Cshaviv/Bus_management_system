 using BLAPI;//
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
    /// Interaction logic for BusData.xaml
    /// </summary>
    public partial class BusData : Window
    {
        IBL bl;
        BO.Bus bus;
        ListBox busesListBox;
        public BusData(bool _isDelete, Bus _bus, IBL _bl, ListBox _busesListBox)
        {
            InitializeComponent();
            bl = _bl;
            bus = _bus;
           if(_isDelete==true)
            {
                delete.Visibility = Visibility.Hidden;
                updateButton.Visibility = Visibility.Hidden;
                reful.Visibility = Visibility.Hidden;
                treat.Visibility = Visibility.Hidden;
            }
            GetBus(bus);
            busesListBox = _busesListBox;
          
        }
        void GetBus(Bus bus)
        {
            licenseNumTextBlock.Text = bus.ToString();
            startDateDatePicker.Text = bus.StartDate.Day + "/" + bus.StartDate.Month + "/" + bus.StartDate.Year;
            lastTreatDatePicker.Text = bus.DateLastTreat.Day + "/" + bus.DateLastTreat.Month + "/" + bus.DateLastTreat.Year;
            totalKmTextBox.Text = bus.TotalKm.ToString();
            fuelTankTextBox.Text = bus.FuelTank.ToString();
            kmafterTreatTextBox.Text = bus.KmLastTreat.ToString();
        }
        void RefreshAllBuses()
        {
            busesListBox.ItemsSource = bl.GetAllBuses().ToList();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
        private void delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("?האם אתה בטוח שברצונך למחוק את האוטובוס", "delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    if (bus != null)
                    {
                        bl.DeleteBus(bus.LicenseNum);
                        MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                        Close();
                        RefreshAllBuses();
                    }
                }
                else
                {
                    return;
                }
            }
    
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message + " " + ex.licenseNum, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void updateButtonClick(object sender, RoutedEventArgs e)//yes
        {
            try
            {
                if (bus.StatusBus == BusStatus.OnTreatment || bus.StatusBus == BusStatus.OnRefueling)// Check if the bus can be sent for refueling
                {
                    MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (MessageBox.Show("?האם אתה בטוח שברצונך לשנות את הנתונים", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    HelpExceptions();
                    double fuel = double.Parse(fuelTankTextBox.Text);
                    DateTime startDate = DateTime.Parse(startDateDatePicker.Text);
                    DateTime lastDate = DateTime.Parse(lastTreatDatePicker.Text);
                    double kmLastTreat = double.Parse(kmafterTreatTextBox.Text);
                    double totalKm = double.Parse(totalKmTextBox.Text);
                    BO.Bus b = new BO.Bus() { LicenseNum = bus.LicenseNum, FuelTank = fuel, StartDate = startDate, DateLastTreat = lastDate, TotalKm = totalKm, KmLastTreat = kmLastTreat };
                    bl.UpdateBusDetails(b);              
                    RefreshAllBuses();
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                licenseNumTextBlock.Text = bus.LicenseNum.ToString();
                startDateDatePicker.Text = bus.StartDate.Day + "/" + bus.StartDate.Month + "/" + bus.StartDate.Year;
                lastTreatDatePicker.Text = bus.DateLastTreat.Day + "/" + bus.DateLastTreat.Month + "/" + bus.DateLastTreat.Year;
                totalKmTextBox.Text = bus.TotalKm.ToString();
                fuelTankTextBox.Text = bus.FuelTank.ToString();
                kmafterTreatTextBox.Text = bus.KmLastTreat.ToString();
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
        private void HelpExceptions()
        {
            startDateDatePicker.BorderBrush = Brushes.Gray;
            lastTreatDatePicker.BorderBrush = Brushes.Gray;
            kmafterTreatTextBox.BorderBrush = Brushes.Gray;
            fuelTankTextBox.BorderBrush = Brushes.Gray;
            totalKmTextBox.BorderBrush = Brushes.Gray;
        }
        private void Exceptions(int num, string massage)
        {
            if (num == 1)
            {
                HelpExceptions();
                startDateDatePicker.BorderBrush = Brushes.Red;
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (num == 2)
            {
                HelpExceptions();
                lastTreatDatePicker.BorderBrush = Brushes.Red;
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (num == 3)
            {
                HelpExceptions();
                totalKmTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (num == 4)
            {
                HelpExceptions();
                kmafterTreatTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (num == 5)
            {
                HelpExceptions();
                fuelTankTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);          
            }
            if (num == 6)
            {
                HelpExceptions();
                startDateDatePicker.BorderBrush = Brushes.Red;
                MessageBox.Show(massage, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);         
            }
        }
        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }
        private void reful_Click(object sender, RoutedEventArgs e)
        {        
            if (bus.FuelTank == 0)
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            BO.Bus b = new BO.Bus() { LicenseNum = bus.LicenseNum, FuelTank = 0, StartDate = bus.StartDate, DateLastTreat = bus.DateLastTreat, /*StatusBus = status,*/ TotalKm = bus.TotalKm, KmLastTreat = bus.KmLastTreat };
            bl.UpdateBusDetails(b);
            RefreshAllBuses();
            closeButton_Click(sender, e);
        }
        private void treat_Click(object sender, RoutedEventArgs e)
        { 
            if (bus.KmLastTreat == 0 && (DateTime.Now == bus.DateLastTreat))//If he did the treatment today and has not traveled since
            {
                MessageBox.Show("The bus was already treatmented", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            BO.Bus b = new BO.Bus() { LicenseNum = bus.LicenseNum, FuelTank = bus.FuelTank, StartDate = bus.StartDate, DateLastTreat = DateTime.Now, /*StatusBus = status,*/ TotalKm = bus.TotalKm, KmLastTreat = 0 };
            bl.UpdateBusDetails(b);
            closeButton_Click(sender, e);
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)//this func close the window
        {
            this.Close();
        }

      
    }
}
