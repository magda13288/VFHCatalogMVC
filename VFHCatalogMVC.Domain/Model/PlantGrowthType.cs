using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGrowthType
    {
        public int GrowthTypeId { get; set; }
        public  GrowthType GrowthType { get; set; }
        public int PlantDetailId { get; set; }
        public  PlantDetail PlantDetail { get; set; }
    }
}
