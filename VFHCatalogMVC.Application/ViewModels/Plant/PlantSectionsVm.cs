﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantSectionsVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantSection>
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantGroupId { get; set; }
        [ForeignKey("PlantGroupId")]
        public PlantGroupsVm PlantGroups { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSection, PlantSectionsVm>();
        }
    }
}
