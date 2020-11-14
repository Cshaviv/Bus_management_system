using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BusLineStation> AllStations = new List<BusLineStation>();


            //initialize();
            CHOICE choice;
            do
            {
                Console.WriteLine("Make your mind:");
                Console.WriteLine("ADD,DELETE,FIND,PRINT,EXIT= -1");
                bool success = Enum.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case CHOICE.ADD:
                        Add();
                        break;
                    case CHOICE.DELETE:
                        break;
                    case CHOICE.FIND:
                        break;
                    case CHOICE.PRINT:
                        break;
                    case CHOICE.EXIT:
                        break;
                    default:
                        break;
                }

            } while (choice != CHOICE.EXIT);


            //private static void initialize()
            //{
            //    //TODO


            
        }
        List<BusLineStation> AllStations = new List<BusLineStation>();
        
        //public List<BusLineStation> AllStations { get; set; }
        static public void Add()
        {
            try
            {
                Console.WriteLine("If you want to add a bus line choose 1, if you want to add a stop to a bus line choose 2");
                int choice= int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Console.WriteLine("Enter the bus number:");
                    int busNum = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter the first station:");
                    int firstStat = int.Parse(Console.ReadLine());
                    //foreach(BusLineStation b in AllStations)
                    //{
                    //    if(b.FirstStation.BusStationKey==firstStat)
                    //    {

                    //    }

                    //}
                }
                else if (choice == 2)
                {
                    Console.WriteLine("Enter the number of the bus that you want to add a station");
                    int busNum = int.Parse(Console.ReadLine());
                    int newStation = int.Parse(Console.ReadLine());
                    search(busNum);

                    //foreach (BusLineStation b in AllStations)
                    //{
                    //    if (b.FirstStation.BusStationKey == newStation)//בדיקה אם התחנה קיימת ברשימת תחנות
                    //    {

                    //    }

                    //}
                }

                AddStation();

                


            }
            catch (FormatException)
            {
                Console.WriteLine("The value must be numeric");
            }

        }

        


    }
}
 

