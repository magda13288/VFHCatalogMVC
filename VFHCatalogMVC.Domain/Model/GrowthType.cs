using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class GrowthType:BasePlantEntityNameProperty
    {    
        public ICollection<PlantGrowthType> PlantGrowthTypes { get; set; }
        //public ICollection<GrowthTypesForListFilters> GrowthTypesForListFilters { get; set; }

    }
}
