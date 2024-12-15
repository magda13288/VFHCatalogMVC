using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VFHCatalogMVC.Application.Interfaces;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Interface;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IPlantRepository _plantRepo;
        public ImageService(IWebHostEnvironment webHostEnvironment, IPlantRepository plantRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _plantRepo = plantRepository;
        }

        public List<string> AddPlantGaleryPhotos(NewPlantVm model, int plantDetailId)
        {
            const string _DIR = "plantGallery/plantDetailsGallery";
            var fileNames = new List<string>();

            foreach (var item in model.PlantDetails.Images)
            {
                string fileName = UploadImage(item, model.FullName, _DIR);
                _plantRepo.AddPlantDetailsImages(fileName, plantDetailId);
                fileNames.Add(fileName);
            }

            return fileNames;
        }

        public string AddPlantSearchPhoto(NewPlantVm model)
        {
            string _DIR = "plantGallery/searchPhoto";
            var fileName = UploadImage(model.Photo, model.FullName, _DIR);
            return fileName;
        }

        public void DeleteImage(string path)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    System.IO.File.Delete(imagePath);
                }
                catch (Exception ex)
                {
                    throw ex;
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
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, path);
                    string extension = Path.GetExtension(file.FileName);
                    fileName = Guid.NewGuid().ToString() + "-" + name + extension;
                    string filePath = Path.Combine(uploadDir, fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return fileName;
        }
    }
}
