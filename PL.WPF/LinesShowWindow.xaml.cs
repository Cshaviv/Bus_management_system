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
        public LinesShowWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            var allLines = bl.GetAllLines().ToList();
            linesListBox.ItemsSource = allLines;
        }
    }
}
