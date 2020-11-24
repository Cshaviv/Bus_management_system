//Ayala Israeli 324207232, Chagit Shaviv 322805482
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{
    class BusStation
    {   
        public string BusStationKey { get; set; }//station number
        public double Latitude { get; set; }//latitude
        public double Longitude { get; set; }//longtitude
        static Random rand = new Random();
        public string AddressStation { get; set; }//address of the station
        public void CheckCode(string c)//check if the code  correct
        {
            int BusCode;
            bool succ = Int32.TryParse(c, out BusCode);
            if (!succ)
            {
                throw new ArgumentException(String.Format("{0} The code is invalid", c), "Code");
            }
            if (c.Length > 6)
            {
                throw new ArgumentException(String.Format("{0} The code is invalid", c), "Code");
            }
            return;
        }
        public BusStation(string code, string adress = " ")//constructor
        {
            this.BusStationKey = code;
            this.Latitude = rand.NextDouble() * (33.3 - 31) + 31;
            this.Longitude = rand.NextDouble() * (35.5 - 34.3) + 34.3;
            this.AddressStation = adress;
        }
        public override string ToString()//string func
        {
            return String.Format("Bus Station Code: {0}, {1}°N {2}°E", BusStationKey, Latitude, Longitude);
        }

    }
}



