using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        private readonly string _DIR_GALLERY = "plantGallery/plantDetailsGallery";
        private readonly string _DIR_SEARCH = "plantGallery/searchPhoto";

        public ImageService(IWebHostEnvironment webHostEnvironment, IPlantRepository plantRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _plantRepo = plantRepository;
        }

        public async Task<List<string>> AddPlantGaleryPhotosAsync(NewPlantVm model, int plantDetailId)
        {
       
            var fileNames = new List<string>();
            var tasks = new List<Task>();

            foreach (var item in model.PlantDetails.Images)
            {
                tasks.Add(ProcessImageAsync(item, model.FullName, plantDetailId, fileNames));
            }

            await Task.WhenAll(tasks);

            return fileNames;
        }
        private async Task ProcessImageAsync(IFormFile image, string fullName, int plantDetailId, List<string> fileNames)
        {
            try
            {
                string fileName = await UploadImageAsync(image, fullName, _DIR_GALLERY);
                await _plantRepo.AddPlantDetailsImagesAsync(fileName, plantDetailId);
                lock (fileNames) // Zapobiega konfliktom w przypadku współbieżnego dostępu,zabezpieczenia dostępu do listy fileNames w przypadku współbieżnych zapisów.
                {
                    fileNames.Add(fileName);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing image: {ex.Message}");
            }
        }
        public async Task<string> AddPlantSearchPhotoAsync(NewPlantVm model)
        {
            var fileName = await UploadImageAsync(model.Photo, model.FullName, _DIR_SEARCH);
            return fileName;
        }

        public async Task DeleteImageAsync(string path)
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    await Task.Run(() => File.Delete(imagePath));
                }
                catch (Exception ex)
                {
                    throw new IOException($"Error deleting file at path {imagePath}.", ex);
                }
            }
        }

        public async Task<string> UploadImageAsync(IFormFile file, string name, string path)
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

                    if (!Directory.Exists(uploadDir))
                    {
                        Directory.CreateDirectory(uploadDir);
                    }

                    await using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                       await file.CopyToAsync(fileStream);
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
