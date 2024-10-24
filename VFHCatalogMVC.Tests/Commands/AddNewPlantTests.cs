using Application.UnitTests.Common;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure.Repositories;
using Xunit;

namespace Application.UnitTests.Commands
{
    public class AddNewPlantTests:CommandTestBase
    {

        private readonly IMapper _mapper;

        public AddNewPlantTests() : base()
        {
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = configurationProvider.CreateMapper();

            _mapper = mapper;
        }

        [Fact]

        public void Add_NewPlant_ProperRequest_ShouldReturnIdNotEquall0()
        {
            //Arrange       
           
            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockUserService = new Mock<IUserService>();
            var mockImageService = new Mock<IImageService>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var mockUser = SetUser();

            mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x=>x.IsInRoleAsync(mockUser,"Admin")).ReturnsAsync(true);

            var plantService = new PlantService(
                mockPlantRepo.Object,
                _mapper,
                mockUserManager.Object,
                mockUserService.Object,
                mockImageService.Object
            );

            var newPlant = SetNewPlantParameters();
             
            //Act

            var id = plantService.AddPlant(newPlant, mockUser.UserName);

            //Assert

            Assert.NotEqual(0,id);
            Assert.Equal(2, _context.Plants.Count());
            _context.ShouldNotBeNull();
           
        }

        [Fact]

        public void Add_NewPlant_CheckThatPlantExist_CompareFullName_ShouldReturnIdEqual0()
        {
            //Arrange
            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockUserService = new Mock<IUserService>();
            var mockImageService = new Mock<IImageService>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);

            var plantService = new PlantService(
               mockPlantRepo.Object,
               _mapper,
               mockUserManager.Object,
               mockUserService.Object,
               mockImageService.Object
           );

            var newPlant = SetNewPlantParameters();
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
            var mockPlantRepo = new Mock<PlantRepository>(_context);
            var mockUserService = new Mock<IUserService>();
            var mockImageService = new Mock<IImageService>();

            var mockUserManager = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var mockUser = SetUser();

            mockUserManager.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            mockUserManager.Setup(x => x.IsInRoleAsync(mockUser, "Admin")).ReturnsAsync(true);

            var plantService = new PlantService(
               mockPlantRepo.Object,
               _mapper,
               mockUserManager.Object,
               mockUserService.Object,
               mockImageService.Object
           );            

            var newPlant = SetNewPlantParameters();

            //Act

            var id = plantService.AddPlant(newPlant, mockUser.UserName);

            //Arrange


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

        //private Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> fakeUsers) where TUser : class
        //{
        //    var store = new Mock<IUserStore<TUser>>();
        //    var mockUserManager = new Mock<UserManager<TUser>>(
        //        store.Object,
        //        null, null, null, null, null, null, null, null);

        //    // Możesz zamockować potrzebne metody UserManager
        //    //mockUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
        //    //    .ReturnsAsync((string email) => fakeUsers.FirstOrDefault(u => (u as ApplicationUser)?.Email == email));

        //    mockUserManager.Setup(x=>x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((string name) => fakeUsers.FirstOrDefault(u=>(u as ApplicationUser)?.UserName == name));

        //    //mockUserManager.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((Task<ApplicationUser> user) => fakeUsers.FirstOrDefault(u => (u as ApplicationUser)?.UserName == user.Result.UserName));

        //    return mockUserManager;
        //}

    }
}
