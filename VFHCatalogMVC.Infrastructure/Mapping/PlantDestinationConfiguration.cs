using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class PlantDestinationConfiguration : IEntityTypeConfiguration<PlantDestination>
    {
        public void Configure(EntityTypeBuilder<PlantDestination> entity)
        {
            entity.HasKey(p => new { p.PlantDetailId, p.DestinationId });

            entity.HasOne<PlantDetail>(p => p.PlantDetail)
            .WithMany(p => p.PlantDestinations)
            .HasForeignKey(p => p.PlantDetailId);

            entity.HasOne<Destination>(p => p.Destinations)
            .WithMany(p => p.PlantDestinations)
            .HasForeignKey(p => p.DestinationId);
        }
    }
}
