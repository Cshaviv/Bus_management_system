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
        public List<BusLine> Buses { get; set; }
        public IEnumerator GetEnumerator()
        {
            return Buses.GetEnumerator();
        }
        public BusCollection(List<BusLine> Buses)
        {
            Buses = new List<BusLine>();
        }
        public void AddBus(BusLine NewBus)
        {
            CheckBus(NewBus);
            Buses.Add(NewBus);
            return;
        }
        public void CheckBus(BusLine newbus)
        {
            foreach (BusLine b in Buses)
            {
                if ((newbus.BusNumber == b.BusNumber)&&(newbus.Area==b.Area))
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
        public void RemoveBus(BusLine bus)
        {
            foreach (BusLine b in Buses)
            {
                if ((bus.BusNumber == b.BusNumber)&&(bus.Area==b.Area))
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
        public string stations(string StationKey)
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
        public int SearchBus(int num, string firstStat, string lastStat)
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
        //public List<BusLine> SortedList()
        //{
        //    List<BusLine> list1 = new List<BusLine>(Buses);
        //    list1.Sort();
        //    return list1;

        //}
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
        public void search(int num)
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
        public List<BusLine> SortedList()
        {
            BusLine[] busLineArr = new BusLine[Buses.Count];
            Buses.CopyTo(busLineArr);//copy the buses to an arrry
            List<BusLine> sortedlist = busLineArr.ToList();//copy the array to a list
            sortedlist.Sort();//sort the list bu the travel time
            return sortedlist;//return the sorted list
        }
        public List<BusLine> this[int lineNum]
        {
            get
            {
                List<BusLine> bsl = Buses.FindAll(item => item.LineNum == lineNum);//list of all the buses with the line number that the function got
                if (bsl.Count != 0)//if there are buses with this line number
                    return bsl;
                else//if there are not buses with this line number
                    throw new BusException("There is no buses with this line number");


            }
            //set { Lines[lineNum] = value; }

        }
    }
}





