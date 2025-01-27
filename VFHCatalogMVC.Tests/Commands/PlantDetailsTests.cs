﻿using Application.UnitTests.Common;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.Services.PlantServices;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure.Repositories;
using Xunit;
using static Application.UnitTests.Commands.AddNewPlantTests;

namespace Application.UnitTests.Commands
{
    public class PlantDetailsTests: CommandTestBase
    {
        public PlantDetailsTests():base()
        {
                
        }

        //[Fact]

        //public void Add_PlantDetails_ProperRequest_ShouldReturnIdNotEquall0()
        //{
        //    //Arrange

        //    var plantDetailService = SetServices();

        //    var plant = SetNewPlantParameters();
        //    plant.Id = 1;

        //    //Act

        //    var plantDetailId = plantDetailService.AddPlantDetails(plant);

        //    //Assert

        //    Assert.NotEqual( 0, plantDetailId );
        //}

        //[Theory]

        //[InlineData(1, 1, 1, "test")]
        //[InlineData(2, null, null, "test")]
        //[InlineData(3, null, null, "test")]
        //[InlineData(1, 3, 11, "test")]


        //public void GetGrowthTypes_ShouldReturnListofGrowthTypes(int typeId, int? groupId, int? sectionId, string stringName)
        //{

        //    //Arrange
        //    var mockUser = SetUser();
        //    var userRole = UserRoles.Admin;
        //    var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        //    mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
        //    mockUserManager.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

        //    var plantService = SetPlantService(mockUserManager);

        //    var newPlant = SetNewPlantParameters();

        //    //Act

        //    var growthTypesList = plantService.GetGrowthTypes(typeId, groupId, sectionId);

        //    //Assert

        //    Assert.Equal(stringName, growthTypesList.AsQueryable().Single().Name);


        //}

        //[Fact]

        //public void GetGrowthTypesNames_ShouldReturnGrowthTypesNamesOfPlantByPlantDetailsID()
        //{
        //    //Arrange
        //    var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

        //    var mapper = configurationProvider.CreateMapper();
        //    var mockPlantRepo = new Mock<PlantRepository>(_context);
        //    var mockImageService = new Mock<IImageService>();

        //    var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        //    var plantDetailService = new PlantDetailsService(

        //         mockPlantRepo.Object,
        //         mapper,
        //         mockImageService.Object,
        //         mockUserManager.Object

        //        );

        //    var id = 1;

        //    //Act 

        //    var growrthTypesNames = plantDetailService.GetGrowthTypesNames(id);

        //    //Assert

        //    Assert.NotNull(growrthTypesNames);
        //    //3 because nedd to add ',' between strings of names
        //    Assert.Equal(3, growrthTypesNames.Count);

        //}

        //[Fact]

        //public void GetPlantDetailsById_ShouldReturnPlantDetail()
        //{
        //    //Arrange

        //    var plantDetailService = SetServices();

        //    //Act

        //    var plantDetails = plantDetailService.GetPlantDetails(1);

        //    //Arrange
        //    Assert.NotNull(plantDetails);
          
        //}

        //[Fact]

        //public void GetPlantDetailsById_ShouldReturnNameOfPlantColor()
        //{
            
        //}

        //[Fact]

        //public void GetPlantDetailsById_ShouldReturnNameOfPlantFruitSize()
        //{

        //}

        //[Fact]

        //public void GetPlantDetailsById_ShouldReturnNameOfPlantFruitType()
        //{

        //}

        //[Fact]

        //public void GetPlantDetailsById_ShouldReturnNameOfPlantGrowthTypes()
        //{

        //}

        //[Fact]

        //public void GetPlantDetailsById_ShouldReturnNameOfPlantGrowingSeazons()
        //{

        //}

        //[Fact]

        //public void GetPlantDetailsById_ShouldReturnNameOfPlantDestinations()
        //{

        //}
        public PlantDetailsService SetServices()
        {
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = configurationProvider.CreateMapper();
            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockImageService = new Mock<IImageService>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            return new PlantDetailsService(

                 mockPlantRepo.Object,
                 mapper,
                 mockImageService.Object,
                 mockUserManager.Object

                );

            
        }
        private static NewPlantVm SetNewPlantParameters()
        {
            var plant = new NewPlantVm()
            {
                /*Id = 1,*/
                TypeId = 1,
                GroupId = 1,
                SectionId = 1,
                FullName = "TestTest",
                PhotoFileName = "TestTest",
                PlantDetails = new PlantDetailsVm()
                {
                    ColorId = 1,
                    FruitSizeId = 1,
                    FruitTypeId = 1,
                    Description = "TestTest",
                    ListGrowingSeazons = new ListGrowingSeazonsVm() { GrowingSeaznosIds = new int[] { 1, 2 } },
                    ListGrowthTypes = new ListGrowthTypesVm() { GrowthTypesIds = new int[] { 1 } },
                    ListPlantDestinations = new ListPlantDestinationsVm() { DestinationsIds = new int[] { 1, 2 } },
                }

            };

            return plant;

        }

    }
}
