using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantPositionsVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantPosition>
    {
        public int PositionId { get; set; }
        public int PlantDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantPosition, PlantPositionsVm>();
        }
    }
}
