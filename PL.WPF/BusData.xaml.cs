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
    /// Interaction logic for BusData.xaml
    /// </summary>
    public partial class BusData : Window
    {
        IBL bl;
        BO.Bus bus;
        Rectangle IsDeletedRectangle;
        public BusData(Bus b,IBL _bl, Rectangle _IsDeletedRectangle)
        {
            InitializeComponent();
            bl = _bl;
            bus = b;
            IsDeletedRectangle = _IsDeletedRectangle;
            licenseNumTextBox.Text = b.LicenseNum.ToString();
            startDateDatePicker.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            lastTreatDatePicker.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            kmTextBox.Text = b.TotalKm.ToString();
            kmafterrefuelingTextBox.Text = b.KmLastTreat.ToString();
            kmafterTreatTextBox.Text = b.kmAfterRefuling.ToString();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure deleting selected bus?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            try
            {
                if (bus != null)
                {
                    bl.DeleteBus(bus.LicenseNum);
                    IsDeletedRectangle.Fill = Brushes.Red;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        


        private void updateButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                int licenseNum = int.Parse(licenseNumTextBox.Text);
                double fuel = double.Parse(kmafterrefuelingTextBox.Text);
                DateTime startDate = DateTime.Parse(startDateDatePicker.Text);
                DateTime lastDate = DateTime.Parse(lastTreatDatePicker.Text);
                double kmLastTreat = double.Parse(kmafterTreatTextBox.Text);
                double totalKm = double.Parse(kmTextBox.Text);
                BO.Bus b = new BO.Bus() { LicenseNum = licenseNum, kmAfterRefuling = fuel, StartDate = startDate, DateLastTreat = lastDate, TotalKm = totalKm, KmLastTreat = kmLastTreat };
                bl.UpdateBus(b);
            }
            catch (Exception ex) { MessageBox.Show("Error", "", MessageBoxButton.OK, MessageBoxImage.Error); }
            Close();

        }
    }
}
