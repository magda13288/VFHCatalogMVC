using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PrivateUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool isActive { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public ICollection<UserPlantSharing> UserPlantSharings { get; set; }
        public virtual ICollection <PlantOpinion> PlantOpinions { get; set; }
        public virtual ICollection<Address> Addresses { get; set; } 

    }
}
