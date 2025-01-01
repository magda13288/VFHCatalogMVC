using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Common
{
    public class BasePropertyForListFilters
    {
        public int PlantTypeId { get; set; }
        public PlantType PlantType { get; set; }
        public int? PlantGroupId { get; set; }
        public PlantGroup PlantGroup { get; set; }
        public int? PlantSectionId { get; set; }
        public PlantSection PlantSection { get; set; }
    }
}
