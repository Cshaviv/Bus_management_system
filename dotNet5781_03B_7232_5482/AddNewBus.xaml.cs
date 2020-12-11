using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace dotNet5781_03B_7232_5482
{
    /// <summary>
    /// Interaction logic for AddNewBus.xaml
    /// </summary>
    public partial class AddNewBus : Window
    {
        public ObservableCollection<Bus> BusesCollection { get; set; }
        public AddNewBus()
        {
            InitializeComponent();
            busStatusTextBox.ItemsSource = Enum.GetValues(typeof(STATUS)).Cast<STATUS>();
            busStatusTextBox.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void AAD_Click(object sender, RoutedEventArgs e)
        {
            Bus newBus = new Bus();
            bool exist = false;
            string liceNum = licenseNumTextBox.Text;         
             if (liceNum.Length != 7 && liceNum.Length != 8 || !int.TryParse(liceNum, out int num)|| liceNum == null)
            {
                ErrorLiceNumText.Text = "ERROR! Please try enter again";
                ErrorLiceNumText.Visibility = Visibility.Visible;
                licenseNumTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                foreach (Bus b in BusesCollection)
                    if (b.LicenseNum.ToString() == liceNum)
                        exist = true;
                if (exist)
                {
                    ErrorLiceNumText.Text = "ERROR! This bus already exist";
                    ErrorLiceNumText.Visibility = Visibility.Visible;
                    licenseNumTextBox.BorderBrush = Brushes.Red;
                }
                else
                {
                    ErrorLiceNumText.Visibility = Visibility.Hidden;
                    licenseNumTextBox.BorderBrush = Brushes.Green;
                }
               
            }
            DateTime date = startDateDatePicker.DisplayDate;
            if(date > DateTime.Now)
            {
                ErrorDateText.Text = "ERROR! This date is incorrect";
                ErrorDateText.Visibility = Visibility.Visible;
                startDateDatePicker.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorDateText.Visibility = Visibility.Hidden;
                startDateDatePicker.BorderBrush = Brushes.Green;
            }
            if (!((date.Year >= 2018) && (liceNum.ToString().Length == 8) || (date.Year < 2018) && (liceNum.ToString().Length == 7)))
            {
                ErrorText.Text = "ERROR! One or more of the data is incorrect";
                licenseNumTextBox.BorderBrush = Brushes.Red;
                startDateDatePicker.BorderBrush = Brushes.Red;
            }
            string km = kmTextBox.Text;
            Double checkKm; 
             if (!Double.TryParse(liceNum, out checkKm)|| checkKm<=0|| km == null)
            {
                ErrorKmText.Text =  "ERROR! Please try enter again";
                ErrorDateText.Visibility = Visibility.Visible;
                kmTextBox.BorderBrush = Brushes.Red;
            }
             else
            {
                ErrorDateText.Visibility = Visibility.Hidden;
                kmTextBox.BorderBrush = Brushes.Green;
            }
            km = kmafterrefuelingTextBox.Text;
            if (!Double.TryParse(liceNum, out checkKm) || checkKm <= 0 || km == null|| checkKm > 1200)
            {
                ErrorKmRefText.Text = "ERROR! Please try enter again";
                ErrorKmRefText.Visibility = Visibility.Visible;
                kmafterrefuelingTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorKmRefText.Visibility = Visibility.Hidden;
                kmafterrefuelingTextBox.BorderBrush = Brushes.Green;
            }
            km = kmaftertreatTextBox.Text;
            if (!Double.TryParse(liceNum, out checkKm) || checkKm <= 0 || km == null || checkKm > 20000)
            {
                ErrorKmTreatText.Text = "ERROR! Please try enter again";
                ErrorKmTreatText.Visibility = Visibility.Visible;
                kmaftertreatTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorKmTreatText.Visibility = Visibility.Hidden;
                kmaftertreatTextBox.BorderBrush = Brushes.Green;
            }
             date = lastTreatDatePicker.DisplayDate;
            if (date > DateTime.Now||date>startDateDatePicker.DisplayDate)
            {
                ErrorDateTreatText.Text = "ERROR! This date is incorrect";
                ErrorDateTreatText.Visibility = Visibility.Visible;
                lastTreatDatePicker.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorDateTreatText.Visibility = Visibility.Hidden;
                lastTreatDatePicker.BorderBrush = Brushes.Green;
            }
        }
    }
}
