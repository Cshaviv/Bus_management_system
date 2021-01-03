using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLAPI;
namespace PL.ViewModel
{
   public class MainWindow : DependencyObject
    {
        IBL bl = BLFactory.GetBL("1");
       // BO.BL bl = new BO.BL();
        public ObservableCollection<PO.Bus> BusList;// { get => (ObservableCollection<PO.Bus>)GetValue(StudentIDsProperty); set => SetValue(StudentIDsProperty, value); }
       // BackgroundWorker getBusListWorker;
        public void blGetBusList()
        {
            //foreach (var item in bl.GetAllBuses())
            //{
            //    PO.Bus bus = new PO.Bus();
            //    item.DeepCopyTo(bus);
            //    BusList.Add(bus);
            //}
            //getBusListWorker = new BackgroundWorker();
            //getBusListWorker.WorkerSupportsCancellation = true;
            //getBusListWorker.WorkerReportsProgress = true;
            //getBusListWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) => getBusListWorker = null;
            //getBusListWorker.ProgressChanged += (object sender, ProgressChangedEventArgs args) =>
            //{
            //    if (!((BackgroundWorker)sender).CancellationPending)
            //    {
            //        PO.Bus bus=new PO.Bus();
            //        ((BL.BO.Bus)args.UserState).DeepCopyTo(bus);
            //        BusList.Add(bus); }
            //};
            //getBusListWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            //{
            //    BackgroundWorker worker = (BackgroundWorker)sender;
            //    foreach (var item in bl.GetAllBuses())
            //    {
            //        if (worker.CancellationPending) break;
            //        worker.ReportProgress(0, item);
            //    }
            //};
            //BusList = new ObservableCollection<PO.Bus>();
            //getBusListWorker.RunWorkerAsync();
        }
    }
}
