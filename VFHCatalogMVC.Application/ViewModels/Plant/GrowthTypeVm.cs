﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class GrowthTypeVm:IMapFrom<VFHCatalogMVC.Domain.Model.GrowthType>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantTypeId { get; set; }
        public int? PlantGroupId { get; set; }
        public int? PlantSectionId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.GrowthType, GrowthTypeVm>();
        }
    }
}
