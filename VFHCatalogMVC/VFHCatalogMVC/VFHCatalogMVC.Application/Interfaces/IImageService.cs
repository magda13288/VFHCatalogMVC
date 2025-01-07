using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IImageService
    {
        string UploadImage(IFormFile file, string name, string path);
        void DeleteImage(string path);
        string AddPlantSearchPhoto(NewPlantVm model);
        List<string> AddPlantGaleryPhotos(NewPlantVm model, int plantDetailId);

    }
}
