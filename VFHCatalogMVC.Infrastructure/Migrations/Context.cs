using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Diagnostics.Tracing;
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

            //builder.Entity<PlantType>().HasData(new List<PlantType>()
            //{
            //   new PlantType{Name= "Warzywo"},
            //   new PlantType{Name = "Owoc"},
            //   new PlantType{Name = "Zioło" },
            //   new PlantType{Name = "Kwiat" }

            //});

            //builder.Entity<PlantGroup>().HasData(new List<PlantGroup>()
            //{
            //    new PlantGroup{PlantTypeId = 1, Name = "Psianka" },
            //    new PlantGroup{PlantTypeId = 1, Name = "Dyniowate" },
            //    new PlantGroup{PlantTypeId = 1, Name = "Strączkowe" },
            //    new PlantGroup{PlantTypeId = 1, Name = "Kapustne" },
            //    new PlantGroup{PlantTypeId = 1, Name = "Liściaste" },
            //    new PlantGroup{PlantTypeId = 1, Name = "Cebulowe" },
            //    new PlantGroup{PlantTypeId = 1, Name = "Korzeniowe" },
            //    new PlantGroup{PlantTypeId = 1, Name = "Rzepowate" },
            //    new PlantGroup{PlantTypeId = 2, Name = "Pestkowe" },
            //    new PlantGroup{PlantTypeId = 2, Name = "Jagodowe" },
            //    new PlantGroup{PlantTypeId = 2, Name = "Ziarnkowe" },
            //    new PlantGroup{PlantTypeId = 2, Name = "Cytrusowe" },
            //    new PlantGroup{PlantTypeId = 2, Name = "Egzotyczne" },
            //    new PlantGroup{PlantTypeId = 3, Name = "Lecznicze" },
            //    new PlantGroup{PlantTypeId = 3, Name = "Przyprawowe" },
            //    new PlantGroup{PlantTypeId = 3, Name = "Olejkodajne" },
            //    new PlantGroup{PlantTypeId = 4, Name = "Zewnętrzne" },
            //    new PlantGroup{PlantTypeId = 4, Name = "Domowe" },
            //    new PlantGroup{PlantTypeId = 6, Name = "Tradycyjne" },
            //    new PlantGroup{PlantTypeId = 6, Name = "Ozdobne" }

            //});

            //builder.Entity<PlantSection>().HasData(new List<PlantSection>()
            //{
            //    new PlantSection{Name="Pomidor", Id=1 },
            //    new PlantSection{Name="Paparyka",Id=1 },
            //    new PlantSection{Name="Ziemniak",Id=1},
            //    new PlantSection{Name="Bakłażan",Id=1},
            //    new PlantSection{Name="Inne",Id=1},
            //    new PlantSection{Name="Ogórek",Id=2},
            //    new PlantSection{Name="Cukinia",Id=2},
            //    new PlantSection{Name="Dynia",Id=2},
            //    new PlantSection{Name="Patison",Id=2},
            //    new PlantSection{Name="Inne",Id=2},
            //    new PlantSection{Name="Fasolka",Id=3},
            //    new PlantSection{Name="Groszek",Id=3},
            //    new PlantSection{Name="Soczewica",Id=3},
            //    new PlantSection{Name="Bób",Id=3},
            //     new PlantSection{Name="Inne",Id=3},
            //     new PlantSection{Name="Kapusta",Id=4},
            //     new PlantSection{Name="Brukselka",Id=4},
            //     new PlantSection{Name="Brokuł",Id=4},
            //     new PlantSection{Name="Kalafior",Id=4},
            //     new PlantSection{Name="Kalarepa",Id=4},
            //     new PlantSection{Name="Inne",Id=4},
            //     new PlantSection{Name="Sałata",Id=5},
            //     new PlantSection{Name="Szpinak",Id=5},
            //     new PlantSection{Name="Natka pietruszki",Id=5},
            //     new PlantSection{Name="Inne",Id=5},
            //     new PlantSection{Name="Cebula",Id=6},
            //     new PlantSection{Name="Czosnek",Id=6},
            //     new PlantSection{Name="Por",Id=6},
            //     new PlantSection{Name="Inne",Id=6},
            //     new PlantSection{Name="Marchewka",Id=7},
            //     new PlantSection{Name="Pietruszka",Id=7},
            //     new PlantSection{Name="Burak",Id=7},
            //     new PlantSection{Name="Seler",Id= 7},
            //     new PlantSection{Name="Inne",Id= 7},
            //     new PlantSection{Name="Rzodkiewka",Id= 8},
            //     new PlantSection{Name="Brukiew",Id= 8},
            //     new PlantSection{Name="Rzepa",Id= 8},
            //     new PlantSection{Name="Inne",Id= 8},
            //     new PlantSection{Name="Wiśnia",Id= 9},
            //     new PlantSection{Name="Brzoskiwnia",Id= 9},
            //     new PlantSection{Name="Śliwka",Id= 9},
            //     new PlantSection{Name="Morela",Id= 9},
            //     new PlantSection{Name="Inne",Id= 9},
            //     new PlantSection{Name="Truskawka",Id= 10},
            //     new PlantSection{Name="Jeżyna",Id= 10},
            //     new PlantSection{Name="Jagoda",Id= 10},
            //     new PlantSection{Name="Malina",Id= 10},
            //     new PlantSection{Name="Porzeczka",Id= 10},
            //     new PlantSection{Name="Inne",Id= 10},
            //     new PlantSection{Name="Jabłko",Id= 11},
            //     new PlantSection{Name="Gruszka",Id= 11},
            //     new PlantSection{Name="Pigwa",Id= 11},
            //     new PlantSection{Name="Granat ",Id= 11},
            //     new PlantSection{Name="Inne",Id= 11},
            //     new PlantSection{Name="Cytryna",Id= 12},
            //     new PlantSection{Name="Mandarynka",Id= 12},
            //     new PlantSection{Name="Pomarańcza",Id= 12},
            //     new PlantSection{Name="Grejfrut",Id= 12},
            //     new PlantSection{Name="Inne",Id= 12},
            //     new PlantSection{Name="Banan",Id= 13},
            //     new PlantSection{Name="Ananas",Id= 13},
            //     new PlantSection{Name="Liczi",Id= 13},
            //     new PlantSection{Name="Inne",Id= 13}

            //});


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

               // entity.HasData(new List<GrowthType>()
               // {
               //     new GrowthType{Name="Wysoko rosnący", PlantTypeId =1, PlantGroupId=1,PlantSectionId=1},
               //      new GrowthType{Name="Karłowy",PlantTypeId =1,PlantGroupId=1,PlantSectionId=1},
               //      new GrowthType{Name="Doniczkowy",PlantTypeId =1,PlantGroupId=1,PlantSectionId=1},
               //      new GrowthType{Name="Samokończący",PlantTypeId =1,PlantGroupId=1,PlantSectionId=1},
               //      new GrowthType{Name="Krzak",PlantTypeId =2,PlantGroupId=null,PlantSectionId=null},
               //      new GrowthType{Name="Drzewo",PlantTypeId =2,PlantGroupId=null,PlantSectionId=null},
               //      new GrowthType{Name="Roślina pnąca",PlantTypeId =2,PlantGroupId=null,PlantSectionId=null},
               //      new GrowthType{Name="Roślina zwisająca",PlantTypeId =2,PlantGroupId=null,PlantSectionId=null},
               //      new GrowthType{Name="Krzew",PlantTypeId =3,PlantGroupId=null,PlantSectionId=null},
               //      new GrowthType{Name="Winorośl",PlantTypeId =3,PlantGroupId=null,PlantSectionId=null},
               //      new GrowthType{Name="Korzeń",PlantTypeId =3,PlantGroupId=null,PlantSectionId=null}

               //});
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

                //entity.HasData(new List<FruitSize>
                //{
                //    new FruitSize{Name="Małe",PlantTypeId=1,PlantGroupId=1,PlantSectionId=1 },
                //    new FruitSize{Name="Typu Cherry",PlantTypeId=1,PlantGroupId=1,PlantSectionId=1},
                //    new FruitSize{Name="Wielkoowocowe",PlantTypeId=1,PlantGroupId=1,PlantSectionId=1},
                //    new FruitSize{Name="Srednioowocowe",PlantTypeId=1,PlantGroupId=1,PlantSectionId=1}
                //});
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


                //entity.HasData(new List<FruitType>
                //{ 
                //    new FruitType{Name="Mięsiste",PlantTypeId=1,PlantGroupId=1,PlantSectionId=1 },
                //    new FruitType{Name="Wielokomorowe",PlantTypeId=1,PlantGroupId=1, PlantSectionId=1},
                //    new FruitType{Name="Ostre",PlantTypeId=1,PlantGroupId=1,PlantSectionId= 2},
                //    new FruitType{Name="Słodkie",PlantTypeId=1,PlantGroupId=1,PlantSectionId= 2}

                //});
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

                entity.HasOne(e => e.Message)
                .WithMany(e => e.PlantMessages)
                .HasForeignKey(e => e.MessageId);
            });

            builder.Entity<PlantPosition>(entity =>
            {
                entity.HasKey(e => new { e.PlantDetailId, e.PositionId });

                entity.HasOne(e => e.PlantDetail)
                .WithMany(e => e.PlantPositions)
                .HasForeignKey(e => e.PlantDetailId);

                entity.HasOne(e => e.Position)
                .WithMany(e => e.PlantPositions)
                .HasForeignKey(e => e.PositionId);

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

            //builder.Entity<Destination>().HasData(new List<Destination>
            //{              
            //    new Destination{Name= "Grunt"},
            //    new Destination{Name="Pod osłony"},
            //    new Destination{Name="Doniczka"}
            //});

            //builder.Entity<GrowingSeazon>().HasData(new List<GrowingSeazon>
            //{
            //    new GrowingSeazon{Name ="Późne"},
            //    new GrowingSeazon{Name="Wczesne"},
            //    new GrowingSeazon{Name="Średniopóźne"},
            //    new GrowingSeazon{Name="Średniowczesne"},
            //    new GrowingSeazon{Name="Jednoroczne"},
            //    new GrowingSeazon{Name="Wieloletnie"}
            //});

            //builder.Entity<Color>().HasData(new List<Color>
            //{
            //     new Color{Name ="Biały"},
            //    new Color{Name="Czarny"},
            //    new Color{Name="Czerwony"},
            //    new Color{Name="Indigo"},
            //    new Color{Name="Pomarańczowy"},
            //    new Color{Name="Różowy"},
            //    new Color{Name="Wielokolorowy"},
            //    new Color{Name="Zielony"},
            //    new Color{Name="Żółty"}


            //    //new Color{Name ="Biały", Id = (int)DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Czarny",Id = (int)DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Czerwony",Id = (int)DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Indigo", Id =(int) DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Pomarańczowy", Id = (int)DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Różowy", Id =(int) DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Wielokolorowy", Id =(int) DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Zielony", Id = (int)DatabaseGeneratedOption.Identity},
            //    //new Color{Name="Żółty", Id =(int) DatabaseGeneratedOption.Identity}

            //});

            //builder.Entity<Position>().HasData(new List<Position>() 
            //{

            //    new Position { Name = "Słoneczne" },
            //    new Position { Name = "Półcień" },
            //    new Position { Name = "Cień" }
            //    });

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
