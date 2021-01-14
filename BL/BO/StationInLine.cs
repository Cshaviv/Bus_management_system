using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class StationInLine// תחנות של קו 
    {
        public int lineNum { get; set; }// מספר הקו
        public int StationCode { get; set; } //קוד התחנה
        public string Name { get; set; } // שם התחנה
        public bool DisabledAccess { get; set; } //גישה לנכים
        public int LineStationIndex { get; set; } ////האינדקס בתחנת קו
        public double DistanceFromNext { get; set; } // מרחק עד לתחנה הבאה
        public TimeSpan TimeFromNext { get; set; }// זמן נסיעה לתחנה הבאה

        public override string ToString()// פונקציית הדפסה
        {
            return "station code: " + StationCode + " station name: " + Name;
        }
    }
}
