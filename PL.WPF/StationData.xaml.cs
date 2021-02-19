using BLAPI;//
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
    /// Interaction logic for StationData.xaml
    /// </summary>
    public partial class StationData : Window
    {
        IBL bl;
        BO.Station station;
       ListBox stationsListBox;
       //Rectangle IsDeletedRectangleStation;
       
        public StationData(bool isDeleted, IBL _bl, BO.Station _station , ListBox _stationsListBox ) //ctor
        {
            InitializeComponent();
            bl = _bl;
            station = _station;
            stationsListBox = _stationsListBox;
            if(isDeleted==true)
            {
                deleteStation.Visibility = Visibility.Hidden;
                updateStation.Visibility = Visibility.Hidden;
            }
            // IsDeletedRectangleStation = _IsDeletedRectangleStation;
            addressTextBox.Text= station.Address.ToString();
            nameTextBox.Text= station.Name.ToString();
            LineInStationListBox.ItemsSource = station.LinesInStation;
           // LineInStationListBox.DataContext = station.LinesInStation;
            LineInStationListBox.Visibility = Visibility.Visible;
             stationCodeTextBlock.Text = station.Code.ToString();
            
            
        }

        /// <summary>
        /// הפונקציה מרעננת את הרשימת אוטובוסים, נקרא לה לאחר ביצוע שינויים ועדכונים
        /// </summary>
        void RefreshAllStations()
        {
            stationsListBox.ItemsSource = bl.GetAllStations().ToList();
            
        }
        /// <summary>
        ///פונקציה זו מעדכנת את פרטי התחנה, בפונקציה זו אנחנו שולחים את קוד התחנה לפונקציה של עדכון 
        /// ששולחת לפונקציה ב  BL התחנה בשכבת ה  
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updateStation_Click(object sender, RoutedEventArgs e)// כפתור עדכון תחנה
        {
            try
            {
                string stationName = nameTextBox.Text;
                string stationAddress = addressTextBox.Text;
                int stationCode = int.Parse(stationCodeTextBlock.Text);
                BO.Station stat = new BO.Station() { Name = stationName, Address = stationAddress, Code = stationCode };// יוצרת אובייקט של תחנה ומציבה בה את הנתונים
                bl.UpdateStation(stat);//שליחה לפונקצית עדכון 
                RefreshAllStations();// רענון הרשימת תחנות
                MessageBox.Show("התחנה עודכנה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);//שליחת הודעה שהעדכון הצליח
                Close();
            }
            catch (BO.BadStationCodeException ex)// חריגות
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    
        private void deleteStation_Click(object sender, RoutedEventArgs e)//כפתור מחיקת תחנה
        {
            try
            { 
                MessageBoxResult res = MessageBox.Show("?אתה בטוח שאתה רוצה למחוק את התחנה", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);//שליחת הודעה למשתמש אם רוצה למחוק את התחנה
                if (res == MessageBoxResult.No)
                    return;
                int stationCode = int.Parse(stationCodeTextBlock.Text);//קוד התחנה
                bl.DeleteStation(stationCode);//שליחה לפנוקצית מחיקה
                RefreshAllStations();//רענון רשימת התחנות
                Close();
                MessageBox.Show("התחנה נמחקה בהצלחה", "", MessageBoxButton.OK, MessageBoxImage.Information);// שליחת הודעה שהמחיקה הצליחה
            }
            catch (BO.BadStationCodeException ex)//חריגה
            {
                MessageBox.Show(ex.Message + ": " + ex.stationCode, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)//חריגה
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource stationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("stationViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // stationViewSource.Source = [generic data source]
        }
    }
}
