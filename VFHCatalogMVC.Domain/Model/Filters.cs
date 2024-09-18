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
        public bool Color { get; set; }
        public bool Destination { get; set; }
        public bool FruitSizeVisible { get; set; }
        public int? FruitSizeId { get; set; }
        public virtual FruitSize FruitSize { get; set; }
        public bool FruitTypeVisible { get; set; }
        public int? FruitTypeId { get; set; }
        public virtual FruitType FruitType { get; set; }
        public bool GrowingSeazon { get; set; }
        //public bool GrowingSeazonVisible { get; set; }
        //public int? GrowingSeazonId { get; set; }
        //public virtual GrowingSeazon GrowingSeazon { get; set; }
        public bool GrowthTypeVisible { get; set; }
        public int? GrowthTypeId { get; set; }
        public virtual GrowthType GrowthType { get; set; }
        public bool HeightVisible { get; set; }
        public int? HeightId { get; set; }
        public virtual Height Height { get; set; }
        public bool PollinationVisible { get; set; }
        public int? PollinationId { get; set; }
        public virtual Pollination Pollination { get; set; }
        public bool Position { get; set; }
        public bool AdditionalFeatures { get; set; }
        public bool Producer { get; set; }
        public bool SoilPh { get; set; }

    }
}
