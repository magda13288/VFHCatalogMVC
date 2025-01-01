using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantGrowthType:BasePlantDetailKeyProperty, IAuditableEntity
    {
        public int GrowthTypeId { get; set; }
        public  GrowthType GrowthType { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public DateTime? InactivatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? InactivatedBy { get; set; }
    }
}
