using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transfer.DAL
{
    public interface ITransferRepository
    {
        bool SaveAll();
    }
}
