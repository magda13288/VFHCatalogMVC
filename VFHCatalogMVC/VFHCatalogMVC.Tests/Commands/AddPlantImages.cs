using Application.UnitTests.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Infrastructure.Repositories;
using Xunit;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Application.UnitTests.Commands
{
    public class AddPlantImages: CommandTestBase
    {
        public AddPlantImages():base()
        {
                
        }

        //[Fact]

        //public void AddNewPlantSearchPhoto_ProperRequest_ShouldReturnFileNameNotNull()
        //{
        //    //Arrange
        //    var mockPlantRepo = new Mock<PlantRepository>(_context);
        //    var mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
            
        //    var imageService = new ImageService(
        //        mockIWebHostEnvironment.Object,
        //        mockPlantRepo.Object
        //        );

        //    var plant = SetNewPlantParameters();
        //    plant.Id = 2;
        //    var mockPhoto = CreateMockFormFile(plant.PhotoFileName, "photo","Photo");
        //    plant.Photo = mockPhoto;

        //    //Act

        //    var fileName = imageService.AddPlantSearchPhoto(plant);

        //    //Assert

        //    Assert.NotNull( fileName );
        //}

        //[Fact]

        //public void AddNewPlantSearchPhoto_WrongRequest_ShouldReturnFileEqualNull()
        //{
        //    //Arrange
        //    var mockPlantRepo = new Mock<PlantRepository>(_context);
        //    var mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
        //    var mockPhoto = new Mock<IFormFile>();

        //    var imageService = new ImageService(
        //        mockIWebHostEnvironment.Object,
        //        mockPlantRepo.Object
        //        );

        //    var plant = SetNewPlantParameters();
        //    plant.PhotoFileName = null;
        //    plant.Id = 2;
        //    plant.Photo = mockPhoto.Object;

        //    //Act

        //    var fileName = imageService.AddPlantSearchPhoto(plant);

        //    //Assert

        //    Assert.Null(fileName);
        //}

        //[Fact]

        //public void AddNewPlantGaleryPhotos_ProperRequest_ShouldReturnNotNullListWithFileNames()
        //{
        //    //Arrange
        //    var mockPlantRepo = new Mock<PlantRepository>(_context);
        //    var mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
        //    var mockPhoto = new Mock<IFormFile>();

        //    var imageService = new ImageService(
        //        mockIWebHostEnvironment.Object,
        //        mockPlantRepo.Object
        //        );

        //    var plant = SetNewPlantParameters();
        //    plant.Id = 2;
        //    plant.PlantDetails.Images.Add(mockPhoto.Object);
            
        //    //Act

        //    var fileName = imageService.AddPlantSearchPhoto(plant);

        //    //Assert

        //    Assert.NotNull(fileName);
        //}

        //[Fact]

        //public void AddNewPlantGaleryPhotos_WrongRequest_ShouldReturnNullListWithFileNames()
        //{
        //    //Arrange
        //    var mockPlantRepo = new Mock<PlantRepository>(_context);
        //    var mockIWebHostEnvironment = new Mock<IWebHostEnvironment>();
        //    var mockPhoto = new Mock<IFormFile>();

        //    var imageService = new ImageService(
        //        mockIWebHostEnvironment.Object,
        //        mockPlantRepo.Object
        //        );

        //    var plant = SetNewPlantParameters();
        //    plant.PhotoFileName = null;
        //    plant.Id = 2;
        //    plant.PlantDetails.Images.Add(mockPhoto.Object);

        //    //Act

        //    var fileName = imageService.AddPlantSearchPhoto(plant);

        //    //Assert

        //    Assert.Null(fileName);
        //}

        //private IFormFile CreateMockFormFile(string fileName, string content, string name)
        //{
       
           
        //    var fileContent = new MemoryStream(Encoding.UTF8.GetBytes(content));
        //    fileContent.Position = 0;

      
        //    var mockFormFile = new Mock<IFormFile>();

        //    mockFormFile.Setup(f => f.FileName).Returns(fileName);
        //    mockFormFile.Setup(f => f.ContentType).Returns("image/png");
        //    mockFormFile.Setup(f => f.Length).Returns(fileContent.Length);
        //    mockFormFile.Setup(f => f.OpenReadStream()).Returns(fileContent);
        //    mockFormFile.Setup(f => f.Name).Returns(name);
        //    mockFormFile.Setup(f => f.CopyToAsync(It.IsAny<Stream>(), It.IsAny<CancellationToken>()))
        //        .Callback<Stream, CancellationToken>((stream, token) => fileContent.CopyTo(stream))
        //        .Returns(Task.CompletedTask);

        //    return mockFormFile.Object;

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
                PhotoFileName = "TestTest.png",
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
