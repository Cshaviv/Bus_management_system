using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line//קו אוטובוס
    {
        public int LineId { get; set; } //מזהה ייחודי של הקו
        public int LineNum { get; set; } //מספר קו
        public Area Area { get; set; } //אזור הקו
        public int FirstStation { get; set; }//תחנה ראשונה
        public int LastStation { get; set; }//תחנה אחרונה
        public List<StationInLine> Stations { get; set; }//רשימת התחנות של הקו
        public override string ToString()// פונקצית הדפסה
        {
            return "Line ID: " + LineId + " Line number: " + LineNum;  
        }
        
    }
}

