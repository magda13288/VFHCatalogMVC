using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Interface
{
    public interface ICurrentSessionProvider
    {
        string? GetUserId();
    }
}