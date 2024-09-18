using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class Producer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlantProducers> PlantProducers { get; set; }
        public virtual ICollection<PlantDetail> PlantDetail { get; set; }
    }
}
