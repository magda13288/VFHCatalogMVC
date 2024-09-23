using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGroup
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int PlantTypeId { get; set; }
        public virtual PlantType PlantType { get; set; }
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<PlantSection> PlantSections { get; set; }
        public virtual ICollection<GrowthType> GrowthTypes  { get; set; }
        public virtual ICollection<FruitSize> FruitSizes { get; set; }
        public virtual ICollection<FruitType> FruitTypes { get; set; }
        public virtual ICollection<Filters> Filters { get; set; }
    }
}

