using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class ContactDetailForSeed:AuditableEntity
    {
        public int PlantSeedId { get; set; }
        public PlantSeed PlantSeed { get; set; }
        public int ContactDetailId { get; set; }
        public ContactDetail ContactDetail { get; set; }
    }
}
