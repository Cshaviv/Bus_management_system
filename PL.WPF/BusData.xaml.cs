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
        public BusData(Bus b,IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            licenseNumTextBlock.Text = b.LicenseNum.ToString();
            startDateTextBlock.Text= b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            dateTreatTextBlock.Text= b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            totalKmTextBlock.Text = b.TotalKm.ToString();
            kmAfterTreatTextBlock.Text = b.KmLastTreat.ToString();
            kmAfterRefulingTextBlock.Text = b.kmAfterRefuling.ToString();
            busStatusTextBlock.Text = b.StatusBus.ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
    }
}
