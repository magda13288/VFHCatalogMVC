using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Application.ViewModels.Adresses;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class ContactDetailVm:IMapFrom<VFHCatalogMVC.Domain.Model.ContactDetail>
    {
        public int Id { get; set; }
        public string ContactDetailInformation { get; set; }
        public int ContactDetailTypeID { get; set; }
        public string UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.ContactDetail, ContactDetailVm>().ReverseMap();
        }
        public class ContactDetailValidation : AbstractValidator<ContactDetailVm>
        {
            public ContactDetailValidation()
            {
                RuleFor(x => x.ContactDetailInformation).Must(BeAValidWebAddress).WithMessage("Niepoprawny format adresu strony");
            }

            private bool BeAValidWebAddress(string webAddress)
            {
                bool match;

                // Regex regex = new Regex(@"(http(s)?://)?([\www]+\.)+[\w-]+(/[\w- ;,./?%&=]*)?");
                Regex regex = new Regex(@"((http | https)://)(www.)?” + “[a-zA - Z0 - 9@:%._\\+~#?&//=]{2,256}\\.[a-z]” + “{ 2,6}\\b([-a - zA - Z0 - 9@:%._\\+~#?&//=]*)");
                match = regex.IsMatch(webAddress);

                return match;

            }
        }
    }
}
