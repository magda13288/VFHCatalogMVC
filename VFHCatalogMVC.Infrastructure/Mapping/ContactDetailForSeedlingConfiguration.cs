using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class ContactDetailForSeedlingConfiguration : IEntityTypeConfiguration<ContactDetailForSeedling>
    {
        public void Configure(EntityTypeBuilder<ContactDetailForSeedling> entity)
        {
            entity.HasKey(e => new { e.PlantSeedlingId, e.ContactDetailId });

            entity.HasOne(e => e.PlantSeedling)
            .WithMany(e => e.ContactDetailForSeedlings)
            .HasForeignKey(e => e.PlantSeedlingId);

            entity.HasOne(e => e.ContactDetail)
            .WithMany(e => e.ContactsForSeedling)
            .HasForeignKey(e => e.ContactDetailId);
        }
    }
}
