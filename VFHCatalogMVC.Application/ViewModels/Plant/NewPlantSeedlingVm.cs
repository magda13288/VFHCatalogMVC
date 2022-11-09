using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class NewPlantSeedlingVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantSeedling>
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSeedling, NewPlantSeedlingVm>().ReverseMap();
        }
    }
}
