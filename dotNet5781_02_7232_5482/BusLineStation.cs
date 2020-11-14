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

        public double My_Distance//לא לשכוח לקלוט מהמשתמש מספר במיין 
        {
            get { return Distance; }
            set { Distance = value; }
        }
        //חישבנו את המהירןת לפי ק"מ לדקה 
        public TimeSpan My_Time
        {
            get { return Time; }
            set { }
        }

        public TimeSpan TravelTime()
        {
            double speed = 1;//km for miniute
            double Time = this.My_Distance / speed;
            TimeSpan TimePerMin = TimeSpan.FromMinutes(Time);
            return TimePerMin;
        }
        public override string ToString()
        {
            String result = base.ToString();
            result += String.Format(" Distance: {0}, Time: {1}", Distance, Time);
            return result;
        }
        public void AddStation()
        {
            Console.WriteLine("Enter ths bus station key:");
            int choice = int.Parse(Console.ReadLine());
        }

        public BusLineStation(string code1, string adress1, double Distance1, TimeSpan Time1) : base( code1, adress1) 
        {
            this.Distance = Distance1;
            this.Time = Time1;
        }
    }



}
