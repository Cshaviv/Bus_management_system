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
            Program.CreatStatAndBus(ref AllStations, busLines);
            cbBusLines.ItemsSource = busLines;
            cbBusLines.DisplayMemberPath = "BusNumber";
            cbBusLines.SelectedIndex = 0;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusNumber);
        }
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busLines[index];
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.Stations;
        }
        //private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        //{

        //}
    }
}
