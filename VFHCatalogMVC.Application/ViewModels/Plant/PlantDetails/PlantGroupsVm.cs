using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantGroupsVm : SelectListItemVm, IMapFrom<Domain.Model.PlantGroup>
    {
        //[Key]
        //public int Id { get; set; }
        //public string Name { get; set; }
        public int PlantTypeId { get; set; }
        [ForeignKey("PlantTypeId")]
        public PlantTypesVm PlantType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantGroup, PlantGroupsVm>();
        }
    }
}
