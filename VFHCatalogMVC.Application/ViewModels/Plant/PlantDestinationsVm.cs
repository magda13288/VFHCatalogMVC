using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantDestinationsVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantDestination>
    {
        public int DestinationId { get; set; }
        public int PlantDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantDestination, PlantDestinationsVm>();
        }
    }
}
