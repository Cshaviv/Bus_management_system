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
        public ProgressBar prop { get; set; }
        public Label label { get; set; }
        public Label action { get; set; }
        public Rectangle statusRectangle { get; set; }
        public BusData(Bus b, ProgressBar p,Label l,Label a, Rectangle s)
        {
            InitializeComponent();
            Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width - Width) / 2;
            licenseNumTextBlock.Text = b.ToString();
            startDateTextBlock.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            dateTreatTextBlock.Text = b.LastTreat.Day + "/" + b.LastTreat.Month + "/" + b.LastTreat.Year;
            totalKmTextBlock.Text = b.Km.ToString();
            kmAfterTreatTextBlock.Text = b.Kmaftertreat.ToString();
            kmAfterRefulingTextBlock.Text = b.Kmafterrefueling.ToString();
            myBus = b;
            prop = p;
            label = l;
            action = a;
            statusRectangle = s;
        }
        private void RefuelBus(object sender, RoutedEventArgs e)
        {
            if (myBus.myStatus == STATUS.OnRide || myBus.myStatus == STATUS.OnTreat || myBus.myStatus == STATUS.OnRefueling)
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.Kmafterrefueling == 0)
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            myBus.myStatus = STATUS.OnRefueling;
            prop.Foreground = Brushes.Yellow;
            statusRectangle.Fill = Brushes.Yellow;
            string massage = "The bus was refueled successfully.";
            string title = "Refuel  ";
            action.Content = "on refueling...";
            DataThread data = new DataThread(prop, label, 12, myBus, massage, title,action,statusRectangle);
            data.Start(data);
            myBus.Kmafterrefueling = 0;
        }
        private void TreatBus(object sender, RoutedEventArgs e)
        {
            if (myBus.myStatus == STATUS.OnRide || myBus.myStatus == STATUS.OnTreat || myBus.myStatus == STATUS.OnRefueling)
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.Kmaftertreat == 0 && (DateTime.Now == myBus.LastTreat))
            {
                MessageBox.Show("The bus was already treatmented", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
         
                myBus.myStatus = STATUS.OnTreat;
            statusRectangle.Fill = Brushes.DeepSkyBlue;
                prop.Foreground = Brushes.DeepSkyBlue;
                string massage = "Treatment successfully";
                string title = "Treat  ";
                action.Content = "in traetment...";
                DataThread data = new DataThread(prop, label, 12, myBus, massage, title,action,statusRectangle);
                data.Start(data);
                myBus.Kmaftertreat = 0;
            
        }
    }
}
