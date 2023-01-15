using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.Interfaces
{
    public interface IImageService
    {
        string UploadImage(IFormFile file, string name, string path);
        void DeleteImage(string path);

    }
}
