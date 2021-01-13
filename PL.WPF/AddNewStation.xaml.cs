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
        Ibl bl;
        public AddNewStation(Ibl _bl)
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
                string name = nameTextBox.Text;
                string address = addressTextBox.Text;
                BO.Station station = new BO.Station() { Code = code, Name = name, Address = address,  };
                if (station != null)
                {
                    bl.AddStation(station);
                    Close();
                }
            }
            catch (BO.BadStationCodeException ex)
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadInputException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
    }
}
