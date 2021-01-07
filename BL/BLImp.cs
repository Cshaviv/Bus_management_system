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
        //private int LengthOfLicenseNumber(int licNum)// This function returns the number of digits in the license number
        //{
        //    int counter = 0;
        //    while (licNum != 0)
        //    {
        //        licNum = licNum / 10;
        //        counter++;
        //    }
        //    return counter;
        //}
        //public void CheckLicNum(BO.Bus bus)
        //{ 
        //    if (bus.StartDate > DateTime.Now)
        //        throw new BadInputException("The date of start operation is not valid");
        //    if (bus.TotalKm < 0)
        //        throw new BadInputException("The total trip is not valid");
        //    if (bus.kmAfterRefuling < 0 || bus.kmAfterRefuling > 1200)
        //        throw new BadInputException("The fuel remain is not valid");
        //    int lengthLicNum = LengthOfLicenseNumber(bus.LicenseNum);
        //    if (!((lengthLicNum == 7 && bus.StartDate.Year < 2018) || (lengthLicNum == 8 && bus.StartDate.Year >= 2018)))
        //        throw new BadInputException("The license number and the date of start operation do not match");
        //    if (bus.DateLastTreat > DateTime.Now || bus.DateLastTreat < bus.StartDate)
        //        throw new BadInputException("The date of last treatment is not valid");
        //    if (bus.KmLastTreat < 0 || bus.KmLastTreat > bus.TotalKm)
        //        throw new BadInputException("The kilometrage of last treatment is not valid");

        //}
        public void AddBus(BO.Bus bus)
        {
            //CheckLicNum(bus);
            DO.Bus busDO = new DO.Bus();
            bus.CopyPropertiesTo(busDO);

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
            List<BO.StationInLine> stations = (from stat in dl.GetAllLineStationsBy(stat => stat.LineId == lineId && stat.IsDeleted == false)//Linestation
                                               let station = dl.GetStation(stat.StationCode)//station
                                               select station.CopyToStationInLine(stat)).ToList();
            stations = (stations.OrderBy(s => s.LineStationIndex)).ToList();
            foreach (StationInLine s in stations)
            {
                if (s.LineStationIndex != stations[stations.Count - 1].LineStationIndex)
                {
                    int sc1 = s.StationCode;//station code 1
                    int sc2 = stations[s.LineStationIndex].StationCode;//station code 2
                    DO.AdjacentStations adjStat = dl.GetAdjacentStations(sc1, sc2);
                    s.Distance = adjStat.Distance;
                    s.Time = adjStat.Time;
                }
            }
            lineBO.Stations = stations;
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
            int sc1 = lineBo.Stations[0].StationCode;//stationCode of the first station
            int sc2 = lineBo.Stations[1].StationCode;//station Code of the last station
            lineDo.FirstStation = sc1;
            lineDo.LastStation = sc2;
            try
            {
                if (!dl.ExistAdjacentStations(sc1, sc2))
                {
                    DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = sc1, StationCode2 = sc2, Distance = lineBo.Stations[0].Distance, Time = lineBo.Stations[0].Time };
                    dl.AddAdjacentStations(adj);
                }

                dl.AddLine(lineDo);
                DO.LineStation first = new DO.LineStation() { LineId = lineDo.LineId, StationCode = sc1, LineStationIndex = lineBo.Stations[0].LineStationIndex, IsDeleted = false };
                DO.LineStation last = new DO.LineStation() { LineId = lineDo.LineId, StationCode = sc2, LineStationIndex = lineBo.Stations[1].LineStationIndex, IsDeleted = false };
                dl.AddLineStation(first);
                dl.AddLineStation(last);

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

      
    }
    #endregion
    #region Station
    #endregion


}






