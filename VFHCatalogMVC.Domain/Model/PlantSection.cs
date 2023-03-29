using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantSection
    {  
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantGroupId { get; set; }
        public virtual PlantGroup PlantGroup { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<GrowthType> GrowthTypes { get; set; }
        public virtual ICollection<FruitSize> FruitSizes { get; set; }
        public virtual ICollection<FruitType> FruitTypes { get; set; }
        public virtual ICollection<Filters> Filters { get; set; }
    }
}

