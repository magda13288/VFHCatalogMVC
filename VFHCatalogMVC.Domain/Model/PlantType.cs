using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantType
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        //jeden do wielu - jedne typ może byc przypisany do wielu plantów
        public virtual ICollection<Plant> Plants { get; set; }
        public virtual ICollection<PlantGroup> PlantGroups { get; set; }
        public virtual ICollection<GrowthType> GrowthTypes { get; set; }
        public virtual ICollection<FruitSize> FruitSizes { get; set; }
        public virtual ICollection<FruitType> FruitTypes { get; set; }
        public virtual ICollection<Filters> Filters { get; set; }
    }
}
