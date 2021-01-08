using System;//fff
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BLAPI;


namespace PL.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {

        IBL bl = BLFactory.GetBL("1");
        public MainWindow()
        {
            InitializeComponent();
            btnGO.Content = "any text";
            var allBuses = bl.GetAllBuses().ToList();
            //Select(b => b.LicenseNum + "_" + b.StartDate.ToString()).ToList();
            busesListBox.ItemsSource = allBuses;
            //InitBuses();
            //try
            //{
            int l = 12345678;
            BO.Bus bus = bl.GetBus(l);
            //}
            //catch(Exception)
            //{ }
            //bl.GetAllBuses()
        }

        //private void InitBuses()
        //{
        //    //Label action = (Label)bus.FindName("action", myContentPresenter);
        //}



        private void btnGO_Click(object sender, RoutedEventArgs e)
        {
            if (rbManagement.IsChecked == true)
            {
                managementWindow win = new managementWindow(bl);
                win.Show();
            }
            else if (rbUser.IsChecked == true)
            {
            //    userWindow win = new userWindow(bl);
            //    win.Show();
            
            }

            //private void busesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //{

            //}
        }

        private void busesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
} 

