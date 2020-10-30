using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_7232_5482
{
    class Program
    {
        enum choose
	{
            exit, add, drive, treat
	}


        bool AddBus(List<Bus> l)
        {
            //input 
            string l =  Console.ReadLine();;//
            string dt =  Console.ReadLine();
            //convert to match types
            int res;
            bool succ = Int32.TryParse(l, out res)
            if (!succ)
            {
                Console.WriteLine("ERROR li");
                return false;
            }
            DateTime dt1;
            succ = DateTime.TryParse(dt, out dt1);
            if(!succ)
            ////
            //check the lic not in list
            Bus bus;
            foreach (Bus bus in Buss)
	        {
                if(bus.LicenseNum==lic)
                {
                    Console.WriteLine("already exist");
                    //return;
                    //break
                }
	        }
            for (int i = 0; i < Buss.Count; i++)
			{
                Bus b = Buss[i];
			}

            //
            Bus b = new Bus(l, dt);
            l.Add(b);
            return true;

        }

        static void printOptions()
            {
            Console.WriteLine("Hi, please choose one of the following options");
            Console.WriteLine("A: Adding a bus to the list of buses in the company");
            Console.WriteLine("B: Adding a bus to the list of buses in the company");
            Console.WriteLine("C: Refueling or handling a bus");
            Console.WriteLine("D: Pr{esentation of the passenger since the last treatment for all vehicles in the company.");
            Console.WriteLine("E: Exit");
        }
      
        static void Main(string[] args)
        {
            f();
            List<Bus> Buss = new List<Bus>();
            printOptions();
            string choose = Console.ReadLine();
            int ch = 
            int.TryParse(choose);
            bool res;
            while (choose != "E") ;
            {
                switch(choose)
                
                    case "A":
                        res=AddBus(Buss);
                 
                        break;
                    case "B":
                        break;
                    case "C":
                        break;
                    case "D":
                        break;
                    case "E":
                        break;
                    default:
                        Console.WriteLine("ERROR");
                        break;






                }
                if(res)
                {
                    //הפעולה הצליחה
                }
                else{
                    //נכשל

                }

                printOptions();
                string choose = Console.ReadLine();




            }

          
          

        }
    }
}
