﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class GrowthType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantTypeId { get; set; }
        public virtual PlantType PlantType { get; set; }
        public int? PlantGroupId { get; set; }
        public virtual PlantGroup PlantGroup { get; set; }
        public int? PlantSectionId { get; set; }
        public virtual PlantSection PlantSection { get; set; }
        public ICollection<PlantGrowthType> PlantGrowthTypes { get; set; }
        //public ICollection<GrowthTypesForListFilters> GrowthTypesForListFilters { get; set; }

    }
}
