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
using System.Collections.ObjectModel;
//ayala

namespace dotNet5781_03B_7232_5482
{
    /// <summary>
    /// Interaction logic for AddNewBus.xaml
    /// </summary>
    public partial class AddNewBus : Window
    {
        ObservableCollection<Bus> BusesCollection;
        public AddNewBus(ObservableCollection<Bus> list)
        {
            InitializeComponent();
            BusesCollection = list;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
           

                checkBus();
          
            

            BusesCollection.Add(new Bus(Int32.Parse(liceNumText.Text), Date.DisplayDate, Date.DisplayDate));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busViewSource.Source = [generic data source]
        }
        private void checkBus()
        {
            int licNum;
            bool succ = Int32.TryParse(liceNumText.Text, out licNum);

            if (!succ)
            {
                MessageBox.Show("This licese num is incorrect");
            }
     

        }

    }
}
