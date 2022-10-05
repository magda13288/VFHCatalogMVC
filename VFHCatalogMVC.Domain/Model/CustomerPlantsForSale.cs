using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class CustomerPlantsForSale
    {
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
