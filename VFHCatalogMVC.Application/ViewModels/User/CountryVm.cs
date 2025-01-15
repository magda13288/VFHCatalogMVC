using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Adresses
{
    public class CountryVm: SelectListItemVm, IMapFrom<VFHCatalogMVC.Domain.Model.Country>
    {   
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Country, CountryVm>();
        }
    }
}
