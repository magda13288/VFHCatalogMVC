using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.ApplicationUser
{
    public class ApplicationUserVm:IMapFrom<VFHCatalogMVC.Domain.Model.ApplicationUser>
    {
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string CEOName { get; set; }
        public string CEOLastName { get; set; }
        public byte[] LogoPic { get; set; }
        public bool isActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.ApplicationUser, ApplicationUserVm>().ReverseMap();
        }
    }
}
