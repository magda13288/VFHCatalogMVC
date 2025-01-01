using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class NewUserPlant : BaseEntityProperty
    {
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
        public string Comment { get; set; }
    }
}
