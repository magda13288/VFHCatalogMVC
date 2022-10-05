using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantGroupsVm: IMapFrom<VFHCatalogMVC.Domain.Model.PlantGroup>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantTypeId { get; set; }
        [ForeignKey("PlantTypeId")]
        public PlantTypesVm PlantType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantGroup, PlantGroupsVm>();
        }
    }
}
