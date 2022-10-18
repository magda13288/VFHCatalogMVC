using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

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

        [NotMapped]
        public PlantDetailsVm PlantDetails { get; set; }

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
                //RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.TypeId).NotEmpty().WithMessage("Pole wymagane");
                RuleFor(x => x.GroupId).NotEmpty().WithMessage("Pole wymagane");
                RuleFor(x => x.FullName).NotNull().WithMessage("Pole wymagane").MaximumLength(255).WithMessage("Maksymalna długość nazwy wynosi 255znaków");
                RuleFor(x => x.isActive).Equals(true);
                
            }
        }
    }
}
