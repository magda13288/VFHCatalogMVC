using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;


namespace VFHCatalogMVC.Domain.Model
{
    public class FruitType: BasePlantEntityNameProperty
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        //public int PlantTypeId { get; set; }
        //public virtual PlantType PlantType { get; set; }
        //public int? PlantGroupId { get; set; }
        //public virtual PlantGroup PlantGroup { get; set; }
        //public int? PlantSectionId { get; set; }
        //public virtual PlantSection PlantSection { get; set; }
        public virtual ICollection<PlantDetail> PlantDetails { get; set; }
        public ICollection<FruitTypeForListFilters> FruitTypeForFilters { get; set; }
    }
}
