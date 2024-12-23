﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantDetailsImagesVm : IMapFrom<Domain.Model.PlantDetailsImages>
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public int PlantDetailId { get; set; }

        [NotMapped]
        public bool IsChecked { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantDetailsImages, PlantDetailsImagesVm>().ReverseMap();
        }
    }
}
