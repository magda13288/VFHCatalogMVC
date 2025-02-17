using Application.UnitTests.Common;
using Moq;
using NPOI.OpenXmlFormats.Dml;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Interfaces.PlantInterfaces;
using VFHCatalogMVC.Application.Interfaces.UserInterfaces;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Services.PlantServices;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using VFHCatalogMVC.Application.ViewModels.Plant;
using Xunit;
using VFHCatalogMVC.Application.Mapping;
using AutoMapper.QueryableExtensions;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;

namespace Application.UnitTests.Commands
{
    public class GetAllActivePlantsForListTest:CommandTestBase
    {
        private readonly Mock<IImageService> _imageServiceMock;
        private readonly Mock<PlantRepository> _plantRepoMock;
        private readonly Mock<IPlantDetailsService> _plantDetailsServiceMock;
        private readonly Mock<IUserPlantService> _userPlantServiceMock;
        private readonly Mock<IListService> _listServiceMock;
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IPlantItemProcessor<PlantSeedVm>> _seedProcessorMock;
        private readonly Mock<IPlantItemProcessor<PlantSeedlingVm>> _seedlingProcessorMock;
        private readonly IMapper _mapper;
        private readonly PlantService _plantService;
        public GetAllActivePlantsForListTest():base()
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
                _mapper=mapper,
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
        public void GetAllActivePlantsForList_ShouldReturnPagedPlants_WhenValidInput()
        {
            // Arrange
            var plants = new List<PlantForListVm>
        {
            new PlantForListVm { Id = 1, FullName = "Plant A", isActive = true},
            new PlantForListVm { Id = 2, FullName = "Plant B", isActive = true }
        };

            _listServiceMock.Setup(service => service.Paginate(It.IsAny<List<PlantForListVm>>(), 10, 1))
                .Returns(plants);

            var pageSize = 10;
            var pageNo = 1;
            var searchString = string.Empty;
            var typeId = (int?)null;
            var groupId = (int?)null;
            var sectionId = (int?)null;

            // Act
            var result = _plantService.GetAllActivePlantsForList(pageSize, pageNo, searchString, typeId, groupId, sectionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            Assert.Equal(pageNo, result.CurrentPage);
            Assert.Equal(plants.Count, result.Plants.Count);
            Assert.Equal(plants, result.Plants);
        }

        //[Fact]
        //public void GetAllActivePlantsForList_ShouldFilterBySearchString()
        //{
        //    // Arrange
        //    var plants = new List<Plant>
        //    {
        //        new Plant { Id = 1, PlantTypeId=1, PlantGroupId =1, FullName = "Plant A", isActive = true },
        //        new Plant { Id = 2, PlantTypeId=1, PlantGroupId =1, FullName = "Plant B", isActive = true }
        //    };

        //    var plantQueryable = plants.AsQueryable();

           

        //    var pageSize = 10;
        //    var pageNo = 1;
        //    var searchString = "Plant A";
        //    var typeId = (int?)null;
        //    var groupId = (int?)null;
        //    var sectionId = (int?)null;

        //    _plantRepoMock.Setup(repo => repo.GetAllActivePlants()).Returns(plantQueryable);

        //    // Act
        //    var result = _plantService.GetAllActivePlantsForList(pageSize, pageNo, searchString, typeId, groupId, sectionId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Single(result.Plants);
        //    Assert.Equal("Plant A", result.Plants.First().FullName);
        //}

        //[Fact]
        //public void GetAllActivePlantsForList_ShouldFilterByTypeGroupAndSection()
        //{
        //    // Arrange
        //    var plants = new List<PlantForListVm>
        //{
        //    new PlantForListVm { Id = 1, FullName = "Plant A", TypeId = 1, GroupId = 2, SectionId = 3 },
        //    new PlantForListVm { Id = 2, FullName = "Plant B", TypeId = 1, GroupId = 2, SectionId = 4 }
        //};

        //    _plantRepoMock.Setup(repo => repo.GetAllActivePlants())
        //        .Returns(plants.AsQueryable());

        //    var filteredPlants = plants.Where(p => p.TypeId == 1 && p.GroupId == 2 && p.SectionId == 3).ToList();

        //    _mapperMock.Setup(mapper => mapper.ConfigurationProvider)
        //        .Returns(new MapperConfiguration(cfg => cfg.CreateMap<Plant, PlantForListVm>()));

        //    _listServiceMock.Setup(service => service.Paginate(filteredPlants, 10, 1))
        //        .Returns(filteredPlants);

        //    var pageSize = 10;
        //    var pageNo = 1;
        //    var searchString = "";
        //    var typeId = 1;
        //    var groupId = 2;
        //    var sectionId = 3;

        //    // Act
        //    var result = _plantService.GetAllActivePlantsForList(pageSize, pageNo, searchString, typeId, groupId, sectionId);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Single(result.Plants);
        //    Assert.Equal(1, result.Plants.First().Id);
        //}
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
