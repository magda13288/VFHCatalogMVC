using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.Mapping;

namespace VFHCatalogMVC.Application.ViewModels.Customer
{
    public class CustomerVm:IMapFrom<VFHCatalogMVC.Domain.Model.Customer>
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string CEOName { get; set; }
        public string CEOLastName { get; set; }
        public byte[] LogoPic { get; set; }
        public int AddressId { get; set; }
        public bool isActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VFHCatalogMVC.Domain.Model.Customer, CustomerVm>();
        }
    }
}
