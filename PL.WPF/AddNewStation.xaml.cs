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
    /// Interaction logic for AddNewStation.xaml
    /// </summary>
    public partial class AddNewStation : Window
    {
        IBL bl;
        public AddNewStation(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
        private void addButton_click(object sender, RoutedEventArgs e)
        {
            try
            {
                int code = int.Parse(codeTextBox.Text);
                if (Int32.TryParse(nameTextBox.Text, out int name))
                    throw new BadInputException(1, "מחרוזת קלט לא היתה בתבנית הנכונה");
                if (Int32.TryParse(addressTextBox.Text, out int address))
                    throw new BadInputException(2, "מחרוזת קלט לא היתה בתבנית הנכונה");
                string name1 = nameTextBox.Text;
                string address1 = addressTextBox.Text;
                BO.Station station = new BO.Station() { Code = code, Name = name1, Address = address1, DisabledAccess= (bool)DisabledAccess.IsChecked };
                if (station != null)
                {
                    bl.AddStation(station);
                    codeTextBox.BorderBrush = Brushes.Gray;
                    nameTextBox.BorderBrush = Brushes.Gray;
                    addressTextBox.BorderBrush = Brushes.Gray;
                    MessageBox.Show("הפעולה בוצעה בהצלחה", "successfully", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                Exceptions(ex.num, ex.Message);
                //MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void keyCheck(object sender, KeyEventArgs e)
        {
            if (((int)e.Key < (int)Key.D0 || (int)e.Key > (int)Key.D9) && ((int)e.Key < (int)Key.NumPad0 || (int)e.Key > (int)Key.NumPad9) && e.Key != Key.Enter && e.Key != Key.Escape && e.Key != Key.Back)
                e.Handled = true;
        }
        private void Exceptions(int num, string message)
        {
            if (num == 1)
            {
                codeTextBox.BorderBrush = Brushes.Gray;
                nameTextBox.BorderBrush = Brushes.Red;
                addressTextBox.BorderBrush = Brushes.Gray;
                MessageBox.Show(message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (num == 2)
            {
                codeTextBox.BorderBrush = Brushes.Gray;
                nameTextBox.BorderBrush = Brushes.Gray;
                addressTextBox.BorderBrush = Brushes.Red;
                MessageBox.Show(message, "ERROR ", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        
    }
}
