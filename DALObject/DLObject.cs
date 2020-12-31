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
        public void DeleteLiine(int code)//?
        {

        }

        public IEnumerable<Station> GetAllStationsBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateStation(int code, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        Bus IDL.GetLine(int lineId)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(Line line)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(int code, Action<Bus> update)
        {
            throw new NotImplementedException();
        }

        public void DeleteLine(int code)
        {
            throw new NotImplementedException();
        }
#endregion
    }
}


