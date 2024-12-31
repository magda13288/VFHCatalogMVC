using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantDetailsImages:BaseEntity
    {
        public string ImageURL { get; set; }
        public int PlantDetailId { get; set; }
        public virtual PlantDetail PlantDetail { get; set; }
    }
}
