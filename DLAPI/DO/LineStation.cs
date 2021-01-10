using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation//תחנה ביחס לקו
    {
        public int LineId { get; set; }//
        public int StationCode { get; set; }//קוד תחנה
        public int LineStationIndex { get; set; }//מספר תחנה במסלול/ ברשימה
        public int PrevStationCode { get; set; }//קוד תחנה קודמת
        public int NextStationCode { get; set; }//קוד תחנה הבאה
        public bool IsDeleted { get; set; }//מחוק או לא
    }
}
