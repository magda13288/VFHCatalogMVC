using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class Color
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PlantDetail> PlantDetails { get; set; }
        //public virtual ICollection<Filters> Filters { get; set; }
    }
}
