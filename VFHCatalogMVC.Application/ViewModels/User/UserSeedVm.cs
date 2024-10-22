using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;
using VFHCatalogMVC.Application.Mapping;
using AutoMapper;
using FluentValidation;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class UserSeedVm : IMapFrom<VFHCatalogMVC.Domain.Model.PlantSeed>
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        //[RegularExpression("[0-9]", ErrorMessage = "Dopuszczalne tylko liczby")]
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        [NotMapped]
        public string Date { get; set; }
        public string UserId { get; set; }

        [NotMapped]
        public PlantForListVm PlantForList { get; set; }
        [NotMapped]
        public ContactDetailVm ContactDetail { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PlantSeed, UserSeedVm>().ReverseMap();
        }

        public class UserSeedValidation : AbstractValidator<UserSeedVm>
        {
            public UserSeedValidation()
            {
                RuleFor(x => x.Id).GreaterThan(0);
                RuleFor(x=>x.PlantId).GreaterThan(0);
                RuleFor(x => x.Count).NotNull().GreaterThan(0).WithMessage("Liczba nasion nie może być mniejsza bądź równa 0");
                RuleFor(x => x.Description).NotEmpty().WithMessage("Pole wymagane");
                //RuleFor(x => x.DateAdded).LessThan(DateTime.Now);
                RuleFor(x => x.UserId).NotEmpty();

                //RuleFor(x => x.ContactDetail.ContactDetailInformation).Must(BeAValidWebAddress).WithMessage("Niepoprawny format adresu strony");
            }

            private bool BeAValidWebAddress(string webAddress)
            {
                //bool match;

                //// Regex regex = new Regex(@"(http(s)?://)?([\www]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?");
                //Regex regex = new Regex(@"((http | https)://)(www.)?” + “[a-zA - Z0 - 9@:%._\\+~#?&//=]{2,256}\\.[a-z]” + “{ 2,6}\\b([-a - zA - Z0 - 9@:%._\\+~#?&//=]*)");
                //match = regex.IsMatch(webAddress);

                //return match;

                var x = Uri.IsWellFormedUriString(webAddress, UriKind.RelativeOrAbsolute);

                return x;
            }
        }
       
    }
}
