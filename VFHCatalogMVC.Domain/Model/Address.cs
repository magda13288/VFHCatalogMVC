using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class Address : BaseEntityProperty
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
        public int CityId { get; set; }
        public virtual City City { get; set; }
        [PersonalData]
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        [PersonalData]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        //[PersonalData]
        //public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }
        

    }
}
