using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class PlantMessageConfiguration : IEntityTypeConfiguration<PlantMessage>
    {
        public void Configure(EntityTypeBuilder<PlantMessage> entity)
        {
            entity.HasKey(e => new { e.PlantId, e.MessageId });

            entity.HasOne(e => e.Plant)
            .WithMany(e => e.PlantMessages)
            .HasForeignKey(e => e.PlantId);

            entity.HasOne(e => e.Message)
            .WithMany(e => e.PlantMessages)
            .HasForeignKey(e => e.MessageId);
        }
    }
}