using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class GrowingSeazon:BasePlantEntityNameProperty
    {      
       public ICollection<PlantGrowingSeazon> PlantGrowingSeazons { get; set; }
    }
}
