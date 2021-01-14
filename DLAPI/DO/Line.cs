using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
   public class Line// קו אוטובוס
    {
        public int LineId { get; set; }//מזהה ייחודי של הקו
        public int LineNum { get; set; }//מספר קו
        public Area Area { get; set; }//אזור הקו
        public int FirstStation { get; set; }//תחנה ראשונה
        public int LastStation { get; set; }//תחנה אחרונה
        public bool IsDeleted { get; set; }//האם מחוק
    }
}
