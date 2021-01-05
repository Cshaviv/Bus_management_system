using BLAPI;
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
    /// Interaction logic for busesShowWindow.xaml
    /// </summary>
    public partial class busesShowWindow : Window
    {
        IBL bl;
        public busesShowWindow(IBL _bL)
        {
            InitializeComponent();
            bl = _bL;
            var allBuses = bl.GetAllBuses().ToList();
            busesListBox.ItemsSource = allBuses;

        }

        private void BusesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{

        //    System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
        //    // Load data by setting the CollectionViewSource.Source property:
        //    // busViewSource.Source = [generic data source]
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddBusWindow win = new AddBusWindow(bl);
            win.Show();
        }
    }
}
