﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Filters
{
    public class PollinationVm:IMapFrom<VFHCatalogMVC.Domain.Model.Pollination>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Pollination, PollinationVm>();
        }
    }
}