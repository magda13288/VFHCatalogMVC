using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantDestinationsVm : IMapFrom<Domain.Model.PlantDestination>
    {
        public int DestinationId { get; set; }
        public int PlantDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantDestination, PlantDestinationsVm>().ReverseMap();
        }
    }
}
