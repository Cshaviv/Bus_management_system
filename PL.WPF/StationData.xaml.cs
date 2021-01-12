using BLAPI;
using BO;
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
    /// Interaction logic for StationData.xaml
    /// </summary>
    public partial class StationData : Window
    {
        IBL bl;
        BO.Station station;
       // ListBox stationsListBox;
        public StationData( IBL _bl, BO.Station _station) //, ListBox _stationsListBox)
        {
            InitializeComponent();
            //stationsListBox.DataContext = stationsListBox;
            station = _station;
            //linesListBox.DataContext = line.Stations;
            //statCodeTextBox.
            //LineNumTextBox.Text = line.LineNum.ToString();
            //AreaTextBlock.Text = line.Area.ToString();
            //AreaComboBox.ItemsSource = Enum.GetValues(typeof(Area));
            //AreaComboBox.Text = line.Area.ToString();

        }
    }
}
