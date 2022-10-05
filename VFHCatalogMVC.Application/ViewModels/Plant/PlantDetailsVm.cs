using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantDetailsVm: Mapping.IMapFrom<VFHCatalogMVC.Domain.Model.PlantDetail>
    {
        public int Id { get; set; }
        public int PlantRef { get; set; }
        [Required]
        public int? ColorId { get; set; }
        public int? FruitSizeId { get; set; }
        public int? FruitTypeId { get; set; }
        // public int ColorId { get; set; }
        public string Description { get; set; }
       // public string Opinion { get; set; }
        public string PlantPassportNumber { get; set; }
        // public List<NewGrowthTypesVm> GrowthType { get; set; }
        [NotMapped]
        public ListGrowthTypesVm ListGrowthTypes { get; set; }
        [NotMapped]
        public ListPlantDestinationsVm ListPlantDestinations { get; set; }
        [NotMapped]
        public ListGrowingSeazonsVm ListGrowingSeazons { get; set; }
        //[NotMapped]
        //public PlantGrowthTypeVm PlantGrowthType { get; set; }
        

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PlantDetailsVm, VFHCatalogMVC.Domain.Model.PlantDetail>().ReverseMap();
        }
        public class PlantDetailsValidation : AbstractValidator<PlantDetailsVm>
        {
            public PlantDetailsValidation()
            {
                //RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.ColorId).NotNull().WithMessage("*");

            }
        }
    }
}
