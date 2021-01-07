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
    /// Interaction logic for LinesShowWindow.xaml
    /// </summary>
    public partial class LinesShowWindow : Window
    {
        IBL bl;
        BO.Line line;
        public LinesShowWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            RefreshAllLinesList();
            //var allLines = bl.GetAllLines().ToList();
            //linesListBox.ItemsSource = allLines;
        }
        public void RefreshAllLinesList()
        {
            List<BO.Line> lines = bl.GetAllLines().ToList();
            linesListBox.DataContext = lines;
        }
        private void doubleClickLineInfromation(object sender, MouseButtonEventArgs e)
        {
            line = (sender as ListBox).SelectedItem as BO.Line;
            if (line == null)
                return;
            LineDeta win = new LineDeta(bl, line);
            win.Closing += winUpdate_Closing;
            win.ShowDialog();
        }
        private void winUpdate_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RefreshAllLinesList();
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            AddNewLine win = new AddNewLine(bl);
            win.Closing += winUpdate_Closing;
            win.ShowDialog();
        }
    }
}
