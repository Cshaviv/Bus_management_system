using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DS;
using DO;
//using DO;

namespace DL
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
                   where bus.IsDeleted == false
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
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNum && b.IsDeleted==false );

            if (bus != null)
                return bus.Clone();
            else
                throw new Exception();
        }
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(bus_ => bus_.LicenseNum == bus.LicenseNum && bus_.IsDeleted == false) != null)
                throw new BadInputException("The bus is already exists");
            if (bus.StartDate > DateTime.Now)
                throw new BadInputException("The date of start operation is not valid");
            if (bus.TotalKm < 0)
                throw new BadInputException("The total trip is not valid");
            if (bus.FuelTank < 0 || bus.FuelTank > 1200)
                throw new BadInputException("The fuel remain is not valid");
            int lengthLicNum = LengthOfLicenseNumber(bus.LicenseNum);
            if (!((lengthLicNum == 7 && bus.StartDate.Year < 2018) || (lengthLicNum == 8 && bus.StartDate.Year >= 2018)))
                throw new BadInputException("The license number and the date of start operation do not match");
            if (bus.DateLastTreat > DateTime.Now || bus.DateLastTreat < bus.StartDate)
                throw new BadInputException("The date of last treatment is not valid");
            if (bus.KmLastTreat < 0 || bus.KmLastTreat > bus.TotalKm)
                throw new BadInputException("The kilometrage of last treatment is not valid");
            DataSource.ListBuses.Add(bus.Clone());
        }
        private int LengthOfLicenseNumber(int licNum)// This function returns the number of digits in the license number
        {
            int counter = 0;
            while (licNum != 0)
            {
                licNum = licNum / 10;
                counter++;
            }
            return counter;
        }
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == bus.LicenseNum && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(bus.LicenseNum, "The bus does not exist");
            DO.Bus newBus = bus.Clone();
            busFind = newBus;//update
            //busFind.IsDeleted = true;
            //DataSource.ListBuses.Add(newBus);
        }
        public void UpdateBus(int licenseNum, Action<DO.Bus> update)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == licenseNum && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(licenseNum, "The bus does not exist");
            update(busFind);
        }
        public void DeleteBus(int licenseNum)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == licenseNum && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(licenseNum, "The bus does not exist");
            busFind.IsDeleted = true;//delete

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
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false));

            if (adjStationsFind != null)
                return adjStationsFind.Clone();
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
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == adjStations.StationCode1 && adj.StationCode2 == adjStations.StationCode2 && adj.IsDeleted == false || adj.StationCode1 == adjStations.StationCode2 && adj.StationCode2 == adjStations.StationCode1 && adj.IsDeleted == false));
            if (adjStationsFind == null)
                throw new Exception();
            DO.AdjacentStations newAdjStations = adjStations.Clone();//copy of the bus that the function got
            adjStationsFind = newAdjStations;//update
        }
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adj => ((adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false || adj.StationCode1 == stationCode2 && adj.StationCode2 == stationCode1) && adj.IsDeleted == false));
            if (adjStationsFind == null)
                throw new Exception();
            update(adjStationsFind);
        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adj => (adj.StationCode1 == stationCode1 && adj.StationCode2 == stationCode2 && adj.IsDeleted == false || adj.StationCode1 == stationCode2 && adj.StationCode2 == stationCode1 && adj.IsDeleted == false));
            if (adjStationsFind == null)
                throw new Exception();
            adjStationsFind.IsDeleted = true;
        }
        public bool AdjacentStationsExist(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(adjacentStat => (adjacentStat.StationCode1 == stationCode1 && adjacentStat.StationCode2 == stationCode2 && adjacentStat.IsDeleted == false));
            if (adjacentStations != null)
                return true;
            return false;
        }
            #endregion

        #region Station
          public IEnumerable<DO.Station> GetAllStations()
        {
            return from stat in DataSource.ListStations
                   select stat.Clone();
        }
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
        }
        public DO.Station GetStation(int code)
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);

            if (stationFind != null)
                return stationFind.Clone();
            else
                throw new BadStationCodeException(code, "The station does not exist");
        }
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(stat => stat.Code == station.Code && stat.IsDeleted == false) != null)
                throw new BadStationCodeException(station.Code, "The station is already exist");
            DataSource.ListStations.Add(station.Clone());
        }
        public void UpdateStation(DO.Station station)
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == station.Code && stat.IsDeleted == false);
            if (stationFind == null)
                throw new BadStationCodeException(stationFind.Code, "The station does not exist");
            DO.Station newStation = station.Clone();//copy of the bus that the function got
            stationFind = newStation;//update
        }
        public void UpdateStation(int code, Action<DO.Station> update)
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);
            if (stationFind == null)
                throw new BadStationCodeException(code, "The station does not exist");
            update(stationFind);
        }
        public void DeleteStation(int code)
        {
            DO.Station stationFind = DataSource.ListStations.Find(s => s.Code == code && s.IsDeleted == false);
            if (stationFind == null)
                throw new BadStationCodeException(code, "The station does not exist");
            stationFind.IsDeleted = true;
            foreach (DO.LineStation s in DataSource.ListLineStations)//delete fron the line station list
            {
                if (s.StationCode == code && s.IsDeleted == false)
                    s.IsDeleted = true;
            }
            foreach (DO.AdjacentStations s in DataSource.ListAdjacentStations)//delete from adjacent Station list
            {
                if ((s.StationCode1 == code || s.StationCode2 == code) && s.IsDeleted == false)
                    s.IsDeleted = true;
            }
        }

        #endregion
        
        #region Line
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   where line.IsDeleted == false
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
                throw new BadLineIdException(lineId, "The Line ID does not exist");
        }
        public void AddLine(DO.Line line)
        {
            if (DataSource.ListLines.FirstOrDefault(l => l.LineId == line.LineId && l.IsDeleted == false) != null)
                throw new BadLineIdException(line.LineId, "The Line ID is already exist exist");
            DataSource.ListLines.Add(line.Clone());
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line lineFind = DataSource.ListLines.Find(l => l.LineId == line.LineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(line.LineId, "The Line ID does not exist");
            DO.Line newLine = line.Clone();//copy of the bus that the function got
            //lineFind.IsDeleted = true;
            //DataSource.ListLines.Add(newLine);
            lineFind = newLine;
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)
        {
            DO.Line lineFind = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(lineId, "The Line ID does not exist");
            update(lineFind);
        }
        public void DeleteLine(int lineId)
        {
            DO.Line lineFind = DataSource.ListLines.Find(l => l.LineId == lineId && l.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(lineId, "The Line ID does not exist");
            lineFind.IsDeleted = true;
            foreach (DO.LineStation s in DataSource.ListLineStations)//delete fron the line station list
            {
                if (s.LineId == lineId && s.IsDeleted == false)
                    s.IsDeleted = true;
            }
            foreach (DO.LineTrip l in DataSource.ListLineTrips)//delete fron line trip list
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
        
        #region Trip   
        public IEnumerable<DO.Trip> GetAllTrips()
        {
            return from trip in DataSource.ListTrips
                   select trip.Clone();
        }
        public IEnumerable<DO.Trip> GetAllTripsBy(Predicate<DO.Trip> predicate)
        {
            return from trip in DataSource.ListTrips
                   where predicate(trip)
                   select trip.Clone();

        }
        public DO.Trip GetTrip(int tripId)
        {
            DO.Trip tripFind = DataSource.ListTrips.Find(trip => trip.TripId == tripId && trip.IsDeleted == false);
            if (tripFind != null)
                return tripFind.Clone();
            else
                throw new Exception();
        }
        public void AddTrip(DO.Trip trip)
        {
            if (DataSource.ListTrips.FirstOrDefault(trip_ => trip_.TripId == trip.TripId && trip_.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListTrips.Add(trip.Clone());
        }
        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip tripFind = DataSource.ListTrips.Find(trip_ => trip_.TripId == trip.TripId && trip_.IsDeleted == false);
            if (tripFind == null)
                throw new Exception();
            DO.Trip newTrip = trip.Clone();//copy of the bus that the function got
            tripFind = newTrip;//update
        }
        public void UpdateTrip(int tripId, Action<DO.Trip> update)
        {
            DO.Trip tripFind = DataSource.ListTrips.Find(trip => trip.TripId == tripId && trip.IsDeleted == false);
            if (tripFind == null)
                throw new Exception();
            update(tripFind);
        }
        public void DeleteTrip(int tripId)
        {
            DO.Trip tripFind = DataSource.ListTrips.Find(trip => trip.TripId == tripId && trip.IsDeleted == false);
            if (tripFind == null)
                throw new Exception();
            tripFind.IsDeleted = true;
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
            DO.LineTrip lineTripFind = DataSource.ListLineTrips.Find(lTrip => lTrip.LineTripId == lineTrip.LineTripId && lTrip.IsDeleted == false);
            if (lineTripFind == null)
                throw new Exception();
            DO.LineTrip newLineTrip = lineTrip.Clone();//copy of the bus that the function got
            lineTripFind = newLineTrip;//update
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
        
        #region User
        public IEnumerable<DO.User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   select user.Clone();
        }
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
            return from user in DataSource.ListUsers
                   where predicate(user)
                   select user.Clone();
        }
        public DO.User GetUser(string userName)
        {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName && user.IsDeleted == false);

            if (userFind != null)
                return userFind.Clone();
            else
                throw new Exception();
        }
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(u => u.UserName == user.UserName && u.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListUsers.Add(user.Clone());
        }
        public void UpdateUser(DO.User user)
        {
            DO.User userFind = DataSource.ListUsers.Find(user_ => user_.UserName == user.UserName && user_.IsDeleted == false);
            if (userFind == null)
                throw new Exception();
            DO.User newUser = user.Clone();//copy of the bus that the function got
            userFind = newUser;//update
        }
        public void UpdateUser(string userName, Action<DO.User> update)
        {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName && user.IsDeleted == false);
            if (userFind == null)
                throw new Exception();
            update(userFind);
        }
        public void DeleteUser(string userName)
        {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName && user.IsDeleted == false);

            if (userFind == null)
                throw new Exception();
            userFind.IsDeleted = true;
        }

        #endregion
    }
}


