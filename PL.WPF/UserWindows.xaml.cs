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
        private string userName1;

        public UserWindows(IBL _bl, string userName)
        {
            InitializeComponent();
            bl = _bl;
            busesListBox.ItemsSource = bl.GetAllBuses().ToList();
            LineesListBox.ItemsSource = bl.GetAllLines().ToList();
            stationsListBox.ItemsSource = bl.GetAllStations().ToList();
            userNameTextBlock.Text = userName;
            string userName1 = userName;

        }

        #region Bus
        private void Bus_Click(object sender, RoutedEventArgs e)
        {
            stationsListBox.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Visible;        
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
        #endregion
        #region Line
        private void Line_Click(object sender, RoutedEventArgs e)
        {
            stationsListBox.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Visible;      
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
        #endregion

        #region station

        private void Station_Click(object sender, RoutedEventArgs e)
        {
            stationsListBox.Visibility = Visibility.Visible;
            busesListBox.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Hidden;
        }
        private void doubleClickStationInfromation(object sender, RoutedEventArgs e)//Clicking "double click" on a bus in the list will open a window showing the bus data
        {
            BO.Station stat = (sender as ListBox).SelectedItem as BO.Station;
            if (stat == null)
            {
                MessageBox.Show("לתחנה זו אין נתונים להציג", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            StationDataUser win = new StationDataUser(bl, stat, stationsListBox);
            win.ShowDialog();
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
