using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{
    class BusStation
    {
        /// </מספר תחנה>
        public string BusStationKey { get; set; }
        public double Latitude { get; set; }//קו רוחב
        public double Longitude { get; set; }//קו אורך
        static Random rand = new Random();
        public string AdressStation { get; set; }
        public void CheckCode(string c)
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
        public BusStation(string code, string adress = " ")
        {
            this.BusStationKey = code;
            this.Latitude = rand.NextDouble() * (33.3 - 31) + 31;
            this.Longitude = rand.NextDouble() * (35.5 - 34.3) + 34.3;
            this.AdressStation = adress;
        }
        public override string ToString()
        {
            return String.Format("Bus Station Code: {0}, {1}°N {2}°E", BusStationKey, Latitude, Longitude);
        }

    }
}



