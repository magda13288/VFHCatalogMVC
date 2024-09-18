using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<PlantPosition> PlantPositions { get; set; }
        public DbSet<PlantProducers> PlantProducers { get; set; }
        public DbSet<PlantSoilPh> PlantSoilPhs { get; set; }
        public DbSet<Height> Heights { get; set; }
        public DbSet<AdditionalFeatures> AdditionalFeatures { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Pollination> Pollinations { get; set; }
        public DbSet<Filters> Filters { get; set; }
        public DbSet<Producer> Producers { get; set; }
        public DbSet<SoilPh> SoilPhs { get; set; }

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

                entity.HasKey(pd => new { pd.PlantDetailId, pd.DestinationId });

                entity.HasOne<PlantDetail>(pd => pd.PlantDetail)
                .WithMany(pd => pd.PlantDestinations)
                .HasForeignKey(pd => pd.PlantDetailId);

                entity.HasOne<Destination>(pd => pd.Destinations)
                .WithMany(pd => pd.PlantDestinations)
                .HasForeignKey(pd => pd.DestinationId);
            });

            builder.Entity<PlantGrowthType>(entity => {

                entity.HasKey(pg => new { pg.PlantDetailId, pg.GrowthTypeId });

                entity.HasOne(pg => pg.PlantDetail)
                  .WithMany(pg => pg.PlantGrowthTypes)
                  .HasForeignKey(pg => pg.PlantDetailId);

                entity.HasOne(pg => pg.GrowthType)
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

                entity.HasOne(e=>e.Message)
                .WithMany(e=>e.PlantMessages)
                .HasForeignKey(e => e.MessageId);
            });

            builder.Entity<PlantPosition>(entity =>
            {
                entity.HasKey(e => new { e.PlantDetailId, e.PositionId });

                entity.HasOne(e => e.PlantDetail)
                .WithMany(e => e.PlantPositions)
                .HasForeignKey(e => e.PlantDetailId);

                entity.HasOne(e => e.Position)
                .WithMany(e=> e.PlantPositions)
                .HasForeignKey(e=>e.PositionId);
               
            });

            builder.Entity<PlantProducers>(entity =>
            {
                entity.HasKey(e => new { e.ProducerId, e.PlantDetailId });

                entity.HasOne(e => e.PlantDetail)
                .WithMany(e => e.PlantProducers)
                .HasForeignKey(e => e.PlantDetailId);

                entity.HasOne(e => e.Producer)
                .WithMany(e => e.PlantProducers)
                .HasForeignKey(e => e.ProducerId);

            });

            builder.Entity<PlantSoilPh>(entity =>
            {
                entity.HasKey(e => new { e.SoilPhId, e.PlantDetailId });

                entity.HasOne(e => e.PlantDetail)
                .WithMany(e => e.PlantSoilPhs)
                .HasForeignKey(e => e.PlantDetailId);

                entity.HasOne(e => e.SoilPh)
                 .WithMany(e => e.PlantSoilPhs)
                 .HasForeignKey(e => e.SoilPhId);
                           
            });

           

            //builder.Entity<Position>().HasData(

            //    new Position {Name = "Słoneczne"},
            //    new Position {Name = "Półcień" },
            //    new Position {Name = "Cień" }
            //    );

            //builder.Entity<Height>().HasData(

            //    new Height {Name = "0,7m - 1,2m" },
            //    new Height {Name = "0,8m - 1,3m" },
            //    new Height {Name = "1m - 1,8m" },
            //    new Height {Name = "1,2m - 1,6m" },
            //    new Height {Name = "1,5m - 2,5m" },
            //    new Height {Name = "powyżej 2m" }
            //    );

            //builder.Entity<AdditionalFeatures>().HasData(

            //    new AdditionalFeatures { Name = "F1" },
            //    new AdditionalFeatures { Name = "Mrozoodpornośc" },
            //    new AdditionalFeatures { Name = "Bezpieczne dla zwierząt" },
            //    new AdditionalFeatures { Name = "Nasiona na taśmie" }
            //    );
        }
    }
}
