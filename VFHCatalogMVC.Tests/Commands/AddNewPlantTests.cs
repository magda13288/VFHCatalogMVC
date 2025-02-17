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
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using VFHCatalogMVC.Infrastructure.Repositories;
using VFHCatalogMVC.Application.Constants;

namespace Application.UnitTests.Commands
{
    public class AddNewPlantTests : CommandTestBase
    {
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly Mock<PlantRepository> _plantRepoMock;
        private readonly Mock<IPlantDetailsService> _plantDetailsServiceMock;
        private readonly Mock<IUserPlantService> _userPlantServiceMock;
        private readonly Mock<IListService> _listServiceMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IPlantItemProcessor<PlantSeedVm>> _seedProcessorMock;
        private readonly Mock<IPlantItemProcessor<PlantSeedlingVm>> _seedlingProcessorMock;
        private readonly PlantService _plantService;
        public AddNewPlantTests() : base()
        {
            var configurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

            var mapper = configurationProvider.CreateMapper();

            _imageServiceMock = new Mock<IImageService>();
            _plantRepoMock = new Mock<PlantRepository>(_context);
            _plantDetailsServiceMock = new Mock<IPlantDetailsService>();
            _userPlantServiceMock = new Mock<IUserPlantService>();
            _userManagerMock = CreateMockUserManager<ApplicationUser>();
            _seedProcessorMock = new Mock<IPlantItemProcessor<PlantSeedVm>>();
            _seedlingProcessorMock = new Mock<IPlantItemProcessor<PlantSeedlingVm>>();
            _listServiceMock = new Mock<IListService>();


            _plantService = new PlantService(
                _plantRepoMock.Object,
                mapper,
                _userManagerMock.Object,
                _imageServiceMock.Object,
                _plantDetailsServiceMock.Object,
                _userPlantServiceMock.Object,
                _seedProcessorMock.Object,
                _seedlingProcessorMock.Object,
                _listServiceMock.Object                
            );
        }
    
        [Fact]

        public void Add_NewPlant_ProperRequest_ShouldReturnIdNotEquall0()
        {
            //Arrange              

            var mockUser = SetUser();
            var userRole = UserRoles.ADMIN;
           
            _userManagerMock.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            _userManagerMock.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var newPlant = SetNewPlantParameters();

            //Act

            var id = _plantService.AddPlant(newPlant, mockUser.UserName);

            //Assert

            Assert.NotEqual(0, id);
            Assert.Equal(1, _context.Plants.Count());
            _context.ShouldNotBeNull();

        }

        [Fact]

        public void Add_NewPlantWithSectionIdEqual0_ShouldSetParamSectionIdOnNull()
        {    
            //Arrange

            var mockUser = SetUser();

            _userManagerMock.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            _userManagerMock.Setup(x => x.IsInRoleAsync(mockUser, UserRoles.ADMIN)).ReturnsAsync(true);


            var newPlant = SetNewPlantParameters();
            newPlant.SectionId = 0;

            //Act

            var id = _plantService.AddPlant(newPlant, mockUser.UserName);
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
            var userRole = UserRoles.ADMIN;
           
            _userManagerMock.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            _userManagerMock.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var newPlant = SetNewPlantParameters();
            //plant with this name exist in mocked database
            _plantService.AddPlant(newPlant, mockUser.UserName);

            //Act

            var id = _plantService.AddPlant(newPlant, mockUser.UserName);

            //Assert

            Assert.Equal(0, id);
            Assert.Equal(1, _context.Plants.Count());

        }

        [Fact]

        public void Add_NewPlant_IfUserRoleIsAdmin_ShouldReturnPropertyIsActiveTrueAndIsNewFalse()
        {
            //Arrange
            var mockUser = SetUser();
            var userRole = UserRoles.ADMIN;

            _userManagerMock.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            _userManagerMock.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var newPlant = SetNewPlantParameters();

            //Act

            var id = _plantService.AddPlant(newPlant, mockUser.UserName);
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
            var userRole = UserRoles.PRIVATE_USER;
         
            _userManagerMock.Setup(x => x.FindByNameAsync(mockUser.UserName)).ReturnsAsync(mockUser);
            _userManagerMock.Setup(x => x.IsInRoleAsync(mockUser, userRole)).ReturnsAsync(true);

            var newPlant = SetNewPlantParameters();

            //Act

            var id = _plantService.AddPlant(newPlant, mockUser.UserName);
            var isActive = _context.Plants.FirstOrDefault(x => x.Id == id).isActive;
            var isNew = _context.Plants.FirstOrDefault(x => x.Id == id).isNew;

            //Arrange

            Assert.Equal(false, isActive);
            Assert.Equal(true, isNew);
        }

        private NewPlantVm SetNewPlantParameters()
        {
            var plant = new NewPlantVm()
            {
                /*Id = 1,*/
                TypeId = 1,
                GroupId = 1,
                SectionId = 1,
                FullName = "Test",
                PhotoFileName = "Test",
                PlantDetails = new PlantDetailsVm()
                {
                    ColorId = 1,
                    FruitSizeId = 1,
                    FruitTypeId = 1,
                    Description = "Test",
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

        // Metoda pomocnicza do tworzenia mocka UserManager
        private static Mock<UserManager<TUser>> CreateMockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var options = new Mock<IOptions<IdentityOptions>>();
            var passwordHasher = new Mock<IPasswordHasher<TUser>>();
            var userValidators = new List<IUserValidator<TUser>> { new Mock<IUserValidator<TUser>>().Object };
            var passwordValidators = new List<IPasswordValidator<TUser>> { new Mock<IPasswordValidator<TUser>>().Object };
            var lookupNormalizer = new Mock<ILookupNormalizer>();
            var identityErrorDescriber = new Mock<IdentityErrorDescriber>();
            var serviceProvider = new Mock<IServiceProvider>();
            var logger = new Mock<ILogger<UserManager<TUser>>>();

            return new Mock<UserManager<TUser>>(
                store.Object,
                options.Object,
                passwordHasher.Object,
                userValidators,
                passwordValidators,
                lookupNormalizer.Object,
                identityErrorDescriber.Object,
                serviceProvider.Object,
                logger.Object
            );
        }
    }
   
}
