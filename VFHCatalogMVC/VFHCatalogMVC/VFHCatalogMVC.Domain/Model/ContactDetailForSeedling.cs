using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class ContactDetailForSeedling
    {
        public int PlantSeedlingId { get; set; }
        public PlantSeedling PlantSeedling { get; set; }
        public int ContactDetailId { get; set; }
        public ContactDetail ContactDetail { get; set; }
    }
}
