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
    /// Interaction logic for AddLineTrip.xaml
    /// </summary>
    public partial class AddLineTrip : Window
    {
        IBL bl;
        BO.Line line;
        public List<string> Hours;
        public List<string> Minutes;

        public AddLineTrip(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            Hours = new List<string>();
            Minutes = new List<string>();

            for (int i = 0; i <= 9; i++)
            {
                Hours.Add("0" + i);
            }
            for (int i = 10; i <= 23; i++)
            {
                Hours.Add(i.ToString());
            }

            for (int i = 0; i <= 9; i++)
            {
                Minutes.Add("0" + i);
            }
            for (int i = 10; i <= 59; i++)
            {
                Minutes.Add(i.ToString());
            }
            HourCombo.ItemsSource = Hours;
            HourCombo.Text = Hours[0];
            MinutesCombo.ItemsSource = Minutes;
            MinutesCombo.Text = Minutes[0];
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hour = HourCombo.SelectedItem.ToString();
            string minutes = MinutesCombo.SelectedItem.ToString();
            TimeSpan lineTime = new TimeSpan(Int32.Parse(hour), Int32.Parse(minutes), 00);
            bl.AddDepTime(line.LineId, lineTime);
            nmnm.Text = lineTime.ToString();
            MessageBox.Show("לjjjjjjj", "Empty", MessageBoxButton.OK, MessageBoxImage.Information);
            //Close();
        }
    }
}
