using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [NotMapped]
        public IFormFile Photo { get; set; }
        public bool isActive { get; set; }

        [NotMapped]
        public PlantDetailsVm PlantDetails { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<NewPlantVm, VFHCatalogMVC.Domain.Model.Plant>()
                .ForMember(m => m.PlantTypeId, opt => opt.MapFrom(d => d.TypeId))
                .ForMember(m => m.PlantGroupId, opt => opt.MapFrom(d => d.GroupId))
                .ForMember(m => m.PlantSectionId, opt => opt.MapFrom(d => d.SectionId));
        }
        public class NewPlantValidation : AbstractValidator<NewPlantVm>
        {
            public NewPlantValidation()
            {
                //RuleFor(x => x.Id).NotEmpty();
                RuleFor(x => x.TypeId).NotNull().WithMessage("*");
                RuleFor(x => x.GroupId).NotNull().WithMessage("*");
                RuleFor(x => x.FullName).NotNull().MaximumLength(255).WithMessage("*");
                RuleFor(x => x.isActive).Equals(true);

            }
        }
    }
}
