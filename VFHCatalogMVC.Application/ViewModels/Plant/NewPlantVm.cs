using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Constants;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class NewPlantVm: IMapFrom<VFHCatalogMVC.Domain.Model.Plant>
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int GroupId { get; set; }
        public int? SectionId { get; set; }

        //[Required] //wymagane pole DataAnnotations
        public string FullName { get; set; }
        public string PhotoFileName { get; set; }
        public IFormFile Photo { get; set; }
        public bool isActive { get; set; }
        public bool isNew { get; set; }

        [NotMapped]
        public PlantDetailsVm PlantDetails { get; set; } = new PlantDetailsVm();

        [NotMapped]
        public AddressVm Address { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPlantVm, VFHCatalogMVC.Domain.Model.Plant>()
                .ForMember(m => m.PlantTypeId, opt => opt.MapFrom(d => d.TypeId))
                .ForMember(m => m.PlantGroupId, opt => opt.MapFrom(d => d.GroupId))
                .ForMember(m => m.PlantSectionId, opt => opt.MapFrom(d => d.SectionId))
                .ForMember(m => m.Photo, opt => opt.MapFrom(d => d.PhotoFileName)).ReverseMap()
                .ForMember(m=>m.TypeId,opt=>opt.MapFrom(d=>d.PlantTypeId))
                .ForMember(m=>m.GroupId,opt=>opt.MapFrom(d=>d.PlantGroupId))
                .ForMember(m=>m.SectionId,opt=>opt.MapFrom(d=>d.PlantSectionId))
                .ForMember(m=>m.PhotoFileName,opt=>opt.MapFrom(d=>d.Photo))
                .ForMember(m => m.Photo, opt => opt.Ignore());
        }
        public class NewPlantValidation : AbstractValidator<NewPlantVm>
        {
            public NewPlantValidation()
            {
                //RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
                RuleFor(x => x.TypeId).NotEmpty().GreaterThan(0).WithMessage(ValidationMessages.REQUIRED);
                RuleFor(x => x.GroupId).NotEmpty().GreaterThan(0).WithMessage(ValidationMessages.REQUIRED);
                RuleFor(x => x.FullName).NotEmpty().WithMessage(ValidationMessages.REQUIRED).MaximumLength(255).WithMessage(ValidationMessages.MAX_VALUE_STRING + " 255");
                RuleFor(x => x.PlantDetails).NotNull().WithMessage("PlantDetails cannot be null.");
                RuleFor(x => x.PlantDetails.FruitSizeId).NotEqual(0).WithMessage(ValidationMessages.SELECT_VALUE)
                 .When(x => x.PlantDetails != null);

                RuleFor(x => x.PlantDetails.FruitTypeId).NotEqual(0).WithMessage(ValidationMessages.SELECT_VALUE)
                    .When(x => x.PlantDetails != null);

                RuleFor(x => x.PlantDetails.ListGrowthTypes.GrowthTypesIds).NotNull().WithMessage(ValidationMessages.SELECT_VALUE)
                    .When(x => x.PlantDetails?.ListGrowthTypes != null);

                RuleFor(x => x.PlantDetails.ListPlantDestinations.DestinationsIds).NotNull().WithMessage(ValidationMessages.SELECT_VALUE)
                    .When(x => x.PlantDetails?.ListPlantDestinations != null);

                RuleFor(x => x.PlantDetails.ListGrowingSeazons.GrowingSeaznosIds).NotNull().WithMessage(ValidationMessages.SELECT_VALUE)
                    .When(x => x.PlantDetails?.ListGrowingSeazons != null);
            }
        }
    }
}
