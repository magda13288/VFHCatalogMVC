﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class AuditTrailConfiguration : IEntityTypeConfiguration<AuditTrial>
    {
        public void Configure(EntityTypeBuilder<AuditTrial> entity)
        {
            entity.ToTable("AuditTrials");
            entity.HasKey(e => e.Id);

            entity.HasIndex(e => e.EntityName);

            entity.Property(e => e.Id);

            entity.Property(e => e.UserId);
            entity.Property(e => e.EntityName).HasMaxLength(100).IsRequired();
            entity.Property(e => e.DateUtc).IsRequired();
            entity.Property(e => e.PrimaryKey).HasMaxLength(100);

            entity.Property(e => e.TrailType).HasConversion<string>();

            entity.Property(e => e.ChangedColumns).HasColumnType("nvarchar(max)");
            entity.Property(e => e.OldValues).HasColumnType("nvarchar(max)");
            entity.Property(e => e.NewValues).HasColumnType("nvarchar(max)");

            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}