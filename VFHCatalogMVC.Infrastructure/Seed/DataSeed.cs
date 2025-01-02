using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Infrastructure.Seed
{
    public static class DataSeed
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<PlantType>()
                 .HasData(
                     new PlantType() { Id = 1, Name = "Vegetable" },
                     new PlantType() { Id = 2, Name = "Fruit" },
                     new PlantType() { Id = 3, Name = "Herb" },
                     new PlantType() { Id = 4, Name = "Flower" },
                     new PlantType() { Id = 5, Name = "Home plant" },
                     new PlantType() { Id = 6, Name = "Grass" }
                 );

            builder.Entity<PlantGroup>()
                 .HasData(
                     new PlantGroup() { Id = 1, Name = "Nightshade", PlantTypeId = 1 },
                     new PlantGroup() { Id = 2, Name = "Cucurbits", PlantTypeId = 1 },
                     new PlantGroup() { Id = 3, Name = "Legumes", PlantTypeId = 1 },
                     new PlantGroup() { Id = 4, Name = "Cruciferous", PlantTypeId = 1 },
                     new PlantGroup() { Id = 5, Name = "Leafy", PlantTypeId = 1 },
                     new PlantGroup() { Id = 6, Name = "Onion", PlantTypeId = 1 },
                     new PlantGroup() { Id = 7, Name = "Root", PlantTypeId = 1 },
                     new PlantGroup() { Id = 8, Name = "Turnip greens", PlantTypeId = 1 },
                     new PlantGroup() { Id = 9, Name = "Pitted", PlantTypeId = 2 },
                     new PlantGroup() { Id = 10, Name = "Berry", PlantTypeId = 2 },
                     new PlantGroup() { Id = 11, Name = "Pome", PlantTypeId = 2 },
                     new PlantGroup() { Id = 12, Name = "Citrus", PlantTypeId = 2 },
                     new PlantGroup() { Id = 13, Name = "Exotic", PlantTypeId = 2 },
                     new PlantGroup() { Id = 14, Name = "Healing", PlantTypeId = 3 },
                     new PlantGroup() { Id = 15, Name = "Spicy", PlantTypeId = 3 },
                     new PlantGroup() { Id = 16, Name = "Essential oil", PlantTypeId = 3 },
                     new PlantGroup() { Id = 17, Name = "Outdoor", PlantTypeId = 4 },
                     new PlantGroup() { Id = 18, Name = "Indoor", PlantTypeId = 4 }
                 );

            builder.Entity<PlantSection>()
                 .HasData(
                     new PlantSection() { Id = 1, Name = "Tomato", PlantGroupId = 1 },
                     new PlantSection() { Id = 2, Name = "Pepper", PlantGroupId = 1 },
                     new PlantSection() { Id = 3, Name = "Potato", PlantGroupId = 1 },
                     new PlantSection() { Id = 4, Name = "Eggplant", PlantGroupId = 1 },
                     new PlantSection() { Id = 5, Name = "Other", PlantGroupId = 1 },
                     new PlantSection() { Id = 6, Name = "Cucumber", PlantGroupId = 2 },
                     new PlantSection() { Id = 7, Name = "Zucchini", PlantGroupId = 2 },
                     new PlantSection() { Id = 8, Name = "Pumpkin", PlantGroupId = 2 },
                     new PlantSection() { Id = 9, Name = "Patison", PlantGroupId = 2 },
                     new PlantSection() { Id = 10, Name = "Other", PlantGroupId = 2 },
                     new PlantSection() { Id = 11, Name = "Beans", PlantGroupId = 3 },
                     new PlantSection() { Id = 12, Name = "Pea", PlantGroupId = 3 },
                     new PlantSection() { Id = 13, Name = "Lentils", PlantGroupId = 3 },
                     new PlantSection() { Id = 14, Name = "Broad bean", PlantGroupId = 3 },
                     new PlantSection() { Id = 15, Name = "Other", PlantGroupId = 3 },
                     new PlantSection() { Id = 16, Name = "Cabbage", PlantGroupId = 4 },
                     new PlantSection() { Id = 17, Name = "Brussels sprouts", PlantGroupId = 4 },
                     new PlantSection() { Id = 18, Name = "Broccoli", PlantGroupId = 4 },
                     new PlantSection() { Id = 19, Name = "Cauliflower", PlantGroupId = 4 },
                     new PlantSection() { Id = 20, Name = "Kohlrabi", PlantGroupId = 4 },
                     new PlantSection() { Id = 21, Name = "Other", PlantGroupId = 4 },
                     new PlantSection() { Id = 22, Name = "Lettuce", PlantGroupId = 5 },
                     new PlantSection() { Id = 23, Name = "Spinach", PlantGroupId = 5 },
                     new PlantSection() { Id = 24, Name = "Leaf parsley", PlantGroupId = 5 },
                     new PlantSection() { Id = 25, Name = "Other", PlantGroupId = 5 },
                     new PlantSection() { Id = 26, Name = "Onion", PlantGroupId = 6 },
                     new PlantSection() { Id = 27, Name = "Garlic", PlantGroupId = 6 },
                     new PlantSection() { Id = 28, Name = "Leek", PlantGroupId = 6 },
                     new PlantSection() { Id = 29, Name = "Other", PlantGroupId = 6 },
                     new PlantSection() { Id = 30, Name = "Carrot", PlantGroupId = 7 },
                     new PlantSection() { Id = 31, Name = "Root parsley", PlantGroupId = 7 },
                     new PlantSection() { Id = 32, Name = "Beetroot", PlantGroupId = 7 },
                     new PlantSection() { Id = 33, Name = "Root celery", PlantGroupId = 7 },
                     new PlantSection() { Id = 34, Name = "Other", PlantGroupId = 7 },
                     new PlantSection() { Id = 35, Name = "Radish", PlantGroupId = 8 },
                     new PlantSection() { Id = 36, Name = "Rutabaga", PlantGroupId = 8 },
                     new PlantSection() { Id = 37, Name = "Turnip", PlantGroupId = 8 },
                     new PlantSection() { Id = 38, Name = "Other", PlantGroupId = 8 },
                     new PlantSection() { Id = 39, Name = "Cherries", PlantGroupId = 9 },
                     new PlantSection() { Id = 40, Name = "Peach", PlantGroupId = 9 },
                     new PlantSection() { Id = 41, Name = "Plum", PlantGroupId = 9 },
                     new PlantSection() { Id = 42, Name = "Apricot", PlantGroupId = 9 },
                     new PlantSection() { Id = 43, Name = "Other", PlantGroupId = 9 },
                     new PlantSection() { Id = 44, Name = "Strawberry", PlantGroupId = 10 },
                     new PlantSection() { Id = 45, Name = "Blackberries", PlantGroupId = 10 },
                     new PlantSection() { Id = 46, Name = "Blueberries", PlantGroupId = 10 },
                     new PlantSection() { Id = 47, Name = "Raspberries", PlantGroupId = 10 },
                     new PlantSection() { Id = 48, Name = "Currants", PlantGroupId = 10 },
                     new PlantSection() { Id = 49, Name = "Berries", PlantGroupId = 10 },
                     new PlantSection() { Id = 50, Name = "Other", PlantGroupId = 10 },
                     new PlantSection() { Id = 51, Name = "Apple", PlantGroupId = 11 },
                     new PlantSection() { Id = 52, Name = "Pear", PlantGroupId = 11 },
                     new PlantSection() { Id = 53, Name = "Quince", PlantGroupId = 11 },
                     new PlantSection() { Id = 54, Name = "Pomegranate", PlantGroupId = 11 },
                     new PlantSection() { Id = 55, Name = "Other", PlantGroupId = 11 },
                     new PlantSection() { Id = 56, Name = "Lemon", PlantGroupId = 12 },
                     new PlantSection() { Id = 57, Name = "Tangerine", PlantGroupId = 12 },
                     new PlantSection() { Id = 58, Name = "Orange", PlantGroupId = 12 },
                     new PlantSection() { Id = 59, Name = "Grapefruit", PlantGroupId = 12 },
                     new PlantSection() { Id = 60, Name = "Other", PlantGroupId = 12 },
                     new PlantSection() { Id = 61, Name = "Banana", PlantGroupId = 13 },
                     new PlantSection() { Id = 62, Name = "Pineapple", PlantGroupId = 13 },
                     new PlantSection() { Id = 63, Name = "Lychee", PlantGroupId = 13 },
                     new PlantSection() { Id = 64, Name = "Other", PlantGroupId = 13 }
                 );

            builder.Entity<GrowthType>()
                .HasData(
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
                );

            builder.Entity<GrowthTypesForListFilters>()
                 .HasData(
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
                 );

            builder.Entity<GrowingSeazon>()
                 .HasData(
                     new GrowingSeazon() { Id = 1, Name = "Late" },
                     new GrowingSeazon() { Id = 2, Name = "Early" },
                     new GrowingSeazon() { Id = 3, Name = "Mid-late" },
                     new GrowingSeazon() { Id = 4, Name = "Mid-early" },
                     new GrowingSeazon() { Id = 5, Name = "Annual" },
                     new GrowingSeazon() { Id = 6, Name = "Perennial" }
                 );

            builder.Entity<FruitType>()
                .HasData(
                    new FruitType() { Id = 1, Name = "Not specified" },
                    new FruitType() { Id = 2, Name = "Fleshy" },
                    new FruitType() { Id = 3, Name = "Multichambered" },
                    new FruitType() { Id = 4, Name = "Spicy" },
                    new FruitType() { Id = 5, Name = "Sweet" }
                );

            builder.Entity<FruitTypeForListFilters>()
                .HasData(
                    new FruitTypeForListFilters() { FruitTypeId = 2, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                    new FruitTypeForListFilters() { FruitTypeId = 3, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                    new FruitTypeForListFilters() { FruitTypeId = 4, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 2 },
                    new FruitTypeForListFilters() { FruitTypeId = 5, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 2 }
                );

            builder.Entity<FruitSize>()
                .HasData(
                    new FruitSize() { Id = 1, Name = "Not specified" },
                    new FruitSize() { Id = 2, Name = "Small" },
                    new FruitSize() { Id = 3, Name = "Cherry type" },
                    new FruitSize() { Id = 4, Name = "Large-fruited" },
                    new FruitSize() { Id = 5, Name = "Medium-fruited" }
                );

            builder.Entity<FruitSizeForListFilters>()
                .HasData(
                    new FruitSizeForListFilters() { FruitSizeId = 2, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                    new FruitSizeForListFilters() { FruitSizeId = 3, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                    new FruitSizeForListFilters() { FruitSizeId = 4, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 },
                    new FruitSizeForListFilters() { FruitSizeId = 5, PlantTypeId = 1, PlantGroupId = 1, PlantSectionId = 1 }
                );

            builder.Entity<Destination>()
                .HasData(
                    new Destination() { Id = 1, Name = "Ground" },
                    new Destination() { Id = 2, Name = "Under covers" },
                    new Destination() { Id = 3, Name = "Pot" }
                );

            builder.Entity<Color>()
                .HasData(
                    new Color() { Id = 1, Name = "White" },
                    new Color() { Id = 2, Name = "Black" },
                    new Color() { Id = 3, Name = "Red" },
                    new Color() { Id = 4, Name = "Indigo" },
                    new Color() { Id = 5, Name = "Orange" },
                    new Color() { Id = 6, Name = "Pink" },
                    new Color() { Id = 7, Name = "Multicolor" },
                    new Color() { Id = 8, Name = "Green" },
                    new Color() { Id = 9, Name = "Yellow" }
                );

            builder.Entity<Country>()
                .HasData(new Country() { Id = 1, Name = "Poland" });

            builder.Entity<Region>()
               .HasData(
                   new Region { Id = 1, Name = "Dolnośląskie", CountryId = 1 },
                   new Region { Id = 2, Name = "Kujawsko-Pomorskie", CountryId = 1 },
                   new Region { Id = 3, Name = "Lubelskie", CountryId = 1 },
                   new Region { Id = 4, Name = "Lubuskie", CountryId = 1 },
                   new Region { Id = 5, Name = "Łódzkie", CountryId = 1 },
                   new Region { Id = 6, Name = "Małopolskie", CountryId = 1 },
                   new Region { Id = 7, Name = "Mazowieckie", CountryId = 1 },
                   new Region { Id = 8, Name = "Opolskie", CountryId = 1 },
                   new Region { Id = 9, Name = "Podkarpackie", CountryId = 1 },
                   new Region { Id = 10, Name = "Podlaskie", CountryId = 1 },
                   new Region { Id = 11, Name = "Pomorskie", CountryId = 1 },
                   new Region { Id = 12, Name = "Śląskie", CountryId = 1 },
                   new Region { Id = 13, Name = "Świętokrzyskie", CountryId = 1 },
                   new Region { Id = 14, Name = "Warmińsko-Mazurskie", CountryId = 1 },
                   new Region { Id = 15, Name = "Wielkopolskie", CountryId = 1 },
                   new Region { Id = 16, Name = "Zachodniopomorskie", CountryId = 1 }
               );

            builder.Entity<City>()
               .HasData(
                   new City { Id = 1, Name = "Katowice", RegionId = 12 },
                   new City { Id = 2, Name = "Gliwice", RegionId = 12 },
                   new City { Id = 3, Name = "Zabrze", RegionId = 12 },
                   new City { Id = 4, Name = "Sosnowiec", RegionId = 12 },
                   new City { Id = 5, Name = "Bytom", RegionId = 12 },
                   new City { Id = 6, Name = "Rybnik", RegionId = 12 },
                   new City { Id = 7, Name = "Chorzów", RegionId = 12 },
                   new City { Id = 8, Name = "Tychy", RegionId = 12 },
                   new City { Id = 9, Name = "Dąbrowa Górnicza", RegionId = 12 },
                   new City { Id = 10, Name = "Jaworzno", RegionId = 12 }
               );

            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Id = "PrivateUser", Name = "PrivateUser", NormalizedName = "PRIVATE_USER" },
                    new IdentityRole { Id = "Company", Name = "Company", NormalizedName = "COMPANY" }
                );

          

            builder.Entity<ApplicationUser>()
            .HasData(new ApplicationUser
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
            },
            new ApplicationUser
            {
                Id = "b9c413fb-7822-4bf2-8028-30597aab757b",
                UserName = "sara2013@gmail.com",
                NormalizedUserName = "SARA2013@GMAIL.COM",
                Email = "sara2013@gmail.com",
                NormalizedEmail = "SARA2013@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = "AQAAAAEAACcQAAAAENew82n5Ros4D7PYuUpk0hyOVL6qkqteLtF1Wrz0uBd0BhoHnHd9VKfzUvIn/ySdRQ==", //Sara_123
                SecurityStamp = "QHE5B4ZDTU3QRAMHMMLIMHOOWXLK73S7",
                ConcurrencyStamp = "b5d8a96f-6606-472d-9646-61ff81fa5a1d",
                AccountName = "Sara",
                FirstName = null,
                LastName = null,
                CompanyName = null,
                NIP = null,
                REGON = null,
                CEOName = null,
                CEOLastName = null,
                LogoPic = null,
                isActive = true
            });

            builder.Entity<IdentityUserRole<string>>()
                .HasData(
                  new IdentityUserRole<string> { UserId = "0a249d73-5e9a-4c07-9832-27645a2c2fe8", RoleId = "Admin" },
                  new IdentityUserRole<string> { UserId = "2ef2b510-aa25-42ca-b68a-ee2fa0635924", RoleId = "PrivateUser" },
                  new IdentityUserRole<string> { UserId = "b9c413fb-7822-4bf2-8028-30597aab757b", RoleId = "PrivateUser" });

            //builder.Entity<Address>()
            //    .HasData(
            //    new Address { Id = 1, BuildingNumber =null, FlatNumber=null, Street=null, ZipCode=null, CountryId = 1, RegionId = 12, CityId = 1, UserId = "0a249d73-5e9a-4c07-9832-27645a2c2fe8" },
            //    new Address { Id = 2, BuildingNumber = null, FlatNumber = null, Street = null, ZipCode = null, CountryId = 1, RegionId = 12, CityId = 1, UserId = "2ef2b510-aa25-42ca-b68a-ee2fa0635924" },
            //    new Address { Id = 3, BuildingNumber = null, FlatNumber = null, Street = null, ZipCode = null, CountryId = 1, RegionId = 12, CityId = 1, UserId = "b9c413fb-7822-4bf2-8028-30597aab757b" });

         }
    }
}
