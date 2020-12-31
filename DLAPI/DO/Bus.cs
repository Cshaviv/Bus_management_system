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

        public Bus(int licNum, DateTime dt, DateTime My_DT, double total_Trip = 0, double My_Kmaftertreat = 0, double My_Kmafterrefueling = 0, STATUS e = STATUS.Available)
        {
            this.FromDate = dt;
            this.LicenseNum = licNum;
            this.TotalTrip = total_Trip ;
            this.Km = My_Km;
            this.KmLastTreat = My_Kmaftertreat;
            this.kmafterrefueling = My_Kmafterrefueling;
            this.Status = e;
        }
        public Bus()
        {

        }
    }
}
