using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Message
{
    public class NewUserPlantMessageVm:IMapFrom<VFHCatalogMVC.Domain.Model.NewUserPlantMessage>
    {
        public int PlantId { get; set; }
        public int MessageId { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewUserPlantMessageVm, VFHCatalogMVC.Domain.Model.NewUserPlantMessage>().ReverseMap();
        }
    }
}
