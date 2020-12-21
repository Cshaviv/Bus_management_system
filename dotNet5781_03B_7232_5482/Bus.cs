using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_7232_5482
{
  public class Bus
    {
        private int licenseNum;
        private DateTime startDate;
        private DateTime lastTreat;
        private double km;
        private double kmafterrefueling;
        private double kmaftertreat;
        private STATUS status;

        public STATUS myStatus
        {
            get { return status; }
            set { status = value; }
        }

        public double Kmaftertreat
        {
            get { return kmaftertreat; }
            set { kmaftertreat = value; }
        }

        public double Kmafterrefueling
        {
            get { return kmafterrefueling; }
            set { kmafterrefueling = value; }
        }
        public double Km
        {
            get { return km; }
            set {
                if (value < km)
                { throw new Exception(); }
                km = value; }
        }

        public DateTime LastTreat
        {
            get { return lastTreat; }
            set { lastTreat = value; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public int LicenseNum
        {
            get { return licenseNum; }
            set { licenseNum = value; }
        }
        public void Refuel()
        {
            kmafterrefueling = 1200;
        }
        public bool needTreat(double RandKm1)
        {
            return ((this.kmaftertreat + RandKm1 >= 20000) || ((DateTime.Now - this.lastTreat).TotalDays >= 365));
        }


        public Bus(int licNum, DateTime dt, DateTime My_DT, double My_Km = 0, double My_Kmaftertreat = 0, double My_Kmafterrefueling = 0, STATUS e= STATUS.Available)
        {
            this.startDate = dt;
            this.licenseNum = licNum;
            this.lastTreat = My_DT;
            this.Km = My_Km;
            this.kmaftertreat = My_Kmaftertreat;
            this.kmafterrefueling = My_Kmafterrefueling;
            this.myStatus = e;
        }
        public Bus()
        {

        }
        
        public override string ToString()// print licene number 
        {
            int year = StartDate.Year;
            string licenNum = LicenseNum.ToString();
            if (year < 2018)
                return "" + licenNum[0] + licenNum[1] + "-" + licenNum[2] + licenNum[3] + licenNum[4] + "-" + licenNum[5] + licenNum[6];
            else
                return "" + licenNum[0] + licenNum[1] + licenNum[2] + "-" + licenNum[3] + licenNum[4] + "-" + licenNum[5] + licenNum[6] + licenNum[7];
        }
    }

}
