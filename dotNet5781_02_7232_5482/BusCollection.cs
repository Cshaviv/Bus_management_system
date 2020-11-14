using System;
using System.Collections;
using System.Collections.Generic;
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
        public void AddBus(BusLine NewBus)
        {
            CheckBus(NewBus);

            Buses.Add(NewBus);
        }
        public void CheckBus(string first, string last, int num)
        {
            foreach(BusLine b in Buses)
            {
                if(num == b.BusNumber)
                {
                    if((b.FirstStation.BusStationKey== last) &&(b.LastStation.BusStationKey== first))
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
                if (bus.BusNumber == b.BusNumber)
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
            string AllBuses = " ";
            foreach(BusLine b in Buses)
            {
                foreach(BusLineStation bus in b.Stations)
                {
                    if(bus.BusStationKey== StationKey)
                    {
                        AllBuses += b.BusNumber + " ";//  לא בדקנו אם יש כפילות של קווי אוטובוס הלוך וחזור שעוברים בתחנה המבוקשת
                    }
                }
            }
            if(AllBuses==" ")
            {
                throw new BusException("At this station no bus line passes");
            }
            return AllBuses;
        }
        public List<BusLine> SortedList()
        {
            List<BusLine> list1 = new List<BusLine>(Buses);
            list1.Sort();
            return list1;
                
        }
        public string Indexer(int busNumber)
        {
            foreach (BusLine b in Buses)
            {
                if(b.BusNumber== busNumber)
                {
                    return b.ToString();
                }
            }
            throw new BusException("this bus doesn't exist");
        }
        public void search(int num)
        {
            foreach (BusLine b in Buses)
            {
                if(b.BusNumber==num)
                {
                    return;
                }
            }
            throw new BusException("This bus doesn't exist");
        }
    }
}   

