using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class FruitSizeVm : SelectListItemVm, IMapFrom<Domain.Model.FruitSize>
    {
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.FruitSize, FruitSizeVm>();
        }
    }
}
