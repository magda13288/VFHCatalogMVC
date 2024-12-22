using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class AddressConfigration: IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.HasOne(p => p.Country)
            .WithMany(p => p.Adresses)
            .HasForeignKey(p => p.CountryId)
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.Region)
            .WithMany(p => p.Address)
            .HasForeignKey(p => p.RegionId)
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(p => p.City)
            .WithMany(p => p.Addresses)
            .HasForeignKey(p => p.CityId)
            .OnDelete(DeleteBehavior.NoAction);

        }

    }
}

