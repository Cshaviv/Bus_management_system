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
    /// Interaction logic for UpdateStationInLine.xaml
    /// </summary>
    public partial class UpdateStationInLine : Window
    {
        IBL bl;
        BO.StationInLine stationInLine;
        public UpdateStationInLine(IBL _bl, StationInLine s)
        {
            InitializeComponent();
            bl = _bl;
            stationInLine = s;
        }
    }
}
