using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;


namespace VFHCatalogMVC.Domain.Model
{
    public class ApplicationUser:IdentityUser
    {
        [PersonalData]
        public string AccountName { get; set; }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
        [PersonalData]
        public string CompanyName { get; set; }
        [PersonalData]
        public string NIP { get; set; }
        [PersonalData]
        public string REGON { get; set; }
        [PersonalData]
        public string CEOName { get; set; }
        [PersonalData]
        public string CEOLastName { get; set; }
        [PersonalData]
        public byte[] LogoPic { get; set; }
        [PersonalData]
        public bool isActive { get; set; }

        public virtual ICollection<PlantOpinion> PlantOpinions { get; set; }
        public virtual ICollection<PlantSeed> PlantSeeds { get; set; }
        public virtual ICollection<PlantSeedling> PlantSeedlings { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public virtual ICollection<CompanyContactInformation> CustomerContactInformation { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<MessageReceiver> MessageReceivers { get; set; }
        public virtual ICollection<NewUserPlant> NewUserPlants { get; set; }

    } 
         
    }

