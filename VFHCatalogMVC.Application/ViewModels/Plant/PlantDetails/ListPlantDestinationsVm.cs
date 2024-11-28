using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Plant.PlantDetails
{
    public class ListPlantDestinationsVm : ListPlantDetails
    {
        public List<SelectListItemVm> DestinationsList { get; set; }
        public int[] DestinationsIds { get; set; }
        public List<string> DestinationsNames { get; set; }
    }
}
