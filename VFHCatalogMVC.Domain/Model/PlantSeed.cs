using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantSeed
    {
        public int Id { get; set; }
        public int PlantId { get; set; }
        public virtual Plant Plant { get; set; }
        public int Count { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [ForeignKey("PrivateUser")]
        public string PrivateUserId { get; set; }
        public virtual PrivateUser PrivateUser { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
