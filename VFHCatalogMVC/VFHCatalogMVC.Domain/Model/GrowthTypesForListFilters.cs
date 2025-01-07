using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class GrowthTypesForListFilters:BasePropertyForListFilters
    {
        public int GrowthTypesId { get; set; }
        public GrowthType GrowthType { get; set; }
       
    }
}
