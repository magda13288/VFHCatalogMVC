using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class GrowthTypesForListFiltersVm: PlantPropertyForListFiltersVm, IMapFrom<Domain.Model.GrowthTypesForListFilters>
    {
        public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Model.GrowthTypesForListFilters, GrowthTypesForListFiltersVm>().ReverseMap()
            .ForMember(m => m.GrowthTypesId, opt => opt.MapFrom(d => d.Id));

    }
    
    }
}
