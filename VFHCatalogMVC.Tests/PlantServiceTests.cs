using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Infrastructure;
using VFHCatalogMVC.Infrastructure.Repositories;
using Xunit;

namespace VFHCatalogMVC.Tests
{
    public class PlantServiceTests 
    {
        private readonly Mock<IMapper> mapper;
        private readonly Mock<IPlantService> plantService;

        [Fact]

        public void AddNewPlant_ShouldAddNewPlantToDatabase()
        {
            //Arrange

            var plant = SetNewPlantParameters();
            var plantRepository = new Mock<IPlantRepository>();

            //mapper = new Mock<IMapper>();


            //var plant = SetNewPlantParameters();

            //var repositoryMock = new Mock<IPlantRepository>();

            //repositoryMock.Setup(repo=>repo.AddPlant(It.IsAny<Plant>())).Returns

            //mockContext.Setup(m => m.Plants).Returns(mockSet.Object);

            //var id = _plantService.AddPlant(plant, "kinga123@gmail.com");



        }

        private static NewPlantVm SetNewPlantParameters()
        {
            var plant = new NewPlantVm()
            {
                TypeId = 1,
                GroupId = 1,
                SectionId = 1,
                FullName = "Test",
                PlantDetails = new PlantDetailsVm()
                {
                    ColorId = 1,
                    FruitSizeId = 1,
                    FruitTypeId = 1,
                    Description = "Test",
                    ListGrowingSeazons = new ListGrowingSeazonsVm() { GrowingSeaznosIds = new int[] { 1, 2 } },
                    ListGrowthTypes = new ListGrowthTypesVm() {GrowthTypesIds = new int[] {1} },
                    ListPlantDestinations = new ListPlantDestinationsVm() {DestinationsIds = new int[] {1,2 } },
                }

            };

            return plant;

        }

        //public void AddNewPlant_ShouldAddNewPlantToDatabase()
        //{
        //    //Arrange
        //    var newPlant = SetPlantParameters();

        //    var mockSet = new Mock<DbSet<Plant>();
        //    var mockContext = new Mock<Context>();

        //    // Ustawienie kontekstu, aby zwracał zamockowany DbSet<>
        //    mockContext.Setup(m=>m.Plants).Returns(mockSet.Object);

        //    // Tworzymy instancję PlantRepository z zamockowanym kontekstem.
        //    var repository = new PlantRepository(mockContext.Object);

        //    //Act

        //    var id = repository.AddPlant(newPlant);

        //    // Assert
        //    // Sprawdzenie, czy metoda Add została wywołana raz z odpowiednim plantem

        //    mockSet.Verify(m=>m.Add(It.Is<Plant>(p=>p == newPlant)), Times.Once());

        //    // Sprawdzenie, czy metoda SaveChangesAsync została wywołana raz

        //    mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());

        //    Assert.NotEqual(0,id);

        //}


        //public Plant SetPlantParameters()
        //{
        //    var plant = new Plant()
        //    {
        //        PlantTypeId = 1,
        //        PlantGroupId = 1,
        //        PlantSectionId = 1,
        //        FullName = "Gold Medal"
        //    };

        //    return plant;
           
        //}
    }
}
