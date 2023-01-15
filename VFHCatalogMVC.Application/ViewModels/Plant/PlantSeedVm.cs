using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;
using VFHCatalogMVC.Application.ViewModels.User;
using System.Text.RegularExpressions;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantSeedVm : IMapFrom<VFHCatalogMVC.Domain.Model.PlantSeed>
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        //[Required(ErrorMessage ="*")]
        public int Count { get; set; }
        //[Required(ErrorMessage ="*")]
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }

        [NotMapped]
        public string Date { get; set; }
        [NotMapped]
        public string AccountName { get; set; }

        [NotMapped]
        //[RegularExpression(@"^(http(s):\/\/.)[-a - zA - Z0 - 9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$",ErrorMessage ="Niepoprawny format adresu strony")]
        public string Link { get; set; }

        [NotMapped]
        public ContactDetailVm ContactDetail { get; set; }

        [NotMapped]
        public List<PlantOpinionsVm> PlantOpinions { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSeed, PlantSeedVm>().ReverseMap();
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
