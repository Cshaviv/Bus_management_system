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
        //public BackgroundWorker worker;
        //public int Length { get; set; }
        //public string Message { get; set; }
        //public DataThread thread { get; set; }
        //public int Counter { get; set; }

        //    public backgroundworker(int length,int sec, string message,ProgressBar p, Label l, Bus b)
        //    {
        //        thread = new DataThread(p, l, sec, b);
        //        Length = length;
        //        Message = message;
        //        worker = new BackgroundWorker();
        //        worker.DoWork += Worker_DoWork;
        //        worker.ProgressChanged += Worker_ProgressChanged;
        //        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
        //        worker.WorkerReportsProgress = true;

        //    }
        //    public void start()
        //    {
        //        Counter = 1;
        //        thread.Label.Visibility = Visibility.Visible;
        //        thread.Label.Content = Length.ToString();
        //        worker.RunWorkerAsync(thread);
        //    }
        //    public void Worker_DoWork(object sender, DoWorkEventArgs e)
        //    {
        //        for (int i = 1; i <= Length; i++)
        //        {
        //            System.Threading.Thread.Sleep(1000);
        //            worker.ReportProgress(i*100/Length);
        //        }
        //        e.Result = e.Argument;
        //    }
        //    private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //    {

        //    }
        //}
        //}
    }
}
