using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class FruitSizeForListFiltersVm: PlantPropertyForListFiltersVm, IMapFrom<Domain.Model.FruitSizeForListFilters>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.FruitSizeForListFilters, FruitSizeForListFiltersVm>()
                .ForMember(m => m.Id, opt => opt.MapFrom(d => d.FruitSizeId));
                

        }
    }
}
