using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NIP { get; set; }
        public string REGON { get; set; }
        public string CEOName { get; set; }
        public string CEOLastName { get; set; }
        public byte[] LogoPic { get; set; }
        public bool isActive { get; set; } // jesli kots usunie uzytkownika to nie zostanie usuniety z bazy tylko bedzie mila false i nie zostanie wyswietlony na lisce 
 
        public CustomerContactInformation CustomerContactInformation { get; set; }
        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
        public virtual ICollection<Address> Adresses { get; set; }
        public ICollection<CustomerPlantsForSale> CustomerPlantsForSale { get; set; }
    }
}
