
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

//xmlns: local = "clr-namespace:dotNet5781_03B_7232_5482"

namespace dotNet5781_03B_7232_5482
{
    /// <summary> 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Bus> BusesCollection;
        public MainWindow(ObservableCollection<Bus> S)
        { 
            
            InitializeComponent();
            ObservableCollection<Bus> BusesCollection = new ObservableCollection<Bus>();
            BusesCollection.Add(new Bus(1234567, new DateTime(2016, 03, 04), new DateTime(2020, 03, 04),20000, 1000 ,500)); // bus 1
            BusesCollection.Add(new Bus(22222222, new DateTime(2019, 01, 04), new DateTime(2020, 01, 01), 30000, 500, 500));//need treat soon (עברה כמעט שנה)
            BusesCollection.Add(new Bus(3434343, new DateTime(2017, 12, 10), new DateTime(2020, 12, 04), 15000, 50, 1100));//need refueling soon (נסע כמעט 1200 ק"מ)
            BusesCollection.Add(new Bus(6668888, new DateTime(2016, 03, 04), new DateTime(2017, 03, 04), 10000, 1000, 500));
            BusesCollection.Add(new Bus(12345678, new DateTime(2018, 03, 04), new DateTime(2018, 02, 05), 1978, 1067, 543));
            BusesCollection.Add(new Bus(12345679, new DateTime(2020, 03, 04), new DateTime(2021, 02, 04), 12345, 1020, 500));
            BusesCollection.Add(new Bus(1234967, new DateTime(2016, 10, 15), new DateTime(2017, 03, 04), 10000, 19800, 480));//need treat soon, almost road 20000 km from last treat
            BusesCollection.Add(new Bus(1223344, new DateTime(2016, 03, 09), new DateTime(2017, 11, 01), 10000, 1000, 500));
            BusesCollection.Add(new Bus(1284666, new DateTime(2015, 06, 12), new DateTime(2016, 05, 01), 10000, 1000, 500));
            BusesCollection.Add(new Bus(1239997, new DateTime(2016, 03, 04), new DateTime(2017, 03, 04), 10000, 1000, 500));
            





            //    List<BusStation> AllStations = new List<BusStation>();
            //    List<BusLine> lines = new List<BusLine>();
            //    busLines = new BusCollection();
            //    Program.CreatStatAndBus(ref AllStations, busLines);//add random buses and random stations
            //    cbBusLines.ItemsSource = busLines;
            //    cbBusLines.DisplayMemberPath = "BusNumber";
            //    cbBusLines.SelectedIndex = 0;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BusList.ItemsSource = BusesCollection;
        }
    }
}
