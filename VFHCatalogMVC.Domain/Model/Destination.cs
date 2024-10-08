﻿using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Domain.Model
{
    public class Destination
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PlantDestination> PlantDestinations { get; set; }
    }
}
