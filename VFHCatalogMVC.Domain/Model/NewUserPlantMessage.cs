using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class NewUserPlantMessage
    {
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
    }
}
