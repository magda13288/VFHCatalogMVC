using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantTag
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
