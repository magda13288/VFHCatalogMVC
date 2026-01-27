using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Web.Mvc;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Interface;

namespace VFHCatalogMVC.Domain.Model
{
    public class Plant:BaseEntity, IAuditableEntity
    {
        public int PlantTypeId { get; set; }
        public virtual PlantType PlantType { get; set; }
        public int PlantGroupId { get; set; }
        public virtual PlantGroup PlantGroup { get; set; }
        public int? PlantSectionId { get; set; }
        public virtual PlantSection PlantSection { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public bool isActive { get; set; }
        public bool isNew { get; set; }
        public PlantDetail PlantDetail { get; set; }
        public TypeOfAvailability TypeOfAvailability { get; set; }
        public ICollection<PlantTag> PlantTags { get; set; }
        public virtual ICollection<PlantSeed> PlantSeeds { get; set; } = new List<PlantSeed>();
        public virtual ICollection<PlantSeedling> PlantSeedlings { get; set; } = new List<PlantSeedling>();
		public virtual ICollection<NewUserPlant> NewUserPlants { get; set; } = new List<NewUserPlant>();
        public virtual ICollection<PlantMessage> PlantMessages { get; set; } = new List<PlantMessage>();
        public DateTime CreatedAtUtc { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }
        public DateTime? InactivatedAtUtc { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? InactivatedBy { get; set; }
    }
}
