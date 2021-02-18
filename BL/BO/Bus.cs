using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class Bus// אוטובוס
    {
        public int LicenseNum { get; set; }//מספר רישוי
        public DateTime StartDate { get; set; }//(תאריך התחלה (התחלת פעילות
        public double TotalKm { get; set; }//סה"כ קילומטר
        public double FuelTank { get; set; }//דלק-כמה נסע מהתדלוק
        //public double kmAfterRefuling { get; set; }//fuel tank
        public BusStatus StatusBus { get; set; }//(סטטוס האוטובוס (באיזה מצב נמצא
        public DateTime DateLastTreat { get; set; }//תאריך הטיפול הקודם
        public double KmLastTreat { get; set; }// כמה קילומטר נסע מהטיפול הקודם
        public bool IsDeleted { get; set; }//האם מחוק

        public override string ToString()//הדפסה של המספר רישוי . יש הבדל במספר הספרות של המספר לפי תאריך ההתחלה של האוטובוס 
        {
            int year = StartDate.Year;// שנת תחילת הפעילות
            string licenNum = LicenseNum.ToString();
            if (year < 2018)
                return "" +  licenNum[0] + licenNum[1] + "-" + licenNum[2] + licenNum[3] + licenNum[4] + "-" + licenNum[5] + licenNum[6]; 
            else
                return "" + licenNum[0] + licenNum[1] + licenNum[2] + "-" + licenNum[3] + licenNum[4] + "-" + licenNum[5] + licenNum[6] + licenNum[7] ;
        }
    }
}
