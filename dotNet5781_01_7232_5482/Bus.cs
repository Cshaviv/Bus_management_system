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

        public void  get_s()
        {

            /*
             מספר רישוי הוא מספר בן 7 לאוטובוסים שנכנסו לפעילות לפני שנת 2018 ו8 ספרות לשאר.
דרך ההצגה של מספר רישוי תתבצע בהתאמה לפי הפורמט הבא: 67-345-12 או 678-45-123
             * 
             */

            string s_l = this.LicenseNum.ToString();
            string prefix, middle, suffix;
            if (startDate.Year < 2018)
            {
                prefix = s_l.Substring(0, 2);
                middle = s_l.Substring(2, 3);
                suffix = s_l.Substring(5, 2);
            }
            else
            {
                prefix = s_l.Substring(0, 3);
                middle = s_l.Substring(3, 2);
                suffix = s_l.Substring(5, 3);
            }
            string registrationString = String.Format("{0}-{1}-{2}", prefix, middle, suffix);
            Console.WriteLine(registrationString+"/n");

        }
//        public bool check_yaer()
//        {
//            if (this.lastTreat + 1)
//                return false;
//        }

//}
        

        //האם צריך טיפול? נסע 20000 קמ מאז הטיפול האחרון או עבר שנה מהטיפול
        //b.needTreat()
        public bool needTreat()
        {
            return ((this.kmaftertreat >= 20000) || 
                    ((DateTime.Now - this.lastTreat).TotalDays >= 365));
        
        }

        public Bus ()
	    {
            this.licenseNum = 0;
            this.km = 0;
            this.kmaftertreat = 0;
          
        }

        public Bus(int licNum, DateTime dt, double km1 = 0, double kmaftertreat1 = 0, double kmafterrefueling1 = 0)
	    {
            //this();
            this.startDate = dt;
            this.licenseNum = licNum;
            this.Km = km1;
            this.kmaftertreat = kmaftertreat1;
            this.kmafterrefueling = kmafterrefueling1;




        }

    //private int licenseNum;
    //private DateTime startDate;
    //private DateTime lastTreat;
    //private double km;
    //private double kmafterrefueling;
    //private double kmaftertreat;
}
}
