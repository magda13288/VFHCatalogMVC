using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class FruitSizeForListFilters:BasePropertyForListFilters
    {
        public int FruitSizeId { get; set; }
        public FruitSize FruitSize { get; set; }
       

    }
}
