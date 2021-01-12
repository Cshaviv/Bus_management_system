using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BLAPI
{
   public interface IBL
    {
        #region Bus
        BO.Bus GetBus(int licenseNum);//ll
        IEnumerable<BO.Bus> GetAllBuses();
        IEnumerable<BO.Bus> GetBusesBy(Predicate<BO.Bus> predicate);
        void UpdateBusDetails(BO.Bus bus);
        void DeleteBus(int licenseNum);
        #endregion
        #region Line
        void AddNewLine(BO.Line lineBo);
        BO.Line GetLine(int lineId);
        IEnumerable<BO.Line> GetAllLines();
        //IEnumerable<BO.ListedPerson> GetStudentIDNameList();
        IEnumerable<BO.Line> GelAllLinesBy(Predicate<BO.Line> predicate);
        void UpdateLineDetails(BO.Line line);
        void DeleteLine(int LineId);
        void AddBus(BO.Bus bus);
        #endregion
        #region LineStation
        void AddLineStation(BO.LineStation s);
        void DeleteLineStation(int lineId, int stationCode);
        #endregion
        #region AdjacentStations
        bool IsExistAdjacentStations(int stationCode1, int stationCode2);
        //void AddAdjacentStations(BO.AdjacentStation adjBO);
        #endregion
        #region Station
        BO.Station stationDoBoAdapter(DO.Station stationDO);
        IEnumerable<BO.Station> GetAllStations();
        BO.Station GetStation(int code);

        #endregion
        #region StationInLine
        void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second);
        void AddStationInLine(int stationID, int busID, int index);

        #endregion
    }
}
