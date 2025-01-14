﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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
        private readonly string _DIR_GALLERY = "plantGallery/plantDetailsGallery";
        private readonly string _DIR_SEARCH = "plantGallery/searchPhoto";

        public ImageService(IWebHostEnvironment webHostEnvironment, IPlantRepository plantRepository)
        {
            _webHostEnvironment = webHostEnvironment;
            _plantRepo = plantRepository;
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
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, path);

            if (System.IO.File.Exists(imagePath))
            {
                try
                {
                    System.IO.File.Delete(imagePath);
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
                    throw new IOException($"Error uploading file to path {path}.", ex);
                }
            }

            return fileName;
        }
    }
}
