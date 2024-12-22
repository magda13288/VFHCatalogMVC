using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class PlantTagConfiguration : IEntityTypeConfiguration<PlantTag>
    {
        public void Configure(EntityTypeBuilder<PlantTag> entity)
        {
            entity.HasKey(pt => new { pt.PlantId, pt.TagId });

            entity.HasOne<Plant>(pt => pt.Plant)
            .WithMany(pt => pt.PlantTags)
            .HasForeignKey(pt => pt.PlantId);

            entity.HasOne<Tag>(pt => pt.Tag)
            .WithMany(pt => pt.PlantTags)
            .HasForeignKey(pt => pt.TagId);
        }
    }
}
