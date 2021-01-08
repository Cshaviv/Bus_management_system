//llll
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
    /// Interaction logic for managementWindow.xaml
    /// </summary>
    public partial class managementWindow : Window
    {
        IBL bl;
        //BO.Student curStu;
        public managementWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            RefreshAllBuses();
            RefreshAllLinesList();
        }


        #region Buses 
        private void Bus_Click(object sender, RoutedEventArgs e)
        {
            LineesListBox.Visibility = Visibility.Hidden;
            busesListBox.Visibility = Visibility.Visible;
            AddBus.Visibility = Visibility.Visible;
            AddLine.Visibility = Visibility.Hidden;
            RefreshAllBuses();
        }
        private void doubleClickBusInfromation(object sender, RoutedEventArgs e)//Clicking "double click" on a bus in the list will open a window showing the bus data
        {
            Bus myBus = (sender as ListBox).SelectedItem as Bus;
            if (myBus != null)
            {
                ListBoxItem myListBoxItem = (ListBoxItem)(busesListBox.ItemContainerGenerator.ContainerFromItem(myBus));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                BusData win = new BusData(myBus, bl);
                win.ShowDialog();
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
        }
        private void updateButtonClick(object sender, RoutedEventArgs e)
        {
            Bus myBus = (sender as Button).DataContext as Bus;
            if (myBus == null)
            {
                MessageBox.Show("The bus can't start driving right now, it isn't availble", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            updateWindow win = new updateWindow(myBus, bl);
            win.ShowDialog();
        }
        private void deleteButtonClick(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            MessageBoxResult res = MessageBox.Show("Delete selected student?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;
            try
            {
                if (b != null)
                {
                    bl.DeleteBus(b.LicenseNum);
                    BO.Bus stuToDel = b;

                    //RefreshAllRegisteredCoursesGrid();
                    //RefreshAllNotRegisteredCoursesGrid();
                    //RefreshAllStudentComboBox();
                }
            }
            catch (BO.BadLicenseNumException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadLineIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RefuelClick(object sender, RoutedEventArgs e)
        {
            Bus myBus = (sender as Button).DataContext as Bus;
            if (myBus.StatusBus == BusStatus.InTravel || myBus.StatusBus == BusStatus.OnTreatment || myBus.StatusBus == BusStatus.OnRefueling)// Check if the bus can be sent for refueling
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.kmAfterRefuling == 0)//When the fuel tank is full to the end can not be sent for refueling.
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            myBus.StatusBus = BusStatus.OnRefueling;//update status
            myBus.kmAfterRefuling = 0;//update fields
        }
        private void TreatClick(object sender, RoutedEventArgs e)
        {
            Bus myBus = (sender as Button).DataContext as Bus;
            if (myBus.StatusBus == BusStatus.InTravel || myBus.StatusBus == BusStatus.OnTreatment || myBus.StatusBus == BusStatus.OnRefueling)// Check if the bus can be sent for refueling
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (myBus.kmAfterRefuling == 0 && (DateTime.Now == myBus.DateLastTreat))//If he did the treatment today and has not traveled since
            {
                MessageBox.Show("The bus was already treatmented", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            myBus.StatusBus = BusStatus.OnTreatment;//update status
            myBus.kmAfterRefuling = 0;//update fields
            myBus.DateLastTreat = DateTime.Now;
            if (myBus.kmAfterRefuling == 1200)
            {
                myBus.kmAfterRefuling = 0;
            }
        }
        private void AddBus_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow win = new AddBusWindow(bl);
            win.ShowDialog();
            RefreshAllBuses();
        }
        void RefreshAllBuses()
        {
            busesListBox.ItemsSource = bl.GetAllBuses().ToList();
        }
        #endregion

        #region Lines 
        public void RefreshAllLinesList()
        {
            List<BO.Line> lines = bl.GetAllLines().ToList();
            LineesListBox.DataContext = lines;
        }
        private void Line_Click(object sender, RoutedEventArgs e)
        {
            busesListBox.Visibility = Visibility.Hidden;
            AddBus.Visibility = Visibility.Hidden;
            LineesListBox.Visibility = Visibility.Visible;
            AddLine.Visibility = Visibility.Visible;
            RefreshAllBuses();
        }
        private void doubleClickLineInfromation(object sender, MouseButtonEventArgs e)
        {
           BO.Line line = (sender as ListBox).SelectedItem as BO.Line;
            if (line == null)
                return;
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


    }
}
