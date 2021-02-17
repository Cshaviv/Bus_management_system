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
        /// <summary>
        ///  A function that returns the list of buses that exist in the system
        /// </summary>
        /// <returns>buses list</returns>
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            return from bus in DataSource.ListBuses
                   where bus.IsDeleted == false
                   select bus.Clone();
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)//???
        {
            return from bus in DataSource.ListBuses
                   where predicate(bus)
                   select bus.Clone();
        }
        /// <summary>
        /// The function received a license number and returns the bus according to the license number
        /// </summary>
        /// <param name="licenseNumber">Bus license number</param>
        /// <returns>bus</returns>
        public DO.Bus GetBus(int licenseNumber)
        {
            DO.Bus bus = DataSource.ListBuses.Find(b => b.LicenseNum == licenseNumber && b.IsDeleted == false);

            if (bus != null)
                return bus.Clone();
            else
                throw new BadLicenseNumException(licenseNumber, "The bus does not exist");
        }
        /// <summary>
        /// A function that receives a bus and adds it to the system
        /// </summary>
        /// <param name="busBo"> bus</param>
        public void AddBus(DO.Bus busBo)//yes
        {
            if (DataSource.ListBuses.FirstOrDefault(bus_ => bus_.LicenseNum == busBo.LicenseNum && bus_.IsDeleted == false) != null)
                throw new BadLicenseNumException(busBo.LicenseNum,"אוטובוס זה כבר קיים במערכת");           
            DataSource.ListBuses.Add(busBo.Clone());
        }
        /// <summary>
        /// A function that recived a bus and updates the bus details
        /// </summary>
        /// <param name="bus"> bus</param>
        public void UpdateBus(DO.Bus bus)
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == bus.LicenseNum && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(bus.LicenseNum, "הקו אינו קיים במערכת");
            DataSource.ListBuses.Remove(busFind);
            DataSource.ListBuses.Add(bus.Clone());
        }//yes
        public void UpdateBus(int licenseNumber, Action<DO.Bus> update)//???
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == licenseNumber && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(licenseNumber, "The bus does not exist");
            update(busFind);
        }
        /// <summary>
        /// A function that delete bus from the system
        /// </summary>
        /// <param name="licenseNumber">Bus license number</param>
        public void DeleteBus(int licenseNumber)//yes
        {
            DO.Bus busFind = DataSource.ListBuses.Find(bus_ => bus_.LicenseNum == licenseNumber && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(licenseNumber, "הקו אינו קיים במערכת");
            busFind.IsDeleted = true;//delete

        }
        #endregion

        #region AdjacentStations
        /// <summary>
        /// A function that returns the list of Adjacent Stations that exist in the system
        /// </summary>
        /// <returns>list of Adjacent Stations</returns>
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            return from adjStations in DataSource.ListAdjacentStations
                   select adjStations.Clone();
        }
        /// <summary>
        /// A function that returns the list of Adjacent Stations that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)//???
        {
            return from adjStations in DataSource.ListAdjacentStations
                   where predicate(adjStations)
                   select adjStations.Clone();
        }
        /// <summary>
        /// The function received Station code of two consecutive stations and returns the Adjacent Stations according to the codes
        /// </summary>
        /// <param name="code1"> code of station 1</param>
        /// <param name="code2">code of station 2</param>
        /// <returns>Adjacent Stations Of the two stations</returns>
        public DO.AdjacentStations GetAdjacentStations(int code1, int code2)
        {
            DO.AdjacentStations adjacentStations = DataSource.ListAdjacentStations.Find(s => s.StationCode1 == code1 && s.StationCode2 == code2);

            if (adjacentStations != null)
                return adjacentStations.Clone();
            else
                throw new DO.BadInputException(code1, "מצטערים חסר מידע על תחנה זו");
        }//yes
        /// <summary>
        /// A function that receives a adjacent Stations and adds it to the system
        /// </summary>
        /// <param name="adjacentStations">adjacent stations</param>
        public void AddAdjacentStations(DO.AdjacentStations adjacentStations)//yes
        {
            if (DataSource.ListAdjacentStations.FirstOrDefault(adjStations => (adjStations.StationCode1 == adjacentStations.StationCode1 && adjStations.StationCode2 == adjacentStations.StationCode2 && adjStations.IsDeleted == false)) != null)//if those adjacent stations already exist in the list
                throw new DO.BadInputException("התחנות עוקבות קיימות במערכת");
            DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
        }
        /// <summary>
        ///  A function that recived a adjacent Stations and updates the adjacent Stations details
        /// </summary>
        /// <param name="adjacentStations"> </param>
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)//yes
        {
            DO.AdjacentStations adjacentStations1 = DataSource.ListAdjacentStations.Find(s => s.StationCode1 == adjacentStations.StationCode1 && s.StationCode2 == adjacentStations.StationCode2);

            if (adjacentStations != null)
            {
                DataSource.ListAdjacentStations.Remove(adjacentStations1);
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
            }
            else
                AddAdjacentStations(adjacentStations);
        }
        /// <summary>
        ///  A function that recived a adjacent Stations and updates the adjacent Stations details
        /// </summary>
        /// <param name="stationCode1"></param>
        /// <param name="stationCode2"></param>
        /// <param name="update"></param>
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adjStations => (adjStations.StationCode1 == stationCode1 && adjStations.StationCode2 == stationCode2 && adjStations.IsDeleted == false));
            if (adjStationsFind == null)
                throw new Exception();
            update(adjStationsFind);
        }
        /// <summary>
        /// A function that delete information from the the system regarding the two following stations
        /// </summary>
        /// <param name="stationCode1"></param>
        /// <param name="stationCode2"></param>
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adjStations => (adjStations.StationCode1 == stationCode1 && adjStations.StationCode2 == stationCode2 && adjStations.IsDeleted == false));
            if (adjStationsFind == null)
                throw new Exception();
            adjStationsFind.IsDeleted = true;
        }
        /// <summary>
        ///  A finction that Checks if there is information  in the system regarding the two following stations
        /// </summary>
        /// <param name="stationCode1"></param>
        /// <param name="stationCode2"></param>
        /// <returns></returns>
        public bool ExistAdjacentStations(int stationCode1, int stationCode2)
        {
            DO.AdjacentStations adjStationsFind = DataSource.ListAdjacentStations.Find(adjStations => (adjStations.StationCode1 == stationCode1 && adjStations.StationCode2 == stationCode2 && adjStations.IsDeleted == false));
            if (adjStationsFind != null)
                return true;
            return false;
        }//yes
        public void UpdateTandDinAdjacentStation(DO.AdjacentStations adjacentStations)
        {
            DO.AdjacentStations adjacentStations1 = DataSource.ListAdjacentStations.Find(s => s.StationCode1 == adjacentStations.StationCode1 && s.StationCode2 == adjacentStations.StationCode2);

            if (adjacentStations != null)
            {
                DataSource.ListAdjacentStations.Remove(adjacentStations1);
                DataSource.ListAdjacentStations.Add(adjacentStations.Clone());
            }
            else
                AddAdjacentStations(adjacentStations);
        }
        #endregion

        #region Line
        /// <summary>
        ///   A function that returns the list of lines that exist in the system
        /// </summary>
        /// <returns>list of lines</returns>       
        public IEnumerable<DO.Line> GetAllLines()
        {
            return from line in DataSource.ListLines
                   where line.IsDeleted == false
                   select line.Clone();

        }
        /// <summary>
        ///  A function that returns the list of lines that exist in the system according predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>list of lines</returns>
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            return from line in DataSource.ListLines
                   where predicate(line)
                   select line.Clone();

        }
        /// <summary>
        /// The function received a line id and returns the line according to the line id
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns>line</returns>
        public DO.Line GetLine(int lineId)
        {
            DO.Line lineFind = DataSource.ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);

            if (lineFind != null)
                return lineFind.Clone();
            else
                throw new BadLineIdException(lineId, "קו זה לא קיים במערכת");
        }
        /// <summary>
        /// A function that receives a line and adds it to the system
        /// </summary>
        /// <param name="line">line</param>
        public void AddLine(DO.Line line)
        {
            line.LineId = Config.LineId++;
            if (DataSource.ListLines.FirstOrDefault(_line => _line.LineId == line.LineId && _line.IsDeleted == false) != null)
                throw new BadLineIdException(1, " כבר קיים במערכת" + line.LineId+ "קו מספר  ");
            DataSource.ListLines.Add(line.Clone());
        }
        /// <summary>
        /// A function that recived a line and updates line bus details
        /// </summary>
        /// <param name="line">line</param>
        public void UpdateLine(DO.Line line)
        {
            DO.Line lineFind = DataSource.ListLines.Find(_line => _line.LineId == line.LineId && _line.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(0 , " לא קיים במערכת" + line.LineId + "קו מספר  ");
            DO.Line newLine = line.Clone();
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
        /// <summary>
        ///  A function that delete line from the system
        /// </summary>
        /// <param name="lineId">line id</param>
        public void DeleteLine(int lineId)
        {
            DO.Line lineFind = DataSource.ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(0, " לא קיים במערכת" + lineId + "קו מספר  ");
            lineFind.IsDeleted = true;
            foreach (DO.LineStation s in DataSource.ListLineStations)
            {
                if (s.LineId == lineId && s.IsDeleted == false)
                    s.IsDeleted = true;
            }
        }
        #endregion

        #region LineStation
        /// <summary>
        /// A function that returns the list of line station that exist in the system
        /// </summary>
        /// <returns>list if line station</returns>
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            return from lineStation in DataSource.ListLineStations
                   select lineStation.Clone();
        }
        /// <summary>
        /// A function that returns the list of line station that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            return from sil in DataSource.ListLineStations
                   where predicate(sil)
                   orderby sil.LineStationIndex
                   select sil;
        }//yes
        /// <summary>
        /// A function that receives a line id and station code and adds it to the system
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <returns></returns>
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            DO.LineStation lineStationFind = DataSource.ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode));

            if (lineStationFind != null)
                return lineStationFind.Clone();
            else
                throw new Exception();
        }
        /// <summary>
        ///  A function that receives a line station and adds it to the system
        /// </summary>
        /// <param name="lineStation">line station</param>
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
        /// <summary>
        /// A function that recived a line station and updates the bus details
        /// </summary>
        /// <param name="lineStation">line station</param>
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
        /// <summary>
        /// A function that delete line station from the system
        /// </summary>
        /// <param name="lineId"> lineId of line</param>
        /// <param name="stationCode">station Code of station</param>
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
        /// <summary>
        ///  A function that returns the list of line in station that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>list of line in station</returns>
        public IEnumerable<DO.Line> GetLinesInStationList(Predicate<DO.LineStation> predicate)
        {
            return from sil in DataSource.ListLineStations
                   where predicate(sil)
                   select GetLine(sil.LineId);
        }
        #endregion

        #region Station
        /// <summary>
        /// A function that returns the list of stations that exist in the system
        /// </summary>
        /// <returns>list of station</returns>
        public IEnumerable<DO.Station> GetAllStations()
        {
            return from station in DataSource.ListStations
                       //where station.IsDeleted == false
                   select station.Clone();
        }
        /// <summary>
        /// A function that returns the list of stations that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            return from station in DataSource.ListStations
                   where predicate(station)
                   select station.Clone();
        }
        /// <summary>
        ///  The function received a station code and returns the station according to the station code
        /// </summary>
        /// <param name="code">code of station</param>
        /// <returns>station</returns>
        public DO.Station GetStation(int code)
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);

            if (stationFind != null)
                return stationFind.Clone();
            else
                throw new BadStationCodeException(code, "תחנה זו לא קיימת במערכת");
        }
        /// <summary>
        /// A function that receives a statioin and adds it to the system
        /// </summary>
        /// <param name="station">station</param>
        public void AddStation(DO.Station station)
        {
            if (DataSource.ListStations.FirstOrDefault(stat => stat.Code == station.Code && stat.IsDeleted == false) != null)
                throw new BadStationCodeException(station.Code, "תחנה זו כבר קיימת במערכת");
            DataSource.ListStations.Add(station.Clone());
        }
        /// <summary>
        /// A function that recived a station and updates the station details
        /// </summary>
        /// <param name="station">station</param>
        public void UpdateStation(DO.Station station)
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == station.Code && stat.IsDeleted == false);
            int code = station.Code;
            if (stationFind == null)
                throw new BadStationCodeException(code, "תחנה זו לא קיימת במערכת");
              DataSource.ListStations.Remove(stationFind);
            DataSource.ListStations.Add(station.Clone());
        }
        public void UpdateStation(int code, Action<DO.Station> update)//?
        {
            DO.Station stationFind = DataSource.ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);
            if (stationFind == null)
                throw new BadStationCodeException(code, "The station does not exist");
            update(stationFind);
        }
        /// <summary>
        ///  A function that delete station from the system
        /// </summary>
        /// <param name="code">code of station</param>
        public void DeleteStation(int code)
        {
            DO.Station statFind = DataSource.ListStations.FirstOrDefault(s => s.Code == code && s.IsDeleted == false);// chrck if station exist in list station
            if (statFind == null)
                throw new BadStationCodeException(code, "תחנה זו לא קיימת במערכת");
            statFind.IsDeleted = true;
            //foreach (DO.AdjacentStations stat in DataSource.ListAdjacentStations)//delete from adjacent Station
            //{
            //    if ((stat.StationCode1 == code || stat.StationCode2 == code) && stat.IsDeleted == false)
            //        stat.IsDeleted = true;
            //}
        }

        #endregion

        #region LineTrip
        public IEnumerable<DO.LineTrip> GetAllLineTrips()
        {
            return from lTrip in DataSource.ListLineTrips
                   select lTrip.Clone();
        }
        public IEnumerable<DO.LineTrip> GetAllLineTripsBy(Predicate<DO.LineTrip> predicate)
        {
            return from lTrip in DataSource.ListLineTrips
                   where predicate(lTrip)
                   select lTrip.Clone();
        }
        public DO.LineTrip GetLineTrip(int lineId, TimeSpan time)
        {
            DO.LineTrip lineTrip = DataSource.ListLineTrips.Find(l => l.LineId == lineId && l.StartAt == time && l.IsDeleted == false);

            if (lineTrip != null)
                return lineTrip.Clone();
            else
                throw new BadLineTripException(lineId, "The line trip does not exist");
        }
        public void AddLineTrip(DO.LineTrip lineTrip)
        {
            if (DataSource.ListLineTrips.FirstOrDefault(l => l.LineId == lineTrip.LineId && l.StartAt == lineTrip.StartAt && l.IsDeleted == false) != null)
                throw new BadLineTripException(lineTrip.LineId, "The line trip is already exist");
            DataSource.ListLineTrips.Add(lineTrip.Clone());
        }
        public void UpdateLineTrip(DO.LineTrip lineTrip)
        {
            DO.LineTrip lTripFind = DataSource.ListLineTrips.Find(l => l.LineId == lineTrip.LineId && l.StartAt == lineTrip.StartAt && l.IsDeleted == false);
            if (lTripFind == null)
                throw new BadLineTripException(lineTrip.LineId, "The line trip does not exist");
            DO.LineTrip newLTrip = lineTrip.Clone();//copy of the bus that the function got
            DataSource.ListLineTrips.Remove(lTripFind);
            DataSource.ListLineTrips.Add(newLTrip);
            //lTripFind = newLTrip;//update
        }
        public void UpdateLineTrip(int lineId, TimeSpan time, Action<DO.LineTrip> update)
        {
            DO.LineTrip lTripFind = DataSource.ListLineTrips.Find(l => l.LineId == lineId && l.StartAt == time && l.IsDeleted == false);
            if (lTripFind == null)
                throw new BadLineTripException(lineId, "The line trip does not exist");
            update(lTripFind);
        }
        public void DeleteLineTrip(int lineId, TimeSpan time)
        {

            DO.LineTrip lineTrip = DataSource.ListLineTrips.Find(l => l.LineId == lineId && l.StartAt == time && l.IsDeleted == false);
            if (lineTrip == null)
                throw new BadLineTripException(lineId, "The line trip does not exist");
            lineTrip.IsDeleted = true;
        }

        #endregion

        #region User
        /// <summary>
        /// A function that returns the list of users that exist in the system
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DO.User> GetAllUsers()
        {
            return from user in DataSource.ListUsers
                   select user.Clone();
        }
        /// <summary>
        /// A function that returns the list of users that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
            return from user in DataSource.ListUsers
                   where predicate(user)
                   select user.Clone();
        }
        /// <summary>
        /// The function received a username and returns the user according to the username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DO.User GetUser(string userName)//yes
          {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName);

            if (userFind == null)
                throw new DO.BadUserException("משתמש זה לא קיים במערכת");
            else
                return userFind.Clone();
        }
        /// <summary>
        /// A function that receives a user and adds it to the system
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(DO.User user)
        {
            if (DataSource.ListUsers.FirstOrDefault(_user => _user.UserName == user.UserName ) != null)
                throw new DO.BadUserException("שם משתמש זה כבר קיים במערכת");
            DataSource.ListUsers.Add(user.Clone());
        }
        /// <summary>
        /// A function that recived a user and updates the user details
        /// </summary>
        /// <param name="user"></param>
        public void UpdateUser(DO.User user)
        {
            DO.User userFind = DataSource.ListUsers.Find(_user => _user.UserName == user.UserName );
            if (userFind == null)
                throw new Exception();
            DO.User newUser = user.Clone();//copy of the bus that the function got
            userFind = newUser;//update
        }
        /// <summary>
        /// A function that recived a user and updates the user details
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="update"></param>
        public void UpdateUser(string userName, Action<DO.User> update)
        {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName );
            if (userFind == null)
                throw new Exception();
            update(userFind);
        }
        /// <summary>
        /// A function that delete user from the system
        /// </summary>
        /// <param name="userName"></param>
        public void DeleteUser(string userName)
        {
            DO.User userFind = DataSource.ListUsers.Find(user => user.UserName == userName);

            if (userFind == null)
                throw new Exception();
            userFind.IsDeleted = true;
            // DataSource.ListUsers.Remove(userFind);
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
            DO.Trip trip = DataSource.ListTrips.Find(t => t.TripId == tripId && t.IsDeleted == false);
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
            DO.Trip tripFind = DataSource.ListTrips.Find(t => t.TripId == trip.TripId && t.IsDeleted == false);
            if (tripFind == null)
                throw new Exception();
            DO.Trip newTrip = trip.Clone();//copy of the bus that the function got
            tripFind = newTrip;//update
        }
        public void UpdateTrip(int tripId, Action<DO.Trip> update)
        {
            DO.Trip tripFind = DataSource.ListTrips.Find(t => t.TripId == tripId && t.IsDeleted == false);
            if (tripFind == null)
                throw new Exception();
            update(tripFind);
        }
        public void DeleteTrip(int tripId)
        {
            DO.Trip trip = DataSource.ListTrips.Find(t => t.TripId == tripId && t.IsDeleted == false);
            if (trip == null)
                throw new Exception();
            trip.IsDeleted = true;
        }

        #endregion

        #region StationInLine
        /// <summary>
        /// A function that receives a station code , bus and index and add the station it to the line's station list
        /// </summary>
        /// <param name="statCode">code of station</param>
        /// <param name="lineID">line id</param>
        /// <param name="index">index of lineStation</param>
        public void AddStationInLine(int statCode, int lineID, int index)
        {
            if (DataSource.ListLineStations.FirstOrDefault(sil => (sil.StationCode == statCode && sil.LineId == lineID)) != null)
                throw new DO.BadStationCodeException(statCode, "התחנה כבר קיימת בקו זה");
            DO.LineStation lineStation = new DO.LineStation() { StationCode = statCode, LineId = lineID, LineStationIndex = index };
            foreach (DO.LineStation lineStat in GetAllLineStationsBy(s => s.LineId == lineID))
                if (lineStat.LineStationIndex >= index)
                    lineStat.LineStationIndex++;
            DataSource.ListLineStations.Add(lineStation);
        }
        /// <summary>
        ///  A function that receives a station code ,and bus  and delete the station from  the line's station list
        /// </summary>
        /// <param name="lineID">line id</param>
        /// <param name="statCode">code of station</param>
        public void DeleteStationInLine(int lineID , int statCode)
        {
            DO.LineStation lineStation = DataSource.ListLineStations.Find(sil => (sil.StationCode == statCode && sil.LineId == lineID));
            int index = lineStation.LineStationIndex;
            if (lineStation != null)
            {
                DataSource.ListLineStations.Remove(lineStation);
                foreach (DO.LineStation lineStat in GetStationInLineList(s => s.LineId == lineID))
                {
                    if (lineStat.LineStationIndex > index)
                        lineStat.LineStationIndex--;
                }
            }
            else
                throw new DO.BadStationCodeException(statCode, "קו אוטובוס זה לא עובר בתחנה זו");
        }
     /// <summary>
     /// A function that get all the station that the line passthrough them
     /// </summary>
     /// <param name="predicate"></param>
     /// <returns>list of stationInLine</returns>
        public IEnumerable<DO.LineStation> GetStationInLineList(Predicate<DO.LineStation> predicate)
        {
            return from sil in DataSource.ListLineStations
                   where predicate(sil)
                   orderby sil.LineStationIndex
                   select sil;

        }
        #endregion
    }
} 