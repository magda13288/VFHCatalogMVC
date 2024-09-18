using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantProducers
    {
        public int ProducerId { get; set; }
        public Producer Producer { get; set; }
        public int PlantDetailId { get; set; }
        public PlantDetail PlantDetail { get; set; }
    }
}
