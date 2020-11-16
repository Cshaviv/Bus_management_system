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
                        Delete_(AllBuses);
                        break;
                    case CHOICE.FIND:
                        Find();
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
                int choice = GetIntNum1_2();
                if (choice == 1)
                    AddNewBus(AllStations, AllBuses);
                else if (choice == 2)
                {
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
            List<BusLineStation> BusStations = new List<BusLineStation>();
            BusLineStation FirstStation = new BusLineStation(firstnum, " ", 0, TimeSpan.Zero);
            FirstStation.Latitude = FirstStat.Latitude;
            FirstStation.Longitude = FirstStat.Longitude;
            Console.WriteLine("Type the distance of the last station from the first station (km)");
            double distanceFromPrev = GetDoubleNum();
            TimeSpan TimeFromPrev = TimeSpan.FromMinutes(distanceFromPrev);
            BusLineStation LastStation = new BusLineStation(lastnum, " ", distanceFromPrev, TimeFromPrev);
            LastStation.Latitude = LastStat.Latitude;
            LastStation.Longitude = LastStat.Longitude;
            BusLine NuwBus = new BusLine(BusStations, busnum, FirstStation, LastStation);
            Console.WriteLine("Enter 1 if you want to add another station, and 0 to continue ");
            int choose = GetIntNum();
            if (choose == 1)
            {
                AddStation(NuwBus, AllStations);
            }
            AllBuses.AddBus(NuwBus);
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
            AddStation(bus, AllStations)
                /*bus.AddStations()*/;
        }
        static public void AddStation(BusLine bus, List<BusStation> AllStations)
        {
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

                Console.WriteLine("Enter 1 if you want to add another station, and 0 to exit ");
                choose = GetIntNum();

            }
            while (choose == 1);
        }
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
            throw new BusException("Sorry, this station doesn't exist");
        }
        static public int GetIntNum()
        {
            bool success = true;
            int num = 1;
            do
            {
                if (!success || (num != 1 && num != 0))
                    Console.WriteLine("ERROR! try enter number again");
                success = int.TryParse(Console.ReadLine(), out num);

            }
            while (!success || (num != 0 && num != 1));
            return num;
        }
        static public int GetIntNum1_2()
        {
            bool success = true;
            int num = 1;
            do
            {
                if (!success || (num != 1 && num !=2))
                    Console.WriteLine("ERROR! try enter number again");
                success = int.TryParse(Console.ReadLine(), out num);

            }
            while (!success || (num != 2 && num != 1));
            return num;
        }
        static public double GetDoubleNum()
        {
            bool success = true;
            double num;
            do
            {
                if (!success)
                    Console.WriteLine("ERROR! try enter number again");
                success = double.TryParse(Console.ReadLine(), out num);
            }
            while (!success);
            return num;
        }
        static public void Delete_(BusCollection AllBuses)
        {
            Console.WriteLine("Enter 1 if you want to delete bus line,Enter 2 if you want to delete station from bus line");
            int choice = GetIntNum1_2();
            if (choice == 1)
            {
                DeleteBus(AllBuses);
            }
            if (choice == 2)
            {
                DeleteStat( AllBuses);
            }
        }
        static public void DeleteBus(BusCollection AllBuses)
        {

            Console.WriteLine("Enter the bus number which you want to delete");
            int busNum = GetNum();
            Console.WriteLine("Enter the first station number");
            string firstStat = GetStat(out firstStat);
            Console.WriteLine("Enter the last station number");
            string lastStat = GetStat(out lastStat);
            BusLine bus = ReturnBus(AllBuses, busNum, firstStat, lastStat);
            AllBuses.RemoveBus(bus);
            Console.WriteLine("The bus line was successfully removed");
        }
        static public void DeleteStat(BusCollection AllBuses)
        {
            Console.WriteLine("Enter the line number from which you want to delete a station");
            int busNum = GetNum();
            Console.WriteLine("Enter the first station number");
            string firstStat = GetStat(out firstStat);
            Console.WriteLine("Enter the last station number");
            string lastStat = GetStat(out lastStat);
            BusLine bus = ReturnBus(AllBuses, busNum, firstStat, lastStat);
            Console.WriteLine("Enter the station number which you want to delete");
            string stat = GetStat(out stat);
            bus.DeleteStation(stat);
        }
        static public void Find(BusCollection AllBuses, List<BusStation> AllStations)
        {
            Console.WriteLine("Enter 1 to search for lines that pass through the station, enter 2 to print the options for traveling between 2 stations");
            int choice = GetIntNum1_2();
            if(choice==1)
            {
                findLineInStat(AllBuses, AllStations);
            }
            if(choice==2)
            {
                FindOptionTravel();
            }
            
        }
        static public void findLineInStat(BusCollection AllBuses, List<BusStation> AllStations)
        {
            Console.WriteLine("Enter the station number you want to know which lines go through it");
            string stat = GetStat(out stat);
            if (!SearchStat(stat, AllStations))
            {
                throw new BusException("This statin doesn't exist");
            }
            AllBuses.stations(stat);
            Console.WriteLine("The transaction completed successfully");
        }


    }

}

    
