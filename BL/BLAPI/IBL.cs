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
        void UpdateBus(int licenseNum, Action<BO.Bus> update); //method that knows to updt specific fields in Bus
        void DeleteBus(int licenseNum);

        #endregion
    }
}
