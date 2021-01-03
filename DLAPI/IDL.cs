using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using DO;

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
            IEnumerable<DO.Bus> GetAllBuses();
            IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate);
             DO.Bus GetBus(int licenseNum);
            void AddBus(DO.Bus bus);
            void UpdateBus(DO.Bus bus);
            void UpdateBus(int licenseNum, Action<DO.Bus> update); //method that knows to updt specific fields in Bus
            void DeleteBus(int licenseNum);
        #endregion
         #region Station
        IEnumerable<DO.Station> GetAllStations();
        IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate);
        DO.Station GetStation(int code);
        void AddStation(DO.Station station);//?
        void UpdateStation(DO.Station station);
        void UpdateStation(int code, Action<DO.Station> update); //method that knows to updt specific fields in Station
        void DeleteStation(int code);
        #endregion
        #region Line
        IEnumerable<DO.Line> GetAllLines();
        IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate);
        DO.Line GetLine(int lineId);
        void AddLine(DO.Line line);//?
        void UpdateLine(DO.Line line);
        void UpdateLine(int code, Action<DO.Line> update); //method that knows to updt specific fields in Bus
        void DeleteLine(int code);
        #endregion
        #region Trip
        IEnumerable<DO.Trip> GetAllTrips();
        IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate);
        DO.Trip GetTrip(int tripId);
        void AddTrip(DO.Trip trip);//?
        void UpdateTrip(DO.Trip trip);
        void UpdateTrip(int tripIdode, Action<DO.Trip> update); //method that knows to updt specific fields in Trip
        void DeleteTrip(int tripId);
        #endregion
        #region LineTrip
        IEnumerable<DO.LineTrip> GetAllLineTrips();
        IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> predicate);
        DO.LineTrip GetLineTrip(int lineTripId);
        void AddLineTrip(DO.LineTrip lineTrip);//?
        void UpdateLineTrip(DO.LineTrip lineTrip);
        void UpdateLineTrip(int lineTripId, Action<DO.LineTrip> update); //method that knows to updt specific fields in Bus
        void DeleteLineTrip(int lineTripId);
        #endregion
    }
}


