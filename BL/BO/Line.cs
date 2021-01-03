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
        public IEnumerable<StationInLine> stations { get; set; }
    }
}

