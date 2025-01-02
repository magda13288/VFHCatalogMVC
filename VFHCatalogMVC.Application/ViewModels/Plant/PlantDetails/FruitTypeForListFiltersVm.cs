using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class FruitTypeForListFiltersVm:PlantPropertyForListFiltersVm, IMapFrom<Domain.Model.FruitTypeForListFilters>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.FruitTypeForListFilters, FruitTypeForListFiltersVm>()
                .ForMember(m => m.Id, opt => opt.MapFrom(d => d.FruitTypeId));
                

        }
    }
}
