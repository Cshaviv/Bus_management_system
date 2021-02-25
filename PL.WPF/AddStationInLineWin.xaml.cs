using BLAPI;
using System;/////
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
    /// Interaction logic for AddStationInLineWin.xaml
    /// </summary>
    public partial class AddStationInLineWin : Window
    {
        IBL bl;
        BO.Line line;
        public AddStationInLineWin(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            List<BO.Station> station = bl.GetAllStations().ToList();
            List<BO.StationInLine> StationInLine = line.Stations.ToList();
            stationComboBox.ItemsSource = station;
            stationComboBox.SelectedIndex = 0;
        }

        private void rbFirst_Checked(object sender, RoutedEventArgs e)
        {
            PrevStatTextBlock.Visibility = Visibility.Hidden;
            PrevstationComboBox.Visibility = Visibility.Hidden;
            PrevDistanceTextBlock.Visibility = Visibility.Hidden;
            PrevDistanceTextBox.Visibility = Visibility.Hidden;
            PrevTimeTextBlock.Visibility = Visibility.Hidden;
            PrevTimeTextBox.Visibility = Visibility.Hidden;
            NextDistanceTextBlock.Visibility = Visibility.Visible;
            NextDistanceTextBox.Visibility = Visibility.Visible;
            NextTimeTextBlock.Visibility = Visibility.Visible;
            NextTimeTextBox.Visibility = Visibility.Visible;
            AddLast.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Hidden;
            AddFirst.Visibility = Visibility.Visible;
        }
        private void rbLast_Checked(object sender, RoutedEventArgs e)
        {
            PrevStatTextBlock.Visibility = Visibility.Hidden;
            PrevstationComboBox.Visibility = Visibility.Hidden;
            PrevDistanceTextBlock.Visibility = Visibility.Visible;
            PrevDistanceTextBox.Visibility = Visibility.Visible;
            PrevTimeTextBlock.Visibility = Visibility.Visible;
            PrevTimeTextBox.Visibility = Visibility.Visible;
            NextDistanceTextBlock.Visibility = Visibility.Hidden;
            NextDistanceTextBox.Visibility = Visibility.Hidden;
            NextTimeTextBlock.Visibility = Visibility.Hidden;
            NextTimeTextBox.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Hidden;
            AddFirst.Visibility = Visibility.Hidden;
            AddLast.Visibility = Visibility.Visible;

        }
        private void rbMiddle_Checked(object sender, RoutedEventArgs e)
        {

            List<BO.StationInLine> StationInLine = line.Stations.ToList();
            PrevstationComboBox.ItemsSource = StationInLine;
            PrevstationComboBox.SelectedItem = "Code";
            PrevstationComboBox.SelectedIndex = 0;
            PrevStatTextBlock.Visibility = Visibility.Visible;
            PrevstationComboBox.Visibility = Visibility.Visible;
            PrevDistanceTextBlock.Visibility = Visibility.Visible;
            PrevDistanceTextBox.Visibility = Visibility.Visible;
            PrevTimeTextBlock.Visibility = Visibility.Visible;
            PrevTimeTextBox.Visibility = Visibility.Visible;
            NextDistanceTextBlock.Visibility = Visibility.Visible;
            NextDistanceTextBox.Visibility = Visibility.Visible;
            NextTimeTextBlock.Visibility = Visibility.Visible;
            NextTimeTextBox.Visibility = Visibility.Visible;
            AddFirst.Visibility = Visibility.Hidden;
            AddLast.Visibility = Visibility.Hidden;
            AddMiddle.Visibility = Visibility.Visible;  

        }
        private void AddFirstClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station stat = (stationComboBox.SelectedItem) as BO.Station;
                bool IsExist=bl.IsAdjacentStat(stat.Code, line.Stations[0].StationCode);
                if (IsExist == false)
                {
                    double distance = double.Parse(NextDistanceTextBox.Text);
                    TimeSpan time = TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text));

                    bl.AddStationInLine(stat.Code, line.LineId, 0, line.Stations[0].StationCode, 0, distance, time, 0, new TimeSpan(0, 0, 0));
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    //bool flag = false;
                    double distance = 0;
                    TimeSpan time = TimeSpan.FromMinutes(0);
                    if (MessageBox.Show("התחנות הסמוכות קיימות במערכת, האם תרצה לעדכן את הזמן והמרחק שלהם", "add", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {       
                        bl.UpdateTandDinAdjacentStation(stat.Code, line.Stations[0].StationCode, double.Parse(NextDistanceTextBox.Text), TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text)));

                    }
                    else
                    {
                        //אל תעשה כלום- כלומר לא ניגע בתחנות עוקבות שקיימות
                    }
                    bl.AddStationInLine(stat.Code, line.LineId, 0, line.Stations[0].StationCode, 0, distance, time, 0, new TimeSpan(0, 0, 0));
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();

                    //if(flag==true)
                    //{
                    //   // bl.UpdateTandDinAdjacentStation((stat.Code, line.Stations[0].StationCode, double.Parse(NextDistanceTextBox.Text, TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text)));
                    //    //double distance = 0;
                    //    //TimeSpan time = TimeSpan.FromMinutes(0);
                    //    // bl.AddStationInLine(stat.Code, line.LineId, 0, line.Stations[0].StationCode, 0, distance, time, 0, new TimeSpan(0, 0, 0));
                    //    line.Stations[0].DistanceFromNext = double.Parse(NextDistanceTextBox.Text);
                    //    line.Stations[0].TimeFromNext = TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text));
                    //}
                }
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception )
            {
                MessageBox.Show("ERROR ", "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddLastClick(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.Station stat = (stationComboBox.SelectedItem) as BO.Station;
                bool IsExist = bl.IsAdjacentStat(line.Stations[line.Stations.Count - 1].StationCode, stat.Code);
                double distance = double.Parse(PrevDistanceTextBox.Text);
                TimeSpan time = TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text));
                if (IsExist == false)
                {
                    //double distance = double.Parse(PrevDistanceTextBox.Text);
                    //TimeSpan time = TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text));
                    bl.AddStationInLine(stat.Code, line.LineId, line.Stations.Count, 0, line.Stations[line.Stations.Count - 1].StationCode, 0, new TimeSpan(0, 0, 0), distance, time);
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                {
                    
                    /*double*/ distance = 0;
                    /*TimeSpan*/ time = TimeSpan.FromMinutes(0);
                    if (MessageBox.Show("התחנות הסמוכות קיימות במערכת, האם תרצה לעדכן את הזמן והמרחק שלהם", "add", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        bl.UpdateTandDinAdjacentStation(line.Stations[line.Stations.Count - 1].StationCode, stat.Code, double.Parse(PrevDistanceTextBox.Text), TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text)));

                    }
                    else
                    {
                        //אל תעשה כלום- כלומר לא ניגע בתחנות עוקבות שקיימות
                    }
                      bl.AddStationInLine(stat.Code, line.LineId, line.Stations.Count, 0, line.Stations[line.Stations.Count - 1].StationCode, 0, new TimeSpan(0, 0, 0), distance, time);
                      MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                      Close();
                    
                }
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR ", "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddMiddleClick(object sender, RoutedEventArgs e)
        
        {
            try
            {
                BO.Station stat = (stationComboBox.SelectedItem) as BO.Station;
                BO.StationInLine prevStat = (PrevstationComboBox.SelectedItem) as BO.StationInLine;

                double distanceNext = double.Parse(NextDistanceTextBox.Text);
                TimeSpan timeNext = TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text));
                double distancePrev = double.Parse(PrevDistanceTextBox.Text);
                TimeSpan timePrev = TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text));
                bool IsExist1 = bl.IsAdjacentStat(prevStat.StationCode, stat.Code);
                bool IsExist2 = bl.IsAdjacentStat( stat.Code, line.Stations[prevStat.LineStationIndex + 1].StationCode);
                if (IsExist1 == false && IsExist2 == false)
                {
                    bl.AddStationInLine(stat.Code, line.LineId, prevStat.LineStationIndex + 1, line.Stations[prevStat.LineStationIndex + 1].StationCode, prevStat.StationCode, distanceNext, timeNext, distancePrev, timePrev);
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else{
                if (IsExist1 == true && IsExist2 == false)
                    {
                        distancePrev = 0;
                        timePrev = TimeSpan.FromMinutes(0);
                        if (MessageBox.Show("התחנות הסמוכות קיימות במערכת, האם תרצה לעדכן את הזמן והמרחק שלהם", "add", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            bl.UpdateTandDinAdjacentStation(prevStat.StationCode, stat.Code, double.Parse(PrevDistanceTextBox.Text), TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text)));
                            
                        }
                        else
                        {
                            //אל תעשה כלום- כלומר לא ניגע בתחנות עוקבות שקיימות
                        }
                    }
                    else if (IsExist1 == false && IsExist2 == true)
                    {
                        distanceNext = 0;
                        timeNext = TimeSpan.FromMinutes(0);
                        if (MessageBox.Show("התחנות הסמוכות קיימות במערכת, האם תרצה לעדכן את הזמן והמרחק שלהם", "add", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            bl.UpdateTandDinAdjacentStation(stat.Code, line.Stations[prevStat.LineStationIndex + 1].StationCode, double.Parse(NextDistanceTextBox.Text), TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text)));

                        }
                        else
                        {
                            //אל תעשה כלום- כלומר לא ניגע בתחנות עוקבות שקיימות
                        }
                    }
                    else
                    {
                        distanceNext = 0;
                        timeNext = TimeSpan.FromMinutes(0);
                        distancePrev = 0;
                        timePrev = TimeSpan.FromMinutes(0);
                        if (MessageBox.Show("התחנות הסמוכות קיימות במערכת, האם תרצה לעדכן את הזמן והמרחק שלהם", "add", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            bl.UpdateTandDinAdjacentStation(stat.Code, line.Stations[prevStat.LineStationIndex + 1].StationCode, double.Parse(NextDistanceTextBox.Text), TimeSpan.FromMinutes(double.Parse(NextTimeTextBox.Text)));
                            bl.UpdateTandDinAdjacentStation(prevStat.StationCode, stat.Code, double.Parse(PrevDistanceTextBox.Text), TimeSpan.FromMinutes(double.Parse(PrevTimeTextBox.Text)));

                        }
                        else
                        {
                            //אל תעשה כלום- כלומר לא ניגע בתחנות עוקבות שקיימות
                        }

                    }
                    bl.AddStationInLine(stat.Code, line.LineId, prevStat.LineStationIndex + 1, line.Stations[prevStat.LineStationIndex + 1].StationCode, prevStat.StationCode, distanceNext, timeNext, distancePrev, timePrev);
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show(ex.Message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception)
            {
                MessageBox.Show("ERROR ", "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.OemPeriod && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }
        private void keyCheckNoPeroid(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9)  && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }

        private void PrevstationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
