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
        //private readonly ImageService _imageService;
        private readonly Mock<IFileSystem> _fileSystem;
        public ImageOperationsTests():base()
        {
            _webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            _plantRepoMock = new Mock<IPlantRepository>();
            //_fileSystem = new Mock<IFileSystem>();

            //_imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object, _fileSystem.Object);
        }

        [Fact]
        public void AddPlantGalleryPhotos_ShouldUploadImagesAndReturnFileNames()
        {
            // Arrange
            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"wwwroot\plantGallery\plantDetailsGallery", new MockDirectoryData() }
            });

            _plantRepoMock.Setup(repo => repo.AddPlantDetailsImages(It.IsAny<string>(), It.IsAny<int>()));

            var model = SetNewPlantParameters();
            int plantDetailId = 1;

            model.PlantDetails = new PlantDetailsVm
            {
                Images = new List<IFormFile>
                {
                     CreateMockFormFile("image1.jpg", "image/jpeg"),
                    CreateMockFormFile("image2.jpg", "image/jpeg")

                    //CreateMockFormFile("image1.jpg"),
                    //CreateMockFormFile("image2.png")
                }

            };
            _webHostEnvironmentMock.Setup(env => env.WebRootPath).Returns("wwwroot");

            var imageService = new ImageService(_webHostEnvironmentMock.Object, _plantRepoMock.Object, mockFileSystem);
     

            // Act
            var result = imageService.AddPlantGaleryPhotos(model, plantDetailId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, fileName => Assert.Contains("TestPlant", fileName));

            _plantRepoMock.Verify(repo => repo.AddPlantDetailsImages(It.IsAny<string>(), 1), Times.Exactly(2));
        }

        //[Fact]
        //public void UploadImage_ShouldUploadFileAndReturnFileName()
        //{
        //    // Arrange
        //    var file = CreateMockFormFile("testImage.jpg");
        //    string name = "TestPlant";
        //    string path = "uploads";

        //    // Act
        //    var result = _imageService.UploadImage(file, name, path);

        //    // Assert
        //    Assert.NotNull(result);
        //    Assert.Contains("TestPlant", result);
        //}


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
        //private IFormFile CreateMockFormFile(string fileName)
        //{
        //    var mockFile = new Mock<IFormFile>();
        //    mockFile.Setup(f => f.FileName).Returns(fileName);
        //    mockFile.Setup(f => f.Length).Returns(1024);
        //    mockFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(new byte[1024]));
        //    return mockFile.Object;
        //}


    }
}
