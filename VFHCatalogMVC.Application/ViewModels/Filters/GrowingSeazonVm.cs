using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Filters
{
    public class GrowingSeazonVm : IMapFrom<Domain.Model.GrowingSeazon>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.GrowingSeazon, GrowingSeazonVm>();
        }
    }
}
