using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantSeedling : BasePlantSeedSeedlingProperty
    {
        //public int Id { get; set; }
        //public int PlantId { get; set; }
        //public virtual Plant Plant { get; set; }
        //public int Count { get; set; }
        //public string Description { get; set; }
        //public DateTime DateAdded { get; set; }
        //public string UserId { get; set; }
        //public virtual ApplicationUser User { get; set; }
        public ICollection<ContactDetailForSeedling> ContactDetailForSeedlings { get; set; }
    }
}
