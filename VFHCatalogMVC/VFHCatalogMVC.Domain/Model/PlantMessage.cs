using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantMessage
    {
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public bool isSeed { get; set; }
        public bool isSeedling { get; set; }
        public bool isNewPlant { get; set; }
    }
}
