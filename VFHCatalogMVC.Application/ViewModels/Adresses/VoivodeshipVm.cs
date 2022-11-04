﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Adresses
{
    public class VoivodeshipVm:IMapFrom<VFHCatalogMVC.Domain.Model.Voivodeship>
    {
        public int Id { get; set; }
        public int CountryId { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Voivodeship,VoivodeshipVm>();
        }
    }
}
