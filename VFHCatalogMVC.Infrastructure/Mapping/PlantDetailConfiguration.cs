using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class PlantDetailConfiguration : IEntityTypeConfiguration<PlantDetail>
    {
        public void Configure(EntityTypeBuilder<PlantDetail> entity)
        {
            entity.HasOne(p => p.Color)
            .WithMany(p => p.PlantDetails)
            .HasForeignKey(p => p.ColorId)
            .IsRequired(true);

            entity.HasOne(p => p.FruitSize)
           .WithMany(p => p.PlantDetails)
           .HasForeignKey(p => p.FruitSizeId)
           .IsRequired(true);

            entity.HasOne(p => p.FruitType)
           .WithMany(p => p.PlantDetails)
           .HasForeignKey(p => p.FruitTypeId)
           .IsRequired(true);
        }
    }
}
