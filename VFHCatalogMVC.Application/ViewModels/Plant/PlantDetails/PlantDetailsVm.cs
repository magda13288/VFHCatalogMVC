using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantDetailsVm : Mapping.IMapFrom<Domain.Model.PlantDetail>
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
        [NotMapped]
        public PlantForListVm Plant { get; set; }
        [NotMapped]
        public List<PlantOpinionsVm> PlantOpinions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PlantDetailsVm, Domain.Model.PlantDetail>().ReverseMap()/*.ForMember(p => p.PlantDetailsImages, opt => opt.Ignore())*/;

        }
        public class PlantDetailsValidation : AbstractValidator<PlantDetailsVm>
        {
            public PlantDetailsValidation()
            {
                RuleFor(x => x.ColorId).NotEqual(0).WithMessage("*");
                RuleFor(x => x.FruitSizeId).NotEqual(0).WithMessage("*");
                RuleFor(x => x.FruitTypeId).NotEqual(0).WithMessage("*");
                RuleFor(x => x.ListGrowthTypes).NotEmpty().NotNull().WithMessage("*");
                RuleFor(x => x.ListGrowingSeazons).NotNull().WithMessage("*");
                RuleFor(x => x.ListPlantDestinations).NotNull().WithMessage("*");

            }
        }
    }
}
