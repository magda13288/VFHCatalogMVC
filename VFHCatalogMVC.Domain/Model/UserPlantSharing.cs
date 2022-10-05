using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class UserPlantSharing
    {
        public int PlantId { get; set; }
        public  Plant Plant { get; set; }
        public int UserId { get; set; }
        public  PrivateUser PrivateUser { get; set; }

    }
}
