using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure;
using VFHCatalogMVC.Infrastructure.Seed;

namespace Application.UnitTests.Common
{
    /// <summary>
    /// create DbContext with database and memory
    /// </summary>
    public class DbContextFactory
    {
        public static Mock<Context> Create()
        {
            //create database in memory
            var options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            var mock = new Mock<Context>(options) {CallBase=true}; //call base construct

            var context = mock.Object;

            //assures us that the database has been created
            context.Database.EnsureCreated();

            DataSeed(context);
                    

            return mock;

        }

        public static void Destroy(Context context)
        {
            context.Database.EnsureDeleted();
            //cleaning context
            context.Dispose();
        }

        public static void  DataSeed(Context context)
        {
            var plantType = new List<PlantType>
            {
                 new PlantType() { Id = 1, Name = "Vegetable" },
                 new PlantType() { Id = 2, Name = "Fruit" },
                 new PlantType() { Id = 3, Name = "Herb" },

            };
            context.AddRange(plantType);

            var plantGroup = new List<PlantGroup>
            {
                new PlantGroup() { Id = 1, Name = "Nightshade", PlantTypeId = 1 },
                new PlantGroup() { Id = 2, Name = "Cucurbits", PlantTypeId = 1 },
                new PlantGroup() { Id = 3, Name = "Pitted", PlantTypeId = 2 },
                new PlantGroup() { Id = 4, Name = "Berry", PlantTypeId = 2 },
                new PlantGroup() { Id = 5, Name = "Healing", PlantTypeId = 3 },
                new PlantGroup() { Id = 6, Name = "Spicy", PlantTypeId = 3 },
            };
            context.AddRange(plantGroup);

            var plantSection = new List<PlantSection>
            {
               new PlantSection() { Id = 1, Name = "Tomato", PlantGroupId = 1 },
               new PlantSection() { Id = 2, Name = "Pepper", PlantGroupId = 1 },
               new PlantSection() { Id = 6, Name = "Cucumber", PlantGroupId = 2 },
               new PlantSection() { Id = 7, Name = "Zucchini", PlantGroupId = 2 },
               new PlantSection() { Id = 39, Name = "Cherries", PlantGroupId = 3 },
               new PlantSection() { Id = 40, Name = "Peach", PlantGroupId = 3 },
               new PlantSection() { Id = 44, Name = "Strawberry", PlantGroupId = 4 },
               new PlantSection() { Id = 45, Name = "Blackberries", PlantGroupId = 4 },

            };
            context.AddRange(plantSection);

            var color = new List<Color>
            {
                new Color() { Id = 1, Name = "White" },
                new Color() { Id = 2, Name = "Black" },
            };
            context.AddRange(color);

            var fruitSize = new List<FruitSize>
            {
                new FruitSize() { Id = 1, Name = "Not specified" },
                new FruitSize() { Id = 2, Name = "Small" },
                new FruitSize() { Id = 3, Name = "Cherry type" },
                new FruitSize() { Id = 4, Name = "Large-fruited" },
                new FruitSize() { Id = 5, Name = "Medium-fruited" }
            };
            context.AddRange(fruitSize);

            var fruitSizeForFilter = new List<FruitSizeForListFilters>
            {
                new FruitSizeForListFilters() { FruitSizeId = 2, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                new FruitSizeForListFilters() { FruitSizeId = 3, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                new FruitSizeForListFilters() { FruitSizeId = 4, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                new FruitSizeForListFilters() { FruitSizeId = 5, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 }
            };
            context.AddRange(fruitSizeForFilter);

            var fruitType = new List<FruitType>
            {
                new FruitType() { Id = 1, Name = "Not specified" },
                new FruitType() { Id = 2, Name = "Fleshy" },
                new FruitType() { Id = 3, Name = "Multichambered" },
                new FruitType() { Id = 4, Name = "Spicy" },
                new FruitType() { Id = 5, Name = "Sweet" }
            };
            context.AddRange(fruitType);

            var fruitTypeForFilter = new List<FruitTypeForListFilters>
            {
                new FruitTypeForListFilters() { FruitTypeId = 2, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                new FruitTypeForListFilters() { FruitTypeId = 3, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                new FruitTypeForListFilters() { FruitTypeId = 4, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 2 },
                new FruitTypeForListFilters() { FruitTypeId = 5, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 2 }
            };
            context.AddRange(fruitTypeForFilter);

            var growthType = new List<GrowthType>
            {
                    new GrowthType() { Id = 1, Name = "Not specified" },
                    new GrowthType() { Id = 2, Name = "Tall growing" },
                    new GrowthType() { Id = 3, Name = "Dwarf" },
                    new GrowthType() { Id = 4, Name = "Potted" },
                    new GrowthType() { Id = 5, Name = "Determinate" },
                    new GrowthType() { Id = 6, Name = "Shrub" },
                    new GrowthType() { Id = 7, Name = "Sweet" },
                    new GrowthType() { Id = 8, Name = "Bush" },
                    new GrowthType() { Id = 9, Name = "Tree" },
                    new GrowthType() { Id = 10, Name = "Climbing plant" },
                    new GrowthType() { Id = 11, Name = "Hanging plant" },
                    new GrowthType() { Id = 12, Name = "Vine" },
                    new GrowthType() { Id = 13, Name = "Root" }
            };
            context.AddRange(growthType);

            var growthTypeForFilter = new List<GrowthTypesForListFilters>
            {
                     new GrowthTypesForListFilters() { GrowthTypesId = 2, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                     new GrowthTypesForListFilters() { GrowthTypesId = 3, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                     new GrowthTypesForListFilters() { GrowthTypesId = 4, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                     new GrowthTypesForListFilters() { GrowthTypesId = 5, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                     new GrowthTypesForListFilters() { GrowthTypesId = 6, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 2 },
                     new GrowthTypesForListFilters() { GrowthTypesId = 6, PlantTypeId = 2, PlantGroupId = null, PlantSectionId = null },
                     new GrowthTypesForListFilters() { GrowthTypesId = 7, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 2 },
                     new GrowthTypesForListFilters() { GrowthTypesId = 8, PlantTypeId = 2, PlantGroupId = null, PlantSectionId = null },
                     new GrowthTypesForListFilters() { GrowthTypesId = 9, PlantTypeId = 2, PlantGroupId = null, PlantSectionId = null },
                     new GrowthTypesForListFilters() { GrowthTypesId = 10, PlantTypeId = 2, PlantGroupId = null, PlantSectionId = null },
                     new GrowthTypesForListFilters() { GrowthTypesId = 11, PlantTypeId = 3, PlantGroupId = null, PlantSectionId = null },
                     new GrowthTypesForListFilters() { GrowthTypesId = 12, PlantTypeId = 3, PlantGroupId = null, PlantSectionId = null },
                     new GrowthTypesForListFilters() { GrowthTypesId = 13, PlantTypeId = 3, PlantGroupId = null, PlantSectionId = null }
            };
            context.AddRange(growthTypeForFilter);

            var growingSeazons = new List<GrowingSeazon>
            {
                     new GrowingSeazon() { Id = 1, Name = "Late" },
                     new GrowingSeazon() { Id = 2, Name = "Early" },
                     new GrowingSeazon() { Id = 3, Name = "Mid-late" },
                     new GrowingSeazon() { Id = 4, Name = "Mid-early" },
                     new GrowingSeazon() { Id = 5, Name = "Annual" },
                     new GrowingSeazon() { Id = 6, Name = "Perennial" }
            };
           context.AddRange(growingSeazons);

            var destinations = new List<Destination>
            {
                    new Destination() { Id = 1, Name = "Ground" },
                    new Destination() { Id = 2, Name = "Under covers" },
                    new Destination() { Id = 3, Name = "Pot" }
            };
            context.AddRange(destinations);

            var plant = new Plant
            {
                Id = 1,
                PlantTypeId = 1,
                PlantGroupId = 1,
                PlantSectionId = 1,
                FullName = "test",
                Photo = "test",
                isActive = true,
                isNew = false,

            };
            context.Add(plant);
            var plantDetails = new PlantDetail
            {
                Id = 1,
                PlantRef = 1,
                Description = "test",
                PlantPassportNumber = "test",
                ColorId = 1,
                FruitSizeId = 1,
                FruitTypeId = 1
              
            };
            context.Add(plantDetails);

            var plantGrowthType = new List<PlantGrowthType>
            {
                new PlantGrowthType {GrowthTypeId =2, PlantDetailId =1},
                new PlantGrowthType{GrowthTypeId =3, PlantDetailId = 1},
            };
            context.AddRange(plantGrowthType);

            var plantGrowingSeazons = new List<PlantGrowingSeazon>
            { 
                new PlantGrowingSeazon{GrowingSeazonId = 1, PlantDetailId =1 },
                new PlantGrowingSeazon{GrowingSeazonId = 4, PlantDetailId =1 }
            };

            var plantDestinations = new List<PlantDestination>
            {
                new PlantDestination {DestinationId =1, PlantDetailId=1 },
                new PlantDestination {DestinationId =2, PlantDetailId=1 },
                new PlantDestination {DestinationId =3, PlantDetailId=1 },

            };

            var userAdmin = new List<ApplicationUser>
            {   new ApplicationUser()
                {
                Id = "0a249d73-5e9a-4c07-9832-27645a2c2fe8",
                UserName = "admin2@gmail.com",
                NormalizedUserName = "ADMIN2@GMAIL.COM",
                Email = "admin2@gmail.com",
                NormalizedEmail = "ADMIN2@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAEDeZRsN0w0Gs6YSisBi9jbmg7ihLkvOxZgsuCjScMg2GD1JtcbU2tSzMjclvwSrSxA==", //Admin1_1
                SecurityStamp = "Z4NQVBZ2LMDZAJM675CY3465JPFGY2PS",
                ConcurrencyStamp = "88df6574-21db-41c6-b833-ce439584e236",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                AccountName = "Admin",
                FirstName = null,
                LastName = null,
                CompanyName = null,
                NIP = null,
                REGON = null,
                CEOName = null,
                CEOLastName = null,
                LogoPic = null,
                isActive = true
                },
                new ApplicationUser
            {
                Id = "2ef2b510-aa25-42ca-b68a-ee2fa0635924",
                UserName = "kinga123@gmail.com",
                NormalizedUserName = "KINGA123@GMAIL.COM",
                Email = "kinga123@gmail.com",
                NormalizedEmail = "KINGA123@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAECycEAX8sBrbNrgbYD2NdV0xoMgU2pJjfmsSi3J+ZczthajMzjaIuU5VMuKVyLGV/w==", // Kinga1_123
                SecurityStamp = "3QZ3HMO2U2QS27FOTYYOKNS2AYMMYTZM",
                ConcurrencyStamp = "cce6f078-25f9-4bb8-9af5-73cd0c91d645",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0,
                AccountName = "Kinga",
                FirstName = null,
                LastName = null,
                CompanyName = null,
                NIP = null,
                REGON = null,
                CEOName = null,
                CEOLastName = null,
                LogoPic = null,
                isActive = true
            }
            };
            context.AddRange(userAdmin);

            var identityRole = new List<IdentityUserRole<string>>
            {
                new IdentityUserRole<string> { UserId = "0a249d73-5e9a-4c07-9832-27645a2c2fe8", RoleId = "Admin" },
                new IdentityUserRole<string> { UserId = "2ef2b510-aa25-42ca-b68a-ee2fa0635924", RoleId = "PrivateUser" },
            };

            context.SaveChanges();
        }
    }
}
