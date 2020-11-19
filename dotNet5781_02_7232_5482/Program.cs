//Ayala and C hagit
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
            List<BusStation> AllStations = new List<BusStation>();
            BusCollection AllBuses = new BusCollection();
            //initialize();
            CHOICE choice;
            CreatStatAndBus(ref AllStations, AllBuses);
            do
            {
                Console.WriteLine("Make your mind:");
                Console.WriteLine("Enter 1 to ADD");
                Console.WriteLine("Enter 2 to DELETE");
                Console.WriteLine("Enter 3 to FIND");
                Console.WriteLine("Enter 4 to PRINT");
                Console.WriteLine("Enter -1 to EXIT");
                bool success = Enum.TryParse(Console.ReadLine(), out choice);
                switch (choice)
                {
                    case CHOICE.ADD:
                        AddNew(ref AllStations, AllBuses);
                        break;
                    case CHOICE.DELETE:
                        Delete_(AllBuses);
                        break;
                    case CHOICE.FIND:
                        Find(AllBuses, ref AllStations);
                        break;
                    case CHOICE.PRINT:
                        Print(AllBuses, ref AllStations);
                        break;
                    case CHOICE.EXIT:
                        break;
                    default:
                        break;
                }
            } 
            while (choice != CHOICE.EXIT);
        }


        static public void CreatStatAndBus(ref List<BusStation> AllStations, BusCollection AllBuses)
        {
            int counter = 1;
            for (int i = 0; i < 40; i++)
            {
                AllStations.Add(new BusStation(counter.ToString(), " "));
                counter++;
            }
            //    AllBuses.AddBus(new BusLine( new List<BusLineStation>() { new BusLineStation(AllStations[0].BusStationKey, " ", 0), new BusLineStation(AllStations[1].BusStationKey, " ", 0.7), new BusLineStation(AllStations[2].BusStationKey, " ", 2), new BusLineStation(AllStations[3].BusStationKey, " ", 2.5) },280, Area.GENERAL));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[4].BusStationKey, " ", 0), new BusLineStation(AllStations[5].BusStationKey, " ", 4), new BusLineStation(AllStations[6].BusStationKey, " ", 2), new BusLineStation(AllStations[7].BusStationKey, " ", 3.3) }, 14, Area.CENTER));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[8].BusStationKey, " ", 0), new BusLineStation(AllStations[9].BusStationKey, " ", 2.9), new BusLineStation(AllStations[10].BusStationKey, " ", 1.87), new BusLineStation(AllStations[11].BusStationKey, " ", 3.8) }, 160, Area.JERUSALEM));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[12].BusStationKey, " ", 0), new BusLineStation(AllStations[13].BusStationKey, " ", 2.65), new BusLineStation(AllStations[14].BusStationKey, " ", 0.9), new BusLineStation(AllStations[15].BusStationKey, " ", 3.4) }, 5, Area.SOUTH));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[16].BusStationKey, " ", 0), new BusLineStation(AllStations[17].BusStationKey, " ", 1.3), new BusLineStation(AllStations[18].BusStationKey, " ", 1.9), new BusLineStation(AllStations[19].BusStationKey, " ", 2.5) }, 76, Area.GENERAL));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[20].BusStationKey, " ", 0), new BusLineStation(AllStations[21].BusStationKey, " ", 0.87), new BusLineStation(AllStations[22].BusStationKey, " ", 2), new BusLineStation(AllStations[23].BusStationKey, " ", 2.8) }, 87, Area.CENTER));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[24].BusStationKey, " ", 0), new BusLineStation(AllStations[25].BusStationKey, " ", 6), new BusLineStation(AllStations[26].BusStationKey, " ", 4.5), new BusLineStation(AllStations[27].BusStationKey, " ", 4.8) }, 48, Area.JERUSALEM));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[28].BusStationKey, " ", 0), new BusLineStation(AllStations[29].BusStationKey, " ", 4.9), new BusLineStation(AllStations[29].BusStationKey, " ", 6.2), new BusLineStation(AllStations[30].BusStationKey, " ", 5.8) }, 80, Area.JERUSALEM));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[32].BusStationKey, " ", 0), new BusLineStation(AllStations[33].BusStationKey, " ", 6), new BusLineStation(AllStations[34].BusStationKey, " ", 4.5), new BusLineStation(AllStations[35].BusStationKey, " ", 5.2) }, 32, Area.JERUSALEM));
            //    AllBuses.AddBus(new BusLine(new List<BusLineStation>() { new BusLineStation(AllStations[36].BusStationKey, " ", 0), new BusLineStation(AllStations[37].BusStationKey, " ", 6.25), new BusLineStation(AllStations[38].BusStationKey, " ", 7.4), new BusLineStation(AllStations[39].BusStationKey, " ", 8.5) }, 950, Area.JERUSALEM));
        }
        static public void AddNew(ref List<BusStation> AllStations, BusCollection AllBuses)
        {
            try
            {
                Console.WriteLine("Enter 1 if you want to add bus line,Enter 2 if you want to add station");
                int choice = GetNum();
                if (choice == 1)
                    AddNewBus(ref AllStations, AllBuses);
                else if (choice == 2)
                {
                    NewStation(AllBuses, ref AllStations);
                }
                else
                    throw new BusException("ERROR,this option doesn't exist");

            }
            catch (FormatException)
            {
                Console.WriteLine("The value must be numeric");
            }
            catch (BusException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }
        static public void AddNewBus(ref List<BusStation> AllStations, BusCollection AllBuses)
        {
            List<BusLineStation> ListStation = new List<BusLineStation>();
            Console.WriteLine("Enter a number of bus line");
            int busnum = GetNum();
            Console.WriteLine("Please enter the bus travel area");
            Area area = TheArea();
            Console.WriteLine("Enter the first station number");
            string firstnum = GetStat(out firstnum);
            Console.WriteLine("Enter the last station number");
            string lastnum = GetStat(out lastnum);
            foreach (BusLine b in AllBuses)
            {
                if ((b.BusNumber == busnum)&&(b.Area==area))
                {
                    if (!SearchBus(AllBuses, busnum, firstnum, lastnum, area))
                    {
                        throw new BusException("Sorry, this bus number already exist");
                    }
                    if(!SearchUpsideBus(AllBuses, busnum, firstnum, lastnum, area))
                    {
                        throw new BusException("Sorry, this bus number already exist");
                    }
                }
            }
            if (!SearchStat(firstnum, ref AllStations))
            {
                AllStations.Add(new BusStation(firstnum, " "));
                BusStation FirstStat = ReturnStation(ref AllStations, firstnum);
                BusLineStation FirstStation = new BusLineStation(firstnum, " ", 0);
                FirstStation.Latitude = FirstStat.Latitude;
                FirstStation.Longitude = FirstStat.Longitude;
                FirstStation.Adress();
            }
            else
            {
               BusStation FirstStat = ReturnStation(ref AllStations, firstnum);


            }
            if (!SearchStat(lastnum, ref AllStations))
            {
                AllStations.Add(new BusStation(lastnum, " "));
            }
            BusStation LastStat = ReturnStation(ref AllStations, lastnum);
            List<BusLineStation> BusStations = new List<BusLineStation>();
            
           
            Console.WriteLine("Type the distance of the last station from the first station (km)");
            double distanceFromPrev = GetDoubleNum();
            BusLineStation LastStation = new BusLineStation(lastnum, " ", distanceFromPrev);
            LastStation.Adress();
            LastStation.Latitude = LastStat.Latitude;
            LastStation.Longitude = LastStat.Longitude;
            BusStations[0] = FirstStation;
            BusStations[BusStations.Count-1] = LastStation;
            BusLine NuwBus = new BusLine(BusStations, busnum, area);
            Console.WriteLine("Enter 1 if you want to add another station, and 0 to continue ");
            int choose = GetIntNum();
            if (choose == 1)
            {
                AddStation_(NuwBus, ref AllStations);
            }
            AllBuses.AddBus(NuwBus);
            Console.WriteLine("The bus was successfully added");
            return;
        }
        static public void NewStation(BusCollection AllBuses, ref List<BusStation> AllStations)
        {
            Console.WriteLine("Enter the bus number to which you want to add a station");
            int num = GetNum();
            SearchBusNum(AllBuses, num);

            Console.WriteLine("Please enter the bus travel area");
            Area area = TheArea();
            Console.WriteLine("Enter the first station");
            string first = GetStat(out first);
            Console.WriteLine("Enter the last station");
            string last = GetStat(out last);
            if (!SearchBus(AllBuses, num, first, last, area))
            {
                throw new Exception("this bus doesn't exist");
            }
            BusLine bus = ReturnBus(AllBuses, num, first, last, area);
            AddStation_(bus, ref AllStations)
                /*bus.AddStations()*/;
            return;
        }
        static public void AddStation_(BusLine bus, ref List<BusStation> AllStations)
        {
            //do
            //{
                Console.WriteLine("Enter the station number you want to add");
                string NewStat = GetStat(out NewStat);
                if (bus.CheckStationExist(NewStat))
                    throw new BusException("This station already exist");
            if (SearchStat(NewStat, ref AllStations))
            {
                BusStation b = ReturnStation(ref AllStations, NewStat);
                bus.AddStations(b);
                Console.WriteLine("The station was seccessfully added");
            }
            else
            {

                BusStation b = new BusStation(NewStat);
                Console.WriteLine("blabla");
                AllStations.Add(b);
                bus.AddStations(b);
            }
            //    Console.WriteLine("Enter 1 if you want to add another station, and 0 to exit ");
            //    choose = GetIntNum();

            //}
            //while (choose == 1);
            return;
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
        static public bool SearchStat(string num, ref List<BusStation> AllStations)
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
        static public bool SearchBus(BusCollection AllBuses, int num, string firstStat, string lastStat,Area area)
        {
            bool flag = false;
            foreach (BusLine b in AllBuses)
            {

                if ((b.BusNumber == num)&&(b.Area ==area))
                {

                    if ((b.FirstStation.BusStationKey == firstStat) && (b.LastStation.BusStationKey == lastStat))
                    {
                        flag = true;
                        return flag;
                    }
                }
            }
            return flag;
        }
        static public bool SearchUpsideBus(BusCollection AllBuses, int num, string firstStat, string lastStat, Area area)
        {
            bool flag = false;
            foreach (BusLine b in AllBuses)
            {

                if ((b.BusNumber == num) && (b.Area == area))
                {

                    if ((b.FirstStation.BusStationKey == lastStat) && (b.LastStation.BusStationKey == firstStat))
                    {
                        flag = true;
                        return flag;
                    }
                }
            }
            return flag;
        }
        static public BusLine ReturnBus(BusCollection AllBuses, int num, string firstStat, string lastStat, Area area)
        {

            foreach (BusLine b in AllBuses)
            {

                if ((b.BusNumber == num) && (b.Area == area))
                {

                    if ((b.FirstStation.BusStationKey == firstStat) && (b.LastStation.BusStationKey == lastStat))
                        return b;
                }
            }
            throw new Exception("this bus doesn't exist");
        }
        static public BusStation ReturnStation(ref List<BusStation> AllStations, string statnum)//נקרא לפונקציה זו רק לאחר שנדע שהתחנה המבוקשת קיימת ברשימה

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
                if (!success || (num != 1 && num != 2))
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
            try
            {
                Console.WriteLine("Enter 1 if you want to delete bus line,Enter 2 if you want to delete station from bus line");
                int choice = GetNum();
                if (choice == 1)
                {
                    DeleteBus(AllBuses);
                }
                else if (choice == 2)
                {
                    DeleteStat(AllBuses);
                }
                else
                    throw new BusException("ERROR,this option doesn't exist");
            }

            catch (FormatException)
            {
                Console.WriteLine("The value must be numeric");
            }
            catch (BusException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }
        static public void DeleteBus(BusCollection AllBuses)
        {

            Console.WriteLine("Enter the bus number which you want to delete");
            int busNum = GetNum();
            SearchBusNum(AllBuses, busNum);
            Console.WriteLine("Please enter the bus travel area");
            Area area = TheArea();
            Console.WriteLine("Enter the first station number");
            string firstStat = GetStat(out firstStat);
            Console.WriteLine("Enter the last station number");
            string lastStat = GetStat(out lastStat);
            BusLine bus = ReturnBus(AllBuses, busNum, firstStat, lastStat, area);
            AllBuses.RemoveBus(bus);
            Console.WriteLine("The bus line was successfully removed");
            return;
        }
        static public void DeleteStat(BusCollection AllBuses)
        {
            Console.WriteLine("Enter the line number from which you want to delete a station");
            int busNum = GetNum();
            SearchBusNum(AllBuses, busNum);
            Console.WriteLine("Please enter the bus travel area");
            Area area = TheArea();
            Console.WriteLine("Enter the first station number");
            string firstStat = GetStat(out firstStat);
            Console.WriteLine("Enter the last station number");
            string lastStat = GetStat(out lastStat);
            BusLine bus = ReturnBus(AllBuses, busNum, firstStat, lastStat, area);
            Console.WriteLine("Enter the station number which you want to delete");
            string stat = GetStat(out stat);
            bus.DeleteStation(stat);
            return;
        }
        static public void Find(BusCollection AllBuses, ref List<BusStation> AllStations)
        {
            try
            {
                Console.WriteLine("Enter 1 to search for lines that pass through the station, enter 2 to print the options for traveling between 2 stations");
                int choice = GetNum();
                if (choice == 1)
                {
                    findLineInStat(AllBuses, ref AllStations);
                }
                else if (choice == 2)
                {
                  //  FindOptionTravel();
                }
                else
                    throw new BusException("ERROR,this option doesn't exist");
            }
            catch (FormatException)
            {
                Console.WriteLine("The value must be numeric");
            }
            catch (BusException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }
        static public void findLineInStat(BusCollection AllBuses, ref List<BusStation> AllStations)
        {
            Console.WriteLine("Enter the station number you want to know which lines go through it");
            string stat = GetStat(out stat);
            if (!SearchStat(stat, ref AllStations))
            {
                throw new BusException("This statin doesn't exist");
            }
            string buses= AllBuses.stations(stat);
            Console.WriteLine(buses);
            Console.WriteLine("The transaction completed successfully");
            return;
        }
        static public void Print(BusCollection AllBuses, ref List<BusStation> AllStations)
        {
            try
            {
                Console.WriteLine("Enter 1 to print all bus lines, enter 2 to print all the list of stations and buses passing through them");
                int choice = GetNum();
                if (choice == 1)
                {
                    foreach (BusLine b in AllBuses)
                    {
                        Console.WriteLine(b.BusNumber + " ");
                    }
                }
                if (choice == 2)
                {

                    foreach (BusStation b in AllStations)
                    {
                        Console.WriteLine(b);
                        Console.WriteLine(AllBuses.stations(b.BusStationKey));
                    }

                }
                else
                    throw new BusException("ERROR,this option doesn't exist");
            }
            catch (FormatException)
            {
                Console.WriteLine("The value must be numeric");
            }
            catch (BusException ex)
            {
                Console.WriteLine(ex.Message);
            }
            return;
        }
        static public Area TheArea()
        {
            Area area = Area.JERUSALEM;
            bool success = true;
            do
            {                            
                Console.WriteLine("1: to GENERAL");
                Console.WriteLine("2: to NORTH");
                Console.WriteLine("3: to SOUTH ");
                Console.WriteLine("4: to CENTER");
                Console.WriteLine("5: to JERUSALEM");
                success = Enum.TryParse(Console.ReadLine(), out area);
                if (!success)
                {
                    Console.WriteLine("Sorry,please enter one of the available options");
                }
            }
            while (!success);
            
            return area;

        }  
        static void SearchBusNum(BusCollection AllBuses, int busnum)
        {
            foreach (BusLine b in AllBuses)
            {
                if (b.BusNumber == busnum)
                    return;
            }
            throw new BusException("This bus doesn't exist");
        }
    }
}


    
