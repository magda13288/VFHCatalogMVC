using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantOpinionsVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantOpinion>
    {
        public int Id { get; set; }
        public string Opinion { get; set; }
        public int PlantDetailId { get; set; }
        public string UserId { get; set; }
        [NotMapped] 
        public string AccountName { get; set; }
       
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantOpinion, PlantOpinionsVm>().ReverseMap();
        }

    }
   
}
