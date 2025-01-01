using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantMessage:IAuditableEntity
    {
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
        public bool isSeed { get; set; }
        public bool isSeedling { get; set; }
        public bool isNewPlant { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public DateTime? InactivatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? InactivatedBy { get; set; }
    }
}
