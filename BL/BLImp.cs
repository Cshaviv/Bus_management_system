using System;//
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

        #region singelton
        static readonly BLImp instance = new BLImp();
        static BLImp() { }// static ctor to ensure instance init is done just before first usage
        BLImp() { } // default => private
        public static BLImp Instance { get => instance; }// The public Instance property to use
        #endregion
        #region Bus
        /// <summary>
        /// A function that converts the bus from the DO to the BO
        /// </summary>
        /// <param name="busDO">bus</param>
        /// <returns></returns>
        BO.Bus busDoBoAdapter(DO.Bus busDO)
        {
            BO.Bus busBO = new BO.Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that delete bus from the system
        /// </summary>
        /// <param name="licenseNum">Bus license number</param>
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
      /// <summary>
      /// A function that calls to another function in the Dl that get all buses 
      /// </summary>
      /// <returns>list of buses</returns>
        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return from item in dl.GetAllBuses()
                   select busDoBoAdapter(item);
        }
        public IEnumerable<BO.Bus> GetAllDeleteBuses()
        {
            return from item in dl.GetAllDeleteBuses()
                   select busDoBoAdapter(item);
        }
        
        /// <summary>
        /// A function that calls to another function in the Dl that get the license number and returns the bus 
        /// </summary>
        /// <param name="licenseNum"></param>
        /// <returns>bus</returns>
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
        public IEnumerable<Bus> GetBusesBy(Predicate<Bus> predicate)///??
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that update bus details 
        /// </summary>
        /// <param name="busBO">bus</param>
        public void UpdateBusDetails(BO.Bus busBO)
        {
            DO.Bus busDO = new DO.Bus();
            busBO.CopyPropertiesTo(busDO);
            try
            {
                dl.UpdateBus(busDO);
                BusException(busBO);
            }
            catch (DO.BadLicenseNumException ex)
            {
                throw new BO.BadLicenseNumException(ex.licenseNum, ex.Message);
            }
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that add bus to the system
        /// 
        /// </summary>
        /// <param name="busBO">bus</param>
        public void AddBus(BO.Bus busBO)
        {
            DO.Bus busDO = new DO.Bus();
            busBO.CopyPropertiesTo(busDO);
            try
            {
                BusException(busBO);
                dl.AddBus(busDO);
                //BusException(busBO);

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
        /// <summary>
        ///  The function checks the integrity of all bus data
        /// </summary>
        /// <param name="busBO">bus</param>
        public void BusException(BO.Bus busBO)
        {
            if (busBO.StartDate > DateTime.Now)
                throw new BadInputException(1, "תאריך תחילת הפעולה אינו תקין");
            if (busBO.DateLastTreat > DateTime.Now)
                throw new BadInputException(2, "תאריך פעולת הטיפול האחרון אינו תקין");
            if (busBO.TotalKm < 0)
                throw new BadInputException(3, "סך כל הקילומטרים אינו תקין");
            if (busBO.TotalKm < busBO.KmLastTreat)
                throw new BadInputException(4, "סך כל הקילומטרים או הקילומטרים מהטיפול האחרון אינם תקינים");
            if (busBO.TotalKm < busBO.FuelTank)
                throw new BadInputException(5, "סך כל הקילומטרים או הקילומרים מהתדלוק האחרון אינם תקינים ");
            if (busBO.FuelTank < 0 || busBO.FuelTank > 1200)
                throw new BadInputException(5, "הקילומרים מהתדלוק האחרון אינם תקינים");
            int lengthLicNumber = LengthOfLicNum(busBO.LicenseNum);
            if (!((lengthLicNumber == 7 && busBO.StartDate.Year < 2018) || (lengthLicNumber == 8 && busBO.StartDate.Year >= 2018)))
                throw new BadInputException(6, "מספר הרישוי ותאריך התחלת הפעולה אינם מתואמים");
            if (busBO.DateLastTreat > DateTime.Now || busBO.DateLastTreat < busBO.StartDate)
                throw new BadInputException(2, "תאריך הטיפול האחרון אינו תקין");
            if (busBO.KmLastTreat < 0 || busBO.KmLastTreat > busBO.TotalKm)
                throw new BadInputException(4, "הקילומטרים מהטיפול האחרון אינם תקינים");
        }
        /// <summary>
        /// The function checks the length of the license number
        /// </summary>
        /// <param name="licNum">Bus license number</param>
        /// <returns></returns>
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
        /// <summary>
        /// A function that converts the line from the DO to the BO
        /// </summary>
        /// <param name="lineDO">line</param>
        /// <returns>line BO</returns>
        BO.Line lineDoBoAdapter(DO.Line lineDO)
        {
            try
            {
                BO.Line lineBO = new BO.Line();
                lineDO.CopyPropertiesTo(lineBO);
                int id = lineBO.LineId;
                lineBO.Stations = (from stat in dl.GetStationInLineList(s => s.LineId == id && s.IsDeleted == false) //רמחפשים תחנות שעוברות בקו מסוים
                                   select new StationInLine { StationCode = stat.StationCode, Name = GetStation(stat.StationCode).Name, LineStationIndex = stat.LineStationIndex }).ToList();//יוצרים רשימה של כל התחנות שעוברות בקו
                if (lineBO.Stations.Count != 0)// 
                {
                    lineBO.Stations[lineBO.Stations.Count - 1].TimeFromNext = new TimeSpan(0, 0, 0);
                    lineBO.Stations[lineBO.Stations.Count - 1].DistanceFromNext = 0;
                }
                if (lineBO.Stations.Count >= 2)
                {
                    lineBO.FirstStation = lineBO.Stations[0].StationCode;
                    lineBO.LastStation = lineBO.Stations[lineBO.Stations.Count - 1].StationCode;
                }
                for (int i = 0; i < lineBO.Stations.Count - 1; i++)
                {
                    DO.AdjacentStations adjacentStations = dl.GetAdjacentStations(lineBO.Stations[i].StationCode, lineBO.Stations[i + 1].StationCode);
                    lineBO.Stations[i].DistanceFromNext = adjacentStations.Distance;
                    lineBO.Stations[i].TimeFromNext = adjacentStations.Time;
                }
                return lineBO;
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }

        }
        BO.Line DeletedlineDoBoAdapter(DO.Line lineDO)
        {
            try
            {
                BO.Line lineBO = new BO.Line();
                lineDO.CopyPropertiesTo(lineBO);
                int id = lineBO.LineId;
                lineBO.Stations = (from stat in dl.GetStationInLineList(s => s.LineId == id && s.IsDeleted == true) //רמחפשים תחנות שעוברות בקו מסוים
                                   select new StationInLine { StationCode = stat.StationCode, Name = GetStation(stat.StationCode).Name, LineStationIndex = stat.LineStationIndex }).ToList();//יוצרים רשימה של כל התחנות שעוברות בקו
                if (lineBO.Stations.Count != 0)// 
                {
                    lineBO.Stations[lineBO.Stations.Count - 1].TimeFromNext = new TimeSpan(0, 0, 0);
                    lineBO.Stations[lineBO.Stations.Count - 1].DistanceFromNext = 0;
                }
                if (lineBO.Stations.Count >= 2)
                {
                    lineBO.FirstStation = lineBO.Stations[0].StationCode;
                    lineBO.LastStation = lineBO.Stations[lineBO.Stations.Count - 1].StationCode;
                }
                for (int i = 0; i < lineBO.Stations.Count - 1; i++)
                {
                    DO.AdjacentStations adjacentStations = dl.GetAdjacentStations(lineBO.Stations[i].StationCode, lineBO.Stations[i + 1].StationCode);
                    lineBO.Stations[i].DistanceFromNext = adjacentStations.Distance;
                    lineBO.Stations[i].TimeFromNext = adjacentStations.Time;
                }
                return lineBO;
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }

        }
        /// <summary>
        ///  A function that calls to another function in the Dl that get the line id and returns the line
        /// </summary>
        /// <param name="lineId">lineId of line</param>
        /// <returns>line</returns>
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
        /// <summary>
        /// A function that calls to another function in the Dl that add line to the system
        /// </summary>
        /// <param name="lineBo">line</param>
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
                    try
                    {
                        DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = stationCode1, StationCode2 = stationCode2, Distance = lineBo.Stations[0].DistanceFromNext, Time = lineBo.Stations[0].TimeFromNext };
                        dl.AddAdjacentStations(adj);
                    }
                    catch(DO.BadInputException )
                    {
                        
                    }                
                }

                dl.AddLine(lineDo);
                DO.LineStation first = new DO.LineStation() { LineId = lineDo.LineId, StationCode = stationCode1, LineStationIndex = lineBo.Stations[0].LineStationIndex, IsDeleted = false };
                DO.LineStation last = new DO.LineStation() { LineId = lineDo.LineId, StationCode = stationCode2, LineStationIndex = lineBo.Stations[1].LineStationIndex, IsDeleted = false };
                dl.AddStationInLine(first.StationCode, first.LineId, first.LineStationIndex);
                dl.AddStationInLine(last.StationCode, last.LineId, last.LineStationIndex);

            }
            catch (BO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }

        }
        /// <summary>
        /// A function that calls to another function in the Dl that get all lines
        /// </summary>
        /// <returns>list of line</returns>
        public IEnumerable<BO.Line> GetAllLines()
        {
            return from item in dl.GetAllLines()
                   where (item.IsDeleted == false)
                   select lineDoBoAdapter(item);
        }
        public IEnumerable<BO.Line> GetAllDeletedLines()
        {
            return from item in dl.GetAllDeletedLines()
                   where (item.IsDeleted == true)
                   select DeletedlineDoBoAdapter(item);
        }
        public IEnumerable<BO.Line> GetAllLinesInArea(string area)
        {
            return from item in dl.GetAllLinesInArea(area)
                   where (item.IsDeleted == false&&item.Area.ToString()==area)
                   select lineDoBoAdapter(item);
        }
        public IEnumerable<BO.Line> GelAllLinesBy(Predicate<BO.Line> predicate)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that update line details 
        /// </summary>
        /// <param name="line">line</param>
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
        /// <summary>
        /// A function that calls to another function in the Dl that delete line from the system
        /// </summary>
        /// <param name="lineId">lineId of line</param>
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
        /// <summary>
        /// A function that calls to another function in the Dl that add Linestation to the system
        /// </summary>
        /// <param name="lineStat">line station</param>
        public void AddLineStation(BO.LineStation lineStat)
        {
            DO.LineStation lineStatDO = (DO.LineStation)lineStat.CopyPropertiesToNew(typeof(DO.LineStation));
            try
            {
                dl.AddLineStation(lineStatDO);
                List<DO.LineStation> lst = ((dl.GetAllLineStationsBy(stat => stat.LineId == lineStatDO.LineId && stat.IsDeleted == false)).OrderBy(stat => stat.LineStationIndex)).ToList();

                //DO.LineStation prev = lst[s.LineStationIndex - 2];
                //DO.LineStation next = lst[s.LineStationIndex + 1];
                if (lineStat.LineStationIndex != 1)//if its the first station- it doesnt have prev
                {
                    DO.LineStation prev = lst[lineStat.LineStationIndex - 2];
                    if (!IsExistAdjacentStations(prev.StationCode, lineStat.StationCode))
                    {
                        DO.AdjacentStations adjPrev = new DO.AdjacentStations() { StationCode1 = prev.StationCode, StationCode2 = lineStat.StationCode };
                        dl.AddAdjacentStations(adjPrev);
                    }
                }
                if (lineStat.LineStationIndex != lst[lst.Count - 1].LineStationIndex)//if its the last station- it doesnt have next
                {
                    DO.LineStation next = lst[lineStat.LineStationIndex];
                    if (!IsExistAdjacentStations(lineStat.StationCode, next.StationCode))
                    {
                        DO.AdjacentStations adjNext = new DO.AdjacentStations() { StationCode1 = lineStat.StationCode, StationCode2 = next.StationCode };
                        dl.AddAdjacentStations(adjNext);
                    }
                }

            }
            catch (Exception)
            {
                throw new Exception();
            }
        }
        
        public void DeleteLineStation(int lineId, int stationCode)//?? חסר חריגה?
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
        /// <summary>
        /// The function checks if it exists AdjacentStation for the two adjacent stations
        /// </summary>
        /// <param name="stationCode1">code of station 1</param>
        /// <param name="stationCode2">code of station 2</param>
        /// <returns>true if exist, else return false</returns>
        public bool IsExistAdjacentStations(int stationCode1, int stationCode2)
        {
            if (dl.ExistAdjacentStations(stationCode1, stationCode2))
                return true;
            return false;
        }

        #endregion

        #region Station
        /// <summary>
        ///  A function that converts the station from the DO to the BO
        /// </summary>
        /// <param name="stationDO">station</param>
        /// <returns>stationBO</returns>
        public BO.Station StationDoBoAdapter(DO.Station stationDO)
        {
            try
            {
                BO.Station stationBO = new BO.Station();
                int stationCode = stationDO.Code;
                stationDO.CopyPropertiesTo(stationBO);
                //stationBO.LinesInStation = (from stat in dl.GetAllLineStationsBy(stat => stat.StationCode == stationCode && stat.IsDeleted == false)//Linestation
                //                            let line = dl.GetLine(stat.LineId)//line
                //                            select line.CopyToLineInStation(stat)).ToList();
                //return stationBO;
                stationBO.LinesInStation = (from l in dl.GetAllLineStationsBy(l => l.StationCode == stationBO.Code && l.IsDeleted == false)
                                            let line = dl.GetLine(l.LineId)
                                            where line != null && dl.GetStation(line.LastStation)!=null
                                            select new LineInStation { LineNum = line.LineNum, LineId = l.LineId, TargetStation = dl.GetStation(line.LastStation).Name }).ToList();
                return stationBO;
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }

            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.code, ex.Message);

            }
        }
        public BO.Station DeletedStationDoBoAdapter(DO.Station stationDO)
        {
            try
            {
                BO.Station stationBO = new BO.Station();
                int stationCode = stationDO.Code;
                stationDO.CopyPropertiesTo(stationBO);
                //stationBO.LinesInStation = (from stat in dl.GetAllLineStationsBy(stat => stat.StationCode == stationCode && stat.IsDeleted == false)//Linestation
                //                            let line = dl.GetLine(stat.LineId)//line
                //                            select line.CopyToLineInStation(stat)).ToList();
                //return stationBO;
                stationBO.LinesInStation = (from l in dl.GetAllLineStationsBy(l => l.StationCode == stationBO.Code && l.IsDeleted == true)
                                            let line = dl.GetLine(l.LineId)
                                            where line != null
                                            select new LineInStation { LineNum = line.LineNum, LineId = l.LineId, TargetStation = dl.GetStation(line.LastStation).Name }).ToList();
                return stationBO;
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }

            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.code, ex.Message);

            }
        }

        /// <summary>
        /// A function that calls to another function in the Dl that get all stations 
        /// </summary>
        /// <returns>list of station</returns>
        public IEnumerable<BO.Station> GetAllStations()
        {
            return from item in dl.GetAllStations()
                   select StationDoBoAdapter(item);
        }
        public IEnumerable<BO.Station> GetAllDeletedStations()
        {
            return from item in dl.GetAllDeletedStations()
                   select DeletedStationDoBoAdapter(item);
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that get the station code and returns the station 
        /// </summary>
        /// <param name="code">code of station</param>
        /// <returns>station</returns>
        public BO.Station GetStation(int code)
        {
            DO.Station station;
            try
            {
                station = dl.GetStation(code);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(0, "התחנה לא קיימת במערכת");
            }
            return StationDoBoAdapter(station);
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that add station to the system
        /// </summary>
        /// <param name="station">station</param>
        public void AddStation(BO.Station station)
        {
            DO.Station stationDO = new DO.Station();
            station.CopyPropertiesTo(stationDO);
            try
            {
                dl.AddStation(stationDO);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }

        }
        /// <summary>
        /// A function that calls to another function in the Dl that delete station from the system
        /// </summary>
        /// <param name="statCode">code of station</param>
        public void DeleteStation(int statCode)
        {
            try
            {
                DO.Station statDO = dl.GetStation(statCode);
                BO.Station statBO = StationDoBoAdapter(statDO);
                if (statBO.LinesInStation.Count == 0)
                    dl.DeleteStation(statCode);
                else
                    throw new BO.BadStationCodeException(statCode, "לא ניתן למחוק את התחנה כיוון שיש קווים שעוברים בה");


            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }
        }
        /// <summary>
        /// A function that calls to another function in the Dl that update station details
        /// </summary>
        /// <param name="stationBO">station</param>
        public void UpdateStation(BO.Station stationBO)
        {
            DO.Station stationDO = new DO.Station();
            stationBO.CopyPropertiesTo(stationDO);
            try
            {
                dl.UpdateStation(stationDO);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }

        }
        public bool IsAdjacentStat(int code1, int code2)
        {
            try
            {
                dl.GetAdjacentStations(code1, code2);
                return true;
            }
            catch (DO.BadLicenseNumException)
            { 
            return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IEnumerable<LineTiming> GetLineTimingPerStation(BO.Station stationBO, TimeSpan currentTime)
        {
            //list of lines that pass in the station
            List<BO.Line> listLines = (from l in GetAllLines()
                                       where l.Stations.Find(s => s.StationCode == stationBO.Code) != null
                                       select l).ToList();

            List<LineTiming> times = new List<LineTiming>();//list of the lines that the function will return
            TimeSpan hour = new TimeSpan(1, 0, 0);//help to find the times that in the range of one hour from currentTime                           
            for (int i = 0; i < listLines.Count(); i++)//for all the lines that pass in the station
            {//calculate the times 
                TimeSpan tmp;//the current time
                int currentLineid = listLines[i].LineId;// line id of the current line
                List<DO.LineTrip> lineSchedual = dl.GetAllLineTripsBy(trip => trip.LineId == currentLineid && trip.IsDeleted == false).ToList();// times of the current Line
                TimeSpan timeTilStatin = travelTime(stationBO.Code, currentLineid);
                int numOfTimes = 0;
                List<int> timesOfCurrentLine = new List<int>();
                for (int j = 0; j < lineSchedual.Count && numOfTimes < 3; j++)//for all the times in line sSchedual
                {
                    //check if currentTime-LeavingTime-travelTime more than zero and in the range of hour
                    if (lineSchedual[j].StartAt + timeTilStatin <= currentTime + hour
                        && lineSchedual[j].StartAt + timeTilStatin >= currentTime)
                    //check if the bus already passed the statioin   
                    {
                        if (currentTime - lineSchedual[j].StartAt >= TimeSpan.Zero)//if the line already get out from the station
                        {
                            tmp = timeTilStatin - (currentTime - lineSchedual[j].StartAt);
                        }
                        else//if the line didnt get out from the station
                            tmp = timeTilStatin + (lineSchedual[j].StartAt - currentTime);

                        timesOfCurrentLine.Add(tmp.Minutes);
                        //timesString = timesString + tmp.Minutes+ ", ";                                                                                                  
                        numOfTimes++;
                    }
                }

                if (timesOfCurrentLine.Count != 0)
                {
                    string timesString = "";//the string of times
                    timesOfCurrentLine = timesOfCurrentLine.OrderBy(s => s).ToList();//order the times in ascending order
                    for (int k = 0; k < timesOfCurrentLine.Count - 1; k++)
                    {
                        timesString = timesString + timesOfCurrentLine[k] + ", ";
                    }
                    timesString = timesString + timesOfCurrentLine[timesOfCurrentLine.Count - 1];//add the last one without ","
                    times.Add(new LineTiming
                    {
                        LineId = currentLineid,
                        LineNum = listLines[i].LineNum,
                        DestinationStation = listLines[i].Stations[listLines[i].Stations.Count() - 1].Name,
                        Stringtimes = timesString,
                    }) ;

                }
                numOfTimes = 0;
                //timesString = "";
            }
            times = times.OrderBy(lt => lt.LineNum).ToList();//order the list by the number of the lines in ascending order
            return times;

        }
        private TimeSpan travelTime(int stationCode, int lineID)
        {//func that return the time from first station in line to specific station
            TimeSpan sumTime = TimeSpan.Zero;
            BO.Line line = GetLine(lineID);
            foreach (var s in line.Stations)
            {
                if (s.StationCode != stationCode)
                    sumTime += s.TimeFromNext;
                else
                {
                    //sumTime += s.TimeFromPrevStation;
                    break;
                }
            }
            return sumTime;
        }
        #endregion

        #region StationInLine
        /// <summary>
        /// A function that calls to another function in the Dl that add stationInLine to line in the system
        /// </summary>
        /// <param name="stationCode">code of station</param>
        /// <param name="lineID">line id</param>
        /// <param name="index">index-The index to which you want to add the station</param>
        /// <param name="nextStatCode">code of next station</param>
        /// <param name="prevStatCode">code of prev station</param>
        /// <param name="distanceNext"><distance to next station/param>
        /// <param name="timeNext">time to next station</param>
        /// <param name="distancePrev">distance from prev station</param>
        /// <param name="timePrev">time from prev station</param>
        public void AddStationInLine(int stationCode, int lineID, int index, int nextStatCode, int prevStatCode, double distanceNext, TimeSpan timeNext, double distancePrev, TimeSpan timePrev)
        {
            try
            {
                if (index == 0)
                {
                    if (!dl.ExistAdjacentStations(stationCode, nextStatCode))
                    {
                        DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = stationCode, StationCode2 = nextStatCode, Distance = distanceNext, Time = timeNext };
                        dl.AddAdjacentStations(adj);
                    }
                }
                else if (index >= GetLine(lineID).Stations.Count - 1)
                {
                    if (!dl.ExistAdjacentStations(prevStatCode, stationCode))
                    {
                        DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = prevStatCode, StationCode2 = stationCode, Distance = distancePrev, Time = timePrev };
                        dl.AddAdjacentStations(adj);
                    }
                }
                else
                {
                    if (!dl.ExistAdjacentStations(stationCode, nextStatCode))
                    {
                        DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = stationCode, StationCode2 = nextStatCode, Distance = distanceNext, Time = timeNext };
                        dl.AddAdjacentStations(adj);
                    }
                    if (!dl.ExistAdjacentStations(prevStatCode, stationCode))
                    {
                        DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = prevStatCode, StationCode2 = stationCode, Distance = distancePrev, Time = timePrev };
                        dl.AddAdjacentStations(adj);
                    }
                }
                dl.AddStationInLine(stationCode, lineID, index);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that update time and distance of station in line
        /// </summary>
        /// <param name="first">first StationInLine </param>
        /// <param name="second">second StationInLine</param>
        public void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second)
        {
            try
            {
                DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = first.StationCode, StationCode2 = second.StationCode, Distance = first.DistanceFromNext, Time = first.TimeFromNext, IsDeleted = false };
                dl.UpdateAdjacentStations(adj);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("מצטערים, לא היה ניתן לעדכן שדה זה");
            }
        }
        /// <summary>
        /// A function that calls to another function in the Dl that delete stationInLine from line
        /// </summary>
        /// <param name="lineID">line id</param>
        /// <param name="code">scode of station</param>
        public void DeleteStationInLine(int lineID, int code)
        {
            BO.Line line = new BO.Line();
            line = GetLine(lineID);

            int index = GetLine(lineID).Stations.FindIndex(s => s.StationCode == code);
            try
            {
                if (line.Stations.Count <= 2)
                {
                    throw new BO.BadStationCodeException(code, "בקו זה אין מספיק תחנות ולכן אין אפשרות למחוק תחנה זו");
                }
                if ((index == 0) || (index == line.Stations.Count - 1))
                {
                    dl.DeleteStationInLine(lineID, code);
                    return;
                }
                int statCode1 = line.Stations[index - 1].StationCode;
                int statCode2 = line.Stations[index + 1].StationCode;
                if (!dl.ExistAdjacentStations(statCode1, statCode2))
                {
                    DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = statCode1, StationCode2 = statCode2, Distance = 0, Time = new TimeSpan(0, 0, 0) };
                    dl.AddAdjacentStations(adj);
                }
                dl.DeleteStationInLine(lineID, code);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.code, " קו האוטובוס או התחנה לא זמינים");
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, "קו אוטובוס זה לא עובר בתחנה זו");
            }
        }
        public void UpdateTandDinAdjacentStation(int code1, int code2, double distanceFromNext, TimeSpan timeFromNext)
        {
            try
            {
                DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = code1, StationCode2 = code2, Distance = distanceFromNext, Time = timeFromNext, IsDeleted = false };
                dl.UpdateTandDinAdjacentStation(adj);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }
            catch (Exception)
            {
                throw new Exception("מצטערים, לא היה ניתן לעדכן שדה זה");
            }
            
        }
        #endregion

         #region user
        /// <summary>
        ///  A function that calls to another function in the Dl that get the username and password and returns the user
        /// </summary>
        /// <param name="username"> user name</param>
        /// <param name="passcode">password code</param>
        /// <returns>user</returns>
        public BO.User SignIn(string username, string passcode)
        {
            BO.User userBo;
            try
            {
                DO.User userDo = dl.GetUser(username);
                if (passcode != userDo.passCode)
                    throw new BO.BadUserException("סיסמא לא נכונה");
                userBo = new BO.User();
                userDo.CopyPropertiesTo(userBo);
            }
            catch (Exception ex)
            {
                throw new BO.BadUserException(ex.Message);
            }
            return userBo;
        }
        /// <summary>
        ///  A function that calls to another function in the Dl that add user to the system
        /// </summary>
        /// <param name="userBo">user</param>
        public void addNewUser(BO.User userBo)
        {
            try
            {
                DO.User UserDo = new DO.User();
                userBo.CopyPropertiesTo(UserDo);
                dl.AddUser(UserDo);
            }
            catch (DO.BadUserException ex)
            {
                throw new BO.BadUserException(ex.Message);
            }
        }

        #endregion

        #region LineTrip
        public void DeleteDepTime(int lineId, TimeSpan dep)
        {
            try
            {
                dl.DeleteLineTrip(lineId, dep);
            }
            catch (DO.BadLineTripException ex)
            {
                throw new BO.BadLineTripException(ex.Message);
            }
        }
        public void AddDepTime(int lineId, TimeSpan dep)
        {
            try
            {
                dl.AddLineTrip(new DO.LineTrip() { LineId = lineId, StartAt = dep, IsDeleted = false });
            }
            catch (DO.BadLineTripException ex)
            {
                throw new BO.BadLineTripException(ex.Message);
            }
        }

        #endregion

        #region Trip
        
        private int IsStationFound(BO.Line line, int stationCode)
        {
            foreach (BO.StationInLine stat in line.Stations)
            {
                if (stat.StationCode == stationCode)
                    return stat.LineStationIndex;
            }
            return -1;
        }
        private TimeSpan TimeTravel(BO.Line line, int stationCode1, int stationCode2)
        {
            int index1 = IsStationFound(line, stationCode1);
            int index2 = IsStationFound(line, stationCode2);
            TimeSpan sum = TimeSpan.Zero;
            for (int i = index1 - 1; i < index2 - 1; i++)
            {
                sum += line.Stations[i].TimeFromNext;
            }
            return sum;
        }
        public List<string> FindRoute(int stationCode1, int stationCode2)
        {
            if (stationCode1 == stationCode2)
                throw new BO.BadInputException("The source station and the destination station must be different");
            List<string> linesInRoute = new List<string>();//list of lines with line number and time travel
            List<BO.Line> linesInRouteTemp = new List<BO.Line>();//list of lines that pass on the two stations-temp list
            List<BO.Line> allLines = GetAllLines().ToList();//all lines
            foreach (BO.Line line in allLines)
            {

                int index1 = IsStationFound(line, stationCode1);
                int index2 = IsStationFound(line, stationCode2);
                if (index1 != -1 && index2 != -1 && index1 < index2)
                {
                    linesInRouteTemp.Add(line);
                }
            }
            if (linesInRouteTemp.Count == 0)
                throw new BO.BadInputException("There are no lines in this route");
            linesInRouteTemp = linesInRouteTemp.OrderBy(l => TimeTravel(l, stationCode1, stationCode2)).ToList();
            foreach (BO.Line line in linesInRouteTemp)
            {
                linesInRoute.Add(line.LineNum + "\t" + TimeTravel(line, stationCode1, stationCode2));
            }
            return linesInRoute;
        }

       
        #endregion


    }
}
