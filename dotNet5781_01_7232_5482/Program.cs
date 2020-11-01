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
        //static int LicNum() 
        // {
        //     Console.WriteLine("Enter the bus license number");
        //     string Lnum = Console.ReadLine();
        //     int licNum;
        //     bool succ = Int32.TryParse(Lnum, out licNum);
        //     if (!succ)
        //     {
        //         return 0;
        //     }
        //     return licNum;

        // }
        //כל הפונקציות שנפעיל בתוך המיין
        static int LicNum()
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
       
        static bool CheckLicAndDt(int Lic_Num, DateTime dt)
        {
            if (!((dt.Year >= 2018) && (Lic_Num.ToString().Length == 8) || (dt.Year < 2018)&&(Lic_Num.ToString().Length == 7)))
            {
                Console.WriteLine("ERROR! ");
                return false;
            }
            return true;
        }
        static int RandKm()
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
                            int Lic_Num = LicNum();//המספר סידורי שהמשתמש הכניס
                            if (Lic_Num == 0)// בדיקה אם המספר תקין
                            {
                                break;
                            }
                            
                            bool found=false;
                            foreach (Bus b in Buss)// b ptr
                            {
                                if (b.LicenseNum == Lic_Num)// בדיקה אם המספר קיים במערכת
                                {
                                    Console.WriteLine("already exist");
                                    found = true;
                                    break;

                                }

                            }
                            if(found)//אם המספר רישוי קיים במערכת-תצא
                                {
                                    break;
                                }

                            Console.WriteLine("Enter an activity start date");//בקשה מהמשתמש להכניס תאריך תחילת פעילות
                            String date = Console.ReadLine();
                            DateTime dt;
                            succ = DateTime.TryParseExact(date, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out dt);
                            if (!succ)
                            {
                                Console.WriteLine("ERROR!");
                                break;
                            }
                            if (!(CheckLicAndDt(Lic_Num, dt)))//בדיקה נוספת של תקינות תאריך ומספר רישוי
                                break;
                
                            Bus NewBus = new Bus(Lic_Num, dt) ;// הוספה של האוטובוס למאגר
                            Buss.Add(NewBus);
                            

                            break;
                        }
                    case "B":
                        {

                            int Lic_Num = LicNum();//המספר רישוי שהמשתמש הכניס
                            if (Lic_Num == 0)// בדיקה אם המספר תקין
                            {
                                break;
                            }
                            bool found1 = false;
                            bool found = false;
                            foreach (Bus b in Buss)// b ptr
                            {
                                if (b.LicenseNum == Lic_Num)
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
                                    if (b.Kmafterrefueling + RandKm() > 1200)
                                    {
                                        Console.WriteLine("You do not have enough fuel to go on this trip");
                                        found = true;
                                        break;
                                    }
                                    if (b.needTreat())
                                    {
                                        Console.WriteLine("The bus needs treatment");
                                        found = true;
                                        break;
                                    }
                                    b.Kmafterrefueling = b.Kmafterrefueling + RandKm();
                                    b.Kmaftertreat = b.Kmaftertreat + RandKm();
                                    b.Km = b.Km + RandKm();
                                    Console.WriteLine("The bus can go for a ride");
                                }


                            }
                            if (!found1)
                            {
                                Console.WriteLine("The bus does not exist in the reservoir");
                                break;
                            }
                           
                            
                               
                            
                            if (found)
                            {
                                break;
                            }

                            break;
                        }
                       
                
            
                    case "C":
                        {
                         
                            int Lic_Num = LicNum();//המספר רישוי שהמשתמש הכניס
                            if (Lic_Num == 0)// בדיקה אם המספר תקין
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
                                    string Choose1or2 = Console.ReadLine();
                                    int YourChoose;
                                    succ = Int32.TryParse(Choose1or2, out YourChoose);
                                    if (!succ)
                                    {
                                        Console.WriteLine("ERROR! ");
                                        Found = true;
                                        break;
                                    }
                                    if (YourChoose == 1)//תדלוק
                                    {
                                        b.Kmafterrefueling = 0;
                                        Console.WriteLine("The fuel tank is full");
                                    }
                                    else if (YourChoose == 2)//טיפול
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
                             if(!Found1)
                            {
                                Console.WriteLine("The bus does not exist in the reservoir");
                                break;
                           
                            }
                            if (Found)
                            {
                                break;
                            }
                            
                            break;
                        }
                    case "D":
                        {
                            foreach (Bus b in Buss)
                            {
                                b.get_s();
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




        




    

    

