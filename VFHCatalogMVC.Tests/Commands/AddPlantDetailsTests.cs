using Application.UnitTests.Common;
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
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure.Repositories;
using Xunit;

namespace Application.UnitTests.Commands
{
    public class AddPlantDetailsTests: CommandTestBase
    {
        public AddPlantDetailsTests():base()
        {
                
        }

        [Fact]

        public void Add_PlantDetails_ProperRequest_ShouldReturnIdNotEquall0()
        {
            //Arrange
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = configurationProvider.CreateMapper();

            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockImageService = new Mock<IImageService>();
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var plantDetailService = new PlantDetailsService(

                mockPlantRepo.Object,             
                mapper,
                mockImageService.Object,
                mockUserManager.Object

               );

            var plant = SetNewPlantParameters();
            plant.Id = 1;

            //Act

            var plantDetailId = plantDetailService.AddPlantDetails(plant);

            //Assert

            Assert.NotEqual( 0, plantDetailId );
        }
        [Fact]

        public void GetGrowthTypesNames_ShouldReturnGrowthTypesNamesOfPlantByPlantDetailsID()
        {
            //Arrange
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = configurationProvider.CreateMapper();
            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockImageService = new Mock<IImageService>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var plantDetailService = new PlantDetailsService(

                 mockPlantRepo.Object,
                 mapper,
                 mockImageService.Object,
                 mockUserManager.Object

                );

            var id = 1;

            //Act 

            var growrthTypesNames = plantDetailService.GetGrowthTypesNames(id);

            //Assert

            Assert.NotNull(growrthTypesNames);
            //3 because nedd to add ',' between strings of names
            Assert.Equal(3, growrthTypesNames.Count);

        }

        [Fact]

        public void PlantService_GetPlantDetailsById_ShouldReturnPlantDetail()
        {
            //Arrange

            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = configurationProvider.CreateMapper();
            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockImageService = new Mock<IImageService>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var plantDetailService = new PlantDetailsService(

                 mockPlantRepo.Object,
                 mapper,
                 mockImageService.Object,
                 mockUserManager.Object

                );

            var plantDetailsMock = mapper.Map<PlantDetailsVm>(_context.PlantDetails.FirstOrDefault(x => x.PlantRef == 1));


            //Act

            var plantDetails = plantDetailService.GetPlantDetails(1);

            //Arrange
            Assert.NotNull(plantDetails);
            //Assert.Equal(plantDetailsMock, plantDetails);

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
