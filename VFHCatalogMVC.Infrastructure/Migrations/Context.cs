using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata;
using VFHCatalogMVC.Infrastructure.Common;
using VFHCatalogMVC.Infrastructure.Mapping;
using VFHCatalogMVC.Infrastructure.Seed;

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
        public DbSet<FruitSizeForListFilters> FruitSizeForListFilters { get; set; }
        public DbSet<FruitTypeForListFilters> FruitTypeForListFilters { get; set; }
        public DbSet<GrowthTypesForListFilters> GrowthTypesForListFilters { get; set; }
        public DbSet<AuditTrial> AuditTrials { get; set; }

        public ICurrentSessionProvider CurrentSessionProvider;
        public Context(DbContextOptions options, ICurrentSessionProvider currentSessionProvider) : base(options)
        {
            CurrentSessionProvider = currentSessionProvider;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            GetApplyConfiguration(builder);
            DataSeed.Seed(builder);

        }

        private static void GetApplyConfiguration(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AuditTrailConfiguration());
            builder.ApplyConfiguration(new AddressConfigration());
            builder.ApplyConfiguration(new PlantConfiguration());
            builder.ApplyConfiguration(new PlantDetailConfiguration());
            builder.ApplyConfiguration(new PlantTagConfiguration());
            builder.ApplyConfiguration(new PlantDestinationConfiguration());
            builder.ApplyConfiguration(new FruitSizeForListFiltersConfiguration());
            builder.ApplyConfiguration(new FruitTypeForListFiltersConfiguration());
            builder.ApplyConfiguration(new GrowthTypesForListFiltersConfiguration());
            builder.ApplyConfiguration(new PlantGrowthTypeConfiguration());
            builder.ApplyConfiguration(new PlantGrowingSeazonConfiguration());
            builder.ApplyConfiguration(new ContactDetailForSeedConfiguration());
            builder.ApplyConfiguration(new ContactDetailForSeedlingConfiguration());
            builder.ApplyConfiguration(new MessageAnswerConfiguration());
            builder.ApplyConfiguration(new NewUserPlantConfiguration());
            builder.ApplyConfiguration(new PlantMessageConfiguration());         
         
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = CurrentSessionProvider.GetUserId();

            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = userId;
                        entry.Entity.CreatedAtUtc = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = userId;
                        entry.Entity.UpdatedAtUtc = DateTime.Now;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.UpdatedBy = userId;
                        entry.Entity.UpdatedAtUtc = DateTime.Now;
                        entry.Entity.InactivatedBy = userId;
                        entry.Entity.UpdatedAtUtc= DateTime.Now;
                        entry.State = EntityState.Modified;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);

            //var userId = CurrentSessionProvider.GetUserId();

            //SetAuditableProperties(userId);

            //var auditEntries = HandleAuditingBeforeSaveChanges(userId).ToList();
            //if (auditEntries.Count > 0)
            //{
            //    AuditTrials.AddRange(auditEntries);
            //}

            //return await base.SaveChangesAsync(cancellationToken);
        }

        private List<AuditTrial> HandleAuditingBeforeSaveChanges(string userId)
        {
            var auditableEntries = ChangeTracker.Entries<IAuditableEntity>()
                     .Where(x => x.State == EntityState.Added ||
                                 x.State == EntityState.Deleted ||
                                 x.State == EntityState.Modified)
                     .Select(x => CreateTrailEntry(userId, x))
                     .ToList();
            return auditableEntries;
        }

        private static AuditTrial CreateTrailEntry(string userId, EntityEntry<IAuditableEntity> entry)
        {
            var trailEntry = new AuditTrial
            {
                Id = Guid.NewGuid(),
                EntityName = entry.Entity.GetType().Name,
                UserId = userId,
                DateUtc = DateTime.UtcNow
            };

            SetAuditTrailPropertyValues(entry, trailEntry); //plain properties
            SetAuditTrailNavigationValues(entry, trailEntry); // reference properites
            SetAuditTrailReferenceValues(entry, trailEntry); //navigation property

            return trailEntry;
        }

        private static void SetAuditTrailReferenceValues(EntityEntry entry, AuditTrial trailEntry)
        {
            foreach (var reference in entry.References.Where(x => x.IsModified))
            {
                var referenceName = reference.EntityEntry.Entity.GetType().Name;
                trailEntry.ChangedColumns.Add(referenceName);
            }
        }

        private static void SetAuditTrailNavigationValues(EntityEntry entry, AuditTrial trailEntry)
        {

            foreach (var navigation in entry.Navigations.Where(x => x.Metadata.IsCollection() && x.IsModified))
            {
                var enumerable = navigation.CurrentValue as IEnumerable<object>;
                if (enumerable == null)
                {
                    continue; //skip is CurrentBalue isn't collection
                }

                var collection = enumerable.ToList();
                if (collection.Count == 0)
                {
                    continue; // skip null collection
                }

                var navigationName = collection.First().GetType().Name;
                trailEntry.ChangedColumns.Add(navigationName);
            }
        }

        private static void SetAuditTrailPropertyValues(EntityEntry entry, AuditTrial trailEntry)
        {
            // Skip temp fields (that will be assigned automatically by ef core engine, for example: when inserting an entity
            foreach (var property in entry.Properties.Where(x => !x.IsTemporary))
            {
                if (property.Metadata.IsPrimaryKey())
                {
                    trailEntry.PrimaryKey = property.CurrentValue?.ToString();
                    continue;
                }

                // Filter properties that should not appear in the audit list
                if (property.Metadata.Name.Equals("PasswordHash"))
                {
                    continue;
                }

                SetAuditTrailPropertyValue(entry, trailEntry, property);
            }
        }

        private static void SetAuditTrailPropertyValue(EntityEntry entry, AuditTrial trailEntry, PropertyEntry property)
        {
            var propertyName = property.Metadata.Name;

            switch (entry.State)
            {
                case EntityState.Added:
                    trailEntry.TrailType = TrailType.Create;
                    trailEntry.NewValues[propertyName] = property.CurrentValue;

                    break;

                case EntityState.Deleted:
                    trailEntry.TrailType = TrailType.Delete;
                    trailEntry.OldValues[propertyName] = property.OriginalValue;

                    break;

                case EntityState.Modified:
                    if (property.IsModified && (property.OriginalValue is null || !property.OriginalValue.Equals(property.CurrentValue)))
                    {
                        trailEntry.ChangedColumns.Add(propertyName);
                        trailEntry.TrailType = TrailType.Update;
                        trailEntry.OldValues[propertyName] = property.OriginalValue;
                        trailEntry.NewValues[propertyName] = property.CurrentValue;
                    }

                    break;
            }

            if (trailEntry.ChangedColumns.Count > 0)
            {
                trailEntry.TrailType = TrailType.Update;
            }
        }

        private void SetAuditableProperties(string userId)
        {
            const string systemSource = "system";
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAtUtc = DateTime.UtcNow;
                        entry.Entity.CreatedBy = userId?.ToString() ?? systemSource;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId?.ToString() ?? systemSource;
                        break;

                    case EntityState.Deleted:
                        entry.Entity.UpdatedAtUtc = DateTime.UtcNow;
                        entry.Entity.UpdatedBy = userId?.ToString() ?? systemSource;
                        entry.Entity.InactivatedBy = userId?.ToString() ?? systemSource;
                        entry.Entity.InactivatedAtUtc = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
