using BL.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLObject;

namespace BL
{
    public class BL : IBL
    {
        BO.Bus BusDoBoAdapter(BO.Bus busDO)
        {
            var busBO = new BO.Bus();

            busDO.CopyPropertiesTo(busBO);

            return busBO;
        }

        public void AddBus(BO.Bus bus)
        {
            throw new NotImplementedException();
        }

        public void DeleteBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Bus> GetAllBuses()
        {
            return from item in DLObject.DLObject.Instance.GetAllBuses()
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

        IEnumerable<BO.Bus> IBL.GetAllBuses()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BO.Bus> GetAllBusesBy(Predicate<BO.Bus> predicate)
        {
            throw new NotImplementedException();
        }

        BO.Bus IBL.GetBus(int licenseNum)
        {
            throw new NotImplementedException();
        }

        public void AddBus(BO.Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(BO.Bus bus)
        {
            throw new NotImplementedException();
        }

        public void UpdateBus(int licenseNum, Action<BO.Bus> update)
        {
            throw new NotImplementedException();
        }
    }
}
