using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus//אוטובוס
    {
        public int LicenseNum { get; set; }//מספר רישוי
        public DateTime StartDate { get; set; }//(תאריך התחלה (התחלת פעילות
        public double TotalKm { get; set; }//סה"כ קילומטר
        public double FuelTank { get; set; }//דלק-כמה נסע מהתדלוק
        public BusStatus StatusBus { get; set; }//(סטטוס האוטובוס (באיזה מצב נמצא
        public DateTime DateLastTreat { get; set; }//תאריך הטיפול הקודם
        public double KmLastTreat { get; set; }//כמה קילומטר נסע מהטיפול הקודם
        public bool IsDeleted { get; set; }//האם מחוק

   
       
    }
}
