using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGrowingSeazon
    {
        public int GrowingSeazonId { get; set; }
        public GrowingSeazon GrowingSeazon { get; set; }
        public int PlantDetailId { get; set; }
        public PlantDetail PlantDetail { get; set; }
    }
}
