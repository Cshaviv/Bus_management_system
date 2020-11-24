//Ayala Israeli 324207232, Chagit Shaviv 322805482
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_7232_5482
{


    class BusCollection : IEnumerable
    {
        public List<BusLine> Buses { get; set; }//list of buses
        public IEnumerator GetEnumerator()
        {
            return Buses.GetEnumerator();
        }
        public BusCollection(/*List<BusLine> Buses*/)
        {
            Buses = new List<BusLine>();
        }
        public void AddBus(BusLine NewBus)//add bus
        {
            CheckBus(NewBus);
            Buses.Add(NewBus);
            return;
        }
        public void CheckBus(BusLine newbus)//check if bus is unside bus line 
        {
            foreach (BusLine b in Buses)
            {
                if ((newbus.BusNumber == b.BusNumber) && (newbus.Area == b.Area))
                {
                    if ((b.FirstStation.BusStationKey == newbus.LastStation.BusStationKey) && (b.LastStation.BusStationKey == newbus.FirstStation.BusStationKey))
                    {
                        return;
                    }
                    throw new BusException("this bus line already exist");
                }
            }
            return;
        }
        public void RemoveBus(BusLine bus)//the func remove bas from list of buses
        {
            foreach (BusLine b in Buses)
            {
                if ((bus.BusNumber == b.BusNumber) && (bus.Area == b.Area))
                {
                    if ((b.FirstStation == bus.FirstStation) && (b.LastStation == bus.LastStation))
                    {
                        Buses.Remove(bus);
                        return;
                    }
                }
            }
            throw new BusException("this bus line dosn't exist");
        }
        public string stations(string StationKey)//the func check which bus line go throught the station.
        {
            string AllBuses_ = null;
            foreach (BusLine b in Buses)
            {
                foreach (BusLineStation bus in b.Stations)
                {
                    if (bus.BusStationKey == StationKey)
                    {
                        AllBuses_ += b.BusNumber + ", ";
                    }
                }
            }
            if (AllBuses_ == " ")
            {
                throw new BusException("At this station no bus line passes");
            }
            return AllBuses_;
        }
        public int SearchBus(int num, string firstStat, string lastStat)//check if bus line exist in list of buses, the func returen count(index)
        {
            int count = 0;
            foreach (BusLine b in Buses)
            {

                if (b.BusNumber == num)
                {
                    if ((b.FirstStation.BusStationKey == firstStat) && (b.LastStation.BusStationKey == lastStat))
                    {
                        return count;
                    }

                }
                count++;
            }
            return -1;
        }
        public BusLine this[int numbus, string firstStat, string lastStat]
        {
            get
            {
                if (SearchBus(numbus, firstStat, lastStat) == -1)
                    throw new BusException("ERROR! this bus line doesn't exist");
                return Buses[SearchBus(numbus, firstStat, lastStat)];
            }
            set
            {
                if (SearchBus(numbus, firstStat, lastStat) == -1)
                    throw new BusException("ERROR! this bus line doesn't exist");
                Buses[SearchBus(numbus, firstStat, lastStat)] = value;
            }

        }
        public void search(int num)//search bus in list( bus line)
        {
            foreach (BusLine b in Buses)
            {
                if (b.BusNumber == num)
                {
                    return;
                }
            }
            throw new BusException("This bus doesn't exist");
        }
        public List<BusLine> PassTheStation(string stationNum)//check if station exist in any bus line
        {
            List<BusLine> busLines = new List<BusLine>();
            BusLineStation station = new BusLineStation(stationNum, " ", 0);
            foreach (BusLine b in Buses)
            {
                if (b.SearchStat(station.BusStationKey) != -1)                
                    busLines.Add(b);              
            }
            if (busLines.Count == 0)
                throw new BusException("The station doesn't exist on any bus line");
            return busLines;
        }
        public List<BusLine> SortLines()//the func sorted buses
        {
            List<BusLine> buses = Buses;
            buses.Sort();
            return buses;
        }
    }
}





