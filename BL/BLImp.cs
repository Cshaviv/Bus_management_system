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
        //  BO.Student studentDoBoAdapter(DO.Student studentDO)
        //{
        //    BO.Student studentBO = new BO.Student();
        //    DO.Person personDO;
        //    int id = studentDO.ID;
        //    try
        //    {
        //        personDO = dl.GetPerson(id);
        //    }
        //    catch (DO.BadPersonIdException ex)
        //    {
        //        throw new BO.BadStudentIdException("Student ID is illegal", ex);
        //    }
        //    personDO.CopyPropertiesTo(studentBO);
        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }



        public void DeleteBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return null;
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }


        public Bus GetBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int licenseNum, Action<Bus> update)
        {
            throw new NotImplementedException();
        }
    }
}
      

 
