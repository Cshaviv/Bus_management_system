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
    /// Interaction logic for UserWindows.xaml
    /// </summary>
    public partial class UserWindows : Window
    {
        IBL bl;
       // private string userName1;

        public UserWindows(IBL _bl, string userName)
        {
            InitializeComponent();
            bl = _bl;
            busesListBox.ItemsSource = bl.GetAllBuses().ToList();
            LineesListBox.ItemsSource = bl.GetAllLines().ToList();
            stationsListBox.ItemsSource = bl.GetAllStations().ToList();
            userNameTextBlock.Text = userName;
            string userName1 = userName;
            userNameTextBlock.Text = ("Hi "+ userName).ToString();
            sarchLineInArea.ItemsSource = Enum.GetValues(typeof(BO.Area));
            sarchLineInArea.Visibility = Visibility.Hidden;
            areaLabel.Visibility = Visibility.Hidden;
        }

        #region Bus
        private void Bus_Click(object sender, RoutedEventArgs e)
        {
            areaLabel.Visibility = Visibility.Hidden;
            sarchLineInArea.Visibility = Visibility.Hidden;
            stationsListBox.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Hidden;
            LineesDeletedListBox.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Visible;
            HistoryBus.Visibility = Visibility.Visible;
            availableBus.Visibility = Visibility.Hidden;
            HistoryLine.Visibility = Visibility.Hidden;
            availableLine.Visibility = Visibility.Hidden;
            HistoryStat.Visibility = Visibility.Hidden;
            availableStat.Visibility = Visibility.Hidden;
            busesListBox.ItemsSource = bl.GetAllBuses().ToList();

        }
        private void doubleClickBusInfromation(object sender, RoutedEventArgs e)//Clicking "double click" on a bus in the list will open a window showing the bus data
        {
            Bus myBus = (sender as ListBox).SelectedItem as Bus;
            if (myBus == null)
            {
                MessageBox.Show("לאוטובוס זה אין נתונים להציג", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;          
            }
            
            ListBoxItem myListBoxItem = (ListBoxItem)(busesListBox.ItemContainerGenerator.ContainerFromItem(myBus));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            BusDataUser win = new BusDataUser(myBus, bl);
            win.ShowDialog();
        }
        private void HistoryBusClick(object sender, RoutedEventArgs e)
        {
            HistoryBus.Visibility = Visibility.Hidden;
            availableBus.Visibility = Visibility.Visible;
            busesListBox.ItemsSource = bl.GetAllDeleteBuses().ToList();
        }
        private void availableBusClick(object sender, RoutedEventArgs e)
        {
            HistoryBus.Visibility = Visibility.Visible;
            availableBus.Visibility = Visibility.Hidden;
            busesListBox.ItemsSource = bl.GetAllBuses().ToList();
        }
        #endregion

        #region Line
        private void Line_Click(object sender, RoutedEventArgs e)
        {
            areaLabel.Visibility = Visibility.Visible;
            sarchLineInArea.Visibility = Visibility.Visible;
            stationsListBox.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Hidden;
            LineesDeletedListBox.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Visible;
            HistoryLine.Visibility = Visibility.Visible;
            availableLine.Visibility = Visibility.Hidden;
            HistoryBus.Visibility = Visibility.Hidden;
            availableBus.Visibility = Visibility.Hidden;
            HistoryStat.Visibility = Visibility.Hidden;
            availableStat.Visibility = Visibility.Hidden;
        }
        private void areaChangeClick(object sender, SelectionChangedEventArgs e)
        {
            if (sarchLineInArea.SelectedItem == null)
                LineesListBox.ItemsSource = bl.GetAllLines().ToList();
            else
                LineesListBox.ItemsSource = bl.GetAllLinesInArea(sarchLineInArea.SelectedItem.ToString()).ToList();

        }
        private void doubleClickLineInfromation(object sender, MouseButtonEventArgs e)
        {
            BO.Line line = (sender as ListBox).SelectedItem as BO.Line;
            if (line == null)
            {
                MessageBox.Show("לקו זה אין נתונים להציג", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ListBoxItem myListBoxItem = (ListBoxItem)(LineesListBox.ItemContainerGenerator.ContainerFromItem(line));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            LineDataUser win = new LineDataUser(bl, line);
            win.ShowDialog();
        }
        private void doubleClickDeletedLine(object sender, MouseButtonEventArgs e)
        {
            BO.Line line = (sender as ListBox).SelectedItem as BO.Line;
            if (line == null)
            {
                MessageBox.Show("לקו זה אין נתונים להציג", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            ListBoxItem myListBoxItem = (ListBoxItem)(LineesDeletedListBox.ItemContainerGenerator.ContainerFromItem(line));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            LineDataUser win = new LineDataUser(bl, line);
            win.ShowDialog();
        }    
        private void HistoryLineClick(object sender, RoutedEventArgs e)
        {
            HistoryLine.Visibility = Visibility.Hidden;
            availableLine.Visibility = Visibility.Visible;
            LineesListBox.Visibility = Visibility.Hidden;
            LineesDeletedListBox.Visibility = Visibility.Visible;
            LineesDeletedListBox.ItemsSource = bl.GetAllDeletedLines().ToList();
        }
        private void availableLineClick(object sender, RoutedEventArgs e)
        {
            HistoryLine.Visibility = Visibility.Visible;
            availableLine.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Visible;
            LineesDeletedListBox.Visibility = Visibility.Hidden;
            LineesListBox.ItemsSource = bl.GetAllLines().ToList();
        }
        #endregion

        #region station
        private void Station_Click(object sender, RoutedEventArgs e)
        {
            areaLabel.Visibility = Visibility.Hidden;
            sarchLineInArea.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Hidden;
            LineesDeletedListBox.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Hidden;
            HistoryLine.Visibility = Visibility.Hidden;
            availableLine.Visibility = Visibility.Hidden;
            HistoryBus.Visibility = Visibility.Hidden;
            availableBus.Visibility = Visibility.Hidden;
            HistoryStat.Visibility = Visibility.Visible;
            availableStat.Visibility = Visibility.Hidden;
            stationsListBox.ItemsSource = bl.GetAllStations().ToList();
            stationsListBox.Visibility = Visibility.Visible;
        }
        private void doubleClickStationInfromation(object sender, RoutedEventArgs e)//Clicking "double click" on a bus in the list will open a window showing the bus data
        {
            bool isDeleted = false;
            BO.Station stat = (sender as ListBox).SelectedItem as BO.Station;
            if (stat == null)
            {
                MessageBox.Show("לתחנה זו אין נתונים להציג", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if(stat.IsDeleted==true)
            {
                isDeleted = true;
            }
            StationDataUser win = new StationDataUser(isDeleted,bl, stat, stationsListBox);
            win.ShowDialog();
        }
        void RefreshAllStations()//yes
        {
            try
            {
                stationsListBox.ItemsSource = bl.GetAllStations().ToList();
            }
            catch (BO.BadLineIdException)
            {
                MessageBox.Show("מצטערים חסר למערכת מידע", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadStationCodeException)
            {
                MessageBox.Show("מצטערים חסר למערכת מידע", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        void RefreshAllDeletedStations()//yes
        {
            try
            {
                stationsListBox.ItemsSource = bl.GetAllDeletedStations().ToList();
            }
            catch (BO.BadLineIdException)
            {
                MessageBox.Show("מצטערים חסר למערכת מידע", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadStationCodeException)
            {
                MessageBox.Show("מצטערים חסר למערכת מידע", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void HistoryStatClick(object sender, RoutedEventArgs e)
        {
            HistoryStat.Visibility = Visibility.Hidden;
            availableStat.Visibility = Visibility.Visible;
            LineesListBox.Visibility = Visibility.Hidden;
            stationsListBox.Visibility = Visibility.Visible;
            RefreshAllDeletedStations();
        }
        private void availableStatClick(object sender, RoutedEventArgs e)
        {
            HistoryStat.Visibility = Visibility.Visible;
            availableStat.Visibility = Visibility.Hidden;
            stationsListBox.Visibility = Visibility.Visible;
            RefreshAllStations();
        }
        #endregion

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
