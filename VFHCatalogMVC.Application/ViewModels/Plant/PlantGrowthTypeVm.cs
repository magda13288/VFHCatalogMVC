using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantGrowthTypeVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantGrowthType>
    {
        public int GrowthTypeId { get; set; }
        public int PlantDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantGrowthType, PlantGrowthTypeVm>().ReverseMap();
        }
    }
}
