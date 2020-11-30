//Ayala Israeli 324207232 and Chagit Shaviv 322805482
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
using dotNet5781_02_7232_5482;

namespace dotNet5781_03A_7232_5482
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
 
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        private BusCollection busLines;
        public MainWindow()
        {
            InitializeComponent();
            List<BusStation> AllStations = new List<BusStation>();
            List<BusLine> lines = new List<BusLine>();
            busLines = new BusCollection();
            Program.CreatStatAndBus(ref AllStations, busLines);//add random buses and random stations
            cbBusLines.ItemsSource = busLines;
            cbBusLines.DisplayMemberPath = "BusNumber";
            cbBusLines.SelectedIndex = 0;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)//bus line show in combobox
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusNumber);
            tbArea.Text = (cbBusLines.SelectedValue as BusLine).Area.ToString();//area show in textbox
        }
       
        private void ShowBusLine(int index)//in func the busline accepted from the bus collection
        {
            currentDisplayBusLine = busLines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
        

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void tbArea_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
