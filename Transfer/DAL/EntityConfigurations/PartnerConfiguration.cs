using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models;

namespace Transfer.DAL.EntityConfigurations
{
    public class PartnerConfiguration : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder
           .HasOne(p => p.Vehicle)
           .WithOne(i => i.Partner)
           .HasForeignKey<Partner>(p => p.VehicleId);

            builder.Property(p => p.VehicleId).IsRequired(false);
        }
    }
}
