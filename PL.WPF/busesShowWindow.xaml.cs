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
using BO;
namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for busesShowWindow.xaml
    /// </summary>
    public partial class busesShowWindow : Window
    {
        IBL bl;
        public busesShowWindow(IBL _bL)
        {
            InitializeComponent();
            bl = _bL;
            var allBuses = bl.GetAllBuses().ToList();
            busesListBox.ItemsSource = allBuses;
        }

        private void BusesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // busViewSource.Source = [generic data source]
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow win = new AddBusWindow(bl);
            win.Show();
        }
        private void doubleClickBusInfromation(object sender, RoutedEventArgs e)//Clicking "double click" on a bus in the list will open a window showing the bus data
        {
            Bus b = (sender as ListBox).SelectedItem as Bus;
            if (b != null)
            {
                ListBoxItem myListBoxItem = (ListBoxItem)(busesListBox.ItemContainerGenerator.ContainerFromItem(b));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                BusData win = new BusData(b, bl);
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
            Bus b = (sender as Button).DataContext as Bus;
            if(b==null)
            {
                MessageBox.Show("The bus can't start driving right now, it isn't availble", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            updateWindow win = new updateWindow(b, bl);
            win.ShowDialog();
        }
        private void deleteButtonClick (object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
        }
        private void RefuelClick(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            if (b.StatusBus == BusStatus.InTravel || b.StatusBus == BusStatus.OnTreatment || b.StatusBus == BusStatus.OnRefueling)// Check if the bus can be sent for refueling
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (b.kmAfterRefuling == 0)//When the fuel tank is full to the end can not be sent for refueling.
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            b.StatusBus = BusStatus.OnRefueling;//update status
            b.kmAfterRefuling = 0;//update fields
        }
        private void TreatClick(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            if (b.StatusBus == BusStatus.InTravel || b.StatusBus == BusStatus.OnTreatment || b.StatusBus == BusStatus.OnRefueling)// Check if the bus can be sent for refueling
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (b.kmAfterRefuling == 0 && (DateTime.Now == b.DateLastTreat))//If he did the treatment today and has not traveled since
            {
                MessageBox.Show("The bus was already treatmented", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            b.StatusBus = BusStatus.OnTreatment;//update status
            b.kmAfterRefuling = 0;//update fields
            b.DateLastTreat = DateTime.Now;
            if (b.kmAfterRefuling == 1200)
            {
                b.kmAfterRefuling = 0;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
    }
}
