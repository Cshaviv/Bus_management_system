using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class LineStation
    {   
            public int LineId { get; set; }//line
            public int StationCode { get; set; }//code
            public int LineStationIndex { get; set; }//station index(in the line)  
    }
}
