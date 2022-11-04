using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PrivateUser
    {
        [Key]
        public string Id { get; set; }
        public string AccountName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public bool isActive { get; set; }
  
        public virtual ICollection <PlantOpinion> PlantOpinions { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }


    }
}
