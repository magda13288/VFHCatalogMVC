using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Common
{
    public class BaseEntityProperty : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
