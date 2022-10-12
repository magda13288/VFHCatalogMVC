using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantDetailsImagesVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantDetailsImages>
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public int PlantDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantDetailsImages, PlantDetailsImagesVm>();
        }
    }
}
