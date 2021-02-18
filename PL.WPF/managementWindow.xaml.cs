//llll
using BLAPI;
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for managementWindow.xaml
    /// </summary>
    public partial class managementWindow : Window
    {
        IBL bl;
      //  public ObservableCollection<Bus> BusesCollection;
        //BO.Student curStu;
        public managementWindow(IBL _bl, string userName)
        {
            InitializeComponent();
            bl = _bl;
            RefreshAllBuses();
           RefreshAllLinesList();
            RefreshAllStations();
            userNameTextBlock.Text = userName;
        }


        #region Buses 
        private void Bus_Click(object sender, RoutedEventArgs e)
        {
            stationsListBox.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Hidden;
            availableBus.Visibility = Visibility.Hidden;
            HistoryBus.Visibility = Visibility.Visible;
            busesListBox.Visibility = Visibility.Visible;
            AddBus.Visibility = Visibility.Visible;
            AddLine.Visibility = Visibility.Hidden;
            AddStation.Visibility = Visibility.Hidden;
            RefreshAllBuses();
        }//yes
        private void doubleClickBusInfromation(object sender, RoutedEventArgs e)//yes Clicking "double click" on a bus in the list will open a window showing the bus data
        {
            Bus myBus = (sender as ListBox).SelectedItem as Bus;
            if (myBus != null)
            {
                ListBoxItem myListBoxItem = (ListBoxItem)(busesListBox.ItemContainerGenerator.ContainerFromItem(myBus));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                ProgressBar prop = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
                Label precent = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
                Label action = (Label)myDataTemplate.FindName("action", myContentPresenter);
                Label timer = (Label)myDataTemplate.FindName("timer", myContentPresenter);
                BusData win = new BusData(myBus, bl, busesListBox, prop, precent, action, timer);
                win.ShowDialog();
                RefreshAllBuses();          
            }

        }
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
        }  //yes
        private void RefuelClick(object sender, RoutedEventArgs e)
        {
            Bus myBus = (sender as Button).DataContext as Bus;
            if (myBus.StatusBus == BusStatus.InTravel || myBus.StatusBus == BusStatus.OnTreatment || myBus.StatusBus == BusStatus.OnRefueling)// Check if the bus can be sent for refueling
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.FuelTank == 0)//When the fuel tank is full to the end can not be sent for refueling.
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            myBus.StatusBus = BusStatus.OnRefueling;//update status
            myBus.FuelTank = 0;//update fields
        }//לא בשימוש בנתיים
        private void TreatClick(object sender, RoutedEventArgs e)//לא בשימוש בנתיים
        {
            Bus myBus = (sender as Button).DataContext as Bus;
            if (myBus.StatusBus == BusStatus.InTravel || myBus.StatusBus == BusStatus.OnTreatment || myBus.StatusBus == BusStatus.OnRefueling)// Check if the bus can be sent for refueling
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.FuelTank == 0 && (DateTime.Now == myBus.DateLastTreat))//If he did the treatment today and has not traveled since
            {
                MessageBox.Show("The bus was already treatmented", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            myBus.StatusBus = BusStatus.OnTreatment;//update status
            myBus.FuelTank = 0;//update fields
            myBus.DateLastTreat = DateTime.Now;
            if (myBus.FuelTank == 1200)
            {
                myBus.FuelTank = 0;
            }
        }
        private void AddBus_Click(object sender, RoutedEventArgs e)//yes
        {
            AddBusWindow win = new AddBusWindow(bl);
            win.ShowDialog();
            RefreshAllBuses();
        }
        void RefreshAllBuses()
        {
            busesListBox.ItemsSource = bl.GetAllBuses().ToList();
        }//yes
        void RefreshAllDeleteBuses()
        {
            busesListBox.ItemsSource = bl.GetAllDeleteBuses().ToList();
        }//yes
        private void HistoryBusClick(object sender, RoutedEventArgs e)
        {
            HistoryBus.Visibility = Visibility.Hidden;           
            availableBus.Visibility = Visibility.Visible;
            RefreshAllDeleteBuses();
        }
        private void availableBusClick(object sender, RoutedEventArgs e)
        {
            HistoryBus.Visibility = Visibility.Visible;
            availableBus.Visibility = Visibility.Hidden;
            RefreshAllBuses();
        }
        
        #endregion

        #region Lines 
        public void RefreshAllLinesList()
        {        
            LineesListBox.ItemsSource = bl.GetAllLines().ToList();
        }//yes
        private void Line_Click(object sender, RoutedEventArgs e)
        {
            stationsListBox.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Hidden;
            AddBus.Visibility = Visibility.Hidden;
            availableBus.Visibility = Visibility.Hidden;
            HistoryBus.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Visible;
            AddLine.Visibility = Visibility.Visible;
            AddStation.Visibility = Visibility.Hidden;
            RefreshAllLinesList();
        }
        private void doubleClickLineInfromation(object sender, MouseButtonEventArgs e)
        {
           BO.Line line = (sender as ListBox).SelectedItem as BO.Line;
            if (line == null)
                return;
            ListBoxItem myListBoxItem = (ListBoxItem)(LineesListBox.ItemContainerGenerator.ContainerFromItem(line));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            LineDeta win = new LineDeta(bl, line);
            win.Closing += winUpdate_Closing;
            win.ShowDialog();
        }
        private void winUpdate_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefreshAllLinesList();
        }
        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            AddNewLine win = new AddNewLine(bl);
            win.ShowDialog();
            RefreshAllLinesList();
        }

        #endregion

        #region Station
        private void Station_Click(object sender, RoutedEventArgs e)
        {
            LineesListBox.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Hidden;
            stationsListBox.Visibility = Visibility.Visible;
            AddBus.Visibility = Visibility.Hidden;
            AddLine.Visibility = Visibility.Hidden;
            availableBus.Visibility = Visibility.Hidden;
            HistoryBus.Visibility = Visibility.Hidden;
            AddStation.Visibility = Visibility.Visible;
            RefreshAllStations();
        }
        void RefreshAllStations()//yes
        {
            try
            {
                stationsListBox.ItemsSource = bl.GetAllStations().ToList();
            }
            catch(BO.BadLineIdException)
            {
                MessageBox.Show("מצטערים חסר למערכת מידע", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadStationCodeException)
            {
                MessageBox.Show("מצטערים חסר למערכת מידע", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void AddStation_Click(object sender, RoutedEventArgs e)
        {
            AddNewStation win = new AddNewStation(bl);
            win.ShowDialog();
            RefreshAllStations();
        }
        private void doubleClickStationInfromation(object sender, MouseButtonEventArgs e)
        {
            BO.Station station = (sender as ListBox).SelectedItem as BO.Station;
            if (station == null)
                return;
            ListBoxItem myListBoxItem = (ListBoxItem)(stationsListBox.ItemContainerGenerator.ContainerFromItem(station));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            StationData win = new StationData(bl, station, stationsListBox);
            // win.Closing += winUpdate_Closing;
            win.ShowDialog();
        }
        public void nana()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

     
    }
}
#endregion