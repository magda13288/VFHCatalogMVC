using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class FruitTypeForListFiltersConfiguration : IEntityTypeConfiguration<FruitTypeForListFilters>
    {
        public void Configure(EntityTypeBuilder<FruitTypeForListFilters> entity)
        {
            entity.HasKey(p => new { p.FruitTypeId, p.PlantTypeId, p.PlantGroupId, p.PlantSectionId });

            entity.HasOne<FruitType>(p => p.FruitType)
            .WithMany(p => p.FruitTypeForFilters)
            .HasForeignKey(p => p.FruitTypeId);

            entity.HasOne<PlantType>(p => p.PlantType)
            .WithMany(p => p.FruitTypeForFilters)
            .HasForeignKey(p => p.PlantTypeId);

            entity.HasOne<PlantGroup>(p => p.PlantGroup)
            .WithMany(p => p.FruitTypeForFilters)
            .HasForeignKey(p => p.PlantGroupId);

            entity.HasOne<PlantSection>(p => p.PlantSection)
            .WithMany(p => p.FruitTypeForFilters)
            .HasForeignKey(p => p.PlantSectionId);
        }
    }
}
