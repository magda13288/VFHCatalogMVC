using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGrowingSeazon:BasePlantDetailKeyProperty
    {
        public int GrowingSeazonId { get; set; }
        public GrowingSeazon GrowingSeazon { get; set; }
        //public int PlantDetailId { get; set; }
        //public PlantDetail PlantDetail { get; set; }
    }
}
