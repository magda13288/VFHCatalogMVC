using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class GrowthTypesForListFiltersConfiguration : IEntityTypeConfiguration<GrowthTypesForListFilters>
    {
        public void Configure(EntityTypeBuilder<GrowthTypesForListFilters> entity)
        {
            entity.HasKey(p => new { p.GrowthTypesId, p.PlantTypeId }); 

            entity.HasIndex(p => new { p.PlantGroupId, p.PlantSectionId }).IsUnique(false);

            entity.HasOne<GrowthType>(p => p.GrowthType)
                .WithMany(p => p.GrowthTypesForListFilters)
                .HasForeignKey(p => p.GrowthTypesId);

            entity.HasOne<PlantType>(p => p.PlantType)
                .WithMany(p => p.GrowthTypesForListFilters)
                .HasForeignKey(p => p.PlantTypeId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne<PlantGroup>(p => p.PlantGroup)
                .WithMany(p => p.GrowthTypesForListFilters)
                .HasForeignKey(p => p.PlantGroupId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            entity.HasOne<PlantSection>(p => p.PlantSection)
                .WithMany(p => p.GrowthTypesForListFilters)
                .HasForeignKey(p => p.PlantSectionId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);
        }
    }
}
