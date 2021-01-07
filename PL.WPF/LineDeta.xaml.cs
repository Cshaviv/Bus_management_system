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
    /// Interaction logic for LineDeta.xaml
    /// </summary>
    public partial class LineDeta : Window
    {
        IBL bl;
        BO.Line line;
        public LineDeta(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            linesListBox.DataContext = line.Stations;
            linesListBox.Visibility = Visibility.Visible;
            //areaComboBox.Text = line.Area.ToString();
            LineNum.Text = "  קו מספר  " + line.LineNum.ToString();
        }
        private void updateStation(object sender, RoutedEventArgs e)//continue
        {

            BO.StationInLine st = (sender as Button).DataContext as BO.StationInLine;
            if (st.StationCode == line.Stations[line.Stations.Count - 1].StationCode)
            {
                MessageBox.Show("travel distance/time from Last station cant be updated.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BO.StationInLine next = line.Stations[st.LineStationIndex];
            //UpdateTimeAndDistance win = new UpdateTimeAndDistance(bl, st, next);
            //win.Closing += winUpdate_Closing;
            //win.ShowDialog();

        }
        //private void deleteStation(object sender, RoutedEventArgs e)
        //{
        //    BO.StationInLine station = (sender as Button).DataContext as BO.StationInLine;
        //    try
        //    {
        //        bl.DeleteLineStation(line.LineId, station.StationCode);
        //        line = bl.GetLine(line.LineId);
        //        linesListBox.DataContext = line.Stations;//refresh
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}
    }
}
