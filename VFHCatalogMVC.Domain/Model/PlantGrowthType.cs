using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGrowthType:BasePlantDetailKeyProperty
    {
        public int GrowthTypeId { get; set; }
        public  GrowthType GrowthType { get; set; }
        //public int PlantDetailId { get; set; }
        //public  PlantDetail PlantDetail { get; set; }
    }
}
