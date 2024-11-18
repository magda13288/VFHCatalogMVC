using Application.UnitTests.Common;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.Services.PlantServices;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure.Repositories;
using Xunit;

namespace Application.UnitTests.Commands
{
    public class AddNewPlantTests : CommandTestBase
    {
       
       public AddNewPlantTests() : base()
        {
           
        }

        
        [Fact]

        public void Add_NewPlant_ProperRequest_ShouldReturnIdNotEquall0()
        {
            //Arrange              

            var mockUser = SetUser();
            var userRole = UserRoles.Admin;
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var plantService = SetPlantService(mockUserManager);

            var newPlant = SetNewPlantParameters();

            //Act

            var id = plantService.AddPlant(newPlant, mockUser.UserName);

            //Assert

            Assert.NotEqual(0, id);
            Assert.Equal(2, _context.Plants.Count());
            _context.ShouldNotBeNull();

        }

        [Fact]

        public void Add_NewPlantWithSectionIdEqual0_ShouldSetParamSectionIdOnNull()
        {//Arrange

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var mockUser = SetUser();

            mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.IsInRoleAsync(mockUser, "Admin")).ReturnsAsync(true);

            var plantService = SetPlantService(mockUserManager);

            var newPlant = SetNewPlantParameters();
            newPlant.SectionId = 0;

            //Act

            var id = plantService.AddPlant(newPlant, mockUser.UserName);
            var addedPlant = _context.Plants.FirstOrDefault(p => p.Id == id);

            //Assert

            Assert.NotEqual(0, id);
            Assert.Equal(null, addedPlant.PlantSectionId);
        }

        [Fact]

        public void Add_NewExistingPlant_CheckThatPlantExist_CompareFullName_ShouldReturnIdEqual0()
        {
            //Arrange
            var mockUser = SetUser();
            var userRole = UserRoles.Admin;
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var plantService = SetPlantService(mockUserManager);

            var newPlant = SetNewPlantParameters();

            //plant with this name exist in mocked database
            newPlant.FullName = "test";

            //Act

            var id = plantService.AddPlant(newPlant, "test");

            //Assert

            Assert.Equal(0, id);
            Assert.Equal(1, _context.Plants.Count());

        }

        
        [Fact]

        public void Add_NewPlant_IfUserRoleIsAdmin_ShouldReturnPropertyIsActiveTrueAndIsNewFalse()
        {
            //Arrange
            var mockUser = SetUser();
            var userRole = UserRoles.Admin;
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var plantService = SetPlantService(mockUserManager);

            var newPlant = SetNewPlantParameters();

            //Act

            var id = plantService.AddPlant(newPlant, mockUser.UserName);
            var isActive = _context.Plants.FirstOrDefault(x => x.Id == id).isActive;
            var isNew = _context.Plants.FirstOrDefault(x => x.Id == id).isNew;

            //Arrange

            Assert.Equal(true, isActive);
            Assert.Equal(false, isNew);

        }

        [Fact]

        public void Add_NewPlant_IfUserRoleIsOtherThanAdmin_ShouldReturnPropertyIsActiveFalseAndIsNewTrue()
        {
            //Arrange
            var mockUser = SetUser();
            var userRole = UserRoles.PrivateUser;
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var plantService = SetPlantService(mockUserManager);

            var newPlant = SetNewPlantParameters();

            //Act

            var id = plantService.AddPlant(newPlant, mockUser.UserName);
            var isActive = _context.Plants.FirstOrDefault(x => x.Id == id).isActive;
            var isNew = _context.Plants.FirstOrDefault(x => x.Id == id).isNew;

            //Arrange

            Assert.Equal(false, isActive);
            Assert.Equal(true, isNew);
        }


        //[Fact]

        //public void GetAllActivePlantsForList_SearchByName_ShouldReturnPlantList()
        //{
        //    //Arrange
        //    var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

        //    var mapper = configurationProvider.CreateMapper();

        //    var mockPlantRepo = new Mock<PlantRepository>(_context);
        //    var mockUserService = new Mock<IUserService>();
        //    var mockImageService = new Mock<IImageService>();
        //    var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

        //    var plantService = new PlantService(
        //       mockPlantRepo.Object,
        //       mapper,
        //       mockUserManager.Object,
        //       mockUserService.Object,
        //       mockImageService.Object
        //   );

        //    var searchString = "test";

        //    //Act

        //    var plantList = plantService.GetAllActivePlantsForList(10, null, "test", null, null, null);

        //    //Arrange

        //    Assert.True(plantList.Count == 1);
        //    Assert.True(plantList.PlantForList.FullName == searchString);
        //}

       


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

        private static ApplicationUser SetUser()
        {
            var mockUser = new ApplicationUser
            {
                Id = "fd0f99e3-184d-4c66-b320-1f063592c1db",
                UserName = "testUser",
                Email = "testUser@gmail.com",
                EmailConfirmed = true,
                isActive = true,
                
            };

            return mockUser;
        }

        private PlantService SetPlantService(Mock<UserManager<ApplicationUser>> mockManager)
        {
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = configurationProvider.CreateMapper();
            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockUserService = new Mock<IUserPlantService>();
            var mockImageService = new Mock<IImageService>();
            var mockPlantDetailsService = new Mock<IPlantDetailsSerrvice>();

            var mockUserManager = mockManager;

            var plantService = new PlantService(
               mockPlantRepo.Object,
               mapper,
               mockUserManager.Object,
               mockUserService.Object,
               mockImageService.Object,
               mockPlantDetailsService.Object
           );

            return plantService;
        }

        public static class UserRoles
        {
            public const string Admin = "Admin";
            public const string PrivateUser = "PrivateUser";
            public const string Company = "Company";
        }     

    }
   
}
