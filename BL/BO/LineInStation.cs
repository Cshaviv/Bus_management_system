using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineInStation// קווים שעוברים בתחנה 
    {
        public int LineId { get; set; }  //מזהה ייחודי של הקו
        public int LineNum { get; set; } //מספר קו
        public int LineStationIndex { get; set; } //האינדקס בתחנת קו                                        
        public string TargetStation { get; set; }// תחנת היעד

    }
}
    

