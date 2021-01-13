 using BLAPI;
using BO;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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
        //Rectangle IsDeletedRectangleLine;
        public LineDeta(IBL _bl, BO.Line _line, Rectangle _IsDeletedRectangleLine)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            //IsDeletedRectangleLine = _IsDeletedRectangleLine;
            linesListBox.DataContext = line.Stations;
            linesListBox.Visibility = Visibility.Visible;
            LineNumTextBlock.Text = line.LineNum.ToString();
            LineNumTextBox.Text = line.LineNum.ToString();
            AreaTextBlock.Text = line.Area.ToString();
            AreaComboBox.ItemsSource= Enum.GetValues(typeof(Area));
            AreaComboBox.Text = line.Area.ToString();
         
        }
        public void RefreshAllLine()
        {
            line = bl.GetLine(line.LineId);
            linesListBox.DataContext = line.Stations;         
        }
        private void winUpdate_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefreshAllLine();
        }
        private void deleteStationClick(object sender, RoutedEventArgs e)
        {
            BO.StationInLine station = (sender as Button).DataContext as BO.StationInLine;
            try
            {
                if (MessageBox.Show("Do you want to delete this station?", "delete", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bl.DeleteStationInLine(line.LineId, station.StationCode/*line.Stations[station.LineStationIndex-1].StationCode, line.Stations[station.LineStationIndex +1].StationCode*/);
                    RefreshAllLine();
                }
                else
                {
                    return;

                }
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {

            }
        }
        private void Addstation_Click(object sender, RoutedEventArgs e)
        {
            AddStationInLineWin win = new AddStationInLineWin(bl, line);
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
                if (line != null)
                {
                    bl.DeleteLine(line.LineId);
                    //IsDeletedRectangleLine.Fill = Brushes.Red;
                    //Close();
                }
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }
        private void update_Click(object sender, RoutedEventArgs e)
        {
            LineNumTextBlock.Visibility = Visibility.Hidden;
            AreaTextBlock.Visibility = Visibility.Hidden;
            AreaComboBox.Visibility = Visibility.Visible;
            LineNumTextBox.Visibility = Visibility.Visible;
            Save.Visibility = Visibility.Visible;
            Cancel.Visibility = Visibility.Visible;
            
        }
        private void SaveClick(object sender, RoutedEventArgs e)
        {         
            RefreshData();
            LineNumTextBlock.Visibility = Visibility.Visible;
            AreaTextBlock.Visibility = Visibility.Visible;
            AreaComboBox.Visibility = Visibility.Hidden;
            LineNumTextBox.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
            Cancel.Visibility = Visibility.Hidden;
        }
        public void RefreshData()
        {
            
                int lineNumber = int.Parse(LineNumTextBox.Text);
                BO.Area area = (BO.Area)Enum.Parse(typeof(BO.Area), AreaComboBox.SelectedItem.ToString());
                BO.Line lineUpdate = new BO.Line() { LineId = line.LineId,FirstStation= line.Stations[0].StationCode, LastStation = line.Stations[line.Stations.Count-1].StationCode, LineNum =lineNumber, Area = area, Stations = line.Stations };
                try
                {
                    bl.UpdateLineDetails(lineUpdate);
                    Close();
                }
                catch (BO.BadLineIdException ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
        }
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            LineNumTextBlock.Visibility = Visibility.Visible;
            AreaTextBlock.Visibility = Visibility.Visible;
            AreaComboBox.Visibility = Visibility.Hidden;
            LineNumTextBox.Visibility = Visibility.Hidden;
            Save.Visibility = Visibility.Hidden;
            Cancel.Visibility = Visibility.Hidden;
        }
        private void updateStationClick(object sender, RoutedEventArgs e)
        {
            BO.StationInLine stat = (sender as Button).DataContext as BO.StationInLine;
            if (stat.StationCode == line.Stations[line.Stations.Count-1].StationCode)
            {
                MessageBox.Show("travel distance/time from Last station cant be updated.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            BO.StationInLine next = line.Stations[stat.LineStationIndex+1];
            UpdateDistanceAndTime win = new UpdateDistanceAndTime(bl, stat, next);
            win.Closing += winUpdate_Closing;
            win.ShowDialog();
        }
    }
}
