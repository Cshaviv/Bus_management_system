using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class Station// תחנה
    {
        public int Code { get; set; } //קוד התחנה
        public string Name { get; set; } // שם התחנה
        public string Address { get; set; } // כתובת התחנה
        public bool DisabledAccess { get; set; } //גישה לנכים
        public bool IsDeleted { get; set; }// האם מחוק
        public List<LineInStation> LinesInStation { get; set; }// תחנה של קווים שעוברים בתחנה
        public override string ToString()// פונקציית הדפסה
        {
            return "station code: " + Code + " station name: " + Name;
        }
    }
}
