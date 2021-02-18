using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using BO;

namespace BLAPI
{
   public interface IBL
    {
        #region Bus
        BO.Bus GetBus(int licenseNum);//ll
        IEnumerable<BO.Bus> GetAllBuses();
         IEnumerable<BO.Bus> GetAllDeleteBuses();
        IEnumerable<BO.Bus> GetBusesBy(Predicate<BO.Bus> predicate);
        void UpdateBusDetails(BO.Bus bus);
        void DeleteBus(int licenseNum);
        void AddBus(BO.Bus bus);
        void BusException(BO.Bus busBO);

        #endregion

        #region Line
        void AddNewLine(BO.Line lineBo);
        BO.Line GetLine(int lineId);
        IEnumerable<BO.Line> GetAllLines();
        IEnumerable<BO.Line> GetAllDeletedLines();

        //IEnumerable<BO.ListedPerson> GetStudentIDNameList();
        IEnumerable<BO.Line> GelAllLinesBy(Predicate<BO.Line> predicate);
        void UpdateLineDetails(BO.Line line);
        void DeleteLine(int LineId);
        
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
        IEnumerable<BO.Station> GetAllStations();
        BO.Station GetStation(int code);
        void AddStation(BO.Station station);
        void DeleteStation(int code);
         void UpdateStation(BO.Station station);
        bool IsAdjacentStat(int code1, int code2);

        #endregion

        #region StationInLine
        void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second);
        void AddStationInLine(int stationCode, int busID, int index, int indexNextCode, int indexPrevCode, double distanceNext, TimeSpan timeNext, double distancePrev, TimeSpan timePrev);
        void DeleteStationInLine(int code, int lineID);
        void UpdateTandDinAdjacentStation(int code1, int code2, double distanceFromNext, TimeSpan timeFromNext);

        #endregion

        #region user
        BO.User SignIn(string username, string passcode);
        void addNewUser(BO.User userBo);


        #endregion

        void DeleteDepTime(int lineId, TimeSpan dep);
        void AddDepTime(int lineId, TimeSpan dep);

        #region Trip
        IEnumerable<LineTiming> GetLineTimingPerStation(BO.Station stationBO, TimeSpan currentTime);
        List<string> FindRoute(int stationCode1, int stationCode2);
        #endregion
    }
}
