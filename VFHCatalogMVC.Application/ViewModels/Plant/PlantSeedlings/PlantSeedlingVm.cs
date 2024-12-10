using AutoMapper;
using FluentValidation;
using System.ComponentModel.DataAnnotations.Schema;
using VFHCatalogMVC.Application.Mapping;

using VFHCatalogMVC.Application.Services.PlantServices;
using VFHCatalogMVC.Application.ViewModels.Plant.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantSeedlings
{
    public class PlantSeedlingVm : PlantItemVm, IMapFrom<Domain.Model.PlantSeedling>
    {

        //[NotMapped]
        //public string Link { get; set; }
        //[NotMapped]
        //public int Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantSeedling, PlantSeedlingVm>().ReverseMap();
        }

        public class PlantSeedlingValidation : AbstractValidator<PlantSeedlingVm>
        {
            public PlantSeedlingValidation()
            {
                RuleFor(x => x.Count).NotNull().GreaterThan(0).WithMessage("Liczba nasion nie może być mniejsza bądź równa 0");
                RuleFor(x => x.Description).NotNull().WithMessage("Pole wymagane");
            }

        }
    }
}
