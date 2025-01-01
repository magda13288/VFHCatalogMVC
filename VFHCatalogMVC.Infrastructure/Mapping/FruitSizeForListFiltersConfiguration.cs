using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class FruitSizeForListFiltersConfiguration : IEntityTypeConfiguration<FruitSizeForListFilters>
    {
        public void Configure(EntityTypeBuilder<FruitSizeForListFilters> entity)
        {
            entity.HasKey(p => new { p.FruitSizeId, p.PlantTypeId });
            entity.HasIndex(p => new { p.PlantGroupId, p.PlantSectionId }).IsUnique(false);

            entity.HasOne<FruitSize>(p => p.FruitSize)
            .WithMany(p => p.FruitSizeForFilters)
            .HasForeignKey(p => p.FruitSizeId);

            entity.HasOne<PlantType>(p => p.PlantType)
            .WithMany(p => p.FruitSizeForFilters)
            .HasForeignKey(p => p.PlantTypeId)
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne<PlantGroup>(p => p.PlantGroup)
            .WithMany(p => p.FruitSizeForFilters)
            .HasForeignKey(p => p.PlantGroupId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

            entity.HasOne<PlantSection>(p => p.PlantSection)
            .WithMany(p => p.FruitSizeForFilters)
            .HasForeignKey(p => p.PlantSectionId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired(false);

        }
    }
}
