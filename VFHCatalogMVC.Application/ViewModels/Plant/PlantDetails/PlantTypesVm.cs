using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantTypesVm : SelectListItemVm, IMapFrom<Domain.Model.PlantType>
    {
        //[Key]
        //public int Id { get; set; }
        //public string Name { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantType, PlantTypesVm>();
        }
    }
}
