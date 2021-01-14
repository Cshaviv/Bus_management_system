using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Bus
    {
        public int LicenseNum { get; set; }//license number
        public DateTime StartDate { get; set; }//start date
        public double TotalKm { get; set; }//total km
        public double FuelTank { get; set; }//fuel tank
        public BusStatus StatusBus { get; set; }//status
        public DateTime DateLastTreat { get; set; }//Date of Last Treat
        public double KmLastTreat { get; set; }//Km of Last Treat
        public bool IsDeleted { get; set; }//if it possible

   
       
    }
}
