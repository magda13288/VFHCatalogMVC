using Application.UnitTests.Common;
using Microsoft.AspNetCore.Hosting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Services;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;
using Xunit;

namespace Application.UnitTests.Commands
{
    public class ImageOperationsTests:CommandTestBase
    {
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly Mock<PlantRepository> _plantRepoMock;
        private readonly ImageService _imageService;
        //private readonly string _DIR_GALLERY = "plantGallery/plantDetailsGallery";
        //private readonly string _DIR_SEARCH = "plantGallery/searchPhoto";
        public ImageOperationsTests():base()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");
            _plantRepoMock = new Mock<PlantRepository>(_context);

            _imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object);
        }

        [Fact]
        public void AddPlantGalleryPhotos_ShouldUploadImagesAndReturnFileNames()
        {
            // Arrange
            var model = SetNewPlantParameters();
            model.PlantDetails = new PlantDetailsVm
            {
                Images = new List<IFormFile>
                {
                    CreateMockFormFile("image1.jpg"),
                    CreateMockFormFile("image2.png")
                }

            };        
            int plantDetailId = 1;

            // Act
            var result = _imageService.AddPlantGaleryPhotos(model, plantDetailId);

            // Assert
            Assert.Equal(2, result.Count);
            _plantRepoMock.Verify(repo => repo.AddPlantDetailsImages(It.IsAny<string>(), plantDetailId), Times.Exactly(2));
        }
        [Fact]
        public void UploadImage_ShouldUploadFileAndReturnFileName()
        {
            // Arrange
            var file = CreateMockFormFile("testImage.jpg");
            string name = "TestPlant";
            string path = "uploads";

            // Act
            var result = _imageService.UploadImage(file, name, path);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("TestPlant", result);
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

        private IFormFile CreateMockFormFile(string fileName)
        {
            var mockFile = new Mock<IFormFile>();
            mockFile.Setup(f => f.FileName).Returns(fileName);
            mockFile.Setup(f => f.Length).Returns(1024);
            mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(new byte[1024]));
            return mockFile.Object;
        }


    }
}
