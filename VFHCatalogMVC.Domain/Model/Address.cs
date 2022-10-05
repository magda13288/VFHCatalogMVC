using System;
using System.Collections.Generic;
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
        public string City { get; set; }
        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int PrivateUserId { get; set; }       
        public virtual PrivateUser PrivateUser { get; set; }
        

    }
}
