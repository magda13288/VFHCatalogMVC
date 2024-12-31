using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class NewUserPlantConfiguration : IEntityTypeConfiguration<NewUserPlant>
    {
        public void Configure(EntityTypeBuilder<NewUserPlant> entity)
        {
            entity.HasKey(e => new { e.PlantId, e.UserId });
            entity.Ignore(e => e.Id);

            entity.HasOne(e => e.Plant)
            .WithMany(e => e.NewUserPlants)
            .HasForeignKey(e => e.PlantId);

            entity.HasOne(e => e.User)
            .WithMany(e => e.NewUserPlants)
            .HasForeignKey(e => e.UserId);
        }
    }
}
