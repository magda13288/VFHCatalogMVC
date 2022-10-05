using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantTypeId { get; set; }
        public virtual PlantType PlantType { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<PlantSection> PlantSections { get; set; }
        public virtual ICollection<GrowthType> GrowthTypes  { get; set; }
        public virtual ICollection<FruitSize> FruitSizes { get; set; }
        public virtual ICollection<FruitType> FruitTypes { get; set; }
    }
}

