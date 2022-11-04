using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Adresses
{
    public class CityVm:IMapFrom<VFHCatalogMVC.Domain.Model.City>
    {
        public int Id { get; set; }
        public int VoivodeshipId { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.City, CityVm>();
        }
    }
}
