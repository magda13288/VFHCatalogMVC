using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class NewUserPlantVm:IMapFrom<VFHCatalogMVC.Domain.Model.NewUserPlant>
    {
        public int PlantId { get; set; }
        public string UserId { get; set; }
        public string Comment { get; set; }
        [NotMapped]
        public string UserName { get; set; }

        [NotMapped]
        public string Status { get; set; }

        [NotMapped]
        public PlantForListVm PlantForList { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewUserPlantVm, VFHCatalogMVC.Domain.Model.NewUserPlant>().ReverseMap();
        }
    }
}
