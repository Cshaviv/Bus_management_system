using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public class DataSource
    {
        public static List<Bus> ListBuses;
        public static List<Station> ListStations;
        public static List<User> ListUsers;
        public static List<AdjacentStations> ListAdjacentStations;
        public static List<Trip> ListTrips;
        public static List<Line> ListLines;
        public static List<LineStation> ListLineStations;
        public static List<LineTrip> ListLineTrips;

        static DataSource()
        {
            InitAllLists();
        }
        static void InitAllLists()
        {
            ListBuses = new List<Bus>() {
            // {new Bus(1234567, new DateTime(2016, 03, 04), new DateTime(2020, 03, 04), 20000, 1000, 500) }
            //,{new Bus(22222222, new DateTime(2019, 01, 04), new DateTime(2019, 12, 24), 30000, 500, 500) }
            //,{new Bus(3434343, new DateTime(2017, 12, 10), new DateTime(2020, 12, 04), 15000, 50, 1190)  }
            //,{new Bus(6668888, new DateTime(2016, 03, 04), new DateTime(2017, 03, 04), 10000, 1000, 500) }
            //,{new Bus(12345678, new DateTime(2018, 03, 04), new DateTime(2020, 04, 05), 1978, 1067, 543) }
            //,{new Bus(12345679, new DateTime(2020, 03, 04), new DateTime(2020, 09, 04), 12345, 1020, 500)}
            //,{new Bus(1234967, new DateTime(2016, 10, 15), new DateTime(2020, 03, 04), 10000, 19950, 480)}
            //,{new Bus(11223344, new DateTime(2020, 03, 09), new DateTime(2020, 11, 01), 10000, 1000, 500)}
            //,{new Bus(11284666, new DateTime(2018, 06, 12), new DateTime(2020, 05, 01), 10000, 1000, 500)}
            //,{new Bus(1239997, new DateTime(2016, 03, 04), new DateTime(2017, 03, 04), 10000, 1000, 1200) }
        };
    }
         }

}

