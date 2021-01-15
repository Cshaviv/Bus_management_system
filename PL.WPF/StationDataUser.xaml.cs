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
    /// Interaction logic for StationDataUser.xaml
    /// </summary>
    public partial class StationDataUser : Window
    {
        IBL BL;
        BO.Station stat;
        public StationDataUser(IBL _bl, Station _stat)
        {
            InitializeComponent();
            BL = _bl;
            stat = _stat;
        }
    }
}
