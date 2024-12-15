using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;

namespace VFHCatalogMVC.Application.ViewModels.Adresses
{
    public class RegionVm: SelectListItemVm, IMapFrom<VFHCatalogMVC.Domain.Model.Region>
    {
        public int CountryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Region,RegionVm>();
        }
    }
}
