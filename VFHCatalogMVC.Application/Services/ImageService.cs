using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using System.IO.Abstractions;

namespace VFHCatalogMVC.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPlantRepository _plantRepo;
        private readonly IFileSystem _fileSystem;
        private readonly string _DIR_GALLERY = "plantGallery/plantDetailsGallery";
        private readonly string _DIR_SEARCH = "plantGallery/searchPhoto";

        public ImageService(IWebHostEnvironment webHostEnvironment, IPlantRepository plantRepository, IFileSystem fileSystem)
        {
            _webHostEnvironment = webHostEnvironment;
            _plantRepo = plantRepository;
            _fileSystem = fileSystem;
        }

        public List<string> AddPlantGaleryPhotos(NewPlantVm model, int plantDetailId)
        {
       
            var fileNames = new List<string>();
            try
            {
                foreach (var item in model.PlantDetails.Images)
                {
                    string fileName = UploadImage(item, model.FullName, _DIR_GALLERY);
                    _plantRepo.AddPlantDetailsImages(fileName, plantDetailId);
                    fileNames.Add(fileName);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error during uploading images", ex);
            }

            return fileNames;
        }

        public string AddPlantSearchPhoto(NewPlantVm model)
        {
            var fileName = UploadImage(model.Photo, model.FullName, _DIR_SEARCH);
            return fileName;
        }

        public void DeleteImage(string path)
        {
            var imagePath = _fileSystem.Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (_fileSystem.File.Exists(imagePath))
            {
                try
                {
                    _fileSystem.File.Delete(imagePath);
                }
                catch (Exception ex)
                {
                    throw new IOException("Error during deleting image", ex);
                }
            }
        }

        public string UploadImage(IFormFile file, string name, string path)
        {
            string fileName = null;

            if (file != null)
            {
                try
                {
                    string uploadDir = _fileSystem.Path.Combine(_webHostEnvironment.WebRootPath, path);

                    if (!_fileSystem.Directory.Exists(uploadDir))
                    {
                        _fileSystem.Directory.CreateDirectory(uploadDir);
                    }

                    string extension = _fileSystem.Path.GetExtension(file.FileName);
                    fileName = Guid.NewGuid().ToString() + "-" + name + extension;
                    string filePath = _fileSystem.Path.Combine(uploadDir, fileName);


                    using (var fileStream = _fileSystem.File.Create(filePath))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    throw new IOException($"Error uploading file to path {path}.", ex);
                }
            }

            return fileName;
        }
    }
}
