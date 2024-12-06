﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class FruitTypeForListFilters
    {
        public int FruitTypeId { get; set; }
        public FruitType FruitType { get; set; }
        public int PlantTypeId { get; set; }
        public PlantType PlantType { get; set; }
        public int? PlantGroupId { get; set; }
        public PlantGroup PlantGroup { get; set; }
        public int? PlantSectionId { get; set; }
        public PlantSection PlantSection { get; set; }
    }
}