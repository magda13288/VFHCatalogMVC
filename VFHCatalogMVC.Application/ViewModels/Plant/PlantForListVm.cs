using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    //przechowuje informacje, które bedą wyswietlane uzytkownikowi ale jeszcze takie które się przydadzą na stronie
    public class PlantForListVm:IMapFrom<VFHCatalogMVC.Domain.Model.Plant>
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int GroupId { get; set; }
        public int SectionId { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public IFormFile Image { get; set; }

        [NotMapped]
        public PlantDetailsVm PlantDetails { get; set; }

        public bool isActive { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Plant, PlantForListVm>()
                .ForMember(m => m.TypeId, opt => opt.MapFrom(d => d.PlantTypeId))
                .ForMember(m => m.GroupId, opt => opt.MapFrom(d => d.PlantGroupId))
                .ForMember(m => m.SectionId, opt => opt.MapFrom(d => d.PlantSectionId))
                ;
                
        }


    }
}
