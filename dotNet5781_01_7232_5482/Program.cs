using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_7232_5482
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bus> Buss = new List<Bus>();
            Console.WriteLine("Hi, please choose one of the following options");
            Console.WriteLine("A: Adding a bus to the list of buses in the company");
            Console.WriteLine("B: Adding a bus to the list of buses in the company");
            Console.WriteLine("C: Refueling or handling a bus");
            Console.WriteLine("D: Presentation of the passenger since the last treatment for all vehicles in the company.");
            Console.WriteLine("E: Exit");
            string choose = Console.ReadLine();
            while (choose != "E") ;
            {
                switch(choose)
                {
                    case "A":
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
            }

          
          

        }
    }
}
