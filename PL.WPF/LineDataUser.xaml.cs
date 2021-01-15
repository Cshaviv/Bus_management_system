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
    /// Interaction logic for LineDataUser.xaml
    /// </summary>
    public partial class LineDataUser : Window
    {
        IBL bl;
        BO.Line line;
        public LineDataUser(IBL _bl, BO.Line _line)
        {
            InitializeComponent();
            bl = _bl;
            line = _line;
            linesListBox.DataContext = line.Stations;
            linesListBox.Visibility = Visibility.Visible;
            linesListBox.Visibility = Visibility.Visible;
            LineNumTextBlock.Text = line.LineNum.ToString();
            AreaTextBlock.Text = line.Area.ToString();
            
        }
    }
}
