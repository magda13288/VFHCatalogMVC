using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Common
{
    public class BasePlantSeedSeedlingProperty: BaseEntityProperty
    {
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
