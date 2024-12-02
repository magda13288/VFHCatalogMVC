using AutoMapper;
using FluentValidation;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.Services.PlantServices;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantSeeds
{
    public class PlantSeedVm : PlantItemVm, IMapFrom<Domain.Model.PlantSeed>
    {

        //[NotMapped]
        ////[RegularExpression(@"^(http(s):\/\/.)[-a - zA - Z0 - 9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$",ErrorMessage ="Niepoprawny format adresu strony")]
        //public string Link { get; set; }

        //[NotMapped]
        //public int Price { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Domain.Model.PlantSeed, PlantSeedVm>().ReverseMap();
        }
        public class PlantSeedValidation : AbstractValidator<PlantSeedVm>
        {
            public PlantSeedValidation()
            {
                RuleFor(x => x.Count).NotNull().GreaterThan(0).WithMessage("Liczba nasion nie może być mniejsza bądź równa 0");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Pole wymagane");
                //RuleFor(x => x.Link).NotEmpty().WithMessage("Pole wymagane").Must(BeAValidWebAddress).WithMessage("Niepoprawny format adresu strony");
            }
            private bool BeAValidWebAddress(string webAddress)
            {
                //bool match;

                // Regex regex = new Regex(@"(http(s)?://)?([\www]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?");
                //Regex regex = new Regex(@"((http | https)://)(www.)?” + “[a-zA - Z0 - 9@:%._\\+~#?&//=]{2,256}\\.[a-z]” + “{ 2,6}\\b([-a - zA - Z0 - 9@:%._\\+~#?&//=]*)");

                //Regex regex = new Regex(@"^(http(s):\/\/.)[-a - zA - Z0 - 9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$");
                //match = regex.IsMatch(webAddress);

                var x = Uri.IsWellFormedUriString(webAddress, UriKind.RelativeOrAbsolute);

                return x;

            }
        }
    }
}
