using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Mapping
{
    public class MessageAnswerConfiguration : IEntityTypeConfiguration<MessageAnswer>
    {
        public void Configure(EntityTypeBuilder<MessageAnswer> entity)
        {
            entity.HasKey(e => new { e.MessageId, e.MessageAnswerId });

            entity.HasOne(e => e.Message)
            .WithMany(e => e.MessageAnswers)
            .HasForeignKey(e => e.MessageId)
            .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(e => e.Message)
           .WithMany(e => e.MessageAnswers)
           .HasForeignKey(e => e.MessageAnswerId)
           .OnDelete(DeleteBehavior.NoAction);
        }
    }
}