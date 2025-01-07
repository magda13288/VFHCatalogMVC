using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class PlantConfiguration : IEntityTypeConfiguration<Plant>
    {
        public void Configure(EntityTypeBuilder<Plant> entity)
        {
            entity.HasOne(p => p.PlantType)
             .WithMany(p => p.Plants)
             .HasForeignKey(p => p.PlantTypeId)
             .OnDelete(DeleteBehavior.NoAction)
             .IsRequired();

            entity.HasOne(p => p.PlantGroup)
           .WithMany(p => p.Plants)
           .HasForeignKey(p => p.PlantGroupId)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired();

            entity.HasOne(p => p.PlantSection)
           .WithMany(p => p.Plants)
           .HasForeignKey(p => p.PlantSectionId)
           .OnDelete(DeleteBehavior.NoAction)
           .IsRequired(false);

            entity.HasOne(a => a.PlantDetail).WithOne(b => b.Plant)
            .HasForeignKey<PlantDetail>(e => e.PlantRef);

            entity.HasOne(a => a.TypeOfAvailability).WithOne(b => b.Plant)
            .HasForeignKey<TypeOfAvailability>(e => e.PlantRef);


        }
    }
}