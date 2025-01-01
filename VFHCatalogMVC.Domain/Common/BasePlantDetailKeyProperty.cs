using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Common
{
    public class BasePlantDetailKeyProperty
    {
        public int PlantDetailId { get; set; }
        public PlantDetail PlantDetail { get; set; }
    }
}
