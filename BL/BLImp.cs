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
        /// <summary>
        /// A function that converts the bus from the DO to the BO
        /// </summary>
        /// <param name="busDO"></param>
        /// <returns></returns>
        BO.Bus busDoBoAdapter(DO.Bus busDO)
        {
            BO.Bus busBO = new BO.Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }//yes
        /// <summary>
        /// A function that delete bus 
        /// </summary>
        /// <param name="licenseNum">Bus license number</param>
        public void DeleteBus(int licenseNum)//yes
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
        /// A function that calls to another function in the Dl that get all busses 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return from item in dl.GetAllBuses()
                   select busDoBoAdapter(item);
        }//yes
        /// <summary>
        /// A function that calls to another function in the Dl that get the license number and returns the bus 
        /// </summary>
        /// <param name="licenseNum"></param>
        /// <returns></returns>
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
        public void UpdateBusDetails(BO.Bus busBO)//yes
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
        public void AddBus(BO.Bus busBO)//yes
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
        ///  
        /// </summary>
        /// <param name="busBO"></param>
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
        /// 
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

        }//yes
        public Line GetLine(int lineId)//yes
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
        public void AddNewLine(BO.Line lineBo)//yes
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
        public IEnumerable<BO.Line> GetAllLines()//yes
        {
            return from item in dl.GetAllLines()
                   where (item.IsDeleted == false)
                   select lineDoBoAdapter(item);
        }
        public IEnumerable<BO.Line> GelAllLinesBy(Predicate<BO.Line> predicate)
        {
            throw new NotImplementedException();
        }
        public void UpdateLineDetails(BO.Line line)//yes
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
        public void DeleteLine(int lineId)//yes
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
        public BO.Station StationDoBoAdapter(DO.Station stationDO)//yes
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
                stationBO.LinesInStation = (from l in dl.GetAllLineStationsBy(l => l.StationCode == stationBO.Code)
                                            let line = dl.GetLine(l.LineId)
                                            select new LineInStation { LineNum = line.LineNum, LineId = l.LineId, TargetStation = dl.GetStation(line.LastStation).Name }).ToList();
                return stationBO;
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }

            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.ID, ex.Message);
            }
        }
        public IEnumerable<BO.Station> GetAllStations()//yes
        {
            return from item in dl.GetAllStations()
                   select StationDoBoAdapter(item);
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
                throw new BO.BadStationCodeException(0, "התחנה לא קיימת במערכת");
            }
            return StationDoBoAdapter(station);
        }
        public void AddStation(BO.Station station)//yes
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
        public void DeleteStation(int statCode)//yes
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
        public void UpdateStation(BO.Station stationBO)//yes
        {
            DO.Station stationDO = new DO.Station();
            stationBO.CopyPropertiesTo(stationDO);
            try
            {
                dl.UpdateStation(stationDO);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadLicenseNumException(ex.stationCode, ex.Message);
            }

        }

        #endregion

        #region StationInLine
        /// <summary>
        /// The function checks if there is information in the system about the following stations, if it does not exist it calls another function that adds information, and after the information enters the function system calls the function that adds the station to the list of stations of the line
        /// </summary>
        /// <param name="stationCode">the code of the station we whant to add</param>
        /// <param name="busID">The id of the bus that we whant to add a station to his station's list</param>
        /// <param name="index">TThe index of where we want to add the station</param>
        /// <param name="nextStatCode"> The code of the station that follows the station we want to add </param>
        /// <param name="prevStatCode">The code of the previous station that  we want to add</param>
        /// <param name="distanceNext">The distance of the new station from the next station</param>
        /// <param name="timeNext">The time of the new station from the next station</param>
        /// <param name="distancePrev">The distance of the new station from the previous station</param>
        /// <param name="timePrev">The time of the new station from the previous station</param>
        public void AddStationInLine(int stationCode, int busID, int index, int nextStatCode, int prevStatCode, double distanceNext, TimeSpan timeNext, double distancePrev, TimeSpan timePrev)
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
                else if (index >= GetLine(busID).Stations.Count - 1)
                {
                    if (!dl.ExistAdjacentStations(stationCode, prevStatCode))
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
                    if (!dl.ExistAdjacentStations(stationCode, prevStatCode))
                    {
                        DO.AdjacentStations adj = new DO.AdjacentStations() { StationCode1 = prevStatCode, StationCode2 = stationCode, Distance = distancePrev, Time = timePrev };
                        dl.AddAdjacentStations(adj);
                    }
                }
                dl.AddStationInLine(stationCode, busID, index);
            }
            catch (DO.BadStationCodeException ex)
            {
                throw new BO.BadStationCodeException(ex.stationCode, ex.Message);
            }
            catch (DO.BadInputException ex)
            {
                throw new BO.BadInputException(ex.Message);
            }
        }//yes
        /// <summary>
        /// The function updates the distance and time between two stations
        /// </summary>
        /// <param name="first">first station</param>
        /// <param name="second">second station</param>
        public void UpdateTimeAndDistance(BO.StationInLine first, BO.StationInLine second)//yes
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
        /// The function checks if the station can be deleted and if so calls another function in the DL that deletes the station from the line list of the line
        /// </summary>
        /// <param name="lineID">The id number of the line from which a station is delited</param>
        /// <param name="code">the code of the station we whant to deleted</param>
        public void DeleteStationInLine(int lineID, int code)//yes
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
        #endregion

        #region user
        /// <summary>
        /// The function calls another function in the DL that checks if the username exists in the system and if it exists it checks if the password is correct
        /// </summary>
        /// <param name="username">the name of the user</param>
        /// <param name="passcode">the passcode of the user</param>
        /// <returns></returns>
        public BO.User SignIn(string username, string passcode)//yes
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
        /// The function calls another function in the DL that checks if the username does not exist in the system and if it does not exist he calls another function in the DL that adds it to the system
        /// </summary>
        /// <param name="userBo">the new user</param>
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

    }
}
