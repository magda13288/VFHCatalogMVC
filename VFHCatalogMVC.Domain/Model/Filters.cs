using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class Filters
    {
        public int Id { get; set; }
        public int PlantTypeId { get; set; }
        public virtual PlantType PlantType { get; set; }
        public int? PlantGroupId { get; set; }
        public virtual PlantGroup PlantGroup { get; set; }
        public int? PlantSectionId { get; set; }
        public virtual PlantSection PlantSection { get; set; }
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
   

    }
}
