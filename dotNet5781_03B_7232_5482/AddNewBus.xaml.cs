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
            //busStatusCombo.ItemsSource = Enum.GetValues(typeof(STATUS)).Cast<STATUS>();
            //busStatusCombo.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }

        private void ADD_Click(object sender, RoutedEventArgs e)//add new bus to the Buses list
        {
            int counter = 0;
            Bus newBus = new Bus();
            bool exist = false;
            string liceNum = licenseNumTextBox.Text;         
             if (liceNum.Length != 7 && liceNum.Length != 8 || !int.TryParse(liceNum, out int num)|| liceNum == null)//Check the length of the license number and check if num in the right format(int)
            {
                ErrorLiceNumText.Text = "ERROR! Please try enter again";//write Error massege
                ErrorLiceNumText.Visibility = Visibility.Visible;
                licenseNumTextBox.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
            }
            else
            {
                foreach (Bus b in BusesCollection)
                    if (b.LicenseNum.ToString() == liceNum)// check if the license num exist in bus list
                        exist = true;
                if (exist)
                {
                    ErrorLiceNumText.Text = "ERROR! This bus already exist";//write Error massege
                    ErrorLiceNumText.Visibility = Visibility.Visible;
                    licenseNumTextBox.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
                }
                else
                {
                    ErrorLiceNumText.Visibility = Visibility.Hidden;
                    licenseNumTextBox.BorderBrush = Brushes.Green;// if its correct, paint the frame in green
                    newBus.LicenseNum = Int32.Parse(liceNum);
                    counter = counter + 1;
                }

            }
            DateTime date = startDateDatePicker.DisplayDate;
            if(date > DateTime.Now)// Checking the correctness of the date
            {
                ErrorDateText.Text = "ERROR! This date is incorrect"; //write Error massege
                ErrorDateText.Visibility = Visibility.Visible;
                startDateDatePicker.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
            }
            else
            {
                ErrorDateText.Visibility = Visibility.Hidden;
                startDateDatePicker.BorderBrush = Brushes.Green;// if its correct, paint the frame in green
                newBus.StartDate = date;
                counter = counter + 1;
            }
            if (!((date.Year >= 2018) && (liceNum.ToString().Length == 8) || (date.Year < 2018) && (liceNum.ToString().Length == 7)||counter==2))//Checking the correctness of the license number by date
            {
                ErrorText.Text = "ERROR! One or more of the data is incorrect";//write Error massege
                licenseNumTextBox.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
                startDateDatePicker.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
                counter = counter - 2;
            }
            string km = kmTextBox.Text;
            Double checkKm; 
             if (!Double.TryParse(km, out checkKm)|| checkKm< 0|| km == null)//Checking the condition of the kilometer
            {
                ErrorKmText.Text =  "ERROR! Please try enter again";//write Error massege
                ErrorDateText.Visibility = Visibility.Visible;
                kmTextBox.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
            }
             else
            {
                ErrorDateText.Visibility = Visibility.Hidden;
                kmTextBox.BorderBrush = Brushes.Green;//if its correct, Paint the frame in green
                newBus.Km = checkKm;
                counter = counter + 1;
            }
            km = kmafterrefuelingTextBox.Text;
            Double checkKm_1;
            if (!Double.TryParse(km, out checkKm_1)|| checkKm_1 < 0 || km == null || checkKm_1 > 1200 || checkKm_1 > checkKm)// Checking the condition of the kilometer after refueling
            {
                ErrorKmRefText.Text = "ERROR! Please try enter again";//write Error massege
                ErrorKmRefText.Visibility = Visibility.Visible;
                kmafterrefuelingTextBox.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
            }

            else
            {
                ErrorKmRefText.Visibility = Visibility.Hidden;
                kmafterrefuelingTextBox.BorderBrush = Brushes.Green;//if its correct, Paint the frame in green
                newBus.Kmafterrefueling = checkKm_1;
                counter = counter + 1;
            }
            km = kmaftertreatTextBox.Text;
            if (!Double.TryParse(km, out checkKm_1) || checkKm_1 < 0 || km == null || checkKm_1 > 20000|| checkKm_1>checkKm)//Checking the condition of the kilometer after treat
            {
                ErrorKmTreatText.Text = "ERROR! Please try enter again";//write Error massege
                ErrorKmTreatText.Visibility = Visibility.Visible;
                kmaftertreatTextBox.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
            }
            else
            {
                ErrorKmTreatText.Visibility = Visibility.Hidden;
                kmaftertreatTextBox.BorderBrush = Brushes.Green;//if its correct, Paint the frame in green
                newBus.Kmaftertreat = checkKm_1;
                counter = counter + 1;
            }
            date = lastTreatDatePicker.DisplayDate;
            if (date > DateTime.Now||date<startDateDatePicker.DisplayDate||date==null)// Checking the correctness of the date of last treat
            {
                ErrorDateTreatText.Text = "ERROR! This date is incorrect";//write Error massege
                ErrorDateTreatText.Visibility = Visibility.Visible;
                lastTreatDatePicker.BorderBrush = Brushes.Red;//if its incorrect, Paint the frame in red
            }
            else
            {
                ErrorDateTreatText.Visibility = Visibility.Hidden;
                lastTreatDatePicker.BorderBrush = Brushes.Green;//if its correct, Paint the frame in green
                newBus.LastTreat = date;
                counter = counter + 1;

            }
            newBus.myStatus = STATUS.Available;
            if (counter==6)//Each time the data we entered was correct we counted in the counter. General check of the integrity of the data
            {
                BusesCollection.Add(newBus);
                MessageBox.Show("The bus secssesfully added", "new bus", MessageBoxButton.OK, MessageBoxImage.Information);

                this.Close();
            }
        }

        private void kmaftertreatTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
