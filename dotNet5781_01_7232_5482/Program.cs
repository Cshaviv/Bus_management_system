//AYALA CHAGIT
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

namespace dotNet5781_01_7232_5482
{
    class Program
    {
        
        //All the functions we run in the main
        static int LicNum()//The function picks up a license number and checks if it is correct
        {
            Console.WriteLine("Enter the bus license number");
            string Lnum = Console.ReadLine();
            int licNum;
            bool succ = Int32.TryParse(Lnum, out licNum);

            if (!succ)
            {
                Console.WriteLine("ERROR! ");
                int x = 0;
                return x;
            }

            return licNum;
        }
       
        static bool CheckLicAndDt(int Lic_Num, DateTime dt)//The function checks if the date of commencement of operation of the bus and the length of the license number are correct
        {
            if (!((dt.Year >= 2018) && (Lic_Num.ToString().Length == 8) || (dt.Year < 2018)&&(Lic_Num.ToString().Length == 7)))
            {
                Console.WriteLine("ERROR! ");
                return false;
            }
          
            return true;
        }
        static int RandKm()//The guerrilla function has a number. (km to ride)
        {
            Random randKm = new Random(DateTime.Now.Millisecond);
            int KmForRide = randKm.Next(1200);

            //bool succ = double.TryParse(randKm.ToString(), out KmForRide);
            //if (!succ)
            //{
            //    Console.WriteLine("ERROR! 2");
            //    double x = 0.0;
            //    return x;
            //}
            //int KmForRide = rand.Next(1, 2000);
            return KmForRide;
        }

        static void Main(string[] args)
        {

            List<Bus> Buss = new List<Bus>();
            string choose = string.Empty;
            while (choose != "E")
            {
                Console.WriteLine("Hi, please choose one of the following options");
                Console.WriteLine("A: Adding a bus to the list of buses in the company");
                Console.WriteLine("B: Adding a bus to the list of buses in the company");
                Console.WriteLine("C: Refueling or handling a bus");
                Console.WriteLine("D: Presentation of the passenger since the last treatment for all vehicles in the company.");
                Console.WriteLine("E: Exit");
                choose = Console.ReadLine();
                bool succ;
                

                switch (choose)
                {
                    case "A":
                        {
                            int Lic_Num = LicNum();//The license number entered by the user.
                            if (Lic_Num == 0)//Check if the number invalid. 
                            {
                                break;
                            }
                            
                            bool found=false;
                            foreach (Bus b in Buss)// b ptr
                            {
                                if (b.LicenseNum == Lic_Num)// Check if the number exists in the system.
                                {
                                    Console.WriteLine("already exist");
                                    found = true;
                                    break;

                                }

                            }
                            if(found)//If the license number exists in the system, go to the main menu.
                                {
                                    break;
                                }
                            if(!((Lic_Num.ToString().Length == 8) || (Lic_Num.ToString().Length == 7)))//If the length of the license number is incorrect - print ERROR
                                {
                                Console.WriteLine("ERROR-1"  );
                                break;
                            
                                }



            

                            Console.WriteLine("Enter an activity start date");//Request the user to enter an activity start date.
                            String date = Console.ReadLine();
                            DateTime dt;
                            succ = DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt);
                            if (!succ)
                            {
                                Console.WriteLine("ERROR!");
                                break;
                            }
                            if (!(CheckLicAndDt(Lic_Num, dt)))//Call to function of integrity check.
                                break;
                
                            Bus NewBus = new Bus(Lic_Num, dt) ;// Add the bus to the list
                            Buss.Add(NewBus);
                            

                            break;
                        }
                    case "B":
                        {

                            int Lic_Num = LicNum();//The license number entered by the user
                            if (Lic_Num == 0)// Check if the number invalid.
                            {
                                break;
                            }
                            bool found1 = false;
                            bool found = false;
                            foreach (Bus b in Buss)// b ptr
                            {
                                if (b.LicenseNum == Lic_Num)//Check if the license number is on the list
                                {
                                    //Random randKm = new Random(DateTime.Now.Millisecond);
                                    //double KmForRide;
                                    //succ = double.TryParse(randKm.ToString(), out KmForRide);
                                    //if (!succ)
                                    //{
                                    //    Console.WriteLine("ERROR! ");
                                    //    found = true;
                                    //    break;
                                    //}

                                    //if (RandKm() == 0.0)
                                    //{
                                    //    found = true;
                                    //    break;
                                    //}
                                    found1 = true;
                                    if (b.Kmafterrefueling + RandKm() > 1200)//Check if the bus has enough fuel for the trip.
                                    {
                                        Console.WriteLine("You do not have enough fuel to go on this trip");
                                        found = true;
                                        break;
                                    }
                                    if (b.needTreat(RandKm()))//Check if the bus does not need treatment.
                                    {
                                        Console.WriteLine("The bus needs treatment");
                                        found = true;
                                        break;
                                    }
                                    b.Kmafterrefueling = b.Kmafterrefueling + RandKm();//Update bus fields due to travel.
                                    b.Kmaftertreat = b.Kmaftertreat + RandKm();
                                    b.Km = b.Km + RandKm();
                                    Console.WriteLine("The bus can go for a ride");
                                }


                            }
                            if (!found1)
                            {
                                Console.WriteLine("The bus does not exist in the reservoir");//if the bus doesnt exist in the list
                                break;
                            }
                           
                            
                               
                            
                            if (found)//if the bus need treatment or fuel, (exit from this case). 
                            {
                                break;
                            }

                            break;
                        }
                       
                
            
                    case "C":
                        {
                         
                            int Lic_Num = LicNum();//The license number entered by the user.
                            if (Lic_Num == 0)// Check if the number invalid.
                            {
                                break;
                            }
                            bool Found = false;
                            bool Found1 = false;
                            foreach (Bus b in Buss)
                            {
                                if (b.LicenseNum == Lic_Num)
                                {
                                    Found1 = true;
                                    Console.WriteLine("Type 1 if you are interested in refueling the bus.Type 2 if you are interested in treatment");
                                    string Choose1or2 = Console.ReadLine();//choose 1 or 2
                                    int YourChoose;
                                    succ = Int32.TryParse(Choose1or2, out YourChoose);
                                    if (!succ)
                                    {
                                        Console.WriteLine("ERROR! ");//Incorrect input.
                                        Found = true;
                                        break;
                                    }
                                    if (YourChoose == 1)//refueling
                                    {
                                        b.Kmafterrefueling = 0;
                                        Console.WriteLine("The fuel tank is full");
                                    }
                                    else if (YourChoose == 2)//treatment
                                    {
                                        b.LastTreat = DateTime.Now;
                                        b.Kmaftertreat = 0;
                                        Console.WriteLine("The treatment was performed successfully");
                                    }
                                    else
                                    {
                                        Console.WriteLine("This option does not exist");
                                    }
                                }
                              
                            }
                             if(!Found1)//the bus dosent exist in the list
                            {
                                Console.WriteLine("The bus does not exist in the reservoir");
                                break;
                           
                            }
                            if (Found)//the bus need to treat or refueling-(exit from the case)
                            {
                                break;
                            }
                            
                            break;
                        }
                    case "D":
                        {
                            foreach (Bus b in Buss)
                            {
                                b.get_s();//Printing of the list of buses with license number and km..
                                Console.WriteLine(b.Km+"\n");


                            }
                        }
                        
                        break;
                    default:
                        {
                            Console.WriteLine("ERROR");
                            break;
                        }
                    
                }
                //if (ActSecc)
                //{
                //    Console.WriteLine("The operation was performed correct");
                //}
            }


        }
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




        




    

    

