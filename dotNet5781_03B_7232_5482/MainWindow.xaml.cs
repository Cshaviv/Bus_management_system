
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
            DataContext = BusesCollection;




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
            //BusList.ItemsSource = BusesCollection;
        }
    }
}
