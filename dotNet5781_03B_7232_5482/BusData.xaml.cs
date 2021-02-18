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
    public partial class BusData : Window// windows of bus data
    {
        public Bus myBus { get; set; }
        public ProgressBar prop { get; set; }
        public Label label { get; set; }
        public Label action { get; set; }
        public Rectangle statusRectangle { get; set; }
        public Label timer { get; set; }
        public TextBlock km { get; set; }
        public BusData(Bus b, ProgressBar p, Label l, Label a, Rectangle s, Label t, TextBlock k)//constructor
        {
            InitializeComponent();
            Left = Application.Current.MainWindow.Left + (Application.Current.MainWindow.Width - Width) / 2;
            licenseNumTextBlock.Text = b.ToString();
            startDateTextBlock.Text = b.StartDate.Day + "/" + b.StartDate.Month + "/" + b.StartDate.Year;
            dateTreatTextBlock.Text = b.LastTreat.Day + "/" + b.LastTreat.Month + "/" + b.LastTreat.Year;
            totalKmTextBlock.Text = b.Km.ToString();
            kmAfterTreatTextBlock.Text = b.Kmaftertreat.ToString();
            kmAfterRefulingTextBlock.Text = b.Kmafterrefueling.ToString();
            busStatusTextBlock.Text = b.myStatus.ToString();
            myBus = b;
            prop = p;
            label = l;
            action = a;
            statusRectangle = s;
            timer = t;
            km = k;
        }
        private void RefuelBus(object sender, RoutedEventArgs e)//When you press the button of RefuelBus
        {
            if (myBus.myStatus == STATUS.OnRide || myBus.myStatus == STATUS.OnTreat || myBus.myStatus == STATUS.OnRefueling)// Check if the bus can be sent for refueling
             {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.Kmafterrefueling == 0)//When the fuel tank is full to the end can not be sent for refueling.
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            myBus.myStatus = STATUS.OnRefueling;//update status
            prop.Foreground = Brushes.Yellow;//the prop will be painted yellow
            statusRectangle.Fill = Brushes.Yellow;//The square that indicates the status of the bus will be painted yellow
            string massage = "The bus was refueled successfully.";
            string title = "Refuel  ";
            action.Content = "on refueling...";
            
            DataThread data = new DataThread(prop, label, 12, myBus, massage, title, action, statusRectangle, timer, km);//Sending the necessary data for the process
            data.Start(data);//Start of the procession

            myBus.Kmafterrefueling = 0;//update fields
            closeButton_Click(sender, e);//close the window of data bus

        }
        private void TreatBus(object sender, RoutedEventArgs e)// When you press the button of treatBus
        {
            if (myBus.myStatus == STATUS.OnRide || myBus.myStatus == STATUS.OnTreat || myBus.myStatus == STATUS.OnRefueling)// Check if the bus can be sent for treat
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.Kmaftertreat == 0 && (DateTime.Now == myBus.LastTreat))//If he did the treatment today and has not traveled since
            {
                MessageBox.Show("The bus was already treatmented", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            myBus.myStatus = STATUS.OnTreat;//update status
            statusRectangle.Fill = Brushes.DeepSkyBlue;//The square that indicates the status of the bus will be painted skyBlue
            prop.Foreground = Brushes.DeepSkyBlue;//the prop will be painted skyBlue
            string massage = "Treatment successfully";
            string title = "Treat  ";
            action.Content = "in traetment...";
            DataThread data = new DataThread(prop, label, 144, myBus, massage, title, action, statusRectangle, timer, km);//Sending the necessary data for the process
            data.Start(data);//Start of the procession
            myBus.Kmaftertreat = 0;//update fields
            myBus.LastTreat = DateTime.Now;
            if (myBus.Kmafterrefueling == 1200)
            {
                myBus.Kmafterrefueling = 0;
            }
            closeButton_Click(sender, e);//close the window of data bus
        }
        private void closeButton_Click(object sender, RoutedEventArgs e)//this func close the window
        {
            this.Close();
        }
    }
}
