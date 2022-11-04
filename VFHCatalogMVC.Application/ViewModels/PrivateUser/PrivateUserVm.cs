using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.PrivateUser
{
    public class PrivateUserVm:IMapFrom<VFHCatalogMVC.Domain.Model.PrivateUser>
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; }
        public int AddressId { get; set; }
        public string AccountName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.PrivateUser, PrivateUserVm>();
        }
    }
}
