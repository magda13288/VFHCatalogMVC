using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Model
{
    public class City
    {
        public int Id { get; set; }
        public int VoivodeshipId { get; set; }
        public virtual Voivodeship Voivodeship { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
