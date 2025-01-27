﻿using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Domain.Common;
using VFHCatalogMVC.Domain.Model;

namespace VFHCatalogMVC.Domain.Model
{
    public class Region:BasePlantEntityNameProperty
    {     
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
      
        public virtual ICollection<City> Cities { get; set; }
        public virtual ICollection<Address> Address { get; set; }
    }
}
