using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantOpinion:BaseEntityProperty, IAuditableEntity
    {
        public string Opinion { get; set; }
        public int PlantDetailId { get; set; }
        public virtual PlantDetail PlantDetail { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public DateTime? InactivatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? InactivatedBy { get; set; }
    }
}
