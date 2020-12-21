//Chagit shaviv 322805482 and Ayala Israeli 324207232
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
//using ToolsWPF;
using System.Data;
using System.Drawing;


//xmlns: local = "clr-namespace:dotNet5781_03B_7232_5482"

namespace dotNet5781_03B_7232_5482
{
    /// <summary> 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Random rand = new Random();
        public ObservableCollection<Bus> BusesCollection;
        public MainWindow()
        {

            InitializeComponent();
            BusesCollection = new ObservableCollection<Bus>();
            Buses.RestartBuses(BusesCollection);
            BusList.ItemsSource = BusesCollection;
        }
        private void Button_Click(object sender, RoutedEventArgs e)//Add new bus button
        {

            AddNewBus add = new AddNewBus();
            add.BusesCollection = BusesCollection;

            add.ShowDialog();
        }
        private void Refuel(object sender, RoutedEventArgs e)//Bus refueling button, When you press the button of Refuel
        {
            Bus b = (sender as Button).DataContext as Bus;//the bus
            if (b.myStatus == STATUS.OnRide || b.myStatus == STATUS.OnTreat || b.myStatus == STATUS.OnRefueling)// Check if the bus can be sent for refueling
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (b.Kmafterrefueling == 0)//When the fuel tank is full to the end can not be sent for refueling.
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            b.myStatus = STATUS.OnRefueling;//update status
            ListBoxItem myListBoxItem = (ListBoxItem)(BusList.ItemContainerGenerator.ContainerFromItem(b));
            ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
            DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
            ProgressBar prop = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
            Label precent = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
            Label action =(Label)myDataTemplate.FindName("action", myContentPresenter);
            Rectangle statusRectangle = (Rectangle)myDataTemplate.FindName("statusRectangle", myContentPresenter);
            Label timer = (Label)myDataTemplate.FindName("timer", myContentPresenter);
            TextBlock km = (TextBlock)myDataTemplate.FindName("kmTextBlock", myContentPresenter);
            TextBlock kmAfterTreat = (TextBlock)myDataTemplate.FindName("kmAfterTreatTextBlock", myContentPresenter);
            TextBlock kmAfterRefueling = (TextBlock)myDataTemplate.FindName("kmAfterRefulingTextBlock", myContentPresenter);

            statusRectangle.Fill = Brushes.Yellow;//The square that indicates the status of the bus will be painted yellow
            prop.Foreground = Brushes.Yellow;//the prop will be painted yellow
            action.Content = "on refueling...";
            string massage = "The bus was refueled successfully.";
            string title = "Refuel  ";
            DataThread data = new DataThread(prop, precent, 12, b, massage, title,action, statusRectangle, timer,km);//Sending the necessary data for the process
            data.Start(data);//Start of the procession
            b.Kmafterrefueling = 0;//update fields

        }
        private void StartDriveButtonClick(object sender, RoutedEventArgs e)//Bus Start drive button, When you press the button of start drive.
        {
            Bus b = (sender as Button).DataContext as Bus;
            if (b.myStatus != STATUS.Available)//Check if the bus can be sent for travel
            {
                MessageBox.Show("The bus can't start driving right now, it isn't availble", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
      
            if(b.myStatus==STATUS.Available)// if status of bus is "Available", 
            {
                ListBoxItem myListBoxItem = (ListBoxItem)(BusList.ItemContainerGenerator.ContainerFromItem(b));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                ProgressBar prop = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
                Label precent = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
                Label action = (Label)myDataTemplate.FindName("action", myContentPresenter);
                Rectangle statusRectangle = (Rectangle)myDataTemplate.FindName("statusRectangle", myContentPresenter);
                Label timer = (Label)myDataTemplate.FindName("timer", myContentPresenter);
                TextBlock km = (TextBlock)myDataTemplate.FindName("kmTextBlock", myContentPresenter);
                DepartureToRide win = new DepartureToRide(b, prop, precent, action, statusRectangle, timer, km);
                win.ShowDialog();//In the window that opens there will be additional health checks regarding departure for the trip

            }
           

        }
        private void doubleClickBusInfromation(object sender, RoutedEventArgs e)//Clicking "double click" on a bus in the list will open a window showing the bus data
        {
            Bus b = (sender as ListBox).SelectedItem as Bus;
            if (b != null)
            {
                ListBoxItem myListBoxItem = (ListBoxItem)(BusList.ItemContainerGenerator.ContainerFromItem(b));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                ProgressBar prop = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
                Label precent = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
                Label action = (Label)myDataTemplate.FindName("action", myContentPresenter);
                Rectangle statusRectangle = (Rectangle)myDataTemplate.FindName("statusRectangle", myContentPresenter);
                Label timer = (Label)myDataTemplate.FindName("timer", myContentPresenter);
                TextBlock km = (TextBlock)myDataTemplate.FindName("kmTextBlock", myContentPresenter);
                BusData win = new BusData(b,prop,precent,action,statusRectangle,timer,km);
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

    }
}