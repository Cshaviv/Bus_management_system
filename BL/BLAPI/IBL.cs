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
        IEnumerable<BO.Bus> GetAllBuses();
        IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate);
        BO.Bus GetBus(int licenseNum);
        void AddBus(BO.Bus bus);
        void UpdateBus(BO.Bus bus);
       // void UpdateBus(int licenseNum, Action<BO.Bus> update); //method that knows to updt specific fields in Bus
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
        #endregion
        #region Station
        //IEnumerable<BO.Station> GetAllStations();
        //IEnumerable<BO.Station> GetAllStationsBy(Predicate<BO.Station> predicate);
        //BO.Station GetStation(int code);
        //void AddStation(BO.Station station);//
        //void UpdateStation(BO.Station station);
        //void UpdateStation(int code, Action<BO.Station> update); //method that knows to updt specific fields in Station
        //void DeleteStation(int code);
        #endregion
    }
}
