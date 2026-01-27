using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantType:BasePlantEntityNameProperty
    {    
        //jeden do wielu - jedne typ może byc przypisany do wielu plantów
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<PlantGroup> PlantGroups { get; set; }
        public ICollection<GrowthTypesForListFilters> GrowthTypesForListFilters { get; set; } = new List <GrowthTypesForListFilters>();
		public ICollection<FruitSizeForListFilters> FruitSizeForFilters { get; set; } = new List<FruitSizeForListFilters>();
        public ICollection<FruitTypeForListFilters> FruitTypeForFilters { get; set; } = new List<FruitTypeForListFilters>();
    }
}
