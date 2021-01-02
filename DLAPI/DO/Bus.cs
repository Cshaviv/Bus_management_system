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
        public DateTime FromDate { get; set; }//start date
        public double TotalTrip { get; set; }//total km
        public double FuelRemain { get; set; }//fuel tank
        public BusStatus Status { get; set; }//status
        public DateTime DateLastTreat { get; set; }
        public double KmLastTreat { get; set; }
        public bool IsDeleted { get; set; }

        public Bus(int licNum, DateTime FromDate, DateTime DateLastTreat, BusStatus status, double total_Trip = 0, double KmLastTreat = 0)
        {
            this.FromDate = FromDate;
            this.DateLastTreat = DateLastTreat;
            this.LicenseNum = licNum;
            this.TotalTrip = total_Trip ;
           // this.Km = My_Km;
            this.KmLastTreat = KmLastTreat;
            this.Status = status;
        }
        public Bus()
        {

        }
    }
}
