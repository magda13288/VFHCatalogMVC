using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class Destination:BasePlantEntityNameProperty
    {   
        public ICollection<PlantDestination> PlantDestinations { get; set; }
    }
}
