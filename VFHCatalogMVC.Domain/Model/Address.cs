using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string FlatNumber { get; set; }
        public string ZipCode { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public int VoivodeshipId { get; set; }
        public virtual Voivodeship Voivodeship { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        

    }
}
