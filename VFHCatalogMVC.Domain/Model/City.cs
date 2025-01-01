using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Model
{
    public class City: BasePlantEntityNameProperty
    {
        public int RegionId { get; set; }
        public virtual Region Region { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
    }
}
