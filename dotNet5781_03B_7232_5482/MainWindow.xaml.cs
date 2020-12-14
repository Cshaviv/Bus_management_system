using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
//using ToolsWPF;
using System.Data;
using System.Drawing;


//xmlns: local = "clr-namespace:dotNet5781_03B_7232_5482"

namespace dotNet5781_03B_7232_5482
{
    /// <summary> 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rand = new Random();
        public ObservableCollection<Bus> BusesCollection;

       // public static object Application { get; internal set; }

        //BackgroundWorker workerRefuel;
        public MainWindow()
        {

            InitializeComponent();
            BusesCollection = new ObservableCollection<Bus>();
            Buses.RestartBuses(BusesCollection);
            BusList.ItemsSource = BusesCollection;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewBus add = new AddNewBus();
            add.BusesCollection = BusesCollection;
            add.ShowDialog();
        }
        private void Refuel(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;//the bus
            if (b.myStatus == STATUS.OnRide || b.myStatus == STATUS.OnTreat || b.myStatus == STATUS.OnRefueling)
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                b.Kmafterrefueling = 0;
                MessageBox.Show("Refueling successfully", "MESSAGE", MessageBoxButton.OK);
            }
        }
        private void StartDriveButtonClick(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;//the bus
            if (b.myStatus == STATUS.OnRefueling)
                MessageBox.Show("The bus is in refueling.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (b.myStatus == STATUS.OnTreat)
                MessageBox.Show("The bus is in treatment.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (b.myStatus == STATUS.OnRide)
                MessageBox.Show("The bus is on ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (((DateTime.Now - b.LastTreat).TotalDays > 365 || b.Kmaftertreat >= 20000) && b.Kmafterrefueling >= 1200)
                MessageBox.Show("The bus needs to be sent for a treatment and refueling.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if ((DateTime.Now - b.LastTreat).TotalDays > 365 || b.Kmaftertreat >= 20000)
                MessageBox.Show("The bus needs to be sent for a treatment.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (b.Kmafterrefueling >= 1200)
                MessageBox.Show("The bus needs to be sent for refueling.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                DepartureToRide win = new DepartureToRide();
                win.myBus = b;
                win.ShowDialog();
            }
        }

    }
}