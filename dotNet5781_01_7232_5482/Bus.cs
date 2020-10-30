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
        private int licenseNum;
        private DateTime startDate;
        private DateTime lastTreat;
        private double km;
        private double kmafterrefueling;
        private double kmaftertreat;

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
                if(value<km)
                    {throw new Exception();}
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

        public string LicenseNum
        {
            get { return licenseNum; }
            set { licenseNum = value; }
        }

        public string get_s()
        {

            /*
             מספר רישוי הוא מספר בן 7 לאוטובוסים שנכנסו לפעילות לפני שנת 2018 ו8 ספרות לשאר.
דרך ההצגה של מספר רישוי תתבצע בהתאמה לפי הפורמט הבא: 67-345-12 או 678-45-123
             * 
             */
            
            string s_l = LicenseNum.ToString();
            string prefix, middle, suffix;
            if (startdate.Year < 2018)
            {
                prefix = s_l.Substring(0, 2);
                middle = s_l.Substring(2, 3);
                suffix = s_l.Substring(5, 2);
            }
            else
            {
                prefix = LicenseNum.Substring(0, 3);
                middle = LicenseNum.Substring(3, 2);
                suffix = LicenseNum.Substring(5, 3);
                }
            string registrationString = String.Format("{0}-{1}-{2}", prefix, middle, suffix);
            return registrationString;

        }

        //האם צריך טיפול? נסע 20000 קמ מאז הטיפול האחרון או עבר שנה מהטיפול
        //b.needTreat()
        public bool needTreat()
        {
            
            return ((this.kmAfterTreat>=20000) || ((DateTime.Now - this.lastTreat).TotalDays()>=365));
        
        }

        public Bus ()
	    {
            this.km = 0;
            this.kmaftertreat = 0;
            this.
            ///
	    }

        public Bus (int l, DateTime dt, int km=0)
	    {
            //this();
            this.licenseNum = l;
            this.startDate = dt;
	    }


    }
}
