using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantDetailsVm: Mapping.IMapFrom<VFHCatalogMVC.Domain.Model.PlantDetail>
    {
        public int Id { get; set; }
        public int PlantRef { get; set; }
        public int? ColorId { get; set; }
        public string Description { get; set; }
        public string PlantPassportNumber { get; set; }
        [NotMapped]
        public string ColorName { get; set; }
        public int? FruitSizeId { get; set; }
        [NotMapped]
        public string FruitSizeName { get; set; }
        public int? FruitTypeId { get; set; }
        [NotMapped]
        public string FruitTypeName { get; set; }
        public List<IFormFile> Images { get; set; }

        [NotMapped]
        public ListGrowthTypesVm ListGrowthTypes { get; set; }
        [NotMapped]
        public ListPlantDestinationsVm ListPlantDestinations { get; set; }
        [NotMapped]
        public ListGrowingSeazonsVm ListGrowingSeazons { get; set; }
        [NotMapped]         
        public List<PlantDetailsImagesVm> PlantDetailsImages { get; set; }
        public PlantForListVm Plant { get; set; }
      
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PlantDetailsVm, VFHCatalogMVC.Domain.Model.PlantDetail>().ReverseMap();

        }
        public class PlantDetailsValidation : AbstractValidator<PlantDetailsVm>
        {
            public PlantDetailsValidation()
            {
                //RuleFor(x => x.Id).NotEmpty();
                //RuleFor(x => x.ColorId).NotNull().WithMessage("*");

            }
        }
    }
}
