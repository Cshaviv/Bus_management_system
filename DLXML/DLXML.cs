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

        string lineTripsPath = @"UserXml.xml"; //XElement

        string busesPath = @"BusesXml.xml"; //XMLSerializer
        string adjacentStationsPath = @"AdjacentStationsXml.xml"; //XElement
        string linesPath = @"LinesXml.xml"; //XMLSerializer
        string lineStationsPath = @"LineStationsXml.xml"; //XMLSerializer
        string stationsPath = @"StationsXml.xml"; //XMLSerializer
        string usersPath = @"TripsXml.xml"; //XMLSerializer
        string runningNumberPath = @"TripsXml.xml"; //XMLSerializer
        #endregion
        #region adjacent station
        public IEnumerable<DO.AdjacentStations> GetAllAdjacentStations()
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            return (from a in adjStationssRootElem.Elements()
                    where bool.Parse(a.Element("IsDeleted").Value) == false
                    select new AdjacentStations()
                    {
                        StationCode1 = Int32.Parse(a.Element("StationCode1").Value),
                        StationCode2 = Int32.Parse(a.Element("StationCode2").Value),
                        Distance = double.Parse(a.Element("Distance").Value),
                        Time = TimeSpan.Parse(a.Element("Time").Value),
                        IsDeleted = Convert.ToBoolean(a.Element("IsDeleted").Value)
                    }
                   );

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

            XElement adj1 = (from a in adjStationssRootElem.Elements()
                             where int.Parse(a.Element("StationCode1").Value) == adjacentStations.StationCode1 && int.Parse(a.Element("StationCode2").Value) == adjacentStations.StationCode2 && bool.Parse(a.Element("IsDeleted").Value) == false
                             select a).FirstOrDefault();

            if (adj1 != null)
                throw new Exception();
               // throw new BadAdjacentStationsException(adjacentStations.StationCode1, adjacentStations.StationCode2, "The adjacent stations are already exist"); ;

            XElement adjElem = new XElement("AdjacentStations",
                                   new XElement("StationCode1", adjacentStations.StationCode1.ToString()),
                                   new XElement("StationCode2", adjacentStations.StationCode2.ToString()),
                                   new XElement("Distance", adjacentStations.Distance.ToString()),
                                   new XElement("Time", adjacentStations.Time.ToString()),
                                   new XElement("IsDeleted", adjacentStations.IsDeleted.ToString()));
            adjStationssRootElem.Add(adjElem);
            XMLTools.SaveListToXMLElement(adjStationssRootElem, adjacentStationsPath);
        }
        //public void UpdateAdjacentStations(DO.AdjacentStations adjacentStations)
        //{
        //    XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

        //    XElement per = (from p in adjStationssRootElem.Elements()
        //                    where int.Parse(p.Element("ID").Value) == person.ID
        //                    select p).FirstOrDefault();

        //    if (per != null)
        //    {
        //        per.Element("ID").Value = person.ID.ToString();
        //        per.Element("Name").Value = person.Name;
        //        per.Element("Street").Value = person.Street;
        //        per.Element("HouseNumber").Value = person.HouseNumber.ToString();
        //        per.Element("City").Value = person.City;
        //        per.Element("BirthDate").Value = person.BirthDate.ToString();
        //        per.Element("PersonalStatus").Value = person.PersonalStatus.ToString();
        //        per.Element("Duration").Value = person.Duration.ToString();

        //        XMLTools.SaveListToXMLElement(personsRootElem, personsPath);
        //    }
        //    else
        //        throw new DO.BadPersonIdException(person.ID, $"bad person id: {person.ID}");
        //}
    
        public void UpdateAdjacentStations(int stationCode1, int stationCode2, Action<DO.AdjacentStations> update)
        {

        }
        public void DeleteAdjacentStations(int stationCode1, int stationCode2)
        {

        }
        public bool ExistAdjacentStations(int stationCode1, int stationCode2)
        {
            XElement adjStationssRootElem = XMLTools.LoadListFromXMLElement(adjacentStationsPath);

            XElement adj = (from a in adjStationssRootElem.Elements()
                            where int.Parse(a.Element("StationCode1").Value) == stationCode1 && int.Parse(a.Element("StationCode2").Value) == stationCode2 && bool.Parse(a.Element("IsDeleted").Value) == false
                            select a).FirstOrDefault();
            if (adj == null)
                return false;
            return true;
        }
        #endregion
    }
}