using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IImageService
    {
        Task<string> UploadImageAsync(IFormFile file, string name, string path);
        Task DeleteImageAsync(string path);
        Task<string> AddPlantSearchPhotoAsync(NewPlantVm model);
        Task<List<string>> AddPlantGaleryPhotosAsync(NewPlantVm model, int plantDetailId);

    }
}
