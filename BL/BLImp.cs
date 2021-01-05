using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using DLAPI;
using BO;

namespace BL
{
    class BLImp : IBL
    {
        IDL dl = DLFactory.GetDL();

        #region Bus
        BO.Bus busDoBoAdapter(DO.Bus busDO)
        {
            BO.Bus busBO = new BO.Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }
        public void DeleteBus(int licenseNum)
        {
            try
            {
                dl.DeleteBus(licenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
            }
        }

        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return from item in dl.GetAllBuses()
                   select busDoBoAdapter(item);
        }

        public Bus GetBus(int licenseNum)
        {
            DO.Bus busDO;
            try
            {
                busDO = dl.GetBus(licenseNum);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
            }
            return busDoBoAdapter(busDO);
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)//?
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            try
            {
                dl.UpdateBus(busDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }
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
        public void CheckLicNum(BO.Bus bus)
        { 
            if (bus.StartDate > DateTime.Now)
                throw new BadInputException("The date of start operation is not valid");
            if (bus.TotalKm < 0)
                throw new BadInputException("The total trip is not valid");
            if (bus.kmAfterRefuling < 0 || bus.kmAfterRefuling > 1200)
                throw new BadInputException("The fuel remain is not valid");
            int lengthLicNum = LengthOfLicenseNumber(bus.LicenseNum);
            if (!((lengthLicNum == 7 && bus.StartDate.Year < 2018) || (lengthLicNum == 8 && bus.StartDate.Year >= 2018)))
                throw new BadInputException("The license number and the date of start operation do not match");
            if (bus.DateLastTreat > DateTime.Now || bus.DateLastTreat < bus.StartDate)
                throw new BadInputException("The date of last treatment is not valid");
            if (bus.KmLastTreat < 0 || bus.KmLastTreat > bus.TotalKm)
                throw new BadInputException("The kilometrage of last treatment is not valid");
           
        }
        public void AddBus(BO.Bus bus)
        {
            CheckLicNum(bus);
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            //CheckLicNum(busDo)
            //if (bus.StartDate > DateTime.Now)
            //    throw new BadInputException("The date of start operation is not valid");
            //if (bus.TotalKm < 0)
            //    throw new BadInputException("The total trip is not valid");
            //if (bus.FuelTank < 0 || bus.FuelTank > 1200)
            //    throw new BadInputException("The fuel remain is not valid");
            //int lengthLicNum = LengthOfLicenseNumber(bus.LicenseNum);
            //if (!((lengthLicNum == 7 && bus.StartDate.Year < 2018) || (lengthLicNum == 8 && bus.StartDate.Year >= 2018)))
            //    throw new BadInputException("The license number and the date of start operation do not match");
            //if (bus.DateLastTreat > DateTime.Now || bus.DateLastTreat < bus.StartDate)
            //    throw new BadInputException("The date of last treatment is not valid");
            //if (bus.KmLastTreat < 0 || bus.KmLastTreat > bus.TotalKm)
            //    throw new BadInputException("The kilometrage of last treatment is not valid");

            try
            {
                dl.AddBus(busDO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }
        }
        #endregion

        #region Line
        BO.Line lineDoBoAdapter(DO.Line lineDO)
        {
            BO.Line lineBO = new BO.Line();
            int lineId = lineDO.LineId;
            lineDO.CopyPropertiesTo(lineBO);
            lineBO.stations = from stat in dl.GetAllLineStationsBy(stat => stat.LineId == lineId)
                              let station = dl.GetStation(stat.StationCode)
                              //select station.CopyToStudentCourse(stat);
                              select (BO.StationInLine)station.CopyPropertiesToNew(typeof(BO.StationInLine));
            return lineBO;
        }
        public Line GetLine(int lineId)
        {
            DO.Line lineDO;
            try
            {
                lineDO = dl.GetLine(lineId);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }
            return lineDoBoAdapter(lineDO);
        }

        public IEnumerable<BO.Line> GetAllLines()
        {
            return from item in dl.GetAllLines()
                   select lineDoBoAdapter(item);
        }

        public IEnumerable<BO.Line> GetAllLinesBy(Predicate<BO.Line> predicate)
        {
            throw new NotImplementedException();
        }

        public void UpdateLine(BO.Line line)
        {
            DO.Line lineDO = new DO.Line();
            line.CopyPropertiesTo(lineDO);
            try
            {
                dl.UpdateLine(lineDO);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }
        }

        public void DeleteLine(int lineId)
        {
            try
            {
                dl.DeleteLine(lineId);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }

        }
        #endregion
        #region Station
        BO.Station StationDoBoAdapter(DO.Station stationDO)
        {
            BO.Station stationBO = new BO.Station();
            int code = stationDO.Code;
            stationDO.CopyPropertiesTo(stationBO);
            stationBO.Lines = from line in dl.GetAllLineStationsBy(line => line.LineId == code)
                              let station = dl.GetStation(line.StationCode)
                              //select station.CopyToStudentCourse(stat);
                              select (BO.LineInStation)line.CopyPropertiesToNew(typeof(BO.LineInStation));
            return stationBO;
        }
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                   select StationDoBoAdapter(item);
        }
        IEnumerable<BO.Station> GetAllStationsBy(Predicate<BO.Station> predicate)
        {
            throw new NotImplementedException();
        }
        public BO.Station GetStation(int code)
        {
            DO.Station stationDO;
            try
            {
                stationDO = dl.GetStation(code);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.code, ex.Message);
            }
            return StationDoBoAdapter(stationDO);
        }
        public void AddStation(BO.Station station)
        {

        }
        public void UpdateStation(BO.Station station)
        {

        }
        public void DeleteStation(int code)
        {

        }

        IEnumerable<Station> IBL.GetAllStationsBy(Predicate<Station> predicate)
        {
            throw new NotImplementedException();
        }
        #endregion

    }

}

      

 
