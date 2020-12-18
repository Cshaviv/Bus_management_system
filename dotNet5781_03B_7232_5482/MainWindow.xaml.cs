//llll
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            AddNewBus add = new AddNewBus();
            add.BusesCollection = BusesCollection;

            add.ShowDialog();
        }
        private void Refuel(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;//the bus
            if (b.myStatus == STATUS.OnRide || b.myStatus == STATUS.OnTreat || b.myStatus == STATUS.OnRefueling)
            {
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            if (b.Kmafterrefueling == 0)
            {
                MessageBox.Show("The fuel tank if full", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }
            b.myStatus = STATUS.OnRefueling;
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

            statusRectangle.Fill = Brushes.Yellow;
            prop.Foreground = Brushes.Yellow;
            action.Content = "on refueling...";
            string massage = "The bus was refueled successfully.";
            string title = "Refuel  ";
            //bool sign = false;
            //double dis = 0;
            DataThread data = new DataThread(prop, precent, 12, b, massage, title,action, statusRectangle, timer,km/*, kmAfterTreat, kmAfterRefueling*/ /* ,sign*/);
            data.Start(data);
            b.Kmafterrefueling = 0;

        }
        private void StartDriveButtonClick(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;
            if (b.myStatus != STATUS.Available)
            {
                MessageBox.Show("The bus can't start driving right now, it isn't availble", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
      
            if(b.myStatus==STATUS.Available)
            {
              
                //if (win.rideDisTextBox.Text == "")
                //    return;
                //double dic;
                //bool sec = double.TryParse(win.rideDisTextBox.Text,out dic);
                //if (!sec)
                //    return;
                //if (((DateTime.Now - b.LastTreat).TotalDays > 365 || b.Kmaftertreat+dic >= 20000) && b.Kmafterrefueling + dic >= 1200)
                //{
                //    MessageBox.Show("1.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //    return;
                //}
                //else if ((DateTime.Now - b.LastTreat).TotalDays > 365 || b.Kmaftertreat + dic >= 20000)
                //{
                //    MessageBox.Show("2.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //    return;
                //}
                //else if (b.Kmafterrefueling + dic >= 1200)
                //{
                //    MessageBox.Show("3.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                //    return;
                //}
                //int speedTravel = rand.Next(20, 50);//rand speed travel
                //int timeTravel = (int)((dic / speedTravel) * 6);//time travel in
                //ListBoxItem myListBoxItem = (ListBoxItem)(BusList.ItemContainerGenerator.ContainerFromItem(b));
                //ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                //DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                //ProgressBar prop = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
                //Label precent = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
                //Rectangle statusRectangle = (Rectangle)myDataTemplate.FindName("statusRectangle", myContentPresenter);
                //Label action = (Label)myDataTemplate.FindName("action", myContentPresenter);
                //Label timer = (Label)myDataTemplate.FindName("timer", myContentPresenter);
                //TextBlock km = (TextBlock)myDataTemplate.FindName("kmTextBlock", myContentPresenter);
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
                win.ShowDialog();
               
            }
           

        }
        private void doubleClickBusInfromation(object sender, RoutedEventArgs e)
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