using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Adresses
{
    public class AddressVm : IMapFrom<VFHCatalogMVC.Domain.Model.Address>
    {
        [PersonalData]
        public int Id { get; set; }
        [PersonalData]
        public string Street { get; set; }
        [PersonalData]
        public string BuildingNumber { get; set; }
        [PersonalData]
        public string FlatNumber { get; set; }
        [PersonalData]
        public string ZipCode { get; set; }
        [PersonalData]
        [Display(Name ="Miasto")]
        public int CityId { get; set; }
        [PersonalData]
        [Display(Name = "Województwo")]
        public int RegionId { get; set; }
        [PersonalData]
        [Display(Name = "Kraj")]
        public int CountryId { get; set; }
        [PersonalData]
        public string UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Address, AddressVm>().ReverseMap();
        }

        public class AddressValidation:AbstractValidator<AddressVm>
        {
            public AddressValidation()
            {
                RuleFor(x => x.Id).GreaterThan(0);
                RuleFor(x => x.CountryId).NotEmpty().WithMessage("Wybierz kraj");
                RuleFor(x => x.RegionId).NotEmpty().WithMessage("Wybierz województwo");
                RuleFor(x => x.CityId).NotEmpty().WithMessage("Wybierz miasto");
                RuleFor(x => x.Street).NotEmpty().WithMessage("*");
                RuleFor(x => x.BuildingNumber).NotEmpty().WithMessage("*");
                RuleFor(x => x.ZipCode).NotEmpty().WithMessage("*").Matches(@"^\d{2}-\d{3}$").WithMessage("Niepoprawny format");
            }
        }


    }
}
