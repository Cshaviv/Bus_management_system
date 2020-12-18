using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_7232_5482
{
    public static class Buses
    {
        public static void RestartBuses(ObservableCollection<Bus> BusesCollection)
        {
            BusesCollection.Add(new Bus(1234567, new DateTime(2016, 03, 04), new DateTime(2020, 03, 04), 20000, 1000, 500,STATUS.Available)); // bus 1
            BusesCollection.Add(new Bus(22222222, new DateTime(2019, 01, 04), new DateTime(2019, 01, 11), 30000, 500, 500));//need treat soon (עברה כמעט שנה)
            BusesCollection.Add(new Bus(3434343, new DateTime(2017, 12, 10), new DateTime(2020, 12, 04), 15000, 50, 1190));//need refueling soon (נסע כמעט 1200 ק"מ)
            BusesCollection.Add(new Bus(6668888, new DateTime(2016, 03, 04), new DateTime(2017, 03, 04), 10000, 1000, 500));
            BusesCollection.Add(new Bus(12345678, new DateTime(2018, 03, 04), new DateTime(2018, 04, 05), 1978, 1067, 543));
            BusesCollection.Add(new Bus(12345679, new DateTime(2020, 03, 04), new DateTime(2020, 09, 04), 12345, 1020, 500));
            BusesCollection.Add(new Bus(1234967, new DateTime(2016, 10, 15), new DateTime(2017, 03, 04), 10000, 19800, 480));//need treat soon, almost road 20000 km from last treat
            BusesCollection.Add(new Bus(11223344, new DateTime(2020, 03, 09), new DateTime(2020, 11, 01), 10000, 1000, 500));
            BusesCollection.Add(new Bus(11284666, new DateTime(2018, 06, 12), new DateTime(2020, 05, 01), 10000, 1000, 500));
            BusesCollection.Add(new Bus(1239997, new DateTime(2016, 03, 04), new DateTime(2017, 03, 04), 10000, 1000, 500));
        }
            
    }
}
