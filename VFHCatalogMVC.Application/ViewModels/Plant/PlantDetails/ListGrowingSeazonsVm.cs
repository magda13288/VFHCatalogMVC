using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using VFHCatalogMVC.Application.ViewModels.Common;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class ListGrowingSeazonsVm : ListPlantDetails
    {
        public List<SelectListItemVm> GrowingSeazonsList { get; set; }
        public int[] GrowingSeaznosIds { get; set; }
        public List<string> GrwoingSeazonsNames { get; set; }
    }
}
