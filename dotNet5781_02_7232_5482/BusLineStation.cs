using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{
      class BusLineStation : BusStation
    {
        static Random rand = new Random();
        private double Distance;
        public TimeSpan Time;

        public double My_Distance
        {
            get { return Distance; }
            set
            {
                Distance = rand.NextDouble();
            }
        }
        public TimeSpan My_Time
        {
            get { return Time; }
            set { }
        }
        public override string ToString()
        {
            String result = base.ToString();
            result += String.Format(" Distance: {0}, Time: {1}", Distance, Time);
            return result;
        }
    }


}
