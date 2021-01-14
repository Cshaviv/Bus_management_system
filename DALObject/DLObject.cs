using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLAPI;
using DS;
using DO;


namespace DL
{
    public class DLObject : IDL//מימוש הפונקציות ב IDL
    {
        #region singelton
        static readonly DLObject instance = new DLObject();
        static DLObject() { }// static ctor to ensure instance init is done just before first usage
        DLObject() { } // default => private
        public static DLObject Instance { get => instance; }// The public Instance property to use
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
        public DO.Bus GetBus(int licenseNumber)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNumber && b.IsDeleted == false);

            if (bus != null)
                return bus.Clone();
            else
                throw new BadLicenseNumException(licenseNumber, "The bus does not exist");
        }
        public void AddBus(DO.Bus bus)
        {
            if (DataSource.ListBuses.FirstOrDefault(bus_ => bus_.LicenseNum == bus.LicenseNum && bus_.IsDeleted == false) != null)
                throw new BadInputException("The bus is already exist");           
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == bus.LicenseNum && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(bus.LicenseNum, "The bus does not exist");
            DataSource.ListBuses.Remove(busFind);
            DataSource.ListBuses.Add(bus.Clone());
        }
        public void UpdateBus(int licenseNumber, Action<DO.Bus> update)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == licenseNumber && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(licenseNumber, "The bus does not exist");
            update(busFind);
        }
        public void DeleteBus(int licenseNumber)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == licenseNumber && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(licenseNumber, "The bus does not exist");
            busFind.IsDeleted = true;//delete

        }
        #endregion

        #region AdjacentStations
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from adjStations in DataSource.ListAdjacentStations
                   select adjStations.Clone();
        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            return from adjStations in DataSource.ListAdjacentStations
                   where predicate(adjStations)
                   select adjStations.Clone();
        }
        public DO.AdjacentStations GetAdjacentStations(int code1, int code2)
        {
            DO.AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(s => s.StationCode1 == code1 && s.StationCode2 == code2);

            if (adjacentStations != null)
                return adjacentStations.Clone();
            else
                throw new DO.BadLicenseNumException(code1, $"bad stations number: {code1}");
        }
        public void AddAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(adjStations => (adjStations.StationCode1 == adjacentStations.StationCode1 && adjStations.StationCode2 == adjacentStations.StationCode2 && adjStations.IsDeleted == false)) != null)//if those adjacent stations already exist in the list
                throw new Exception();
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations adjacentStations1 = DataSource.ListAdjacentStations.Find(s => s.StationCode1 == adjacentStations.StationCode1 && s.StationCode2 == adjacentStations.StationCode2);

            if (adjacentStations != null)
            {
                DataSource.ListAdjacentStations.Remove(adjacentStations1);
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
            }
            else
                throw new DO.BadLicenseNumException(adjacentStations.StationCode1, $"bad person id: {adjacentStations.StationCode1}");

        }
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adjStations => (adjStations.StationCode1 == stationCode1 && adjStations.StationCode2 == stationCode2 && adjStations.IsDeleted == false));
            if (adjStationsFind == null)
                throw new Exception();
            update(adjStationsFind);
        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adjStations => (adjStations.StationCode1 == stationCode1 && adjStations.StationCode2 == stationCode2 && adjStations.IsDeleted == false));
            if (adjStationsFind == null)
                throw new Exception();
            adjStationsFind.IsDeleted = true;
        }
        public bool ExistAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adjStations => (adjStations.StationCode1 == stationCode1 && adjStations.StationCode2 == stationCode2 && adjStations.IsDeleted == false));
            if (adjStationsFind != null)
                return true;
            return false;
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
            DO.Line lineFind = DataSource.ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);

            if (lineFind != null)
                return lineFind.Clone();
            else
                throw new BadLineIdException(lineId, "The Line ID does not exist");
        } 
        public void AddLine(DO.Line line)
        {
            line.LineId = Config.LineId++;
            if (DataSource.ListLines.FirstOrDefault(_line => _line.LineId == line.LineId && _line.IsDeleted == false) != null)
                throw new BadLineIdException(line.LineId, "The Line ID is already  exist");
            DataSource.ListLines.Add(line.Clone());
        }
        public void UpdateLine(DO.Line line)
        {
            DO.Line lineFind = DataSource.ListLines.Find(_line => _line.LineId == line.LineId && _line.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(line.LineId, "The Line ID does not exist");
            DO.Line newLine = line.Clone();//copy of the line that the function got
            DataSource.ListLines.Remove(lineFind);
            DataSource.ListLines.Add(newLine);
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)
        {
            DO.Line lineFind = DataSource.ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(lineId, "The Line ID does not exist");
            update(lineFind);
        }
        public void DeleteLine(int lineId)
        {
            DO.Line lineFind = DataSource.ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(lineId, "The Line ID does not exist");
            lineFind.IsDeleted = true;
            foreach (DO.LineStation s in DataSource.ListLineStations)
            {
                if (s.LineId == lineId && s.IsDeleted == false)
                    s.IsDeleted = true;
            }
            // DataSource.ListLineStations.RemoveAll(ls => ls.LineId == lineId);
        }
        #endregion

        #region LineStation
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            return from lineStation in DataSource.ListLineStations
                   select lineStation.Clone();
        }
        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            //return from lineStation in DataSource.ListLineStations
            //       where predicate(lineStation)
            //       select lineStation.Clone();
            return from sil in DataSource.ListLineStations
                   where predicate(sil)
                   orderby sil.LineStationIndex
                   select sil;
        }//new
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            DO.LineStation lineStationFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode));

            if (lineStationFind != null)
                return lineStationFind.Clone();
            else
                throw new Exception();
        }
        public void AddLineStation(DO.LineStation lineStation)
        {
            if (DataSource.ListLineStations.FirstOrDefault(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.StationCode == lineStation.StationCode && lineStat.IsDeleted == false)) != null)//if this line station already exists in the list
                throw new Exception();
           // update the line station index of all the next station
            DO.LineStation next = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == lineStation.LineStationIndex && lineStat.IsDeleted == false));
            DO.LineStation temp;
            int index;
            while (next != null)
            {

                temp = next;
                index = next.LineStationIndex + 1;
                temp = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == index && lineStat.IsDeleted == false));
                ++next.LineStationIndex;
                next = temp;
            }

            //update prev and next
            DO.LineStation prev = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == lineStation.LineStationIndex - 1 && lineStat.IsDeleted == false));
            next = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == lineStation.LineStationIndex + 1 && lineStat.IsDeleted == false));
            if (prev != null)
            {
                prev.NextStationCode = lineStation.StationCode;
                lineStation.PrevStationCode = prev.StationCode;
            }
            if (next != null)
            {
                lineStation.NextStationCode = next.StationCode;
                next.PrevStationCode = lineStation.StationCode;
            }
            DataSource.ListLineStations.Add(lineStation.Clone());
        }
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            DO.LineStation lineStationFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.StationCode == lineStation.StationCode && lineStat.IsDeleted == false));
            if (lineStationFind == null)
                throw new Exception();
            DO.LineStation newAdj = lineStation.Clone();//copy of the line station that the function got
            lineStationFind = newAdj;//update
        }
        public void UpdateLineStation(int lineId, int stationCode, Action<DO.LineStation> update)
        {
            DO.LineStation lineStationFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode && lineStat.IsDeleted == false));
            if (lineStationFind == null)
                throw new Exception();
            update(lineStationFind);
        }
        public void DeleteLineStation(int lineId, int stationCode)
        {
            DO.LineStation lineStationFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode && lineStat.IsDeleted == false));
            if (lineStationFind == null)
                throw new Exception();
            lineStationFind.IsDeleted = true;
            DO.LineStation NextFind;
            if (lineStationFind.LineStationIndex > 1)
            {
                DO.LineStation PrevFind = DataSource.ListLineStations.Find(prevLineStat => (prevLineStat.LineId == lineId && prevLineStat.LineStationIndex == lineStationFind.LineStationIndex - 1 && prevLineStat.IsDeleted == false));
                NextFind = DataSource.ListLineStations.Find(next => (next.LineId == lineId && next.LineStationIndex == lineStationFind.LineStationIndex + 1 && next.IsDeleted == false));
                if (NextFind != null)//if its not the last station
                {
                    PrevFind.NextStationCode = NextFind.StationCode;
                    NextFind.PrevStationCode = PrevFind.StationCode;
                }
            }
            else
            {
                NextFind = DataSource.ListLineStations.Find(nextLineStat => (nextLineStat.LineId == lineId && nextLineStat.LineStationIndex == lineStationFind.LineStationIndex + 1 && nextLineStat.IsDeleted == false));
                if (NextFind != null)
                {
                    NextFind.PrevStationCode = 0;
                }
            }
            int index;
            while (NextFind != null)
            {
                index = NextFind.LineStationIndex;
                NextFind.LineStationIndex = NextFind.LineStationIndex - 1;
                NextFind = DataSource.ListLineStations.Find(next => (next.LineId == lineId && next.LineStationIndex == index + 1 && next.IsDeleted == false));
            }
        }
        public IEnumerable<DO.Line> GetLinesInStationList(Predicate<DO.LineStation> predicate)
        {
            return from sil in DataSource.ListLineStations
                   where predicate(sil)
                   select GetLine(sil.LineId);
        }



        #endregion
        
        #region Station
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                       //where station.IsDeleted == false
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
              DataSource.ListStations.Remove(stationFind);//delete the station without update details
            DataSource.ListStations.Add(station.Clone());// add the station with update details
        }
        public void UpdateStation(int code, Action<DO.Station> update)//?
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);
            if (stationFind == null)
                throw new BadStationCodeException(code, "The station does not exist");
            update(stationFind);
        }
        public void DeleteStation(int code)
        {
            DO.Station statFind = DataSource.ListStations.FirstOrDefault(s => s.Code == code && s.IsDeleted == false);// chrck if station exist in list station
            if (statFind == null)
                throw new BadStationCodeException(code, "The station does not exist");
            statFind.IsDeleted = true;
            foreach (DO.AdjacentStations stat in DataSource.ListAdjacentStations)//delete from adjacent Station
            {
                if ((stat.StationCode1 == code || stat.StationCode2 == code) && stat.IsDeleted == false)
                    stat.IsDeleted = true;
            }
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
            DO.LineTrip lineTripFind = DataSource.ListLineTrips.Find(lTrip => lTrip.LineTripId == lineTripId && lTrip.IsDeleted == false);

            if (lineTripFind != null)
                return lineTripFind.Clone();
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

            DO.LineTrip lineTripFind = DataSource.ListLineTrips.Find(lTrip => lTrip.LineTripId == lineTripId && lTrip.IsDeleted == false);
            if (lineTripFind == null)
                throw new Exception();
            lineTripFind.IsDeleted = true;
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
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName);

            if (userFind == null)
                throw new Exception();
            else
                return userFind.Clone();
        }
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(_user => _user.UserName == user.UserName ) != null)
                throw new Exception();
            DataSource.ListUsers.Add(user.Clone());
        }
        public void UpdateUser(DO.User user)
        {
            DO.User userFind = DataSource.ListUsers.Find(_user => _user.UserName == user.UserName );
            if (userFind == null)
                throw new Exception();
            DO.User newUser = user.Clone();//copy of the bus that the function got
            userFind = newUser;//update
        }
        public void UpdateUser(string userName, Action<DO.User> update)
        {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName );
            if (userFind == null)
                throw new Exception();
            update(userFind);
        }
        public void DeleteUser(string userName)
        {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName);

            if (userFind == null)
                throw new Exception();
            DataSource.ListUsers.Remove(userFind);
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
            if (DataSource.ListTrips.FirstOrDefault(_trip => _trip.TripId == trip.TripId && _trip.IsDeleted == false) != null)
                throw new Exception();
            DataSource.ListTrips.Add(trip.Clone());
        }
        public void UpdateTrip(DO.Trip trip)
        {
            DO.Trip tripFind = DataSource.ListTrips.Find(_trip => _trip.TripId == trip.TripId && _trip.IsDeleted == false);
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


        public void AddStationInLine(int stationID, int busID, int index)//new
        {
            if (DataSource.ListLineStations.FirstOrDefault(sil => (sil.StationCode == stationID && sil.LineId == busID)) != null)
                throw new DO.BadStationCodeException(stationID, "station ID is already registered to line ID");
            DO.LineStation lineStation = new DO.LineStation() { StationCode = stationID, LineId = busID, LineStationIndex = index };
            foreach (DO.LineStation s in GetAllLineStationsBy(s => s.LineId == busID))
                if (s.LineStationIndex >= index)
                    s.LineStationIndex++;
            DataSource.ListLineStations.Add(lineStation);
        }
        public IEnumerable<DO.LineStation> GetStationInLineList(Predicate<DO.LineStation> predicate)
        {
            return from sil in DataSource.ListLineStations
                   where predicate(sil)
                   orderby sil.LineStationIndex
                   select sil;

        }
        public void DeleteStationInLine(int busID , int stationID)
        {
            DO.LineStation lineStation = DataSource.ListLineStations.Find(sil => (sil.StationCode == stationID && sil.LineId == busID));
            int index = lineStation.LineStationIndex;
            if (lineStation != null)
            {
                DataSource.ListLineStations.Remove(lineStation);
                foreach (DO.LineStation s in GetStationInLineList(s => s.LineId == busID))
                {
                    if (s.LineStationIndex > index)
                        s.LineStationIndex--;
                }
            }
            else
                throw new DO.BadStationCodeException(stationID, "station ID is NOT registered to bus ID");
        }
    }
} 