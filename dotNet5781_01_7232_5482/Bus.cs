
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_7232_5482
{
    class Bus
    {

        private string LicenseNum;//מספר רישוי
        public DateTime startdate;

        public Bus(string LicenseNum, DateTime startdate)
        {
            this.startdate = startdate;
            this.LicenseNum = LicenseNum;
        }


        #region מספר רישוי
        public string licenseNum
        {
            get { return LicenseNum; }
            set
            {
                if ((startdate.Year >= 2018) && (value.Length == 8))
                    LicenseNum = value;
                else if (value.Length == 7)
                    LicenseNum = value;
                else
                    throw new Exception("invalid data");


            }

        }
        #endregion

        //חסר הקטע של ההדפסה עם הפסים
        private int totalkm;

        public int MyProperty
        {
            get { return totalkm; }
            set { 
               totalkm = value; }
        }

        public const int FullTank = 1200;
        public int Fuel { get; set; }

        private int km;

        public int Mykm
        {
            get { return km; }
            set
            {
                if (value >= 0)
                    km = value;
                else
                    throw new Exception("Invalid input");

            }
        }
        public override string ToString()
        {
            string prefix, middle, suffix;
            if (startdate.Year < 2018)
            {
                prefix = LicenseNum.Substring(0, 2);
                middle = LicenseNum.Substring(2, 3);
                suffix = LicenseNum.Substring(5, 2);
            }
            else
            {
                prefix = LicenseNum.Substring(0, 3);
                middle = LicenseNum.Substring(3, 2);
                suffix = LicenseNum.Substring(5, 3);
            }
            string registrationString = String.Format("{0}-{1}-{2}", prefix, middle, suffix);

            return String.Format("[ {0}, {1} ]", registrationString, startdate.ToShortDateString());
        }




    }
}






//}

