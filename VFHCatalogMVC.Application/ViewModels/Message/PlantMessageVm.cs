using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Message
{
    public class PlantMessageVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantMessage>
    {
        public int PlantId { get; set; }
        public int MessageId { get; set; }
        public bool isSeed { get; set; }
        public bool isSeedling { get; set; }
        public bool isNewPlant { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PlantMessageVm, VFHCatalogMVC.Domain.Model.PlantMessage>().ReverseMap();
        }
    }
}
