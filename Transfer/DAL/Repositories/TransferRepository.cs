using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models; // promijeniti ili ime projekta ili ime entiteta, nauciti iz ove greske
using Transfer.DAL.Repositories.Interfaces;

namespace Transfer.DAL.Repositories
{
    public class TransferRepository : GenericRepository<Transfer.Models.Transfer>, ITransferRepository
    {
        public TransferRepository(TransferContext ctx) : base(ctx)
        {

        }

    }
}
