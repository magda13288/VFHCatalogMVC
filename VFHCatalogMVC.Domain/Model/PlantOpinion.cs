﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class PlantOpinion
    {
        public int Id { get; set; }
        public string Opinion { get; set; }
        public int PlantDetailId { get; set; }
        public virtual PlantDetail PlantDetail { get; set; }
        public DateTime DateAdded { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}
