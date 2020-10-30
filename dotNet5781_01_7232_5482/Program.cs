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
        //       enum choose
        //{
        //           exit, add, drive, treat
        //}


        //     static bool AddBus(List<Bus> x)
        //     {
        //     //input
        //     Console.WriteLine("Enter the bus license number");
        //         string Lnum =  Console.ReadLine();
        //     int y;
        //     bool succ = Int32.TryParse(Lnum, out y);
        //     if (!succ)
        //     {
        //         Console.WriteLine("ERROR! Enter the bus license number");
        //         Console.ReadLine();
        //     }
        //         Console.WriteLine("Enter an activity start date");
        //         string dt =  Console.ReadLine();
        //         //convert to match types
        //         int res;
        //     return false;
        //         }
        //         DateTime dt1;
        //         succ = DateTime.TryParse(dt, out dt1);
        //         if(!succ)
        //         ////
        //         //check the lic not in list
        //         Bus bus;
        //         foreach (Bus bus in Buss)
        //      {
        //             if(bus.LicenseNum==lic)
        //             {
        //                 Console.WriteLine("already exist");
        //                 //return;
        //                 //break
        //             }
        //      }
        //         for (int i = 0; i < Buss.Count; i++)
        //{
        //             Bus b = Buss[i];
        //}

        //         //
        //         Bus b = new Bus(l, dt);
        //         l.Add(b);
        //         return true;

        //     }


        //static void printOptions()
        //{
        //}

        static void Main(string[] args)
        {
            List<Bus> Buss = new List<Bus>();

            Console.WriteLine("Hi, please choose one of the following options");
            Console.WriteLine("A: Adding a bus to the list of buses in the company");
            Console.WriteLine("B: Adding a bus to the list of buses in the company");
            Console.WriteLine("C: Refueling or handling a bus");
            Console.WriteLine("D: Pr{esentation of the passenger since the last treatment for all vehicles in the company.");
            Console.WriteLine("E: Exit");
            string choose = Console.ReadLine();
            bool succ;

            while (choose != "E")
            {
                switch (choose)
                {
                    case "A":
                        Console.WriteLine("Enter the bus license number");
                        string Lnum = Console.ReadLine();
                        int licNum;
                        succ = Int32.TryParse(Lnum, out licNum);

                        if (!succ)
                        {
                            Console.WriteLine("ERROR! ");
                            break;
                        }

                        Console.WriteLine("Enter an activity start date");
                        String date = Console.ReadLine();
                        DateTime dt;
                        succ = DateTime.TryParse(date, out dt);
                          if(!succ)
                        {
                            Console.WriteLine("ERROR! ");
                            break;
                        }

                        if (!(dt.Year >= 2018) && (Lnum.Length == 8)||( Lnum.Length == 7))
                        {
                            Console.WriteLine("ERROR! ");
                            break;
                        }

                        foreach (Bus b in Buss)// b ptr
                        {
                            if (b.LicenseNum == licNum)
                            {
                                Console.WriteLine("already exist");
                                break;
                            }
                        }
                        Bus NewBus = new Bus(licNum, dt);
                        Buss.Add(NewBus);

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
            //if(res)
            //{
            //    //הפעולה הצליחה
            //}
            //else{
            //    //נכשל

            //}

            //printOptions();
            //string choose = Console.ReadLine();




        }




    }
}
    

