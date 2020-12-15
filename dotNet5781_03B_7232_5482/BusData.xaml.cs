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

namespace dotNet5781_03B_7232_5482
{
    /// <summary>
    /// Interaction logic for BusData.xaml
    /// </summary>
    public partial class BusData : Window
    {
        public Bus myBus { get; set; }
        public BusData(Bus b)
        {
            InitializeComponent();
            Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width - Width) / 2;
            licenseNumTextBlock.Text = b.ToString();
            startDateTextBlock.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            dateTreatTextBlock.Text = b.LastTreat.Day + "/" + b.LastTreat.Month + "/" + b.LastTreat.Year;
            totalKmTextBlock.Text = b.Km.ToString();
            kmAfterTreatTextBlock.Text = b.Kmaftertreat.ToString();
            kmAfterRefulingTextBlock.Text = b.Kmafterrefueling.ToString();
            busStatusTextBlock.Text = b.myStatus.ToString();//לבדו'
            myBus = b;
        }
    }
}
// השןרה של האפליקציה שורה מספר 26, 
//bus data xaml line 10?
//לבדוקקק//