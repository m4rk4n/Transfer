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
    public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
    {
        public void Configure(EntityTypeBuilder<Vehicle> builder)
        {
            builder
          .HasOne(p => p.LastService)
          .WithOne(i => i.Vehicle)
          .HasForeignKey<Vehicle>(v => v.LastServiceTransferId);

            // builder.Property(p => p.RegistrationDate).IsRequired(false);
            builder.Property(p => p.LastServiceTransferId).IsRequired(false);
        }
    }
}
