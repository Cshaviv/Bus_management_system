using BLAPI;//
using BO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SimulateOneStationWindow.xaml
    /// </summary>
    public partial class SimulateOneStationWindow : Window
    {
        IBL BL;
        BO.Station station;
        ObservableCollection<BO.LineTiming> lineTimingList;
        Stopwatch stopwatch;
        BackgroundWorker timerworker;
        TimeSpan tsStartTime;
        bool isTimerRun;
        public SimulateOneStationWindow(IBL _bl, Station _stat)
        {
            InitializeComponent();
            Closing += Window_Closing;
            BL = _bl;
            station = _stat;
           // gridOneStation.DataContext =station;
            statName.Text = _stat.Name;
            statCode.Text = _stat.Code.ToString();
            statAdress.Text = _stat.Address;
            stopwatch = new Stopwatch();// יצירת שעון עצר
            timerworker = new BackgroundWorker();//יצירת תהליכון
            timerworker.DoWork += Worker_DoWork;// הרשמה לפונקציה
            timerworker.ProgressChanged += Worker_ProgressChanged;// הרשמה לפונקציה
            timerworker.WorkerReportsProgress = true;//דגל לדיווח על התקדמות התהליך
            tsStartTime = DateTime.Now.TimeOfDay;//הזמן שהתחלנו את התהליך-הזמן האמיתי
            stopwatch.Restart();// השעון עצר מתחיל לזוז
            isTimerRun = true;//דגל האם השעון רץ-עדיין באמצע התהליך

           
            timerworker.RunWorkerAsync();//הפעלת התהליכון

        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)//כל עוד אנחנו באמצע התהליך תעצור שנייה ותשלח לפונקציית ההתקדמות
        {
            while (isTimerRun)
            {
                timerworker.ReportProgress(231);//דיווח על התקדמות
                Thread.Sleep(1000);//תעצור שנייה
            }
        }
       
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)//פונקציית ההתקדמות של השעון ושל הדקות
        {
            
            TimeSpan tsCurrentTime = tsStartTime + stopwatch.Elapsed;//בזמן שהתחלנו בו את התהליכון +הזמן שעבר=הזמן הנוכחי
            string timmerText = tsCurrentTime.ToString().Substring(0, 8);//הזמן שיוצג זה 8 הספרות השמאליות - בפורמט 00:00:00
            this.timerTextBlock.Text = timmerText;//השעה הנוכחית שיהיה בתצוגה בשעון
            
            nisayon.ItemsSource = BL.GetLineTimingPerStation(station, tsCurrentTime).ToList();// עדכון הגריד שמראה את כל הזמנים והאוטובוסים שאמורים להגיע בקרוב לפי הזמן הנוכחי
        }
        private void Window_Closing(object sender, CancelEventArgs e)//עוצר את השעון ומפסיק את התהליך עם סגירת החלון
        {
            stopwatch.Stop();
            isTimerRun = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }
    }
}
