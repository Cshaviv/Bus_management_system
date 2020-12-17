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
        public BackgroundWorker worker;
        public int Seconds { get; set; }
        public Bus Bus { get; set; }
        public string message { get; set; }
        public string title { get; set; }
        //public TextBlock TBTotalKm { get; set; }

        public DataThread(ProgressBar pb, Label label, int sec, Bus b, string m, string t)
        {
            ProgressBar = pb;
            Label = label;
            Seconds = sec;
            Bus = b;
            message = m;
            title = t;

            //TBTotalKm = TotalKm;
        }
        public void Start(DataThread d)
        {
            DataThread data = d;
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(data);
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
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = (int)e.ProgressPercentage;//i
            DataThread data = (DataThread)e.UserState;
            //int result = data.Seconds - progress;
            data.Label.Content = progress * 100 / data.Seconds + "%";
            data.ProgressBar.Value = (progress * 100) / data.Seconds;
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            //MessageBox.Show("The bus was refueled successfully.", "Refuel  ", MessageBoxButton.OK, MessageBoxImage.Information);
            DataThread data = ((DataThread)(e.Result));
            //MessageBox.Show(data.m, t, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            data.ProgressBar.Visibility = Visibility.Hidden;
            data.Label.Visibility = Visibility.Hidden;
            data.Bus.myStatus = STATUS.ReadyToRide;
        }
        //private void Worker_RunWorkerCompleted_Driving(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    MessageBox.Show("The ride went successfully.", "Finished a driving  ", MessageBoxButton.OK, MessageBoxImage.Information);
        //    DataThread data = ((DataThread)(e.Result));
        //    data.ProgressBar.Visibility = Visibility.Hidden;
        //    data.Label.Visibility = Visibility.Hidden;
        //    data.Bus.myStatus = STATUS.ReadyToRide;
        //}
    }
}