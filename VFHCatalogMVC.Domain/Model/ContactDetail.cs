using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class ContactDetail:BaseEntity
    {
        //public int Id { get; set; }
        public string ContactDetailInformation { get; set; }
        public int ContactDetailTypeID { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public ICollection<ContactDetailForSeed> ContactDetailForSeeds { get; set; }
        public ICollection<ContactDetailForSeedling> ContactsForSeedling { get; set; }
       
    }
}
