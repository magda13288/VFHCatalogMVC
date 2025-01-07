using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface IAuditableEntity
    {
        DateTime CreatedAtUtc { get; set; }
        DateTime? UpdatedAtUtc { get; set; }
        DateTime? InactivatedAtUtc { get; set; }
        string CreatedBy { get; set; }
        string? UpdatedBy { get; set; }
        string? InactivatedBy { get; set; }
    }
}
