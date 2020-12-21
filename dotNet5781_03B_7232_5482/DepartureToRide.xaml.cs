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

namespace dotNet5781_03B_7232_5482
{
    /// <summary>
    /// Interaction logic for DepartureToRide.xaml
    /// </summary>
    public partial class DepartureToRide : Window
    {
        static Random rand = new Random();
        public Bus myBus { get; set; }
        public ProgressBar prop { get; set; }
        public Label label { get; set; }
        public Label action { get; set; }
        public Rectangle statusRectangle { get; set; }
        public Label timer { get; set; }
        public TextBlock km { get; set; }
        public DepartureToRide(Bus b, ProgressBar p, Label l, Label a, Rectangle s, Label t, TextBlock k)//ctor
        {
            InitializeComponent();
            myBus = b;
            prop = p;
            label = l;
            action = a;
            statusRectangle = s;
            timer = t;
            km = k;
        }
      
        private void TakeForRide_KeyDown(object sender, KeyEventArgs e)//Enter travel distance by pressing enter
        {
            if (e.Key == Key.Enter)
            {
                double num;
                if (!double.TryParse(rideDisTextBox.Text, out num)|| num < 0)//Check the integrity of the input (distance)
                {
                    ErrorDistance.Visibility = Visibility.Visible;
                    rideDisTextBox.BorderBrush = Brushes.Red;
                    return;
                }
                else if (((DateTime.Now - myBus.LastTreat).TotalDays > 365 || myBus.Kmaftertreat + num >= 20000) && myBus.Kmafterrefueling + num >= 1200)//Check the integrity of the input (distance), Check if you do not need care and refuel before the trip
                    MessageBox.Show("You need to take the bus for a treatment and refuling before this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
 
                else if ((myBus.Kmafterrefueling + num) > 1200)//Check the integrity of the input (distance), Check if there is enough fuel for the trip
                    MessageBox.Show("There is no enough fuel for this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else if ((myBus.Kmaftertreat + num) > 20000|| (DateTime.Now - myBus.LastTreat).TotalDays > 365)//Check if you do not need treatment before the trip
                    MessageBox.Show("You need to take the bus for a treatment before this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    myBus.myStatus = STATUS.OnRide;//update status
                    statusRectangle.Fill = Brushes.DeepPink;//The square that indicates the status of the bus will be painted pink
                    prop.Foreground = Brushes.DeepPink;//the prop will be painted pink
                    int speedTravel = rand.Next(20, 50);//rand speed travel
                    int timeTravel = (int)((num / speedTravel) * 6);//time travel in
                    string massage = "The ride went successfully.";
                    string title = "Finished a driving  ";
                    action.Content = "on driving...";
                    timer.Content = timeTravel.ToString();
                    DataThread data = new DataThread(prop, label, timeTravel, myBus, massage, title, action, statusRectangle, timer, km, num);//Sending the necessary data for the process
                    data.Start(data);//Start of the procession
                    myBus.Kmaftertreat += num;//update fields
                    myBus.Kmafterrefueling += num;
                    myBus.Km += num;
                    Close();
                }

            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
