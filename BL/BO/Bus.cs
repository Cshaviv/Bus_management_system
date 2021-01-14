using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
   public class Bus
    {
        public int LicenseNum { get; set; }//license number
        public DateTime StartDate { get; set; }//start date(date of start)
        public double TotalKm { get; set; }//total km
        public double FuelTank { get; set; }//fuel tank
        //public double kmAfterRefuling { get; set; }//fuel tank
        public BusStatus StatusBus { get; set; }//status of the bus
        public DateTime DateLastTreat { get; set; }//date of the last treatment
        public double KmLastTreat { get; set; }// total km from the last treatment
        public override string ToString()
        {
            int year = StartDate.Year;
            string licenNum = LicenseNum.ToString();
            if (year < 2018)
                return "" +  licenNum[0] + licenNum[1] + "-" + licenNum[2] + licenNum[3] + licenNum[4] + "-" + licenNum[5] + licenNum[6]; 
            else
                return "" + licenNum[0] + licenNum[1] + licenNum[2] + "-" + licenNum[3] + licenNum[4] + "-" + licenNum[5] + licenNum[6] + licenNum[7] ;
        }
    }
}
