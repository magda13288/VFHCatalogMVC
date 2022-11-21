﻿using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Adresses;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class PlantSeedsForListVm
    {
        public List<PlantSeedVm> PlantSeeds { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }
        public int Count { get; set; }
        public AddressVm Address { get; set; }

    }
}