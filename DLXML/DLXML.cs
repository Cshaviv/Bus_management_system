using DLAPI;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DL
{
    public class DLXML
    {
        #region singelton
        static readonly DLXML instance = new DLXML();
        static DLXML() { }// static ctor to ensure instance init is done just before first usage
        DLXML() { } // default => private
        public static DLXML Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files

       // string lineTripsPath = @"TripsXml.xml"; //XElement

        string busesPath = @"BusesXml.xml"; //XMLSerializer
        string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
        string linesPath = @"LinesXml.xml"; //XMLSerializer
        string lineStationsPath = @"LineStationsXml.xml"; //XMLSerializer
        string stationsPath = @"StationsXml.xml"; //XMLSerializer
        string usersPath = @"UserXml.xml"; //XMLSerializer
        string runningNumberPath = @"TripsXml.xml"; //XMLSerializer
        #endregion

        #region Bus
        public IEnumerable<DO.Bus> GetAllBuses()
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            return from bus in ListBuses
                   where bus.IsDeleted == false
                   select bus;
        }
        public IEnumerable<DO.Bus> GetAllBusesBy(Predicate<DO.Bus> predicate)//???
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            return from bus in ListBuses
                   where predicate(bus)
                   select bus;
        }
        public DO.Bus GetBus(int licenseNumber)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            DO.Bus busFind = ListBuses.Find(bus => bus.LicenseNum == licenseNumber && bus.IsDeleted == false);

            if (busFind != null)
                return busFind;
            else
                throw new BadLicenseNumException(licenseNumber, "The bus does not exist");
        }
        public void AddBus(DO.Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            if (ListBuses.FirstOrDefault(bus_ => bus_.LicenseNum == bus.LicenseNum && bus_.IsDeleted == false) != null)
                throw new BadLicenseNumException(bus.LicenseNum, "אוטובוס זה כבר קיים במערכת");
            ListBuses.Add(bus);
            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        public void UpdateBus(DO.Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            DO.Bus busFind = ListBuses.Find(bus_ => bus_.LicenseNum == bus.LicenseNum && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(bus.LicenseNum, "הקו אינו קיים במערכת");
            ListBuses.Remove(busFind);
            ListBuses.Add(bus);
            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        public void UpdateBus(int licenseNumber, Action<DO.Bus> update)//???
        {
            //List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            //DO.Bus busFind = ListBuses.Find(bus_ => bus_.LicenseNum == licenseNumber && bus_.IsDeleted == false);
            //if (busFind == null)
            //    throw new BadLicenseNumException(licenseNumber, "The bus does not exist");
            //update(busFind);
        }
        public void DeleteBus(int licenseNumber)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);
            DO.Bus busFind = ListBuses.Find(bus_ => bus_.LicenseNum == licenseNumber && bus_.IsDeleted == false);
            if (busFind == null)
                throw new BadLicenseNumException(licenseNumber, "הקו אינו קיים במערכת");
            busFind.IsDeleted = true;//delete
            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        #endregion

        #region AdjacentStation
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            return (from adj in adjStationssRootElem.Elements()
                    where bool.Parse(adj.Element("IsDeleted").Value) == false
                    select new AdjacentStations()
                    {
                        StationCode1 = Int32.Parse(adj.Element("StationCode1").Value),
                        StationCode2 = Int32.Parse(adj.Element("StationCode2").Value),
                        Distance = double.Parse(adj.Element("Distance").Value),
                        Time = TimeSpan.Parse(adj.Element("Time").Value),
                        IsDeleted = Convert.ToBoolean(adj.Element("IsDeleted").Value)
                    }
                   );

        }
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStationsBy(Predicate<DO.AdjacentStations> predicate)
        {
            XElement adjStationsRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            return from adj in adjStationsRootElem.Elements()
                   let adj1 = new AdjacentStations()
                   {
                       StationCode1 = Int32.Parse(adj.Element("StationCode1").Value),
                       StationCode2 = Int32.Parse(adj.Element("StationCode2").Value),
                       Distance = double.Parse(adj.Element("Distance").Value),
                       Time = TimeSpan.Parse(adj.Element("Time").Value),
                       IsDeleted = Convert.ToBoolean(adj.Element("IsDeleted").Value)
                   }
                   where predicate(adj1)
                   select adj1;
        }
        public DO.AdjacentStations GetAdjacentStations(int code1, int code2)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            AdjacentStations adjStation = (from adj in adjStationssRootElem.Elements()
                                  where int.Parse(adj.Element("StationCode1").Value) == code1 && int.Parse(adj.Element("StationCode2").Value) == code2 && bool.Parse(adj.Element("IsDeleted").Value) == false
                                  select new AdjacentStations()
                                  {
                                      StationCode1 = Int32.Parse(adj.Element("StationCode1").Value),
                                      StationCode2 = Int32.Parse(adj.Element("StationCode2").Value),
                                      Distance = double.Parse(adj.Element("Distance").Value),
                                      Time = TimeSpan.Parse(adj.Element("Time").Value),
                                      IsDeleted = Convert.ToBoolean(adj.Element("IsDeleted").Value)
                                  }
                        ).FirstOrDefault();

            if (adjStation == null)
                throw new Exception();
            //throw new DO.BadAdjacentStationsException(stationCode1, stationCode2, "The adjacent stations does not exist");

            return adjStation;
        }
        public void AddAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adjStat = (from adj in adjStationssRootElem.Elements()
                             where int.Parse(adj.Element("StationCode1").Value) == adjacentStations.StationCode1 && int.Parse(adj.Element("StationCode2").Value) == adjacentStations.StationCode2 && bool.Parse(adj.Element("IsDeleted").Value) == false
                             select adj).FirstOrDefault();

            if (adjStat != null)
                throw new Exception();
               // throw new BadAdjacentStationsException(adjacentStations.StationCode1, adjacentStations.StationCode2, "The adjacent stations are already exist"); ;

            XElement adjStatElem = new XElement("AdjacentStations",
                                   new XElement("StationCode1", adjacentStations.StationCode1.ToString()),
                                   new XElement("StationCode2", adjacentStations.StationCode2.ToString()),
                                   new XElement("Distance", adjacentStations.Distance.ToString()),
                                   new XElement("Time", adjacentStations.Time.ToString()),
                                   new XElement("IsDeleted", adjacentStations.IsDeleted.ToString()));
            adjStationssRootElem.Add(adjStatElem);
            XMLTools.SaveListToXMLElement(adjStationssRootElem, adjacentStationsPath);
        }
        public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adjStat = (from adj in adjStationssRootElem.Elements()
                             where int.Parse(adj.Element("StationCode1").Value) == adjacentStations.StationCode1 && int.Parse(adj.Element("StationCode2").Value) == adjacentStations.StationCode2 && bool.Parse(adj.Element("IsDeleted").Value) == false
                             select adj).FirstOrDefault();

            if (adjStat != null)
            {
                adjStat.Element("StationCode1").Value = adjacentStations.StationCode1.ToString();
                adjStat.Element("StationCode2").Value = adjacentStations.StationCode2.ToString();
                adjStat.Element("Distance").Value = adjacentStations.Distance.ToString();
                adjStat.Element("Time").Value = adjacentStations.Time.ToString();
                adjStat.Element("IsDeleted").Value = adjacentStations.IsDeleted.ToString();

                XMLTools.SaveListToXMLElement(adjStationssRootElem, adjacentStationsPath);
            }
            else
                throw new Exception();
                //throw new DO.BadPersonIdException(person.ID, $"bad person id: {person.ID}");


        }
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {
             throw new NotImplementedException();
        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adjStat = (from adj in adjStationssRootElem.Elements()
                            where int.Parse(adj.Element("StationCode1").Value) == stationCode1 && int.Parse(adj.Element("StationCode2").Value) == stationCode2 && bool.Parse(adj.Element("IsDeleted").Value) == false
                            select adj).FirstOrDefault();

            if (adjStat != null)
            {
                adjStat.Element("IsDeleted").Value = true.ToString();

                XMLTools.SaveListToXMLElement(adjStationssRootElem, adjacentStationsPath);
            }
            else
                throw new Exception();
                //throw new DO.BadPersonIdException(id, $"bad person id: {id}");

        }
        public bool ExistAdjacentStations(int stationCode1, int stationCode2)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adjStat = (from adj in adjStationssRootElem.Elements()
                            where int.Parse(adj.Element("StationCode1").Value) == stationCode1 && int.Parse(adj.Element("StationCode2").Value) == stationCode2 && bool.Parse(adj.Element("IsDeleted").Value) == false
                            select adj).FirstOrDefault();
            if (adjStat == null)
                return false;
            return true;
        }
        #endregion

        #region Line
        /// <summary>
        ///   A function that returns the list of lines that exist in the system
        /// </summary>
        /// <returns>list of lines</returns>       
        public IEnumerable<DO.Line> GetAllLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            return from line in ListLines
                   where line.IsDeleted == false
                   select line;

        }
        /// <summary>
        ///  A function that returns the list of lines that exist in the system according predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>list of lines</returns>
        public IEnumerable<DO.Line> GetAllLinesBy(Predicate<DO.Line> predicate)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            return from line in ListLines
                   where predicate(line)
                   select line;

        }
        /// <summary>
        /// The function received a line id and returns the line according to the line id
        /// </summary>
        /// <param name="lineId"></param>
        /// <returns>line</returns>
        public DO.Line GetLine(int lineId)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            DO.Line lineFind = ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);

            if (lineFind != null)
                return lineFind;
            else
                throw new BadLineIdException(lineId, "קו זה לא קיים במערכת");
        }
        /// <summary>
        /// A function that receives a line and adds it to the system
        /// </summary>
        /// <param name="line">line</param>
        public void AddLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            line.LineId =  XMLTools.GetRunningNumber(runningNumberPath);
            if (ListLines.FirstOrDefault(_line => _line.LineId == line.LineId && _line.IsDeleted == false) != null)
                throw new BadLineIdException(line.LineId, "קו זה כבר קיים במערכת");
            ListLines.Add(line);
            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);

        }
        /// <summary>
        /// A function that recived a line and updates line bus details
        /// </summary>
        /// <param name="line">line</param>
        public void UpdateLine(DO.Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            DO.Line lineFind = ListLines.Find(_line => _line.LineId == line.LineId && _line.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(line.LineId, "קו זה לא קיים במערכת");
            DO.Line newLine = line;
            ListLines.Remove(lineFind);
            ListLines.Add(newLine);
            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
        }
        public void UpdateLine(int lineId, Action<DO.Line> update)
        {
            //List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            //DO.Line lineFind = ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);
            //if (lineFind == null)
            //    throw new BadLineIdException(lineId, "The Line ID does not exist");
            //update(lineFind);
        }
        /// <summary>
        ///  A function that delete line from the system
        /// </summary>
        /// <param name="lineId">line id</param>
        public void DeleteLine(int lineId)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);
            List<DO.LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.Line lineFind = ListLines.Find(line => line.LineId == lineId && line.IsDeleted == false);
            if (lineFind == null)
                throw new BadLineIdException(lineId, "קו אוטובוס זה לא קיים במערכת");
            lineFind.IsDeleted = true;
            foreach (DO.LineStation lineStat in ListLineStations)
            {
                if (lineStat.LineId == lineId && lineStat.IsDeleted == false)
                    lineStat.IsDeleted = true;
            }
            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        #endregion
        
        #region LineStation
        /// <summary>
        /// A function that returns the list of line station that exist in the system
        /// </summary>
        /// <returns>list if line station</returns>
        public IEnumerable<DO.LineStation> GetAllLineStations()
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from lineStation in ListLineStations
                   select lineStation;
        }
        /// <summary>
        /// A function that returns the list of line station that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>list of lineStation</returns>
        public IEnumerable<DO.LineStation> GetAllLineStationsBy(Predicate<DO.LineStation> predicate)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from sil in ListLineStations
                   where predicate(sil)
                   orderby sil.LineStationIndex
                   select sil;
        }
        /// <summary>
        /// A function that receives a line id and station code and adds it to the system
        /// </summary>
        /// <param name="lineId"></param>
        /// <param name="stationCode"></param>
        /// <returns>line station</returns>
        public DO.LineStation GetLineStation(int lineId, int stationCode)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);

            DO.LineStation lineStationFind = ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode));

            if (lineStationFind != null)
                return lineStationFind;
            else
                throw new Exception();
        }
        /// <summary>
        ///  A function that receives a line station and adds it to the system
        /// </summary>
        /// <param name="lineStation">line station</param>
        public void AddLineStation(DO.LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);

            if (ListLineStations.FirstOrDefault(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.StationCode == lineStation.StationCode && lineStat.IsDeleted == false)) != null)//if this line station already exists in the list
                throw new Exception();
            // update the line station index of all the next station
            DO.LineStation next = ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == lineStation.LineStationIndex && lineStat.IsDeleted == false));
            DO.LineStation temp;
            int index;
            while (next != null)
            {

                temp = next;
                index = next.LineStationIndex + 1;
                temp =ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == index && lineStat.IsDeleted == false));
                ++next.LineStationIndex;
                next = temp;
            }

            //update prev and next
            DO.LineStation prev = ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == lineStation.LineStationIndex - 1 && lineStat.IsDeleted == false));
            next = ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.LineStationIndex == lineStation.LineStationIndex + 1 && lineStat.IsDeleted == false));
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
            ListLineStations.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        /// <summary>
        /// A function that recived a line station and updates the bus details
        /// </summary>
        /// <param name="lineStation">line station</param>
        public void UpdateLineStation(DO.LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation lineStationFind = ListLineStations.Find(lineStat => (lineStat.LineId == lineStation.LineId && lineStat.StationCode == lineStation.StationCode && lineStat.IsDeleted == false));
            if (lineStationFind == null)
                throw new Exception();
            DO.LineStation newAdj = lineStation;//copy of the line station that the function got
            lineStationFind = newAdj;//update
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        public void UpdateLineStation(int lineId, int stationCode, Action<DO.LineStation> update)
        {
            throw new NotImplementedException();
            //List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            //DO.LineStation lineStationFind = ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode && lineStat.IsDeleted == false));
            //if (lineStationFind == null)
            //    throw new Exception();
            //update(lineStationFind);
        }
        /// <summary>
        /// A function that delete line station from the system
        /// </summary>
        /// <param name="lineId"> lineId of line</param>
        /// <param name="stationCode">station Code of station</param>
        public void DeleteLineStation(int lineId, int stationCode)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation lineStationFind = ListLineStations.Find(lineStat => (lineStat.LineId == lineId && lineStat.StationCode == stationCode && lineStat.IsDeleted == false));
            if (lineStationFind == null)
                throw new Exception();
            lineStationFind.IsDeleted = true;
            DO.LineStation NextFind;
            if (lineStationFind.LineStationIndex > 1)
            {
                DO.LineStation PrevFind = ListLineStations.Find(prevLineStat => (prevLineStat.LineId == lineId && prevLineStat.LineStationIndex == lineStationFind.LineStationIndex - 1 && prevLineStat.IsDeleted == false));
                NextFind = ListLineStations.Find(next => (next.LineId == lineId && next.LineStationIndex == lineStationFind.LineStationIndex + 1 && next.IsDeleted == false));
                if (NextFind != null)//if its not the last station
                {
                    PrevFind.NextStationCode = NextFind.StationCode;
                    NextFind.PrevStationCode = PrevFind.StationCode;
                }
            }
            else
            {
                NextFind =ListLineStations.Find(nextLineStat => (nextLineStat.LineId == lineId && nextLineStat.LineStationIndex == lineStationFind.LineStationIndex + 1 && nextLineStat.IsDeleted == false));
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
                NextFind =ListLineStations.Find(next => (next.LineId == lineId && next.LineStationIndex == index + 1 && next.IsDeleted == false));
            }
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        /// <summary>
        ///  A function that returns the list of line in station that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>list of line in station</returns>
        public IEnumerable<DO.Line> GetLinesInStationList(Predicate<DO.LineStation> predicate)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from sil in ListLineStations
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
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            return from station in ListStations
                       //where station.IsDeleted == false
                   select station;
        }
        /// <summary>
        /// A function that returns the list of stations that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.Station> GetAllStationsBy(Predicate<DO.Station> predicate)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            return from station in ListStations
                   where predicate(station)
                   select station;
        }
        /// <summary>
        ///  The function received a station code and returns the station according to the station code
        /// </summary>
        /// <param name="code">code of station</param>
        /// <returns>station</returns>
        public DO.Station GetStation(int code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            DO.Station stationFind = ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);

            if (stationFind != null)
                return stationFind;
            else
                throw new BadStationCodeException(code, "תחנה זו לא קיימת במערכת");
        }
        /// <summary>
        /// A function that receives a statioin and adds it to the system
        /// </summary>
        /// <param name="station">station</param>
        public void AddStation(DO.Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            if (ListStations.FirstOrDefault(stat => stat.Code == station.Code && stat.IsDeleted == false) != null)
                throw new BadStationCodeException(station.Code, "תחנה זו כבר קיימת במערכת");
            ListStations.Add(station);
            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        /// <summary>
        /// A function that recived a station and updates the station details
        /// </summary>
        /// <param name="station">station</param>
        public void UpdateStation(DO.Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            DO.Station stationFind =ListStations.Find(stat => stat.Code == station.Code && stat.IsDeleted == false);
            int code = station.Code;
            if (stationFind == null)
                throw new BadStationCodeException(code, "תחנה זו לא קיימת במערכת");
            ListStations.Remove(stationFind);
            ListStations.Add(station);
            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        public void UpdateStation(int code, Action<DO.Station> update)//?
        {
            throw new NotImplementedException();
            //List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            //DO.Station stationFind = ListStations.Find(stat => stat.Code == code && stat.IsDeleted == false);
            //if (stationFind == null)
            //    throw new BadStationCodeException(code, "The station does not exist");
            //update(stationFind);
        }
        /// <summary>
        ///  A function that delete station from the system
        /// </summary>
        /// <param name="code">code of station</param>
        public void DeleteStation(int code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);
            DO.Station statFind =ListStations.FirstOrDefault(s => s.Code == code && s.IsDeleted == false);// chrck if station exist in list station
            if (statFind == null)
                throw new BadStationCodeException(code, "תחנה זו לא קיימת במערכת");
            statFind.IsDeleted = true;
            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        #endregion

        #region User
        /// <summary>
        /// A function that returns the list of users that exist in the system
        /// </summary>
        /// <returns>list of users</returns>
        public IEnumerable<DO.User> GetAllUsers()
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            return from user in ListUsers
                   select user;
        }
        /// <summary>
        /// A function that returns the list of users that exist in the system
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<DO.User> GetAllUsersBy(Predicate<DO.User> predicate)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            return from user in ListUsers
                   where predicate(user)
                   select user;
        }
        /// <summary>
        /// The function received a username and returns the user according to the username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>user</returns>
        public DO.User GetUser(string userName)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            DO.User userFind = ListUsers.Find(user => user.UserName == userName);

            if (userFind == null)
                throw new DO.BadUserException("משתמש זה לא קיים במערכת");
            else
                return userFind;
        }
        /// <summary>
        /// A function that receives a user and adds it to the system
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(DO.User user)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            if (ListUsers.FirstOrDefault(_user => _user.UserName == user.UserName) != null)
                throw new DO.BadUserException("שם משתמש זה כבר קיים במערכת");
            ListUsers.Add(user);
            XMLTools.SaveListToXMLSerializer(ListUsers, usersPath);
        }
        /// <summary>
        /// A function that recived a user and updates the user details
        /// </summary>
        /// <param name="user">user</param>
        public void UpdateUser(DO.User user)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            DO.User userFind = ListUsers.Find(_user => _user.UserName == user.UserName);
            if (userFind == null)
                throw new Exception();
            DO.User newUser = user;//copy of the bus that the function got
            userFind = newUser;//update
            XMLTools.SaveListToXMLSerializer(ListUsers, usersPath);

        }
        /// <summary>
        /// A function that recived a user and updates the user details
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="update"></param>
        public void UpdateUser(string userName, Action<DO.User> update)
        {
            throw new NotImplementedException();
            //List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            //DO.User userFind = ListUsers.Find(user => user.UserName == userName);
            //if (userFind == null)
            //    throw new Exception();
            //update(userFind);
        }
        /// <summary>
        /// A function that delete user from the system
        /// </summary>
        /// <param name="userName">user name</param>
        public void DeleteUser(string userName)
        {
            List<User> ListUsers = XMLTools.LoadListFromXMLSerializer<User>(usersPath);
            DO.User userFind = ListUsers.Find(user => user.UserName == userName);

            if (userFind == null)
                throw new Exception();
            userFind.IsDeleted = true;
            //ListUsers.Remove(userFind);
            XMLTools.SaveListToXMLSerializer(ListUsers, usersPath);
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
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            if (ListLineStations.FirstOrDefault(sil => (sil.StationCode == statCode && sil.LineId == lineID)) != null)
                throw new DO.BadStationCodeException(statCode, "התחנה כבר קיימת בקו זה");
            DO.LineStation lineStation = new DO.LineStation() { StationCode = statCode, LineId = lineID, LineStationIndex = index };
            foreach (DO.LineStation lineStat in GetAllLineStationsBy(s => s.LineId == lineID))
                if (lineStat.LineStationIndex >= index)
                    lineStat.LineStationIndex++;
            ListLineStations.Add(lineStation);
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        /// <summary>
        ///  A function that receives a station code ,and bus  and delete the station from  the line's station list
        /// </summary>
        /// <param name="lineID">line id</param>
        /// <param name="statCode">code of station</param>
        public void DeleteStationInLine(int lineID, int statCode)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            DO.LineStation lineStation = ListLineStations.Find(sil => (sil.StationCode == statCode && sil.LineId == lineID));
            int index = lineStation.LineStationIndex;
            if (lineStation != null)
            {
                ListLineStations.Remove(lineStation);
                foreach (DO.LineStation lineStat in GetStationInLineList(s => s.LineId == lineID))
                {
                    if (lineStat.LineStationIndex > index)
                        lineStat.LineStationIndex--;
                }
            }
            else
                throw new DO.BadStationCodeException(statCode, "קו אוטובוס זה לא עובר בתחנה זו");
            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        /// <summary>
        /// A function that get all the station that the line passthrough them
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>list of stationInLine</returns>
        public IEnumerable<DO.LineStation> GetStationInLineList(Predicate<DO.LineStation> predicate)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<DO.LineStation>(lineStationsPath);
            return from sil in ListLineStations
                   where predicate(sil)
                   orderby sil.LineStationIndex
                   select sil;

        }
        #endregion


    }
}