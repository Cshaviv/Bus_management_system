using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        BackgroundWorker worker;
        // public static object Application { get; internal set; }

        //BackgroundWorker workerRefuel;
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
                MessageBox.Show("The bus is unavailable.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else
            {
                b.Kmafterrefueling = 0;
                b.myStatus = STATUS.OnRefueling;
                ListBoxItem myListBoxItem = (ListBoxItem)(BusList.ItemContainerGenerator.ContainerFromItem(b));
                ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                ProgressBar p = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
                Label l = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
                l.Visibility = Visibility.Visible;
                p.Visibility = Visibility.Visible;
                p.Foreground = Brushes.Red;
                string massage = "The bus was refueled successfully.";
                string title = "Refuel  ";
                DataThread data = new DataThread(p, l, 12, b, massage, title);
                data.Start(data);
                //worker = new BackgroundWorker();
                //worker.DoWork += Worker_DoWork;
                //worker.ProgressChanged += Worker_ProgressChanged;
                //worker.RunWorkerCompleted += Worker_RunWorkerCompleted_Refuel;
                //worker.WorkerReportsProgress = true;
                //worker.RunWorkerAsync(data);
            }



        }
        private void StartDriveButtonClick(object sender, RoutedEventArgs e)
        {
            Bus b = (sender as Button).DataContext as Bus;//the bus
            if (b.myStatus == STATUS.OnRefueling)
                MessageBox.Show("The bus is in refueling.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (b.myStatus == STATUS.OnTreat)
                MessageBox.Show("The bus is in treatment.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (b.myStatus == STATUS.OnRide)
                MessageBox.Show("The bus is on ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            else if (b.myStatus == STATUS.ReadyToRide)
            {
                if (((DateTime.Now - b.LastTreat).TotalDays > 365 || b.Kmaftertreat >= 20000) && b.Kmafterrefueling >= 1200)
                    MessageBox.Show("The bus needs to be sent for a treatment and refueling.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else if ((DateTime.Now - b.LastTreat).TotalDays > 365 || b.Kmaftertreat >= 20000)
                    MessageBox.Show("The bus needs to be sent for a treatment.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else if (b.Kmafterrefueling >= 1200)
                    MessageBox.Show("The bus needs to be sent for refueling.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    DepartureToRide win = new DepartureToRide();
                    win.myBus = b;
                    win.ShowDialog();
                    double dic;
                    dic = double.Parse(win.rideDisTextBox.Text);
                    b.Km += dic;
                    int speedTravel = rand.Next(50, 90);//rand speed travel
                    int timeTravel = (int)((dic / speedTravel) * 6);//time travel in
                    ListBoxItem myListBoxItem = (ListBoxItem)(BusList.ItemContainerGenerator.ContainerFromItem(b));
                    ContentPresenter myContentPresenter = FindVisualChild<ContentPresenter>(myListBoxItem);
                    DataTemplate myDataTemplate = myContentPresenter.ContentTemplate;
                    ProgressBar p = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
                    Label l = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
                    b.myStatus = STATUS.OnRide;
                    l.Visibility = Visibility.Visible;
                    p.Visibility = Visibility.Visible;
                    p.Foreground = Brushes.BlueViolet;
                    string massage = "The ride went successfully.";
                    string title = "Finished a driving  ";
                    DataThread data = new DataThread(p, l, 12, b, massage, title);
                    data.Start(data);
                    //DataThread data = new DataThread(p, l, timeTravel, b);
                    //worker = new BackgroundWorker();
                    //worker.DoWork += Worker_DoWork;
                    //worker.ProgressChanged += Worker_ProgressChanged;
                    //worker.RunWorkerCompleted += Worker_RunWorkerCompleted_Driving;
                    //worker.WorkerReportsProgress = true;
                    //worker.RunWorkerAsync(data);
                    b.Kmafterrefueling += dic;
                    b.Kmaftertreat += dic;
                    b.myStatus = STATUS.ReadyToRide;  
                }
            }
            else
            {
                MessageBox.Show("bla bala ");
                return;
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
                ProgressBar p = (ProgressBar)myDataTemplate.FindName("pbThread", myContentPresenter);
                Label l = (Label)myDataTemplate.FindName("progressLabel", myContentPresenter);
                BusData win = new BusData(b,p,l);
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