﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.Mapping;
using AutoMapper;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class UserSeedsVm : IMapFrom<VFHCatalogMVC.Domain.Model.PlantSeed>
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        //[RegularExpression("[0-9]", ErrorMessage = "Dopuszczalne tylko liczby")]
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }

        [NotMapped]
        public PlantForListVm PlantForList { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSeed, UserSeedsVm>().ReverseMap();
        }

    }
}
