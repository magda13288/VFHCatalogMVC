using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class Tag:BasePlantEntityNameProperty
    {
        public ICollection<PlantTag> PlantTags { get; set; }

    }
}
