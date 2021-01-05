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
    /// Interaction logic for userWindow.xaml
    /// </summary>
    public partial class userWindow : Window
    {

        IBL bl;
        //BO.user curUser;
        public userWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
        }
    }
}
