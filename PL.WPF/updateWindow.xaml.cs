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
        public updateWindow(Bus b, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            licenseNumTextBox.Text = b.LicenseNum.ToString();
            startDateDatePicker.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            lastTreatDatePicker.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            kmTextBox.Text = b.TotalKm.ToString();
            kmafterrefuelingTextBox.Text = b.KmLastTreat.ToString();
            kmaftertreatTextBox.Text = b.kmAfterRefuling.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
    }
}
