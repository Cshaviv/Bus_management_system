using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DS;
using DO;

namespace DALObject
{
    public class DLObject : IDL//מימוש הפונקציות ב IDL
    {

        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
        #endregion

        #region Bus

        public IEnumerable<DO.Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBuses
                   select bus.Clone();
            //return DataSource.ListBuses;
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)//?
        {
            throw new NotImplementedException();
            //return DataSource.ListBuses.FindAll(predicate);
        }
        public DO.Bus GetBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum);

            if (bus != null)
                return bus.Clone();
            else
                throw new Exception();
        }
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum) != null)
                throw new Exception();
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(b => b.LicenseNum == bus.LicenseNum);
            if (busFind == null)
                throw new Exception();
            DO.Bus newBus = bus.Clone();//copy of the bus that the function got
            busFind = newBus;//update
        }
        public void UpdateBus(int licenseNum, Action<DO.Bus> update)//?
        {
            throw new NotImplementedException();
        }
        public void DeleteBus(int licenseNum)//?
        {

        }
        #endregion
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                   select station.Clone();


        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)//?
        {
            throw new NotImplementedException();
        }
        public DO.Station GetStation(int code)
        {
            DO.Station station = DataSource.ListStations.Find(b => b.Code == code);

            if (station != null)
                return station.Clone();
            else
                throw new Exception();
        }
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(b => b.Code == station.Code) != null)
                throw new Exception();
            DataSource.ListStations.Add(station.Clone());
        }
        public void UpdateStation(DO.Station station)
        {
            DO.Station stationFind = DataSource.ListStations.Find(b => b.Code == station.Code);
            if (stationFind == null)
                throw new Exception();
            DO.Station newStation = station.Clone();//copy of the station that the function got
            stationFind = newStation;//update
        }
        public void UpdateStation(int code, Action<DO.Station> update)//?
        {
            throw new NotImplementedException();
        }
        public void DeleteStation(int code)//?
        {

        }
        #endregion
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   select line.Clone();


        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)//?
        {
            throw new NotImplementedException();
        }
        public DO.Line GetLine(int lineId)
        {
            DO.Line line = DataSource.ListLines.Find(b => b.LineId == lineId);

            if (line != null)
                return line.Clone();
            else
                throw new Exception();
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(b => b.LineId == line.LineId) != null)
                throw new Exception();
            DataSource.ListLines.Add(line.Clone());
        }
        public void UpdateLines(DO.Line line)
        {
            DO.Line lineFind = DataSource.ListLines.Find(b => b.LineId == line.LineId);
            if (lineFind == null)
                throw new Exception();
            DO.Line newLine = line.Clone();//copy of the line that the function got
            lineFind = newLine;//update
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)//?
        {
            throw new NotImplementedException();
        }
        public void DeleteLine(int code)//?
        {

        }

        #endregion
        #region Trip
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            return from trip in DataSource.ListTrips
                   select trip.Clone();
        }
        public IEnumerable<Trip> GetAllTripsBy(Predicate<Trip> predicate)
        {
            throw new NotImplementedException();
        }
        public DO.Trip GetTrip(int tripId)
        {
            DO.Trip trip = DataSource.ListTrips.Find(b => b.TripId == tripId);

            if (trip != null)
                return trip.Clone();
            else
                throw new Exception();
        }
        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip tripFind = DataSource.ListTrips.Find(b => b.TripId == trip.TripId);
            if (tripFind == null)
                throw new Exception();
            DO.Trip newTrip = trip.Clone();//copy of the bus that the function got
            tripFind = newTrip;//update
        }
        public void UpdateTrip(int tripId, Action<Trip> update)
        {
            throw new NotImplementedException();
        }
        public void DeleteTrip(int tripId)//?
        {

        }
        #endregion
        #region LineTrip
        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            return from line in DataSource.ListLineTrips
                   select line.Clone();


        }
        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> predicate)//?
        {
            throw new NotImplementedException();
        }
        public DO.LineTrip GetLineTrip(int lineTripId)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrips.Find(b => b.LineTripId == lineTripId);

            if (lineTrip != null)
                return lineTrip.Clone();
            else
                throw new Exception();
        }
        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            if (DataSource.ListLineTrips.FirstOrDefault(b => b.LineTripId == lineTrip.LineTripId) != null)
                throw new Exception();
            DataSource.ListLineTrips.Add(lineTrip.Clone());
        }
        public void UpdateLineTrips(DO.LineTrip lineTrip)
        {
            DO.LineTrip lineTripFind = DataSource.ListLineTrips.Find(b => b.LineTripId == lineTrip.LineTripId);
            if (lineTripFind == null)
                throw new Exception();
            DO.LineTrip newLineTrip = lineTrip.Clone();//copy of the line that the function got
            lineTripFind = newLineTrip;//update
        }
        public void UpdateLineTrip(int lineTripId, Action<DO.LineTrip> update)//?
        {
            throw new NotImplementedException();
        }
        public void DeleteLineTrip(int code)//?
        {

        }

        #endregion
    }
}


