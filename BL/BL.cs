using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALObject;

namespace BL
{
    public class BL : IBL
    {
        BO.Bus BusDoBoAdapter(DO.Bus busDO)
        {
            var busBO = new BO.Bus();

            busDO.CopyPropertiesTo(busBO);

            return busBO;
        }

        public void AddBus(Bus bus)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bus> GetAllBuses()
        {
            return from item in DLObject.Instance.GetAllBuses()
                   select BusDoBoAdapter(item);
        }

        public IEnumerable<Bus> GetAllBusesBy(Predicate<Bus> predicate)
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
