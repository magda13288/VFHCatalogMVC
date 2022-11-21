using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantSeedlingVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantSeedling>
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        [RegularExpression("[0-9]", ErrorMessage = "Dopuszczalne tylko liczby")]
        public int Count { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSeedling, PlantSeedlingVm>().ReverseMap();
        }
    }
}
