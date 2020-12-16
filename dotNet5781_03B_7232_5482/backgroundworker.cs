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
    public class backgroundworker
    {
        public BackgroundWorker worker;
        public int Length { get; set; }
        public string Message { get; set; }
        public DataThread thread { get; set; }
        public int Counter { get; set; }
        
        public backgroundworker(int sec, string message,ProgressBar p, Label l, Bus b)
        {
            thread = new DataThread(p, l, sec, b) 

        }
    }
}
