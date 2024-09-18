using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantSoilPh
    {
        public int SoilPhId { get; set; }
        public SoilPh SoilPh { get; set; }
        public int PlantDetailId { get; set; }
        public PlantDetail PlantDetail { get; set; }
    }
}
