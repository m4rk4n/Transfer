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
    public class AgencyPartnerConfiguration : IEntityTypeConfiguration<AgencyPartner>
    {
        public void Configure(EntityTypeBuilder<AgencyPartner> builder)
        {
            builder
           .HasKey(a => new { a.AgencyId, a.PartnerId });

            builder
            .HasOne(ag => ag.Agency)
            .WithMany(a => a.AgencyPartners)
            .HasForeignKey(ag => ag.AgencyId);

            builder
            .HasOne(ag => ag.Partner)
            .WithMany(p => p.AgencyPartners)
            .HasForeignKey(ag => ag.PartnerId);
        }
    }
}
