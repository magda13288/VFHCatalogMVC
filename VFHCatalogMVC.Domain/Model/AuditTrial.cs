using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;

namespace VFHCatalogMVC.Domain.Model
{
    public class AuditTrial
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser? User { get; set; }

        public TrailType TrailType { get; set; }

        public DateTime DateUtc { get; set; }

        public string EntityName { get; set; }

        public string? PrimaryKey { get; set; }

        public Dictionary<string, object?> OldValues { get; set; } = new Dictionary<string, object?>();

        public Dictionary<string, object?> NewValues { get; set; } = new Dictionary<string, object?>();

        public List<string> ChangedColumns { get; set; } = new List<string>();
    }
}
