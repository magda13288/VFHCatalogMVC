using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class ContactDetailForSeedlingVm:IMapFrom<VFHCatalogMVC.Domain.Model.ContactDetailForSeedling>
    {
        public int PlantSeedlingId { get; set; }
        public int ContactDetailId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.ContactDetailForSeedling, ContactDetailForSeedlingVm>().ReverseMap();
        }
    }
}
