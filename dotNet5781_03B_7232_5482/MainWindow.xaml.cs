
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
        public MainWindow()
        { 
            
            InitializeComponent();
            BusesCollection = new ObservableCollection<Bus>();
            Buses.RestartBuses(BusesCollection);
            BusList.ItemsSource = BusesCollection;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddNewBus add = new AddNewBus(BusesCollection);
            add.ShowDialog();
        }
    }
}
