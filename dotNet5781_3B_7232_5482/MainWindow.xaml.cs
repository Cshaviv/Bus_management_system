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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace dotNet5781_3B_7232_5482
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
            AddNewBus add = new AddNewBus();
            add.BusesCollection = BusesCollection;
            add.ShowDialog();
        }
    }
}
