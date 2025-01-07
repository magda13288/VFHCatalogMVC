using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class ContactDetailForSeed
    {
        public int PlantSeedId { get; set; }
        public PlantSeed PlantSeed { get; set; }
        public int ContactDetailId { get; set; }
        public ContactDetail ContactDetail { get; set; }
    }
}
