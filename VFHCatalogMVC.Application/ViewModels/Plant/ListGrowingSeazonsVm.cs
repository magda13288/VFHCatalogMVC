using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class ListGrowingSeazonsVm
    {
        public List<SelectListItem> GrowingSeazonsList { get; set; }
        public int[] GrowingSeaznosIds { get; set; }
        public List<string> GrwoingSeazonsNames { get; set; }
    }
}
