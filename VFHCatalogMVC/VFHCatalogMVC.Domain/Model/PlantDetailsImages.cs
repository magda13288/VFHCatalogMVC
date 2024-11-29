using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantDetailsImages
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public int PlantDetailId { get; set; }
        public virtual PlantDetail PlantDetail { get; set; }
    }
}
