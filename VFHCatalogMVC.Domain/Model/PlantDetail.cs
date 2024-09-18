using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantDetail
    {
        public int Id { get; set; }       
        public string Description { get; set; }
        public string PlantPassportNumber { get; set; }
        public int? ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int? DestinationId { get; set; }
        public virtual Destination Destination { get; set; }
        public int? FruitSizeId { get; set; }
        public virtual FruitSize FruitSize { get; set; }
        public int? FruitTypeId { get; set; }
        public virtual FruitType FruitType { get; set; }
        public int? GrowingSeazonId { get; set; }
        public virtual GrowingSeazon GrowingSeazon { get; set; }
        public int? GrowthTypeId { get; set; }
        public virtual GrowthType GrowthType { get; set; }
        public int? HeightId { get; set; }
        public virtual Height Height { get; set; }
        public int? PollinationId { get; set; }
        public virtual Pollination Pollination { get; set; }
        public int? PositionId { get; set; }
        public virtual Position Position { get; set; }
        public int? AdditionalFeaturesId { get; set; }
        public virtual AdditionalFeatures AdditionalFeatures { get; set; }
        public int? ProducerId { get; set; }
        public virtual Producer Producer { get; set; }

        public int PlantRef { get; set; }
        public Plant Plant { get; set; }

        public ICollection<PlantGrowthType> PlantGrowthTypes { get; set; }
        public ICollection<PlantDestination> PlantDestinations { get; set; }
        public virtual ICollection<PlantOpinion> PlantOpinions { get; set; }
        public ICollection<PlantGrowingSeazon> PlantGrowingSeazons { get; set; }
        public virtual ICollection<PlantDetailsImages> PlantDetailsImages { get; set; }
        public ICollection<PlantPosition> PlantPositions { get; set; } 
        public ICollection<PlantProducers> PlantProducers { get; set; }
        public ICollection<PlantSoilPh> PlantSoilPhs { get; set; } 
    }
}
