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

            var plantType = new PlantType
            {
                Id = 1,
                Name = "test",
            };
            context.Add(plantType);

            var plantGroup = new PlantGroup
            {
                Id=1,
                Name ="test",
            };
            context.Add(plantGroup);

            var plantSection = new PlantSection
            {
                Id=1,
                Name="test",
            };
            context.Add(plantSection);

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

            //var growthTypes = new List<GrowthType>
            //{
            //   new GrowthType {Id=1,Name="test" },
            //   new GrowthType{Id=2,Name="test"},
            //};
            //context.Add(growthTypes);

            //var growingSeazons = new List<GrowingSeazon>
            //{
            //   new GrowingSeazon {Id=1,Name="test" },
            //   new GrowingSeazon{Id=2,Name="test"},
            //};
            //context.Add(growingSeazons);

            //var destinations = new List<Destination>
            //{
            //   new Destination {Id=1,Name="test" },
            //   new Destination {Id=2,Name="test"},
            //};
            //context.Add(destinations);

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
