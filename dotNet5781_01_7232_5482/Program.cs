//AYALA CHAGIT
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//       enum choose
//{
//           exit, add, drive, treat
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
        static double BusTotalKm()
       
        {
            Console.WriteLine("Press 'Y' to update the number of kilometers the bus has traveled or 'N' to skip ");
            string choose = Console.ReadLine();
            double TotalKm = 0.0;
            if ((choose == "Y") || (choose == "y"))
            {
                Console.WriteLine("Please type");
                string km = Console.ReadLine();
                bool succses = double.TryParse(km, out TotalKm);
                if (!succses)
                {
                    Console.WriteLine("ERROR!, Please type digits only");
                    BusTotalKm();

                }
                if(TotalKm<0)
                {
                    Console.WriteLine("Sorry this km number could not be true");
                    BusTotalKm();
                }
                Console.WriteLine("This figure has been updated successfully");
                return TotalKm;

            }
            else if ((choose == "N") || (choose == "n"))
            {
                Console.WriteLine("This figure has been given a default value");
                return TotalKm;

            }
            else
            {
                Console.WriteLine("Sorry, this option does not exist");
                BusTotalKm();
            }
            return 0;
        }
        static double LastTreat(double TotalKm)
        {
            double My_TotalKm = TotalKm;
            Console.WriteLine("Press 'Y' to update the number of kilometers the bus has traveled since last treatmant or 'N' to skip  ");
            string choose = Console.ReadLine();
            double KmFromLastTreat = 0.0;
            if ((choose == "Y") || (choose == "y"))
            {
                Console.WriteLine("Please type");
                string km = Console.ReadLine();
                bool succses = double.TryParse(km, out KmFromLastTreat);
                if (!succses)
                {
                    Console.WriteLine("ERROR!, Please type digits only");
                    return LastTreat(My_TotalKm);
                }
                if(KmFromLastTreat>20000)
                {
                    Console.WriteLine("Sorry this km number could not be true");
                    return LastTreat(My_TotalKm);
                }
                if (KmFromLastTreat>TotalKm)
                {
                    Console.WriteLine("Sorry this km number could not be true");
                    return LastTreat(My_TotalKm);
                }
                Console.WriteLine("This figure has been updated successfully");
                return KmFromLastTreat;

            }

            else if ((choose == "N") || (choose == "n"))
            {
                Console.WriteLine("This figure has been given a default value");
                return KmFromLastTreat;

            }
            else
            {
                Console.WriteLine("Sorry, this option does not exist");
                return LastTreat(My_TotalKm);
            }
            return 0;
        }
        static double LastRefueling(double TotalKm)
        {
            Console.WriteLine("Press 'Y' to update the number of kilometers the bus has traveled since last refueling or 'N' to skip");
            string choose = Console.ReadLine();
            double KmFromLastrefueling = 0.0;
            double My_TotalKm = TotalKm;
            if ((choose == "Y") || (choose == "y"))
            {
                Console.WriteLine("Please type");
                string km = Console.ReadLine();
                bool succses = double.TryParse(km, out KmFromLastrefueling);
                if (!succses)
                {
                    Console.WriteLine("ERROR!, Please type digits only");
                    LastRefueling(TotalKm);
                }
                if (KmFromLastrefueling > 1200)
                {
                    Console.WriteLine("Sorry this km number could not be true");
                    LastRefueling(TotalKm);
                }
                if (KmFromLastrefueling > TotalKm)
                {
                    Console.WriteLine("Sorry this km number could not be true");
                    LastTreat(My_TotalKm);
                }
                Console.WriteLine("This figure has been updated successfully");
                return KmFromLastrefueling;

            }

            else if ((choose == "N") || (choose == "n"))
            {
                Console.WriteLine("This figure has been given a default value");
                return KmFromLastrefueling;

            }
            else
            {
                Console.WriteLine("Sorry, this option does not exist");
                LastRefueling(TotalKm);
            }
            return 0;
        }

        static DateTime DateOfLastTreat()
        {
            Console.WriteLine("Press 'Y' To type in what date was the last treatment of the bus  or 'N' to skip ");
            string choose = Console.ReadLine();
            if ((choose == "Y") || (choose == "y"))
            {
                Console.WriteLine("Please type");
                String date = Console.ReadLine();
                DateTime DT;
                bool succses = DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DT);
                if (!succses)
                {
                    Console.WriteLine("ERROR!,Please enter a valid date");
                    DateOfLastTreat();
                }
                Console.WriteLine("This figure has been updated successfully");
                return DT;

            }

            else if ((choose == "N") || (choose == "n"))
            {
                Console.WriteLine("This figure has been given a default value");
                return DateTime.Now;

            }
            else
            {
                Console.WriteLine("Sorry, this option does not exist");
                DateOfLastTreat();
            }
            return DateTime.Now ;
        }
        static double RandKm()//The guerrilla function has a number. (km to ride)
        {
            Random randKm = new Random(DateTime.Now.Millisecond);
            double KmForRide = randKm.NextDouble()*(1200.0-0.0)+(0.0);
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
                Console.WriteLine("B: Choosing a bus to travel");
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
                            if (!((Lic_Num.ToString().Length == 8) || (Lic_Num.ToString().Length == 7)))//If the length of the license number is incorrect - print ERROR
                            {
                                Console.WriteLine("ERROR");
                                break;

                            }

                            Console.WriteLine("Enter an activity start date");//Request the user to enter an activity start date.
                            String date = Console.ReadLine();
                            DateTime DT;
                            succ = DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DT);
                            if (!succ)
                            {
                                Console.WriteLine("ERROR!, Please enter a valid date");
                                break;
                            }
                            if (!(CheckLicAndDt(Lic_Num, DT)))//Call to function of integrity check.
                                break;
                            double TotalKm = BusTotalKm();
                            double KmFromLastTreat = LastTreat(TotalKm);
                            double KmFromLastRefuling = LastRefueling(TotalKm);
                            DateTime DateFromLastTreat = DateOfLastTreat();
                            Bus NewBus = new Bus(Lic_Num, DT, DateFromLastTreat, TotalKm, KmFromLastTreat, KmFromLastRefuling) ;// Add the bus to the list
                            Buss.Add(NewBus);
                            Console.WriteLine("The bus successfully added to the list of buses");
                            

                            break;
                        }
                    case "B":
                        {

                            int Lic_Num = LicNum();//The license number entered by the user
                            if (Lic_Num == 0)// Check if the number invalid.
                            {
                                break;
                            }
                            bool found = false;
                            //bool found = false;
                            foreach (Bus b in Buss)// b ptr
                            {
                                if (b.LicenseNum == Lic_Num)//Check if the license number is on the list
                                {
              
                                    found = true;
                                    double KmForRide = RandKm();
                                    if (b.Kmafterrefueling + KmForRide > 1200)//Check if the bus has enough fuel for the trip.
                                    {
                                        Console.WriteLine("You do not have enough fuel to go on this trip");
                                        //found = true;
                                        break;
                                    }
                                    if (b.needTreat(KmForRide))//Check if the bus does not need treatment.
                                    {
                                        Console.WriteLine("The bus needs treatment");
                                        //found = true;
                                        break;
                                    }
                                    b.Kmafterrefueling = b.Kmafterrefueling + KmForRide;//Update bus fields due to travel.
                                    b.Kmaftertreat = b.Kmaftertreat + KmForRide;
                                    b.Km = b.Km + KmForRide;
                                    Console.WriteLine("The bus can go for a ride");
                                }


                            }
                            if (!found)
                            {
                                Console.WriteLine("The bus does not exist in the reservoir");//if the bus doesnt exist in the list
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
                            //bool Found = false;
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
                                        //Found = true;
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
                            //if (Found)//the bus need to treat or refueling-(exit from the case)
                            //{
                            //    break;
                            //}
                            
                            break;
                        }
                    case "D":
                        {
                            foreach (Bus b in Buss)
                            {
                                b.get_LicesNum();//Printing of the list of buses with license number and km.
                                Console.WriteLine(b.Km+"\n");


                            }
                        }
                        
                        break;
                    default:
                        {
                            Console.WriteLine("Sorry, this option does not exist in the system");
                            break;
                        }
                    
                }
               
            }


        }
    }
    
       
}
   




        




    

    

