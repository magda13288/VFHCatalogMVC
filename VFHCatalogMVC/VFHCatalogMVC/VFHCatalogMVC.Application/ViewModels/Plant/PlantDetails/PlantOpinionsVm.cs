using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class PlantOpinionsVm : IMapFrom<Domain.Model.PlantOpinion>
    {
        public int Id { get; set; }
        public string Opinion { get; set; }
        public int PlantDetailId { get; set; }
        public DateTime DateAdded { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantOpinion, PlantOpinionsVm>().ReverseMap();
        }

        public class PlantOpinionValidation : AbstractValidator<PlantOpinionsVm>
        {
            public PlantOpinionValidation()
            {
                RuleFor(x => x.Opinion).NotEmpty().WithMessage("Pole wymagane");
            }
        }

    }

}
