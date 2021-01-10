using BLAPI;
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

namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for AddStationToLine.xaml
    /// </summary>
    public partial class AddStationToLine : Window
    {
        IBL bl;
        BO.Line line;
        public AddStationToLine(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            List<BO.Station> station = bl.GetAllStations().ToList();
            List<BO.StationInLine> StationInLine = line.Stations.ToList();
            stationComboBox.ItemsSource = station;
            stationComboBox.SelectedItem = "Code";
            PrevstationComboBox.ItemsSource = StationInLine;
            PrevstationComboBox.SelectedItem = "Code";
        }
    }
}
