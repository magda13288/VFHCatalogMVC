using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Common
{
    public class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime Created { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? Modified { get; set; }
        public int StatusId { get; set; }
        public string InactivatedBy { get; set; }
        public DateTime? Inactivated { get; set; }
    }
}
