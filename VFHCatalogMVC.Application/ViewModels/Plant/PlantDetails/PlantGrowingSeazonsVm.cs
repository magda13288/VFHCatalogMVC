using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantGrowingSeazonsVm : IMapFrom<Domain.Model.PlantGrowingSeazon>
    {
        public int GrowingSeazonId { get; set; }
        public int PlantDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantGrowingSeazon, PlantGrowingSeazonsVm>().ReverseMap();
        }
    }
}
