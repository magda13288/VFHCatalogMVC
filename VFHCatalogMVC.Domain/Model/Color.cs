using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;


namespace VFHCatalogMVC.Domain.Model
{
    public class Color:BasePlantEntityNameProperty
    {    
        public virtual ICollection<PlantDetail> PlantDetails { get; set; }
    }
}
