using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class Country :BasePlantEntityNameProperty
    {    
        public virtual ICollection<Address> Adresses { get; set; }
        public virtual ICollection<Region> Regions { get; set; }
    }
}
