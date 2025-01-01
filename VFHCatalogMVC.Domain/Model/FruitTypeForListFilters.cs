using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class FruitTypeForListFilters:BasePropertyForListFilters
    {
        public int FruitTypeId { get; set; }
        public FruitType FruitType { get; set; }
       
    }
}
