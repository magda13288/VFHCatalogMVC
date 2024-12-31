using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class TypeOfAvailability:BaseEntity
    {
        //public int Id { get; set; }
       // public bool ForSale { get; set; }
        public bool ToReplace { get; set; }
        public bool ForFree { get; set; }
        public bool Seed { get; set; }
        public bool Seedling { get; set; }
        public bool None { get; set; }
        public int PlantRef { get; set; }
        public Plant Plant { get; set; }

    }
}
