using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure;

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

            var plantType = new List<PlantType>
            {
                new PlantType {Id = 1, Name = "test" },
                new PlantType {Id = 2, Name = "test" },
                new PlantType {Id = 3 , Name = "test" },

            };
            foreach(var items in plantType)
            context.Add(items);

            var plantGroup = new List<PlantGroup>
            {
               new PlantGroup { Id=1, PlantTypeId = 1, Name = "test" },
               new PlantGroup { Id=2, PlantTypeId = 2, Name = "test" },
               new PlantGroup { Id=3, PlantTypeId = 3, Name = "test" },
            };
            foreach (var items in plantGroup)
            context.Add(items);

            var plantSection = new List<PlantSection>
            {
               new PlantSection{Id=1, PlantGroupId = 1, Name="test"},
               new PlantSection{Id=2, PlantGroupId = 2, Name="test"},
               new PlantSection{Id=3, PlantGroupId = 3, Name="test" },
               new PlantSection{Id=11, PlantGroupId = 3, Name="test" },

            };
  
            foreach (var items in plantSection)
            context.Add(items);

            var color = new Color
            {
                Id= 1,
                Name = "test",
            };
            context.Add(color);

            var fruitSize = new FruitSize 
            { 
                Id=1, 
                Name="test" 
            };
            context.Add(fruitSize);

            var fruitType = new FruitType
            {
                Id=1,
                Name="test",
            };
            context.Add(fruitType);

            //based on data from data base
            var growthTypes = new List<GrowthType>
            {
               new GrowthType {Id=1,PlantTypeId=1, PlantGroupId =1, PlantSectionId = 1, Name="test1" },
               new GrowthType {Id=2,PlantTypeId=2, PlantGroupId= null, PlantSectionId = null, Name="test2" },
               new GrowthType{Id=3,PlantTypeId=3, PlantGroupId= null, PlantSectionId = null, Name="test3"},
               new GrowthType{Id=4,PlantTypeId=1, PlantGroupId=3, PlantSectionId = 11, Name="test4"},

            };
            foreach (var items in growthTypes)
            context.Add(items);
           

            var growingSeazons = new List<GrowingSeazon>
            {
               new GrowingSeazon {Id=1,Name="test" },
               new GrowingSeazon {Id=2,Name="test" },
               new GrowingSeazon{Id=3,Name="test"},
               new GrowingSeazon{Id=4,Name="test"},
            };
            foreach (var items in growingSeazons)
            context.Add(items);


            var destinations = new List<Destination>
            {
               new Destination {Id=1,Name="test" },
               new Destination {Id=2,Name="test" },
               new Destination {Id=3,Name="test"},
               new Destination {Id=4,Name="test"},
            };
            foreach (var items in destinations)
            context.Add(items);
           
            var plantDetails = new PlantDetail
            {
                Id = 1,
                Description = "test",
                PlantPassportNumber = "test",
                PlantRef = 1,
                ColorId = 1,
                FruitSizeId = 1,
                FruitTypeId = 1,
            };
            context.Add(plantDetails);

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

            var userAdmin = new ApplicationUser
            {
                Id = "fd0f99e3-184d-4c66-b320-1f063592c1db",
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                isActive = true,
            };
            context.Add(userAdmin);

            context.SaveChanges();

            return mock;

        }

        public static void Destroy(Context context)
        {
            context.Database.EnsureDeleted();
            //cleaning context
            context.Dispose();
        }
    }
}
