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

        private List<BusLineStation> stations = new List<BusLineStation>();

        public List<BusLineStation> Stations
        {
            get
            {
                return stations;
            }
        }
        public Area Area { get; set; }

        public BusLineStation FirstStation { get => Stations[0]; set => Stations[0] = value; }
        public BusLineStation LastStation { get => Stations[stations.Count - 1]; set => Stations[stations.Count - 1] = value; }
        public BusLineStation this[int index] => stations[index];
        public void AddFirst(BusLineStation b)
        {
            Stations.Insert(0, b);
            FirstStation = Stations[0];
        }
        public void AddLast(BusLineStation b)
        {
            Stations.Add(b);
            LastStation = Stations[Stations.Count - 1];
        }
        public void Add(int index, BusLineStation b)
        {
            if (index == 0)
            {
                AddFirst(b);
            }
            else
            {
                if (index > Stations.Count)
                {
                    throw new ArgumentOutOfRangeException("index", "index should be less than or equal to" + busstations.Count);
                }
                if (index == Stations.Count)
                {
                    Stations.Insert(index, b);
                    LastStation = Stations[Stations.Count - 1];
                }
            }
        }
        public override string ToString()
        {
            PrintStations();
            return String.Format(" Number: {0}, Area: {1}", BusNumber, Area );

        }
        public void PrintStations()
        {
            foreach (BusLineStation b in Stations)
            {
                Console.WriteLine(b.BusStationKey);
            }
        }
        public double timeBetween(BusLineStation one, BusLineStation two)
        {
            return one.Time.Subtract(two.Time).TotalMinutes;
        }
        public int CompareTo(BusLine other)
        {
            double mytotal = totalTime();
            double othertotal = other.totalTime();

            return mytotal.CompareTo(othertotal);
        }

        private double totalTime()
        {
            double total = 0;
            for (int i = 0; i < Stations.Count - 1; i++)
            {
                total += timeBetween(Stations[i], Stations[i + 1]);
            }

            return total;
        }
    }
}
