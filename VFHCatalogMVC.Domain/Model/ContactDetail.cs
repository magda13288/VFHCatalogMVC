using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class ContactDetail
    {
        public int Id { get; set; }
        public string ContactDetailInformation { get; set; }
        public int ContactDetailTypeID { get; set; }
        public ContactDetailType ContactDetailType { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }
        public int PrivateUserId { get; set; }
        public virtual PrivateUser PrivateUser { get; set; }
       
    }
}
