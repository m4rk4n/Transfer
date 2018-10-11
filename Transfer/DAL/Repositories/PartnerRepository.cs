using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL.Repositories.Interfaces;
using Transfer.Models;

namespace Transfer.DAL.Repositories
{

    public class PartnerRepository : GenericRepository<Partner>, IPartnerRepository
    {
        public PartnerRepository(TransferContext context) : base(context)
        {
            // context.Partners.ToList();
        }
    }
}
