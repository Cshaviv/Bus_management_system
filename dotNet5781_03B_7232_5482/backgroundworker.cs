using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;



namespace dotNet5781_03B_7232_5482
{
    public class DataThread
    {
        public ProgressBar ProgressBar { get; set; }
        public Label Label { get; set; }

        public int Seconds { get; set; }
        public Bus Bus { get; set; }
        //public TextBlock TBTotalKm { get; set; }

        public DataThread(ProgressBar pb, Label label, int sec, Bus b/*, TextBlock TotalKm*/)
        {
            ProgressBar = pb;
            Label = label;
            Seconds = sec;
            Bus = b;
            //TBTotalKm = TotalKm;
        }
    }
}