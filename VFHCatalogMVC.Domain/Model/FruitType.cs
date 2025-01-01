using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;


namespace VFHCatalogMVC.Domain.Model
{
    public class FruitType: BasePlantEntityNameProperty
    {       
        public virtual ICollection<PlantDetail> PlantDetails { get; set; }
        public ICollection<FruitTypeForListFilters> FruitTypeForFilters { get; set; }
    }
}
