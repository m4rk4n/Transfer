using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.DAL.EntityConfigurations;
using Transfer.Models;

namespace Transfer.DAL
{
    public class TransferContext : IdentityDbContext<TransferUser>
    {
        public TransferContext(DbContextOptions<TransferContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PartnerConfiguration());
            builder.ApplyConfiguration(new TransferConfiguration());
            builder.ApplyConfiguration(new VehicleConfiguration());
            builder.ApplyConfiguration(new AgencyConfiguration());
            builder.ApplyConfiguration(new AgencyPartnerConfiguration());
            base.OnModelCreating(builder);
        }

        public DbSet<Partner> Partners {get; set;}
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Transfer.Models.Transfer> Transfers { get; set; }
    }
}
