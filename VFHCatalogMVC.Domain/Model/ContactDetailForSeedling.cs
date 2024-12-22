using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class ContactDetailForSeedling:AuditableEntity
    {
        public int PlantSeedlingId { get; set; }
        public PlantSeedling PlantSeedling { get; set; }
        public int ContactDetailId { get; set; }
        public ContactDetail ContactDetail { get; set; }
    }
}
