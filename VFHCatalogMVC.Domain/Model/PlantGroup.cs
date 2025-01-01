using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGroup:BasePlantEntityNameProperty
    {     
        public int PlantTypeId { get; set; }
        public virtual PlantType PlantType { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<PlantSection> PlantSections { get; set; }   
        public ICollection<GrowthTypesForListFilters> GrowthTypesForListFilters { get; set; }
        public ICollection<FruitSizeForListFilters> FruitSizeForFilters { get; set; }
        public ICollection<FruitTypeForListFilters> FruitTypeForFilters { get; set; }
    }
}

