using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class PlantGrowthTypeConfiguration : IEntityTypeConfiguration<PlantGrowthType>
    {
        public void Configure(EntityTypeBuilder<PlantGrowthType> entity)
        {
            entity.HasKey(pg => new { pg.PlantDetailId, pg.GrowthTypeId });

            entity.HasOne<PlantDetail>(pg => pg.PlantDetail)
            .WithMany(pg => pg.PlantGrowthTypes)
            .HasForeignKey(pg => pg.PlantDetailId);

            entity.HasOne<GrowthType>(pg => pg.GrowthType)
            .WithMany(pg => pg.PlantGrowthTypes)
            .HasForeignKey(pg => pg.GrowthTypeId);
        }
    }
}