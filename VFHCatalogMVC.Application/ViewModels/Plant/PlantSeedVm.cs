﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Adresses;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantSeedVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantSeed>
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        [RegularExpression("[0-9]", ErrorMessage ="Dopuszczalne tylko liczby")]
        public int Count { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [NotMapped]
        public List<PlantOpinionsVm> PlantOpinions { get; set; }
        //[NotMapped]
        //public AddressVm Address { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSeed, PlantSeedVm>().ReverseMap();
        }
    }
}