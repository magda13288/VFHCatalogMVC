using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class ContactDetailForSeedVm:IMapFrom<VFHCatalogMVC.Domain.Model.ContactDetailForSeed>
    {
        public int PlantSeedId { get; set; }
        public int ContactDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContactDetailForSeedVm,ContactDetailForSeed>().ReverseMap();

        }
    }
}
