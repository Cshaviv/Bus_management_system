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
    /// Interaction logic for BusDataWindows.xaml
    /// </summary>
    public partial class BusDataUser : Window
    {
        IBL bl;
        BO.Bus bus;
        public BusDataUser(Bus b, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            bus = b;
            licenseNumTextBlock.Text = b.ToString();
            startDateTextBlock.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            lastTreatTextBlock.Text = b.DateLastTreat.Day + "/" + b.DateLastTreat.Month + "/" + b.DateLastTreat.Year;
            totalKmTextBlock.Text = b.TotalKm.ToString();
            fuelTankTextBlock.Text = b.FuelTank.ToString();
            kmafterTreatTextBlock.Text = b.KmLastTreat.ToString();
            
        }
    }
}
