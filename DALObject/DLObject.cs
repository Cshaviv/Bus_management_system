using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DS;
using DO;
//using DO;

namespace DLObject
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
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)
        {
            return from bus in DataSource.ListBuses
                   where predicate(bus)
                   select bus.Clone();
        }
        public DO.Bus GetBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum );

            if (bus != null)
                return bus.Clone();
            else
                throw new Exception();
        }
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(b => b.LicenseNum == bus.LicenseNum && b.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(b => b.LicenseNum == bus.LicenseNum && b.IsDeleted == false);
            if (busFind == null)
                throw new Exception();
            DO.Bus newBus = bus.Clone();//copy of the bus that the function got
            busFind = newBus;//update
        }
        public void UpdateBus(int licenseNum, Action<DO.Bus> update)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted == false);
            if (busFind == null)
                throw new Exception();
            update(busFind);
        }
        public void DeleteBus(int licenseNum)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted == false);
            if (bus == null)
                throw new Exception();
            bus.IsDeleted = true;//delete

        }
        #endregion

        #region AdjacentStations
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from adj in DataSource.ListAdjacentStations
                   select adj.Clone();
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            return from adj in DataSource.ListAdjacentStations
                   where predicate(adj)
                   select adj.Clone();
        }
        public DO.AdjacentStations GetAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjStations = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false));

            if (adjStations != null)
                return adjStations.Clone();
            else
                throw new Exception();
        }
        public void AddAdjacentStations(DO.AdjacentStations adjStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(adj => (adj.StationCode1 == adjStations.StationCode1 && adj.StationCode2 == adjStations.StationCode2 && adj.IsDeleted == false)) != null)//if those adjacent stations already exist in the list
                throw new Exception();
            DataSource.ListAdjacentStations.Add(adjStations.Clone());
        }
        public void UpdateAdjacentStations(DO.AdjacentStations adjStations)
        {
            DO.AdjacentStations adjFind = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == adjStations.StationCode1 && adj.StationCode2 == adjStations.StationCode2 && adj.IsDeleted == false || adj.StationCode1 == adjStations.StationCode2 && adj.StationCode2 == adjStations.StationCode1 && adj.IsDeleted == false));
            if (adjFind == null)
                throw new Exception();
            DO.AdjacentStations newAdj = adjStations.Clone();//copy of the bus that the function got
            adjFind = newAdj;//update
        }
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {
            DO.AdjacentStations adjFind = DataSource.ListAdjacentStations.Find(adj => ((adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false || adj.StationCode1 == stationCode2 && adj.StationCode2 == stationCode1) && adj.IsDeleted == false));
            if (adjFind == null)
                throw new Exception();
            update(adjFind);
        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjFind = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false || adj.StationCode1 == stationCode2 && adj.StationCode2 == stationCode1 && adj.IsDeleted == false));
            if (adjFind == null)
                throw new Exception();
            adjFind.IsDeleted = true;
        }

        #endregion
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                   select station.Clone();
        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
        }
        public DO.Station GetStation(int code)
        {
            DO.Station station = DataSource.ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);

            if (station != null)
                return station.Clone();
            else
                throw new Exception();
        }
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(stat => stat.Code == station.Code && stat.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListStations.Add(station.Clone());
        }
        public void UpdateStation(DO.Station station)
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == station.Code && stat.IsDeleted == false);
            if (stationFind == null)
                throw new Exception();
            DO.Station newStation = station.Clone();//copy of the bus that the function got
            stationFind = newStation;//update
        }
        public void UpdateStation(int code, Action<DO.Station> update)
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);
            if (stationFind == null)
                throw new Exception();
            update(stationFind);
        }
        public void DeleteStation(int code)
        {

        }

        #endregion
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   select line.Clone();

        }
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            return from line in DataSource.ListLines
                   where predicate(line)
                   select line.Clone();

        }
        public DO.Line GetLine(int lineId)
        {
            DO.Line line = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);

            if (line != null)
                return line.Clone();
            else
                throw new Exception();
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(l => l.LineId == line.LineId && l.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListLines.Add(line.Clone());
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line lineFind = DataSource.ListLines.Find(l => l.LineId == line.LineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new Exception();
            DO.Line newLine = line.Clone();//copy of the bus that the function got
            //lineFind.IsDeleted = true;
            //DataSource.ListLines.Add(newLine);
            lineFind = newLine;
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)
        {
            DO.Line lineFind = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new Exception();
            update(lineFind);
        }
        public void DeleteLine(int lineId)
        {
            DO.Line lineFind = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new Exception();
            lineFind.IsDeleted = true;
            foreach (DO.LineStation s in DataSource.ListLineStations)
            {
                if (s.LineId == lineId && s.IsDeleted == false)
                    s.IsDeleted = true;
            }
            foreach (DO.LineTrip l in DataSource.ListLineTrips)
            {
                if (l.LineId == lineId && l.IsDeleted == false)
                    l.IsDeleted = true;
            }
        }

        #endregion
        #region LineStation
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            return from lineStat in DataSource.ListLineStations
                   select lineStat.Clone();
        }
        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            return from lineStat in DataSource.ListLineStations
                   where predicate(lineStat)
                   select lineStat.Clone();
        }
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            DO.LineStation lineStation = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode));

            if (lineStation != null)
                return lineStation.Clone();
            else
                throw new Exception();
        }
        public void AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.StationCode == lineStation.StationCode && lineStat.IsDeleted == false)) != null)//if this line station already exists in the list
                throw new Exception();
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            DO.LineStation lineStatFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.StationCode == lineStation.StationCode && lineStat.IsDeleted == false));
            if (lineStatFind == null)
                throw new Exception();
            DO.LineStation newAdj = lineStation.Clone();//copy of the bus that the function got
            lineStatFind = newAdj;//update
        }
        public void UpdateLineStation(int lineId, int stationCode, Action<DO.LineStation> update)
        {
            DO.LineStation lineStatFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode && lineStat.IsDeleted == false));
            if (lineStatFind == null)
                throw new Exception();
            update(lineStatFind);
        }
        public void DeleteLineStation(int lineId, int stationCode)
        {
            DO.LineStation lineStatFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode && lineStat.IsDeleted == false));
            if (lineStatFind == null)
                throw new Exception();
            lineStatFind.IsDeleted = true;
            DO.LineStation NextFind;
            if (lineStatFind.LineStationIndex > 1)
            {
                DO.LineStation PrevFind = DataSource.ListLineStations.Find(prevLineStat => (prevLineStat.LineId == lineId && prevLineStat.LineStationIndex == lineStatFind.LineStationIndex - 1 && prevLineStat.IsDeleted == false));
                NextFind = DataSource.ListLineStations.Find(nextLineStat => (nextLineStat.LineId == lineId && nextLineStat.LineStationIndex == lineStatFind.LineStationIndex + 1 && nextLineStat.IsDeleted == false));
                PrevFind.NextStationCode = NextFind.StationCode;
                NextFind.PrevStationCode = PrevFind.StationCode;
            }
            else
            {
                NextFind = DataSource.ListLineStations.Find(nextLineStat => (nextLineStat.LineId == lineId && nextLineStat.LineStationIndex == lineStatFind.LineStationIndex + 1 && nextLineStat.IsDeleted == false));
                //NextFind.PrevStationCode= לשנות!!!
            }
            int index;
            while (NextFind != null)
            {
                index = NextFind.LineStationIndex;
                NextFind.LineStationIndex--;
                NextFind = DataSource.ListLineStations.Find(nextLineStat => (nextLineStat.LineId == lineId && nextLineStat.LineStationIndex == index + 1 && nextLineStat.IsDeleted == false));
            }
        }



        #endregion
        #region Trip   לא המעודכן l
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
        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(t => t.TripId == trip.TripId && t.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListTrips.Add(trip.Clone());
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
            return from lineTrip in DataSource.ListLineTrips
                   select lineTrip.Clone();
        }
        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> predicate)
        {
            return from lineTrip in DataSource.ListLineTrips
                   where predicate(lineTrip)
                   select lineTrip.Clone();
        }
        public DO.LineTrip GetLineTrip(int lineTripId)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrips.Find(lTrip => lTrip.LineTripId == lineTripId && lTrip.IsDeleted == false);

            if (lineTrip != null)
                return lineTrip.Clone();
            else
                throw new Exception();
        }
        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            if (DataSource.ListLineTrips.FirstOrDefault(lTrip => lTrip.LineTripId == lineTrip.LineTripId && lTrip.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListLineTrips.Add(lineTrip.Clone());
        }
        public void UpdateLineTrip(DO.LineTrip lineTrip)
        {
            DO.LineTrip lTripFind = DataSource.ListLineTrips.Find(lTrip => lTrip.LineTripId == lineTrip.LineTripId && lTrip.IsDeleted == false);
            if (lTripFind == null)
                throw new Exception();
            DO.LineTrip newLineTrip = lineTrip.Clone();//copy of the bus that the function got
            lTripFind = newLineTrip;//update
        }
        public void UpdateLineTrip(int lineTripId, Action<DO.LineTrip> update)
        {
            DO.LineTrip lineTripFind = DataSource.ListLineTrips.Find(lTrip => lTrip.LineTripId == lineTripId && lTrip.IsDeleted == false);
            if (lineTripFind == null)
                throw new Exception();
            update(lineTripFind);
        }
        public void DeleteLineTrip(int lineTripId)
        {

            DO.LineTrip lineTrip = DataSource.ListLineTrips.Find(lTrip => lTrip.LineTripId == lineTripId && lTrip.IsDeleted == false);
            if (lineTrip == null)
                throw new Exception();
            lineTrip.IsDeleted = true;
        }

        #endregion
       
    }
}


