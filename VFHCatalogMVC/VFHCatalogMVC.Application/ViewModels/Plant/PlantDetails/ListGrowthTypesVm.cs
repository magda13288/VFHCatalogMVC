﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class ListGrowthTypesVm: ListPlantDetails
    {
        public List<SelectListItemVm> GrowthTypesList { get; set; }
        public int[] GrowthTypesIds { get; set; }
        public List<string> GrowthTypesNames { get; set; }
    }
}

