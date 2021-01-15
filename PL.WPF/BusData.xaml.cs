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
        //Rectangle IsDeletedRectangle;
        ListBox busesListBox;
        

        public BusData(Bus _bus,IBL _bl,  ListBox _busesListBox)
        {
            InitializeComponent();
            bl = _bl;
            bus = _bus;
            busesListBox = _busesListBox;
            licenseNumTextBox.Text = bus.LicenseNum.ToString();
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

        private void delete_Click(object sender, RoutedEventArgs e)//yes
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
                MessageBoxResult res = MessageBox.Show("?האם אתה בטוח שברצונך לשנות את הנתונים", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (res == MessageBoxResult.Yes)
                {
                    int licenseNum = int.Parse(licenseNumTextBox.Text);
                    double fuel = double.Parse(fuelTankTextBox.Text);
                    DateTime startDate = DateTime.Parse(startDateDatePicker.Text);
                    DateTime lastDate = DateTime.Parse(lastTreatDatePicker.Text);
                    double kmLastTreat = double.Parse(kmafterTreatTextBox.Text);
                    //BO.BusStatus status = (BO.BusStatus)Enum.Parse(typeof(BO.BusStatus), busStatusCombo.SelectedItem.ToString());
                    double totalKm = double.Parse(totalKmTextBox.Text);
                    BO.Bus b = new BO.Bus() { LicenseNum = licenseNum, FuelTank = fuel, StartDate = startDate, DateLastTreat = lastDate, /*StatusBus = status,*/ TotalKm = totalKm, KmLastTreat = kmLastTreat };
                    bl.UpdateBusDetails(b);
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    licenseNumTextBox.Text = bus.LicenseNum.ToString();
                    startDateDatePicker.Text = bus.StartDate.Day + "/" + bus.StartDate.Month + "/" + bus.StartDate.Year;
                    lastTreatDatePicker.Text = bus.DateLastTreat.Day + "/" + bus.DateLastTreat.Month + "/" + bus.DateLastTreat.Year;
                    totalKmTextBox.Text = bus.TotalKm.ToString();
                    fuelTankTextBox.Text = bus.FuelTank.ToString();
                    kmafterTreatTextBox.Text = bus.KmLastTreat.ToString();
                }
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message + " " + ex.licenseNum, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
