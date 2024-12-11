using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantDestination:BasePlantDetailKeyProperty
    {
        public int DestinationId { get; set; }
        public Destination Destinations { get; set; }
        //public int PlantDetailId { get; set; }
        //public PlantDetail PlantDetail { get; set; }
    }
}
