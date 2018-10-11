using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL.Repositories.Interfaces;
using Transfer.Models;
using Microsoft.EntityFrameworkCore;

namespace Transfer.DAL.Repositories
{
    public class AgencyRepository : GenericRepository<Agency>, IAgencyRepository
    {
        public AgencyRepository(TransferContext ctx) : base(ctx)
        {
            ctx.Agencies.Include(a => a.AgencyPartners).ToList();
        }
    }
}
