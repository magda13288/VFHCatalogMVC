using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantSection:BasePlantEntityNameProperty
    {          
        public int PlantGroupId { get; set; }
        public virtual PlantGroup PlantGroup { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
       
        public ICollection<GrowthTypesForListFilters> GrowthTypesForListFilters { get; set; } = new List<GrowthTypesForListFilters>();
        public ICollection<FruitSizeForListFilters> FruitSizeForFilters { get; set; } = new List<FruitSizeForListFilters>();
        public ICollection<FruitTypeForListFilters> FruitTypeForFilters { get; set; } = new List<FruitTypeForListFilters>();
    }
}

