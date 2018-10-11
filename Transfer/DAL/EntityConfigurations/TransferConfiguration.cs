using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transfer.Models; // promjenit ime ili klase ili projekta

namespace Transfer.DAL.EntityConfigurations
{
    public class TransferConfiguration : IEntityTypeConfiguration<Transfer.Models.Transfer>
    {
        public void Configure(EntityTypeBuilder<Transfer.Models.Transfer> builder)
        {
           // builder
           //.HasOne(p => p.Vehicle)
           //.WithOne(i => i.LastService)
           //.HasForeignKey<Vehicle>(v => v.LastServiceTransferId);
        }
    }
}
