using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;

namespace Transfer.BLL.Interfaces
{
    public interface ITransferService
    {
        IEnumerable<Transfer.Models.Transfer> GetAll();
        Transfer.Models.Transfer GetById(object id);
        Transfer.Models.Transfer Add(Transfer.Models.Transfer entity);
        Transfer.Models.Transfer Update(Transfer.Models.Transfer entity, object id);
        void Remove(Transfer.Models.Transfer entity);
    }
}
