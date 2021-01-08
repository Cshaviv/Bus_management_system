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
        public void RefreshAllLine()
        {
            line = bl.GetLine(line.LineId);
            LineNum.DataContext = line;
            linesListBox.DataContext = line.Stations;
            //List<BO.Bus> buses = bl.GetAllBuses().ToList();
            //LBBuses.DataContext = buses;
        }
        private void winUpdate_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefreshAllLine();
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
            UpdateDistanceAndTime win = new UpdateDistanceAndTime(bl, st, next);
            win.Closing += winUpdate_Closing;
            win.ShowDialog();

        }
        private void deleteStationClick(object sender, RoutedEventArgs e)
        {
            BO.StationInLine station = (sender as Button).DataContext as BO.StationInLine;
            try
            {
                if (MessageBox.Show("Do you want to delete this station?", "delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bl.DeleteLineStation(line.LineId, station.StationCode);
                    line = bl.GetLine(line.LineId);
                    linesListBox.DataContext = line.Stations;//refresh                
                }
                else
                {
                    return;

                }
            } 
            catch (Exception)
            {

            }
        }
        private void Addstation_Click(object sender, RoutedEventArgs e)
        {
            AddStationToLine win = new AddStationToLine(bl, line);
            win.Closing += winUpdate_Closing;
            win.ShowDialog();
        }
        private void deleteLine_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Are you sure deleting selected line?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            try
            {
                bl.DeleteLine(line.LineId);
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
