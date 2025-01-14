﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class ColorsVm : SelectListItemVm, IMapFrom<Domain.Model.Color>
    {
        //public int Id { get; set; }
        //public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.Color, ColorsVm>();
        }

    }
}
