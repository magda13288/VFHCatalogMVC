using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantOpinion
    {
        public int Id { get; set; }
        public string Opinion { get; set; }
        public int PlantDetailId { get; set; }
        public virtual PlantDetail PlantDetail { get; set; }
        public int PrivateUserId { get; set; }
        public PrivateUser PrivateUser { get; set; }



    }
}
