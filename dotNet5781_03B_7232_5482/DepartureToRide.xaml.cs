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
        public DepartureToRide(Bus b, ProgressBar p, Label l, Label a, Rectangle s, Label t, TextBlock k)
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
      
        private void TakeForRide_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                double num;
                if (!double.TryParse(rideDisTextBox.Text, out num)|| num < 0)
                {
                    ErrorDistance.Visibility = Visibility.Visible;
                    rideDisTextBox.BorderBrush = Brushes.Red;
                    return;
                }
                else if (((DateTime.Now - myBus.LastTreat).TotalDays > 365 || myBus.Kmaftertreat + num >= 20000) && myBus.Kmafterrefueling + num >= 1200)
                    MessageBox.Show("You need to take the bus for a treatment and refuling before this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                else if ((myBus.Kmafterrefueling + num) > 1200)
                    MessageBox.Show("There is no enough fuel for this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else if ((myBus.Kmaftertreat + num) > 20000)
                    MessageBox.Show("You need to take the bus for a treatment before this ride.", "WARNING", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                else
                {
                    myBus.myStatus = STATUS.OnRide;
                    statusRectangle.Fill = Brushes.DeepPink;
                    prop.Foreground = Brushes.DeepPink;
                    int speedTravel = rand.Next(20, 50);//rand speed travel
                    int timeTravel = (int)((num / speedTravel) * 6);//time travel in
                    string massage = "The ride went successfully.";
                    string title = "Finished a driving  ";
                    action.Content = "on driving...";
                    timer.Content = timeTravel.ToString();
                    DataThread data = new DataThread(prop, label, timeTravel, myBus, massage, title, action, statusRectangle, timer, km, num);
                    data.Start(data);
                    myBus.Kmaftertreat += num;
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
