using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class TypeOfAvailabilityVm:IMapFrom<VFHCatalogMVC.Domain.Model.TypeOfAvailability>
    {
        public int Id { get; set; }
        public bool ForSale { get; set; }
        public bool ToReplace { get; set; }
        public bool None { get; set; }
        public int PlantRef { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.TypeOfAvailability, TypeOfAvailabilityVm>();
        }
    }
}
