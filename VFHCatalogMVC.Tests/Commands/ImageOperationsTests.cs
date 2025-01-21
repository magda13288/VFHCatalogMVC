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
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using NPOI.SS.Formula.Functions;

namespace Application.UnitTests.Commands
{
    public class ImageOperationsTests:CommandTestBase
    {
        private readonly Mock<IWebHostEnvironment> _webHostEnvironmentMock;
        private readonly Mock<IPlantRepository> _plantRepoMock;
        private readonly MockFileSystem _mockFileSystem;
        public ImageOperationsTests() : base()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _plantRepoMock = new Mock<IPlantRepository>();
            _mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>());
        }

        [Fact]
        public void AddPlantGalleryPhotos_ShouldUploadImagesAndReturnFileNames()
        {
            // Arrange
            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");
            _plantRepoMock.Setup(repo => repo.AddPlantDetailsImages(It.IsAny<string>(), It.IsAny<int>()));

            var model = SetNewPlantParameters();
            model.PlantDetails.Images = new List<IFormFile>
            {
                CreateMockFormFile("image1.jpg", "image/jpeg"),
                CreateMockFormFile("image2.jpg", "image/jpeg")
            };
            int plantDetailId = 1;

            var imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object, _mockFileSystem);

            // Act
            var result = imageService.AddPlantGaleryPhotos(model, plantDetailId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, fileName => Assert.Contains(model.FullName, fileName));
            _plantRepoMock.Verify(repo => repo.AddPlantDetailsImages(It.IsAny<string>(), plantDetailId), Times.Exactly(2));
        }

        [Fact]
        public void AddPlantSearchPhoto_ShouldUploadImageAndReturnFileName()
        {
            // Arrange
            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");

            var model = SetNewPlantParameters();
            model.Photo = CreateMockFormFile("photo.jpg", "image/jpeg");

            var imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object, _mockFileSystem);

            // Act
            var result = imageService.AddPlantSearchPhoto(model);

            // Assert
            Assert.NotNull(result);
            Assert.Contains(model.FullName, result);
            Assert.EndsWith(".jpg", result);
        }

        [Fact]
        public void DeleteImage_ShouldRemoveFileIfExists()
        {
            // Arrange
            string filePath = @"wwwroot/plantGallery/searchPhoto/test.jpg";
            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");
            _mockFileSystem.AddFile(filePath, new MockFileData("File content"));

            var imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object, _mockFileSystem);

            // Act
            imageService.DeleteImage("plantGallery/searchPhoto/test.jpg");

            // Assert
            Assert.False(_mockFileSystem.File.Exists(filePath));
        }

        [Fact]
        public void DeleteImage_ShouldNotThrowIfFileDoesNotExist()
        {
            // Arrange
            string filePath = @"wwwroot/plantGallery/searchPhoto/nonexistent.jpg";
            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");

            var imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object, _mockFileSystem);

            // Act & Assert
            imageService.DeleteImage("plantGallery/searchPhoto/nonexistent.jpg");
        }

        [Fact]
        public void UploadImage_ShouldUploadFileAndReturnFileName()
        {
            // Arrange
            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");

            var file = CreateMockFormFile("testImage.jpg", "image/jpeg");
            var imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object, _mockFileSystem);

            // Act
            var result = imageService.UploadImage(file, "TestPlant", "uploads");

            // Assert
            Assert.NotNull(result);
            Assert.Contains("TestPlant", result);
            Assert.True(_mockFileSystem.File.Exists($"wwwroot/uploads/{result}"));
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
        private IFormFile CreateMockFormFile(string fileName, string contentType)
        {
            var content = "Fake content for testing";
            var fileStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content));
            return new FormFile(fileStream, 0, fileStream.Length, "file", fileName)
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }     
    }
}
