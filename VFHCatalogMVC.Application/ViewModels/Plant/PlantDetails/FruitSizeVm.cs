using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class FruitSizeVm : SelectListItemVm, IMapFrom<Domain.Model.FruitSize>
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        public int PlantTypeId { get; set; }
        public int? PlantGroupId { get; set; }
        public int? PlantSectionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.FruitSize, FruitSizeVm>();
        }
    }
}
