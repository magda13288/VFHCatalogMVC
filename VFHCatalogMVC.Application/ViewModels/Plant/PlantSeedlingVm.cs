using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.User;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantSeedlingVm:IMapFrom<VFHCatalogMVC.Domain.Model.PlantSeedling>
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        [RegularExpression("[0-9]", ErrorMessage = "Dopuszczalne tylko liczby")]
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        [NotMapped]
        public string AccountName { get; set; }
        [NotMapped]
        public string Date { get; set; }
        [NotMapped]
        public ContactDetailVm ContactDetail { get; set; }

        [NotMapped]
        public List<PlantOpinionsVm> PlantOpinions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSeedling, PlantSeedlingVm>().ReverseMap();
        }

        public class PlantSeedlingValidation : AbstractValidator<PlantSeedlingVm>
        {
            public PlantSeedlingValidation()
            {
                RuleFor(x => x.ContactDetail.ContactDetailInformation).NotEmpty().WithMessage("Pole wymagane");
            }
        }
    }
}
