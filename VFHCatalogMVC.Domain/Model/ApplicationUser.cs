using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace VFHCatalogMVC.Domain.Model
{
    public class ApplicationUser:IdentityUser
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

        public virtual ICollection<PlantOpinion> PlantOpinions { get; set; }
        public virtual ICollection<PlantSeed> PlantSeeds { get; set; }
        public virtual ICollection<PlantSeedling> PlantSeedlings { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public virtual ICollection<CustomerContactInformation> CustomerContactInformation { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }

        } 
         
    }

