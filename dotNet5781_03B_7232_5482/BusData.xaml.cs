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
        private void RefuelBus(object sender, RoutedEventArgs e)
        {
            if (myBus.myStatus == STATUS.OnRide)
                MessageBox.Show("The bus in Ride, try refueling later.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (myBus.myStatus == STATUS.OnTreat)
                MessageBox.Show("The bus on treatment try refueling later.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (myBus.myStatus == STATUS.OnRefueling)
                MessageBox.Show("The bus already on refueling.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
           if(myBus.Kmafterrefueling==0)
                MessageBox.Show("The fuel tank on this bus is full.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                myBus.Kmafterrefueling = 0;
                MessageBox.Show("Refueling successfully", "MESSAGE", MessageBoxButton.OK);
            }
        }
        private void TreatBus(object sender, RoutedEventArgs e)
        {         
            if (myBus.myStatus == STATUS.OnRide)
                MessageBox.Show("The bus in Ride, try to treat later.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (myBus.myStatus == STATUS.OnRefueling)
                MessageBox.Show("The bus on refueling try to treat later.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if (myBus.myStatus == STATUS.OnTreat)
                MessageBox.Show("The bus already on Treatment.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            if(myBus.Kmaftertreat==0&& (DateTime.Now==myBus.LastTreat))
                MessageBox.Show("The bus was already treatmented", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            else
            {
                myBus.Kmaftertreat = 0;
                MessageBox.Show("Treatment successfully", "MESSAGE", MessageBoxButton.OK);
            }
        }
    }
}

// השןרה של האפליקציה שורה מספר 26, 
//bus data xaml line 10?
//לבדוקקק//