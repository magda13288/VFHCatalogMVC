﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure
{
    public class Context : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ContactDetailType> ContactDetailTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CompanyContactInformation> CompanyContactInformations { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantGroup> PlantGroups { get; set; }
        public DbSet<PlantSection> PlantSections { get; set; }
        public DbSet<PlantTag> PlantTags { get; set; }
        public DbSet<PlantType> PlantTypes { get; set; }
        public DbSet<PlantDetail> PlantDetails { get; set; }
        public DbSet<PlantOpinion> PlantOpinions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TypeOfAvailability> TypeOfAvailabilities { get; set; }
        public DbSet<Destination> Destinations { get; set; }
        public DbSet<PlantDestination> PlantDestinations { get; set; }
        public DbSet<GrowthType> GrowthTypes { get; set; }
        public DbSet<PlantGrowthType> PlantGrowthTypes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<FruitSize> FruitSizes { get; set; }
        public DbSet<FruitType> FruitTypes { get; set; }
        public DbSet<GrowingSeazon> GrowingSeazons { get; set; }
        public DbSet<PlantGrowingSeazon> PlantGrowingSeazons { get; set; }
        public DbSet<PlantDetailsImages> PlantDetailsImages { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<PlantSeed> PlantSeeds { get; set; }
        public DbSet<PlantSeedling> PlantSeedlings { get; set; }
        public DbSet<ContactDetailForSeed> ContactDetailForSeeds { get; set; }
        public DbSet<ContactDetailForSeedling> ContactDetailForSeedlings { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageReceiver> MessageReceivers { get; set; }
        public DbSet<MessageAnswer> MessageAnswers { get; set; }
        public DbSet<NewUserPlant> NewUserPlants { get; set; }
        public DbSet<PlantMessage> PlantMessages { get; set; }
        //public DbSet<FruitSizeForListFilters> FruitSizeForListFilters { get; set; }
        //public DbSet<FruitTypeForListFilters> FruitTypeForListFilters { get; set; }

        //public DbSet<GrowthTypesForListFilters> GrowthTypesForListFilters { get; set; }

        public Context(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Address>(entity =>
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

            });

            builder.Entity<Plant>(entity =>
            {
                entity.HasOne(p => p.PlantType)
                .WithMany(p => p.Plants)
                .HasForeignKey(p => p.PlantTypeId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

                entity.HasOne(p => p.PlantGroup)
               .WithMany(p => p.Plants)
               .HasForeignKey(p => p.PlantGroupId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

                entity.HasOne(p => p.PlantSection)
               .WithMany(p => p.Plants)
               .HasForeignKey(p => p.PlantSectionId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false);

            });

            builder.Entity<GrowthType>(entity =>
            {
                entity.HasOne(p => p.PlantType)
                .WithMany(p => p.GrowthTypes)
                .HasForeignKey(p => p.PlantTypeId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

                entity.HasOne(p => p.PlantGroup)
               .WithMany(p => p.GrowthTypes)
               .HasForeignKey(p => p.PlantGroupId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false);

                entity.HasOne(p => p.PlantSection)
               .WithMany(p => p.GrowthTypes)
               .HasForeignKey(p => p.PlantSectionId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false);
            });


            builder.Entity<FruitSize>(entity =>
            {
                entity.HasOne(p => p.PlantType)
                .WithMany(p => p.FruitSizes)
                .HasForeignKey(p => p.PlantTypeId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

                entity.HasOne(p => p.PlantGroup)
               .WithMany(p => p.FruitSizes)
               .HasForeignKey(p => p.PlantGroupId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false);

                entity.HasOne(p => p.PlantSection)
               .WithMany(p => p.FruitSizes)
               .HasForeignKey(p => p.PlantSectionId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false);
            });


            builder.Entity<FruitType>(entity =>
            {
                entity.HasOne(p => p.PlantType)
                .WithMany(p => p.FruitTypes)
                .HasForeignKey(p => p.PlantTypeId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

                entity.HasOne(p => p.PlantGroup)
               .WithMany(p => p.FruitTypes)
               .HasForeignKey(p => p.PlantGroupId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false);

                entity.HasOne(p => p.PlantSection)
               .WithMany(p => p.FruitTypes)
               .HasForeignKey(p => p.PlantSectionId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired(false);
            });

            builder.Entity<PlantDetail>(entity =>
            {
                entity.HasOne(p => p.Color)
                .WithMany(p => p.PlantDetails)
                .HasForeignKey(p => p.ColorId)
                .IsRequired(false);

                entity.HasOne(p => p.FruitSize)
               .WithMany(p => p.PlantDetails)
               .HasForeignKey(p => p.FruitSizeId)
               .IsRequired(false);

                entity.HasOne(p => p.FruitType)
               .WithMany(p => p.PlantDetails)
               .HasForeignKey(p => p.FruitTypeId)
               .IsRequired(false);
            });

            builder.Entity<Plant>()
                .HasOne(a => a.TypeOfAvailability).WithOne(b => b.Plant)
                .HasForeignKey<TypeOfAvailability>(e => e.PlantRef);

            builder.Entity<Plant>()
                .HasOne(a => a.PlantDetail).WithOne(b => b.Plant)
                .HasForeignKey<PlantDetail>(e => e.PlantRef);

            builder.Entity<PlantTag>()
                .HasKey(pt => new { pt.PlantId, pt.TagId });

            builder.Entity<PlantTag>()
                .HasOne<Plant>(pt => pt.Plant)
                .WithMany(pt => pt.PlantTags)
                .HasForeignKey(pt => pt.PlantId);

            builder.Entity<PlantTag>()
                .HasOne<Tag>(pt => pt.Tag)
                .WithMany(pt => pt.PlantTags)
                .HasForeignKey(pt => pt.TagId);



            builder.Entity<PlantDestination>(entity => {

                entity.HasKey(p => new { p.PlantDetailId, p.DestinationId });

                entity.HasOne<PlantDetail>(p => p.PlantDetail)
                .WithMany(p => p.PlantDestinations)
                .HasForeignKey(p => p.PlantDetailId);

                entity.HasOne<Destination>(p => p.Destinations)
                .WithMany(p => p.PlantDestinations)
                .HasForeignKey(p => p.DestinationId);
            });

            //builder.Entity<FruitSizeForListFilters>(entity =>
            // {
            //     entity.HasKey(p => new { p.FruitSizeId, p.PlantTypeId, p.PlantGroupId, p.PlantSectionId });

            //     entity.HasOne<FruitSize>(p => p.FruitSize)
            //     .WithMany(p => p.FruitSizeForFilters)
            //     .HasForeignKey(p => p.FruitSizeId);

            //     entity.HasOne<PlantType>(p => p.PlantType)
            //     .WithMany(p => p.FruitSizeForFilters)
            //     .HasForeignKey(p => p.PlantTypeId);

            //     entity.HasOne<PlantGroup>(p => p.PlantGroup)
            //     .WithMany(p => p.FruitSizeForFilters)
            //     .HasForeignKey(p => p.PlantGroupId);

            //     entity.HasOne<PlantSection>(p=>p.PlantSection)
            //     .WithMany(p=>p.FruitSizeForFilters)
            //     .HasForeignKey(p=>p.PlantSectionId);

            //     });

            //builder.Entity<FruitTypeForListFilters>(entity =>
            //{
            //    entity.HasKey(p => new { p.FruitTypeId, p.PlantTypeId, p.PlantGroupId, p.PlantSectionId });

            //    entity.HasOne<FruitType>(p => p.FruitType)
            //    .WithMany(p => p.FruitTypeForFilters)
            //    .HasForeignKey(p => p.FruitTypeId);

            //    entity.HasOne<PlantType>(p => p.PlantType)
            //    .WithMany(p => p.FruitTypeForFilters)
            //    .HasForeignKey(p => p.PlantTypeId);

            //    entity.HasOne<PlantGroup>(p => p.PlantGroup)
            //    .WithMany(p => p.FruitTypeForFilters)
            //    .HasForeignKey(p => p.PlantGroupId);

            //    entity.HasOne<PlantSection>(p => p.PlantSection)
            //    .WithMany(p => p.FruitTypeForFilters)
            //    .HasForeignKey(p => p.PlantSectionId);

            //});

            //builder.Entity<GrowthTypesForListFilters>(entity => {

            //    entity.HasKey(p => new { p.GrowthTypesId, p.PlantTypeId, p.PlantGroupId, p.PlantSectionId });

            //    entity.HasOne<GrowthType>(p => p.GrowthType)
            //    .WithMany(p => p.GrowthTypesForListFilters)
            //    .HasForeignKey(p => p.GrowthTypesId);

            //    entity.HasOne<PlantType>(p => p.PlantType)
            //    .WithMany(p => p.GrowthTypesForListFilters)
            //    .HasForeignKey(p => p.PlantTypeId);

            //    entity.HasOne<PlantGroup>(p => p.PlantGroup)
            //   .WithMany(p => p.GrowthTypesForListFilters)
            //   .HasForeignKey(p => p.PlantGroupId);

            //    entity.HasOne<PlantSection>(p => p.PlantSection)
            //    .WithMany(p => p.GrowthTypesForListFilters)
            //    .HasForeignKey(p => p.PlantSectionId);
            //});


            builder.Entity<PlantGrowthType>(entity => {

                entity.HasKey(pg => new { pg.PlantDetailId, pg.GrowthTypeId });

                entity.HasOne<PlantDetail>(pg => pg.PlantDetail)
                  .WithMany(pg => pg.PlantGrowthTypes)
                  .HasForeignKey(pg => pg.PlantDetailId);

                entity.HasOne<GrowthType>(pg => pg.GrowthType)
                    .WithMany(pg => pg.PlantGrowthTypes)
                    .HasForeignKey(pg => pg.GrowthTypeId);
            });

            builder.Entity<PlantGrowingSeazon>(entity =>
            {
                entity.HasKey(e => new { e.PlantDetailId, e.GrowingSeazonId });

                entity.HasOne<PlantDetail>(e => e.PlantDetail)
                .WithMany(e => e.PlantGrowingSeazons)
                .HasForeignKey(e => e.PlantDetailId);

                entity.HasOne<GrowingSeazon>(e => e.GrowingSeazon)
                .WithMany(e => e.PlantGrowingSeazons)
                .HasForeignKey(e => e.GrowingSeazonId);
            });

            builder.Entity<ContactDetailForSeed>(entity =>
            {
                entity.HasKey(e => new { e.PlantSeedId, e.ContactDetailId });

                entity.HasOne(e => e.PlantSeed)
                .WithMany(e => e.ContactDetailForSeeds)
                .HasForeignKey(e => e.PlantSeedId);

                entity.HasOne(e => e.ContactDetail)
                .WithMany(e => e.ContactDetailForSeeds)
                .HasForeignKey(e => e.ContactDetailId);
            });

            builder.Entity<ContactDetailForSeedling>(entity =>
            {
                entity.HasKey(e => new { e.PlantSeedlingId, e.ContactDetailId });

                entity.HasOne(e => e.PlantSeedling)
                .WithMany(e => e.ContactDetailForSeedlings)
                .HasForeignKey(e => e.PlantSeedlingId);

                entity.HasOne(e => e.ContactDetail)
                .WithMany(e => e.ContactsForSeedling)
                .HasForeignKey(e => e.ContactDetailId);
            });

            builder.Entity<MessageAnswer>(entity =>
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
            });

            builder.Entity<NewUserPlant>(entity =>
            {
                entity.HasKey(e => new { e.PlantId, e.UserId });
                entity.Ignore(e => e.Id);

                entity.HasOne(e => e.Plant)
                .WithMany(e => e.NewUserPlants)
                .HasForeignKey(e => e.PlantId);

                entity.HasOne(e => e.User)
                .WithMany(e => e.NewUserPlants)
                .HasForeignKey(e => e.UserId);
            });

            builder.Entity<PlantMessage>(entity =>
            {
                entity.HasKey(e => new { e.PlantId, e.MessageId });

                entity.HasOne(e => e.Plant)
                .WithMany(e => e.PlantMessages)
                .HasForeignKey(e => e.PlantId);

                entity.HasOne(e => e.Message)
                .WithMany(e => e.PlantMessages)
                .HasForeignKey(e => e.MessageId);
            });
        }
    }
}
