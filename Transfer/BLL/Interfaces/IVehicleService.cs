using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;

namespace Transfer.BLL.Interfaces
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> GetAll();
        Vehicle GetById(object id);
        Vehicle Add(Vehicle entity);
        Vehicle Update(Vehicle entity, object id);
        void Remove(object id);
    }
}
