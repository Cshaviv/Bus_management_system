using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using BO;
using BLAPI;

namespace PL.WPF
{
    class DataThread
    {
        IBL bl;
        ListBox busesListBox;
        public ProgressBar ProgressBar { get; set; }
        public Label Label { get; set; }
        BackgroundWorker worker;
        public int Seconds { get; set; }
        public Bus bus { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        public Label action { get; set; }
        public Label timer { get; set; }
        public TextBlock km { get; set; }
        public double distance { get; set; }
        public DataThread(IBL _bl,ProgressBar pb, Label label, int sec, Bus b, ListBox _busesListBox, string m, string t, Label a, Label time)//ctor
        {
            bl = _bl;
            ProgressBar = pb;
            Label = label;
            Seconds = sec;
            bus = b;
            busesListBox = _busesListBox;
            message = m;
            title = t;
            action = a;
            //statusRectangle = s;
            timer = time;
           // km = k;
            //distance = d;
        }

        public void Start(DataThread d)//Start of the procession
        {
            DataThread data = d;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            data.ProgressBar.Visibility = Visibility.Visible;
            data.Label.Visibility = Visibility.Visible;
            data.action.Visibility = Visibility.Visible;
            data.timer.Visibility = Visibility.Visible;
            worker.RunWorkerAsync(data);//Activating the func Worker_DoWork of process

        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DataThread data = (DataThread)e.Argument;
            int length = data.Seconds;
            for (int i = 1; i <= length; i++)
            {
                System.Threading.Thread.Sleep(1000);
                worker.ReportProgress(i, data);
            }
            e.Result = data;
        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)//Updating data during the process
        {
            int progress = (int)e.ProgressPercentage;//i
            DataThread data = (DataThread)e.UserState;
            data.timer.Content = (data.Seconds - progress) + "  seconds to finish";
            data.Label.Content = progress * 100 / data.Seconds + "%";
            data.ProgressBar.Value = (progress * 100) / data.Seconds;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)//End the process and update the fields
        {
            BO.Bus b = new BO.Bus() { LicenseNum = bus.LicenseNum, FuelTank = 0, StartDate = bus.StartDate, DateLastTreat = bus.DateLastTreat, /*StatusBus = status,*/ TotalKm = bus.TotalKm, KmLastTreat = bus.KmLastTreat };
            bl.UpdateBusDetails(b);
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
            DataThread data = ((DataThread)(e.Result));
            data.ProgressBar.Visibility = Visibility.Hidden;
            data.Label.Visibility = Visibility.Hidden;
            data.action.Visibility = Visibility.Hidden;
            data.timer.Visibility = Visibility.Hidden;
            data.bus.StatusBus = BusStatus.Available;
         
        }

    }
}
