using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{


    class BusLineStation : BusStation
    {
        public double Distance;// distance from previous station
        public TimeSpan Time;//time from previous station
        public double My_Distance 
        {
            get { return Distance; }
            set { Distance = value; }
        }
        public TimeSpan My_Time
        {
            get { return Time; }
            set { }
        }
        public TimeSpan TravelTime( double Distance)// one km to a minutes
        {
            double speed = 1;//km for miniute
            double Time = this.Distance / speed;
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
            return;
        }
        public BusLineStation(string code1, string adress1, double Distance1) : base(code1, adress1)//Constractor
        {
            this.Distance = Distance1;
            this.Time = TravelTime(Distance1);
        }     
         public void Address()//ask if the user whant to add adress
        {
            int num=1;
            Console.WriteLine("Enter 1 if you want to add the station's addrees, and 0 to continue");
            bool success = false;
            string address = " ";
            do
            {
                 success = int.TryParse(Console.ReadLine(), out num);
             
                if (!success || (num != 1 && num != 0))
                {
                    Console.WriteLine("This option doesn't exist, please enter number again");
                }
            }
            while (!success || (num != 1 && num != 0));
            if (num == 1)
            {
                Console.WriteLine("Please enter the address");
                address = Console.ReadLine();
                this.AddressStation = address;
                return;
            }
            else if (num==0)
            {
                this.AddressStation = " ";
                return;
            }

            return;
        }
    }

}