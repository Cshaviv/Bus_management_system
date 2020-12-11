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
            busStatusCombo.ItemsSource = Enum.GetValues(typeof(STATUS)).Cast<STATUS>();
            busStatusCombo.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void AAD_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
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
                    newBus.LicenseNum = Int32.Parse(liceNum);
                    counter = 1;
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
                newBus.StartDate = date;
                counter = 2;
            }
            if (!((date.Year >= 2018) && (liceNum.ToString().Length == 8) || (date.Year < 2018) && (liceNum.ToString().Length == 7)||counter==2))
            {
                ErrorText.Text = "ERROR! One or more of the data is incorrect";
                licenseNumTextBox.BorderBrush = Brushes.Red;
                startDateDatePicker.BorderBrush = Brushes.Red;
                counter = counter - 2;
            }
            string km = kmTextBox.Text;
            Double checkKm; 
             if (!Double.TryParse(km, out checkKm)|| checkKm< 0|| km == null)
            {
                ErrorKmText.Text =  "ERROR! Please try enter again";
                ErrorDateText.Visibility = Visibility.Visible;
                kmTextBox.BorderBrush = Brushes.Red;
            }
             else
            {
                ErrorDateText.Visibility = Visibility.Hidden;
                kmTextBox.BorderBrush = Brushes.Green;
                newBus.Km = checkKm;
                counter = 3;
            }
            km = kmafterrefuelingTextBox.Text;
            Double checkKm_1;
            if (!Double.TryParse(km, out checkKm_1)|| checkKm_1 < 0 || km == null || checkKm_1 > 1200 || checkKm_1 > checkKm)
            {
                ErrorKmRefText.Text = "ERROR! Please try enter again";
                ErrorKmRefText.Visibility = Visibility.Visible;
                kmafterrefuelingTextBox.BorderBrush = Brushes.Red;
            }

            else
            {
                ErrorKmRefText.Visibility = Visibility.Hidden;
                kmafterrefuelingTextBox.BorderBrush = Brushes.Green;
                newBus.Kmafterrefueling = checkKm_1;
                counter = 4;
            }
            km = kmaftertreatTextBox.Text;
            if (!Double.TryParse(km, out checkKm_1) || checkKm_1 < 0 || km == null || checkKm_1 > 20000|| checkKm_1>checkKm)
            {
                ErrorKmTreatText.Text = "ERROR! Please try enter again";
                ErrorKmTreatText.Visibility = Visibility.Visible;
                kmaftertreatTextBox.BorderBrush = Brushes.Red;
            }
            else
            {
                ErrorKmTreatText.Visibility = Visibility.Hidden;
                kmaftertreatTextBox.BorderBrush = Brushes.Green;
                newBus.Kmaftertreat = checkKm_1;
                counter = 5;

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

                counter = 6;
            }
       
        }
    }
}
