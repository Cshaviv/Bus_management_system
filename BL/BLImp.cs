using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLAPI;
using DLAPI;
using BO;
using System.Threading;

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
        public IEnumerable<Bus> GetBusesBy(Predicate<Bus> predicate)
        {
            throw new NotImplementedException();
        }
        public void UpdateBusDetails(BO.Bus bus)
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
        public void AddBus(BO.Bus bus)
        {
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);
            try
            {
                if (bus.StartDate > DateTime.Now)
                    throw new BadInputException("The date of start operation is not valid");
                if (bus.TotalKm < 0)
                    throw new BadInputException("The total km is not valid");
                if (bus.TotalKm < bus.KmLastTreat)
                    throw new BadInputException("The total km or km last treat are not correct");
                if (bus.TotalKm < bus.FuelTank)
                    throw new BadInputException("The total km or fuel Tank treat are not correct");
                if (bus.FuelTank < 0 || bus.FuelTank > 1200)
                    throw new BadInputException("The fuel tank is not valid");
                int lengthLicNumber = LengthOfLicNum(bus.LicenseNum);
                if (!((lengthLicNumber == 7 && bus.StartDate.Year < 2018) || (lengthLicNumber == 8 && bus.StartDate.Year >= 2018)))
                    throw new BadInputException("The license number and the date of start operation do not match");
                if (bus.DateLastTreat > DateTime.Now || bus.DateLastTreat < bus.StartDate)
                    throw new BadInputException("The date of last treatment is not valid");
                if (bus.KmLastTreat < 0 || bus.KmLastTreat > bus.TotalKm)
                    throw new BadInputException("The kilometrage of last treatment is not valid");
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
        private int LengthOfLicNum(int licNum)// This function returns the number of digits in the license number
        {
            int counter = 0;
            while (licNum != 0)
            {
                licNum = licNum / 10;
                counter++;
            }
            return counter;
        }
        #endregion

        #region Line
        BO.Line lineDoBoAdapter(DO.Line lineDO)
        {
            BO.Line lineBO = new BO.Line();
            lineDO.CopyPropertiesTo(lineBO);
            int id = lineBO.LineId;
            lineBO.Stations = (from s in dl.GetStationInLineList(s => s.LineId == id) //רמחפשים תחנות שעוברות בקו מסוים
                                     select new StationInLine { StationCode = s.StationCode, Name = GetStation(s.StationCode).Name, LineStationIndex=s.LineStationIndex, DistanceFromNext=s.DistanceFromNext, TimeFromNext=s.TimeFromNext }).ToList();//יוצרים רשימה של כל התחנות שעוברות בקו
            if (lineBO.Stations.Count != 0)// 
            {
                lineBO.Stations[lineBO.Stations.Count-1].TimeFromNext = new TimeSpan(0, 0, 0);
                lineBO.Stations[lineBO.Stations.Count-1].DistanceFromNext = 0;
            }
            for (int i = 0; i < lineBO.Stations.Count - 1; i++)
            {
                DO.AdjacentStations adjacentStations = dl.GetAdjacentStations(lineBO.Stations[i].StationCode, lineBO.Stations[i + 1].StationCode);
                lineBO.Stations[i].DistanceFromNext = adjacentStations.Distance;
                lineBO.Stations[i].TimeFromNext = adjacentStations.Time;
            }
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
        public void AddNewLine(BO.Line lineBo)
        {
            DO.Line lineDo = new DO.Line();
            lineBo.CopyPropertiesTo(lineDo);
            lineDo.LineId = DO.Config.LineId++;
            int stationCode1 = lineBo.Stations[0].StationCode;//stationCode of the first station
            int stationCode2 = lineBo.Stations[1].StationCode;//station Code of the last station
            lineDo.FirstStation = stationCode1;
            lineDo.LastStation = stationCode2;
            try
            {
                if (!dl.ExistAdjacentStations(stationCode1, stationCode2))
                {
                    DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = stationCode1, StationCode2 = stationCode2, Distance = lineBo.Stations[0].DistanceFromNext, Time = lineBo.Stations[0].TimeFromNext };
                    dl.AddAdjacentStations(adj);
                }

                dl.AddLine(lineDo);
                DO.LineStation first = new DO.LineStation() { LineId = lineDo.LineId, StationCode = stationCode1, LineStationIndex = lineBo.Stations[0].LineStationIndex, IsDeleted = false };
                DO.LineStation last = new DO.LineStation() { LineId = lineDo.LineId, StationCode = stationCode2, LineStationIndex = lineBo.Stations[1].LineStationIndex, IsDeleted = false };
                dl.AddStationInLine(first.StationCode,first.LineId,first.LineStationIndex);
                dl.AddStationInLine(last.StationCode, last.LineId, last.LineStationIndex);

            }
            catch (BO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }

        }
        public IEnumerable<BO.Line> GetAllLines()
        {
            return from item in dl.GetAllLines()
                   select lineDoBoAdapter(item);
        }
        public IEnumerable<BO.Line> GelAllLinesBy(Predicate<BO.Line> predicate)
        {
            throw new NotImplementedException();
        }
        public void UpdateLineDetails(BO.Line line)
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

        #region LineStation לא ברור

        public void AddLineStation(BO.LineStation s)
        {
            DO.LineStation sDO = (DO.LineStation)s.CopyPropertiesToNew(typeof(DO.LineStation));
            try
            {
                dl.AddLineStation(sDO);
                List<DO.LineStation> lst = ((dl.GetAllLineStationsBy(stat => stat.LineId == sDO.LineId && stat.IsDeleted == false)).OrderBy(stat => stat.LineStationIndex)).ToList();
                //lst.Order

                //DO.LineStation prev = lst[s.LineStationIndex - 2];
                //DO.LineStation next = lst[s.LineStationIndex + 1];
                if (s.LineStationIndex != 1)//if its the first station- it doesnt have prev
                {
                    DO.LineStation prev = lst[s.LineStationIndex - 2];
                    if (!IsExistAdjacentStations(prev.StationCode, s.StationCode))
                    {
                        DO.AdjacentStations adjPrev = new DO.AdjacentStations() { StationCode1 = prev.StationCode, StationCode2 = s.StationCode };
                        dl.AddAdjacentStations(adjPrev);
                    }
                }
                if (s.LineStationIndex != lst[lst.Count - 1].LineStationIndex)//if its the last station- it doesnt have next
                {
                    DO.LineStation next = lst[s.LineStationIndex];
                    if (!IsExistAdjacentStations(s.StationCode, next.StationCode))
                    {
                        DO.AdjacentStations adjNext = new DO.AdjacentStations() { StationCode1 = s.StationCode, StationCode2 = next.StationCode };
                        dl.AddAdjacentStations(adjNext);
                    }
                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        public void DeleteLineStation(int lineId, int stationCode)
        {
            try
            {
                dl.DeleteLineStation(lineId, stationCode);
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region AdjacentStations
        public bool IsExistAdjacentStations(int stationCode1, int stationCode2)
        {
            if (dl.ExistAdjacentStations(stationCode1, stationCode2))
                return true;
            return false;
        }
        #endregion

        #region Station
        public BO.Station stationDoBoAdapter(DO.Station stationDO)
        {
            BO.Station stationBO = new BO.Station();
            int stationCode = stationDO.Code;
            stationDO.CopyPropertiesTo(stationBO);
            //lineBO.Stations = from stat in dl.GetAllLineStationsBy(stat => stat.LineId == lineId)//Linestation
            //                                         let station = dl.GetStation(stat.StationCode)//station
            //                                         select (BO.StationInLine)station.CopyPropertiesToNew(typeof(BO.StationInLine));
            stationBO.Lines = (from stat in dl.GetAllLineStationsBy(stat => stat.StationCode == stationCode && stat.IsDeleted == false)//Linestation
                               let line = dl.GetLine(stat.LineId)//station
                               select line.CopyToLineInStation(stat)).ToList();
            //select (BO.StationInLine)station.CopyPropertiesToNew(typeof(BO.StationInLine));
            //stationBO. = stations.OrderBy(s => s.LineStationIndex);
            return stationBO;
        }
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                   select stationDoBoAdapter(item);
        }
        public BO.Station GetStation(int code)
        {
            DO.Station station;
            try
            {
                station = dl.GetStation(code);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(0,"station code does not exist or he is not a student");
            }
            return stationDoBoAdapter(station);
        }
        #endregion
      public  void AddStation(BO.Station station )
        {
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            try
            {
                dl.AddStation(stationDO);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException( ex.stationCode , ex.Message);
            }
        }

        #region StationInLine
        public void AddStationInLine(int stationID, int busID, int index)
        {
            try
            {
                dl.AddAdjacentStations(new DO.AdjacentStations { StationCode1 = GetLine(busID).Stations[index - 1].StationCode, StationCode2 = stationID, Distance = 0, Time = new TimeSpan(0, 0, 0) });
                if (!(index >= GetLine(busID).Stations.Count))
                    dl.AddAdjacentStations(new DO.AdjacentStations { StationCode1 = stationID, StationCode2 = GetLine(busID).Stations[index].StationCode, Distance = 0, Time = new TimeSpan(0, 0, 0) });
                dl.AddStationInLine(stationID, busID, index);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException("Station code and line ID is Not exist");
            }
        }
        public void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second)
        {
            try
            {
                DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = first.StationCode, StationCode2 = second.StationCode, Distance = second.DistanceFromNext, Time = second.TimeFromNext, IsDeleted = false };
                dl.UpdateAdjacentStations(adj);
            }
            catch (Exception ex)
            {
                throw new Exception("Error, it cannot be update");
            }
        }
        #endregion


    }




}
