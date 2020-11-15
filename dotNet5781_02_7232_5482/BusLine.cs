using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{

    class BusLine : IComparable<BusLine>
    {
        public int BusNumber { get; set; }
        private List<BusLineStation> stations;
        public List<BusLineStation> Stations
        {
            get { return stations; }
            set { stations = value; }
        }
        public Area Area { get; set; }
        public BusLineStation FirstStation { get => Stations[0]; set => Stations[0] = value; }
        public BusLineStation LastStation { get => Stations[stations.Count - 1]; set => Stations[stations.Count - 1] = value; }
        public BusLineStation this[int index] => stations[index];
        public BusLine(List<BusLineStation> L, int BusNumber, BusLineStation first, BusLineStation last, Area a)
        {
            this.Stations = L;
            this.BusNumber = BusNumber;
            this.FirstStation = first;
            this.LastStation = last;
            this.Stations[0] = FirstStation;
            this.Stations[Stations.Count - 1] = LastStation;
            this.Area = a;
        }
        public int FindIndex(string PrevStation)
        {
            int index = 0;
            foreach (BusLineStation b in Stations)
            {

                if (b.BusStationKey == PrevStation)
                {
                    return index;
                }
                index++;
            }
            index = -1;
            return index;
        }
        public void CheckStationExist(BusLineStation bus)
        {
            foreach (BusLineStation b in Stations)
            {

                if (b.BusStationKey == bus.BusStationKey)
                {
                    throw new BusException("the previous station entered exist");
                }
            }
            return;

        }
        public void AddStation(BusLineStation b, Insert Choice)
        {
            CheckStationExist(b);//צריך לעשות חריגה ביציאה מהפונקציה הזאת
            if (Choice == Insert.FIRST)
            {
                Stations.Add(b);
                FirstStation = b;
                return;
            }
            if (Choice == Insert.MIDDLE)
            {
                Console.WriteLine("Enter the code of the station before the station you want to add");
                int PrevStation;
                if (!int.TryParse(Console.ReadLine(), out PrevStation))
                    throw new BusException("Error, InvalidCastException output");
                int index = FindIndex(PrevStation.ToString());
                if (index == -1)
                {
                    throw new BusException("the previous station entered doesn't exist");
                }
                stations.Insert(++index, b);
                return;
            }
            else
            {
                stations.Insert(stations.Count - 1, b);
                LastStation = b;
                return;
            }

        }
        public double timeBetween(BusLineStation one, BusLineStation two)
        {
            return one.Time.Subtract(two.Time).TotalMinutes;
        }
        public void DeleteStation(BusLineStation bus)
        {
            int index = 0;
            foreach (BusLineStation b in Stations)
            {
                if (b.BusStationKey == bus.BusStationKey)
                {
                    Stations.RemoveAt(index);
                    return;
                }
                index++;
            }
            return;
        }
        public bool CheckStationInBusLIne(string StationKey)
        {
            bool flag = false;
            for (int i = 0; i < this.stations.Count; i++)
            {
                if (this.stations[i].BusStationKey == StationKey)
                {
                    flag = true;
                    return flag;
                }
            }
            return flag;
        }
        public double DistanceBetweenStations(BusLineStation stat1, BusLineStation stat2)
        {
            int index1 = FindIndex(stat1.BusStationKey);
            int index2 = FindIndex(stat2.BusStationKey);
            if (index1 == -1 || index2 == -1)
            {
                throw new BusException("the previous station entered doesn't exist on the route of this line bus");
            }
            double distance = 0;
            for (int i = index1 + 1; i < index2 + 1; i++)
            {
                distance = distance + Stations[i].My_Distance;
            }
            return distance;

        }
        public TimeSpan TimeBetweenStations(BusLineStation stat1, BusLineStation stat2)
        {
            int index1 = FindIndex(stat1.BusStationKey);
            int index2 = FindIndex(stat2.BusStationKey);
            if (index1 == -1 || index2 == -1)
            {
                throw new BusException(" the ststion doesn't exist on the route of this bus line");
            }
            TimeSpan time = TimeSpan.Zero;
            for (int i = index1 + 1; i < index2 + 1; i++)
            {
                time = time + Stations[i].My_Time;
            }
            return time;

        }
        public BusLine SubPath(BusLineStation first, BusLineStation last)
        {
            List<BusLineStation> UserStations = new List<BusLineStation>();
            BusLine bus = new BusLine(UserStations, this.BusNumber, first, last, this.Area);
            int firstIndex = FindIndex(first.BusStationKey);
            int lastIndex = FindIndex(last.BusStationKey);
            if (firstIndex == -1 || lastIndex == -1)
            {
                throw new BusException(" one of the  ststion doesn't exist on the route of this bus line");
            }
            for (int i = firstIndex + 1; i < lastIndex + 1; i++)
            {
                bus.Stations.Add(Stations[i]);
            }
            return bus;
        }
        public string PrintStations()
        {
            string station = " ";
            foreach (BusLineStation b in Stations)
            {
                station += b.BusStationKey + " ";
            }
            return station;
        }
        public override string ToString()
        {


            return String.Format(" Bus Number: {0}, Area: {1}, ListOfStation:{2}", BusNumber, Area, PrintStations());

        }
        public int CompareTo(BusLine other)
        {
            TimeSpan time1 = TimeBetweenStations(this.FirstStation, this.LastStation);
            TimeSpan time2 = TimeBetweenStations(other.FirstStation, other.LastStation);
            return time1.CompareTo(time2);
        }
        public BusLine(int busnum, int firststat, int laststat)
        {
            this.BusNumber = busnum;
            this.FirstStation.BusStationKey = firststat.ToString();
            this.LastStation.BusStationKey = laststat.ToString();
        }
    }
}



