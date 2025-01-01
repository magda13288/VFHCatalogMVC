using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class GrowingSeazon:BasePlantEntityNameProperty
    {      
       public ICollection<PlantGrowingSeazon> PlantGrowingSeazons { get; set; }
    }
}
