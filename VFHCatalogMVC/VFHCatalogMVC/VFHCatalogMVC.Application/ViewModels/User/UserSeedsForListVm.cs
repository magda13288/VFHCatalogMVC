﻿using System;
using System.Collections.Generic;
using System.Text;
using VFHCatalogMVC.Application.ViewModels.Plant;

namespace VFHCatalogMVC.Application.ViewModels.User
{
    public class UserSeedsForListVm
    {
        public List<UserSeedVm> UserSeeds { get; set; }
        public int? CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public string SearchString { get; set; }
        public PlantForListVm PlantForList { get; set; }
    }
}
