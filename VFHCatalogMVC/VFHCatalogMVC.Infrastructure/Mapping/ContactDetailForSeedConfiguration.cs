using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class ContactDetailForSeedConfiguration : IEntityTypeConfiguration<ContactDetailForSeed>
    {
        public void Configure(EntityTypeBuilder<ContactDetailForSeed> entity)
        {
            entity.HasKey(e => new { e.PlantSeedId, e.ContactDetailId });

            entity.HasOne(e => e.PlantSeed)
            .WithMany(e => e.ContactDetailForSeeds)
            .HasForeignKey(e => e.PlantSeedId);

            entity.HasOne(e => e.ContactDetail)
            .WithMany(e => e.ContactDetailForSeeds)
            .HasForeignKey(e => e.ContactDetailId);
        }
    }
}