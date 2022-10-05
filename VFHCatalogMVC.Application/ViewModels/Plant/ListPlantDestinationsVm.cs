using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace VFHCatalogMVC.Application.ViewModels.Plant
{
    public class ListPlantDestinationsVm
    {
        public List<SelectListItem> DestinationsList { get; set; }
        public int[] DestinationsIds { get; set; }
    }
}
