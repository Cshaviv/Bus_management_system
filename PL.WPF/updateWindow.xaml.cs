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
    /// Interaction logic for updateWindow.xaml
    /// </summary>
    public partial class updateWindow : Window
    {
        IBL bl;
        BO.Bus curBus;
        public updateWindow(Bus b, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
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

        private void update_Click(object sender, RoutedEventArgs e)
        {
           
                try
                {
                    int licenseNum = int.Parse(licenseNumTextBox.Text);
                    double fuel = double.Parse(kmafterrefuelingTextBox.Text);
                    DateTime startDate = DateTime.Parse(startDateDatePicker.Text);
                    DateTime lastDate = DateTime.Parse(lastTreatDatePicker.Text);
                    double kmLastTreat = double.Parse(kmafterTreatTextBox.Text);
                    double totalKm = double.Parse(kmTextBox.Text);
                    BO.Bus b = new BO.Bus() { LicenseNum = licenseNum, kmAfterRefuling = fuel, StartDate = startDate, DateLastTreat = lastDate,  TotalKm = totalKm, KmLastTreat = kmLastTreat };
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
                if (curBus != null)
                    bl.AddBus(curBus);
            
        }

    }
}
