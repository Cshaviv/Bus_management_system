using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DLAPI
{
  
        //CRUD Logic:
        // Create - add new instance
        // Request - ask for an instance or for a collection
        // Update - update properties of an instance
        // Delete - delete an instance
        public interface IDL//הצהרה  על פונקציות של הDL
        {
            #region Bus
            IEnumerable<Bus> GetAllBuses();
            IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate);
            Bus GetBus(int licenseNum);
            void AddBus(Bus bus);
            void UpdateBus(Bus bus);
            void UpdateBus(int licenseNum, Action<Bus> update); //method that knows to updt specific fields in Bus
            void DeleteBus(int licenseNum);
        #endregion
         #region Station
        IEnumerable<Station> GetAllStations();
        IEnumerable<Station> GetAllStationsBy(Predicate<Station> predicate);
        Station GetStation(int code);
        void AddStation(Station station);//?
        void UpdateStation(Station station);
        void UpdateStation(int code, Action<Station> update); //method that knows to updt specific fields in Station
        void DeleteStation(int code);
        #endregion
        #region Line
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        Line GetLine(int lineId);
        void AddLine(Line line);//?
        void UpdateLine(Line line);
        void UpdateLine(int code, Action<Bus> update); //method that knows to updt specific fields in Bus
        void DeleteLine(int code);
        #endregion
        #region Trip
        IEnumerable<Trip> GetAllTrips();
        IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> predicate);
        Trip GetTrip(int tripId);
        void AddTrip(Trip trip);//?
        void UpdateTrip(Trip trip);
        void UpdateTrip(int tripIdode, Action<Trip> update); //method that knows to updt specific fields in Trip
        void DeleteTrip(int tripId);
        #endregion
        #region LineTrip
        IEnumerable<LineTrip> GetAllLineTrips();
        IEnumerable<LineTrip> GetAllLineTripsBy(Predicate<LineTrip> predicate);
        LineTrip GetLineTrip(int lineTripId);
        void AddLineTrip(LineTrip lineTrip);//?
        void UpdateLineTrip(LineTrip lineTrip);
        void UpdateLineTrip(int lineTripId, Action<LineTrip> update); //method that knows to updt specific fields in Bus
        void DeleteLineTrip(int lineTripId);
        #endregion
    }
}


