using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantDetail:BaseEntity
    {      
        public string Description { get; set; }
        public string PlantPassportNumber { get; set; }
        public int ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int FruitSizeId { get; set; }
        public virtual FruitSize FruitSize { get; set; }
        public int FruitTypeId { get; set; }
        public virtual FruitType FruitType { get; set; }
        public int PlantRef { get; set; }
        public Plant Plant { get; set; }

        public ICollection<PlantGrowthType> PlantGrowthTypes { get; set; }
        public ICollection<PlantDestination> PlantDestinations { get; set; }
        public virtual ICollection<PlantOpinion> PlantOpinions { get; set; }
        public ICollection<PlantGrowingSeazon> PlantGrowingSeazons { get; set; }
        public virtual ICollection<PlantDetailsImages> PlantDetailsImages { get; set; }
      
    }
}
