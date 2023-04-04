﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class AdditionalFeatures
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public virtual ICollection<Filters> Filters { get; set; }
        public virtual ICollection<PlantDetail> PlantDetails { get; set; }
    }
}