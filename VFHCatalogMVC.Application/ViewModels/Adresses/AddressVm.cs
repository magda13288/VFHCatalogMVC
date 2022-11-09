using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Adresses
{
    public class AddressVm:IMapFrom<VFHCatalogMVC.Domain.Model.Address>
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string ZipCode { get; set; }
        [Display(Name ="Miasto")]
        public int CityId { get; set; }
        [Display(Name = "Województwo")]
        public int VoivodeshipId { get; set; }
        [Display(Name = "Kraj")]
        public int CountryId { get; set; }
        public string UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Address, AddressVm>().ReverseMap();
        }

        public class AddressValidation:AbstractValidator<AddressVm>
        {
            public AddressValidation()
            {
                RuleFor(x => x.CountryId).NotEmpty().WithMessage("Wybierz kraj");
                RuleFor(x => x.VoivodeshipId).NotEmpty().WithMessage("Wybierz województwo");
                RuleFor(x => x.CityId).NotEmpty().WithMessage("Wybierz miasto");
            }
        }


    }
}
