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
        IEnumerable<Station> GetAllStationsBy(Predicate<Bus> predicate);
        Station GetStation(int code);
        void AddStation(Station station);//?
        void UpdateStation(Station station);
        void UpdateStation(int code, Action<Bus> update); //method that knows to updt specific fields in Bus
        void DeleteStation(int code);
        #endregion
        #region Line
        IEnumerable<Line> GetAllLines();
        IEnumerable<Line> GetAllLinesBy(Predicate<Line> predicate);
        Bus GetLine(int lineId);
        void AddLine(Line line);//?
        void UpdateLine(Line line);
        void UpdateLine(int code, Action<Bus> update); //method that knows to updt specific fields in Bus
        void DeleteLine(int code);
        #endregion
    }
}


