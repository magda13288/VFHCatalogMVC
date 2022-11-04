using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string ContactDetailInformation { get; set; }
        public int ContactDetailTypeID { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("Customer")]
        public string CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        //public string PrivateUserId { get; set; }
        //public virtual PrivateUser PrivateUser { get; set; }
       
    }
}
