using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class PlantGrowingSeazonConfiguration : IEntityTypeConfiguration<PlantGrowingSeazon>
    {
        public void Configure(EntityTypeBuilder<PlantGrowingSeazon> entity)
        {
            entity.HasKey(e => new { e.PlantDetailId, e.GrowingSeazonId });

            entity.HasOne<PlantDetail>(e => e.PlantDetail)
            .WithMany(e => e.PlantGrowingSeazons)
            .HasForeignKey(e => e.PlantDetailId);

            entity.HasOne<GrowingSeazon>(e => e.GrowingSeazon)
            .WithMany(e => e.PlantGrowingSeazons)
            .HasForeignKey(e => e.GrowingSeazonId);
        }
    }
}
