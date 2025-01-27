﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantSectionsVm : SelectListItemVm, IMapFrom<Domain.Model.PlantSection>
    {
        public int PlantGroupId { get; set; }
        [ForeignKey("PlantGroupId")]
        public PlantGroupsVm PlantGroups { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantSection, PlantSectionsVm>();
        }
    }
}
