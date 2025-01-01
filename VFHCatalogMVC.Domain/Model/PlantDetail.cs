using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantDetail:BaseEntity, IAuditableEntity
    {      
        public string Description { get; set; }
        public string PlantPassportNumber { get; set; }
        public int? ColorId { get; set; }
        public virtual Color Color { get; set; }
        public int? FruitSizeId { get; set; }
        public virtual FruitSize FruitSize { get; set; }
        public int? FruitTypeId { get; set; }
        public virtual FruitType FruitType { get; set; }
        public int PlantRef { get; set; }
        public Plant Plant { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public DateTime? InactivatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? InactivatedBy { get; set; }

        public ICollection<PlantGrowthType> PlantGrowthTypes { get; set; }
        public ICollection<PlantDestination> PlantDestinations { get; set; }
        public virtual ICollection<PlantOpinion> PlantOpinions { get; set; }
        public ICollection<PlantGrowingSeazon> PlantGrowingSeazons { get; set; }
        public virtual ICollection<PlantDetailsImages> PlantDetailsImages { get; set; }

    }
}
