using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;

namespace Transfer.DAL
{
    public class TransferContext : IdentityDbContext<TransferUser>
    {
        public TransferContext(DbContextOptions<TransferContext> options) : base(options)
        {

        }

        public DbSet<Partner> Partners {get; set;}
    }
}
