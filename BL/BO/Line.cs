using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Line
    {
        public int LineId { get; set; } //id of the line
        public int LineNum { get; set; } //number of the line
        public Area Area { get; set; } //area of the line
        public int FirstStation { get; set; }//תחנה ראשונה
        public int LastStation { get; set; }//תחנה אחרונה
        public List<StationInLine> Stations { get; set; }
        public override string ToString()
        {
            return "Line ID: " + LineId + " Line number: " + LineNum;
        }
        
    }
}

