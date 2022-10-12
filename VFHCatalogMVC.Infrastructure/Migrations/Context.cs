using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure
{
    public class Context : IdentityDbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<ContactDetailType> ContactDetailTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContactInformation> CustomerContactInformation { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantGroup> PlantGroups { get; set; }
        public DbSet<PlantSection> PlantSections { get; set; }
        public DbSet<PlantTag> PlantTags { get; set; }
        public DbSet<PlantType> PlantTypes { get; set; }
        public DbSet<PlantDetail> PlantDetails { get; set; }
        public DbSet<PlantOpinion> PlantOpinions { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TypeOfAvailability> TypeOfAvailabilities { get; set; }
        public DbSet<PrivateUser> PrivateUsers { get; set; }
        public DbSet<CustomerPlantsForSale> CustomerPlantsForSale { get; set; }
        public DbSet<UserPlantSharing> UserPlantSharing { get; set; }
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
        public Context(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


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

            builder.Entity<GrowthType>(entity=>
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

            builder.Entity<Customer>()
                .HasOne(a => a.CustomerContactInformation).WithOne(b => b.Customer)
                .HasForeignKey<CustomerContactInformation>(e => e.CustomerRef);

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

            builder.Entity<UserPlantSharing>()
                .HasKey(ups => new { ups.PlantId, ups.UserId });

            builder.Entity<UserPlantSharing>()
                .HasOne<Plant>(ups => ups.Plant)
                .WithMany(ups => ups.UserPlantSharings)
                .HasForeignKey(ups => ups.PlantId);

            builder.Entity<UserPlantSharing>()
                .HasOne<PrivateUser>(ups => ups.PrivateUser)
                .WithMany(ups => ups.UserPlantSharings)
                .HasForeignKey(ups => ups.UserId);

            builder.Entity<CustomerPlantsForSale>()
                .HasKey(cps => new { cps.PlantId, cps.CustomerId });

            builder.Entity<CustomerPlantsForSale>()
                .HasOne<Plant>(cps => cps.Plant)
                .WithMany(cps => cps.CustomerPlantsForSale)
                .HasForeignKey(cps => cps.PlantId);

            builder.Entity<CustomerPlantsForSale>()
                .HasOne<Customer>(cps => cps.Customer)
                .WithMany(cps => cps.CustomerPlantsForSale)
                .HasForeignKey(cps => cps.CustomerId);


            builder.Entity<PlantDestination>()
                .HasKey(pd => new { pd.PlantDetailId, pd.DestinationId });

            builder.Entity<PlantDestination>()
                .HasOne<PlantDetail>(pd => pd.PlantDetail)
                .WithMany(pd => pd.PlantDestinations)
                .HasForeignKey(pd => pd.PlantDetailId);

            builder.Entity<PlantDestination>()
                .HasOne<Destination>(pd => pd.Destinations)
                .WithMany(pd => pd.PlantDestinations)
                .HasForeignKey(pd => pd.DestinationId);

            builder.Entity<PlantGrowthType>()
                .HasKey(pg => new { pg.PlantDetailId, pg.GrowthTypeId });

            builder.Entity<PlantGrowthType>()
                .HasOne(pg => pg.PlantDetail)
                .WithMany(pg => pg.PlantGrowthTypes)
                .HasForeignKey(pg => pg.PlantDetailId);

            builder.Entity<PlantGrowthType>()
                .HasOne(pg => pg.GrowthType)
                .WithMany(pg => pg.PlantGrowthTypes)
                .HasForeignKey(pg => pg.GrowthTypeId);

            builder.Entity<PlantGrowingSeazon>(entity =>
            {
                entity.HasKey(e => new { e.PlantDetailId, e.GrowingSeazonId });

                entity.HasOne<PlantDetail>(e=>e.PlantDetail)
                .WithMany(e=>e.PlantGrowingSeazons)
                .HasForeignKey(e => e.PlantDetailId);

                entity.HasOne<GrowingSeazon>(e => e.GrowingSeazon)
                .WithMany(e => e.PlantGrowingSeazons)
                .HasForeignKey(e => e.GrowingSeazonId);
            });

            builder.Entity<PlantOpinion>()
            .Property(p => p.PrivateUserId)
            .IsRequired(false);

            builder.Entity<PlantOpinion>()
                .Property(p => p.CustomerId)
                .IsRequired(false);

        }
    }
}
