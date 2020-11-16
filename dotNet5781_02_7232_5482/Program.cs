//Ayala and C hagit
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{

    //  enum Options { InsertBus, InsertStation, RemoveBus, RemoveStation, LinesPassStation, LinesPassRoute, PrintAllLines, PrintStations, Exit };
    class Program
    {
        static void Main(string[] args)
        {
            List<BusStation> AllStations = new List<BusStation>();
            BusCollection AllBuses = new BusCollection();
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
                        AddNew(AllStations, AllBuses);
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
        }

        static public void AddNew(List<BusStation> AllStations, BusCollection AllBuses)
        {
            try
            {
                Console.WriteLine("Enter 1 if you want to add bus line,Enter 2 if you want to add station");
                int choice = int.Parse(Console.ReadLine());
                if (choice != 1 || choice != 2)
                    throw new BusException("your choice is incorrect");
                else if (choice == 1)
                    AddNewBus(AllStations, AllBuses);
                else if (choice == 2)
                {
                    //BusLine bus= SearchBus(AllBuses);
                    NewStation(AllBuses, AllStations);
                }

            }
            catch (FormatException)
            {
                Console.WriteLine("The value must be numeric");
            }
        }
        static public void AddNewBus(List<BusStation> AllStations, BusCollection AllBuses)
        {
            List<BusLineStation> ListStation = new List<BusLineStation>();
            Console.WriteLine("Enter a number of bus line");
            int busnum = GetNum();
            Console.WriteLine("Enter the first station number");
            string firstnum = GetStat(out firstnum);
            Console.WriteLine("Enter the last station number");
            string lastnum = GetStat(out lastnum);
            foreach (BusLine b in AllBuses)
            {
                if (b.BusNumber == busnum)
                {
                    if (!SearchBus(AllBuses, busnum, firstnum, lastnum))
                    {
                        throw new BusException("Sorry, this bus number already exist");
                    }

                }
            }

            if (!SearchStat(firstnum, AllStations))
            {
                AllStations.Add(new BusStation(firstnum, " "));
            }
            BusStation FirstStat = ReturnStation(AllStations, firstnum);

            if (!SearchStat(lastnum, AllStations))
            {
                AllStations.Add(new BusStation(lastnum, " "));
            }
            BusStation LastStat = ReturnStation(AllStations, lastnum);
            BusLine NuwBus = new BusLine(busnum,firstnum,lastnum);
            Console.WriteLine("Enter the first station");
            string first = GetStat(out first);
            Console.WriteLine("Enter the last station");
            string last = GetStat(out last);
           


        }
        static public void NewStation(BusCollection AllBuses, List<BusStation> AllStations)
        {
            Console.WriteLine("Enter the bus number to which you want to add a station");
            int num = GetNum();
            Console.WriteLine("Enter the first station");
            string first = GetStat(out first);
            Console.WriteLine("Enter the last station");
            string last = GetStat(out last);
            if (!SearchBus(AllBuses, num, first, last))
            {
                throw new Exception("this bus doesn't exist");
            }
            BusLine bus = ReturnBus(AllBuses, num, first, last);
            int choose = 1;
            do
            {
                Console.WriteLine("Enter the station number you want to add");
                string NewStat = GetStat(out NewStat);
                if (bus.CheckStationExist(NewStat))
                    throw new BusException("This station already exist");
                if (SearchStat(NewStat, AllStations))
                {
                    BusStation b = ReturnStation(AllStations, NewStat);
                    bus.AddStations(b);
                }
                else
                {

                    BusStation b = new BusStation(NewStat);
                    AllStations.Add(b);
                    bus.AddStations(b);
                }

                Console.WriteLine("Enter 1 if you whant to add another station, and 0 to exit ");
                choose = GetIntNum();

            }
            while (choose==1);
            /*bus.AddStations()*/;
        }
        //static public void AddNewBus(List<BusStation> AllStations, BusCollection AllBuses)
        //{
        //    List<BusLineStation> ListStation = new List<BusLineStation>();
        //    Console.WriteLine("Enter a number of bus line");
        //    int num = GetNum();
        //    int YourChoice;
        //    Console.WriteLine("Enter at least 2 stations");
        //    do
        //    {
        //        Console.WriteLine("for enter station enter 1,for stop enter 2");
        //        YourChoice = GetNum();
        //        while (YourChoice != 0 && YourChoice != 1)
        //        {
        //            YourChoice = GetNum();
        //            if (YourChoice != 0 && YourChoice != 1)
        //                Console.WriteLine("ERROR, try enter number again");
        //        }
        //        if (YourChoice == 1)
        //        {
        //            Console.WriteLine("Insert bus station number");
        //            string stringnum;
        //            GetStat(out stringnum);
        //            if (!SearchStat(stringnum, AllStations))
        //                AllStations.Add(new BusStation(stringnum));
        //            Console.WriteLine("Enter distance in km from last station");
        //            double distance = GetNumDistance();
        //            Console.WriteLine("Insert time from last station (format hh:mm::ss)");
        //            TimeSpan time = TravelTime(distance);
        //            YourChoice = -1;
        //            bool flag = false;
        //            foreach (BusLineStation b in ListStation)
        //                if (b.BusStationKey == stringnum)
        //                {
        //                    flag = true;
        //                    Console.WriteLine("You already entered this bus station");
        //                }
        //            if (!flag)
        //                ListStation.Add(new BusLineStation(stringnum, " ", distance, time));
        //        }
        //    }
        //    while (YourChoice != 2);
        //    if (ListStation.Count > 1)
        //    {
        //        ListStation[0].My_Distance = 0;
        //        ListStation[0].My_Time = TimeSpan.Zero;
        //        AllBuses.AddBus(new BusLine(ListStation, num, ListStation[0], ListStation[ListStation.Count - 1], Area.GENERAL));
        //    }
        //    else
        //        throw new BusException("You entered less than 2 stations, so the bus line was not added to the collection");


        //}
        static string GetStat(out string stringnum)
        {
            stringnum = Console.ReadLine();
            int StatNum;
            if (!(stringnum.Length < 7 && int.TryParse(stringnum, out StatNum)))
            {
                do
                {
                    if (!(stringnum.Length < 7 && int.TryParse(stringnum, out StatNum)))
                    {
                        Console.WriteLine("ERROR, try enter bus station number again");
                        stringnum = Console.ReadLine();
                    }
                }
                while (!(stringnum.Length < 7 && int.TryParse(stringnum, out StatNum)));
            }

            return StatNum.ToString();
        }
        static public int GetNum()
        {
            int num = int.Parse(Console.ReadLine());
            return num;
        }
        static public bool SearchStat(string num, List<BusStation> AllStations)
        {
            bool flag = false;
            foreach (BusStation b in AllStations)
            {
                if (b.BusStationKey == num)
                {
                    flag = true;
                    return flag;
                }
            }
            return flag;
        }
        static public double GetNumDistance()
        {
            double distance = double.Parse(Console.ReadLine());
            return distance;
        }
        static public TimeSpan TravelTime(double distance)
        {
            double speed = 1;//km for miniute
            double Time = distance / speed;
            TimeSpan TimePerMin = TimeSpan.FromMinutes(Time);
            return TimePerMin;
        }
        static public bool SearchBus(BusCollection AllBuses, int num, string firstStat, string lastStat)
        {
            bool flag = false;
            foreach (BusLine b in AllBuses)
            {

                if (b.BusNumber == num)
                {

                    if ((b.FirstStation.BusStationKey == firstStat) && (b.LastStation.BusStationKey == lastStat))
                        flag = true;
                        return flag;
                }
            }
            return flag;
        }
        static public BusLine ReturnBus(BusCollection AllBuses, int num, string firstStat, string lastStat)
        {

            foreach (BusLine b in AllBuses)
            {

                if (b.BusNumber == num)
                {

                    if ((b.FirstStation.BusStationKey == firstStat) && (b.LastStation.BusStationKey == lastStat))
                        return b;
                }
            }
            throw new Exception("this bus doesn't exist");
        }
        static public BusStation ReturnStation(List<BusStation> AllStations, string statnum)//נקרא לפונקציה זו רק לאחר שנדע שהתחנה המבוקשת קיימת ברשימה

        {

            foreach (BusStation b in AllStations)
            {
                if (b.BusStationKey == statnum)
                    return b;
            }
            throw new BusException("bechlal lo tagia lepo :)");
        }
        static public int GetIntNum()
        {
            bool success = true;
            int num = 1;
            do
            {
                if (!success || num != 1 || num != 0)
                    Console.WriteLine("ERROR! try enter number again");
                success = int.TryParse(Console.ReadLine(), out num);

            }
            while (!success||num!=0||num!=1);
            return num;
        }
    }

}

    
